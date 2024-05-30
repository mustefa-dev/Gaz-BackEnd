using AutoMapper;
using BackEndStructuer.Entities;
using BackEndStructuer.Helpers.OneSignal;
using BackEndStructuer.Repository;
using Gaz_BackEnd.DATA.DTOs.Notifications;
using Gaz_BackEnd.DATA.DTOs.Order;
using Gaz_BackEnd.Entities;
using Gaz_BackEnd.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Gaz_BackEnd.Services{
    public interface IOrderService{
        Task<(List<OrderDto>? orderDtos, int? totalCount, string? error)> GetAll(OrderFilters filters, Guid userId,
            UserRole role);

        Task<(OrderDto? orderDto, string? error)> Add(OrderFormDto orderForm, Guid userId);

        Task<(string? done, string? error)> Approve(Guid id, Guid userId);
        Task<(string? done, string? error)> Delivered(Guid id, Guid userId);
        Task<(string? done, string? error)> Cancel(Guid id, Guid userId);
        Task<(string? done, string? error)> Rating(Guid id, Guid userId, RatingOrderForm ratingOrderForm);
        //GetOrderByProviderId
        Task<(List<OrderDto>? orderDtos, int? totalCount, string? error)> GetOrderByProviderId(Guid id,OrderFilters filters);
    }

    public class OrderService : IOrderService{
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public OrderService(IRepositoryWrapper repositoryWrapper, IMapper mapper) {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<(List<OrderDto>? orderDtos, int? totalCount, string? error)> GetAll(OrderFilters filters,
            Guid userId, UserRole role) {
            // add order status filter

            var (orders, totalCount) = await _repositoryWrapper.Order.GetAll(
                (x => (role == UserRole.SuperAdmin || x.ProviderId == userId || x.UserId == userId) &&
                      (filters.OrderStatuses == null || filters.OrderStatuses.Contains((OrderStatus)x.OrderStatus)))
                , i => i.Include(a => a.Address)
                    .ThenInclude(g => g.Governorate)
                    .Include(a => a.Address)
                    .ThenInclude(d => d.District)
                    .Include(a => a.Address)
                    .ThenInclude(c => c.City)
                    .Include(p => p.OrderProducts)
                    .ThenInclude(p => p.Product)
                    .ThenInclude(f => f.File)
                    .Include(c => c.User)
                    .Include(p => p.Provider), filters.PageNumber, filters.PageSize);
            var orderDtos = _mapper.Map<List<OrderDto>>(orders);
            return (orderDtos, totalCount, null);
        }

        public async Task<(OrderDto? orderDto, string? error)> Add(OrderFormDto orderForm, Guid userId) {
            var product = _mapper.Map<Order>(orderForm);
            product.UserId = userId;
            var user = await _repositoryWrapper.User.Get(x => x.Id == userId,
                i => i.Include(s => s.Addresses.Where(m => m.IsMain == true)));
            if (user.Addresses.Count == 0) return (null, "لا يوجد عنوان للمستخدم");

            // get station based on user city id
            var station = await _repositoryWrapper.Station.Get(x => x.CityId == user.Addresses.FirstOrDefault().CityId);
            if (station == null) return (null, "لا يوجد محطة لهذه المدينة");
            product.AddressId = user.Addresses.FirstOrDefault().Id;
            product.OrderStatus = OrderStatus.Pending;
            var order = await _repositoryWrapper.Order.Add(product);

            var notificationForm = new Notifications
            {
                Title = "طلب جديد",
                Description = "طلب جديد",
            };

            var getOrder = await _repositoryWrapper.Order.Get(x => x.Id == order.Id,
                i => i.Include(a => a.Address).ThenInclude(g => g.Governorate).Include(a =>
                    a.Address).ThenInclude(d => d.District).Include(a
                    => a.Address).ThenInclude(c => c.City).Include(p
                    => p.OrderProducts).ThenInclude(p => p.Product).ThenInclude(f => f.File));

            await _repositoryWrapper.Notification.Add(notificationForm);

            // all keys to LowerCase 
            PusherChannel.Trigger(_mapper.Map<OrderDto>(getOrder), station.Id.ToString(), "order");
            if (order == null) return (null, "لا يمكن اضافة الطلب");
            var orderDto = _mapper.Map<OrderDto>(getOrder);
            var oneSignalNotification = new Notifications
            {
                Title = "تمت الموافقة على الطلب",
                Description = "تمت الموافقة على الطلب",
            };
            await OneSignal.SendNoitications(oneSignalNotification, userId.ToString());
            PusherChannel.Trigger(_mapper.Map<OrderDto>(getOrder),station.Id.ToString(),"order");
            return (orderDto, null);
        }

        public async Task<(string? done, string? error)> Approve(Guid id, Guid userId) {
            var order = await _repositoryWrapper.Order.Get(x => x.Id == id,
                i => i.Include(s => s.Address).ThenInclude(c => c.City));

            if (order == null) return (null, "الطلب غير موجود");
            if (order.ProviderId != null) return (null, "الطلب موافق عليه من قبل مزود الخدمة");
            if (order.OrderStatus != OrderStatus.Pending) return (null, "لا يمكن الموافقة على الطلب");

            order.ProviderId = userId;

            var notificationForm = new Notifications
            {
                Title = "تمت الموافقة على الطلب",
                Description = "تمت الموافقة على الطلب",
            };

            var approvalNotification = await _repositoryWrapper.Notification.Add(notificationForm);


            order.OrderStatus = OrderStatus.Accepted;
            order.DateOfAccepted = DateTime.UtcNow;
            var update = await _repositoryWrapper.Order.Update(order);

            if (update == null) return (null, "لا يمكن الموافقة على الطلب");

            var station = await _repositoryWrapper.Station.Get(x => x.CityId == order.Address.CityId);

            var oneSignalNotification = new Notifications
            {
                Title = "تمت الموافقة على الطلب",
                Description = "تمت الموافقة على الطلب",
            };

            await OneSignal.SendNoitications(oneSignalNotification, userId.ToString());
            PusherChannel.Trigger(approvalNotification, station.Id.ToString(), order.Id.ToString());

            return ("تمت الموافقة على الطلب", null);
        }


        public async Task<(string? done, string? error)> Delivered(Guid id, Guid userId) {
            var order = await _repositoryWrapper.Order.Get(x => x.Id == id, i => i.Include(a => a.Address).ThenInclude(c => c.City));
            if (order == null) return (null, "الطلب غير موجود");
            if (order.ProviderId != userId) return (null, "لا يمكن تسليم الطلب");
            order.OrderStatus = OrderStatus.Delivered;
            order.DateOfDelivered = DateTime.UtcNow;
            var update = await _repositoryWrapper.Order.Update(order);
            var notificationForm = new Notifications
            {
                Title = "تم تسليم الطلب",
                Description = "تم تسليم الطلب",
                UserId = order.UserId,
            };
            await _repositoryWrapper.Notification.Add(notificationForm);
             OneSignal.SendNoitications(notificationForm, userId.ToString());
            var station = await _repositoryWrapper.Station.Get(x => x.CityId == order.Address.CityId);
            if (station == null) return (null, "لا يوجد محطة لهذه المدينة");
             PusherChannel.Trigger(_mapper.Map<OrderDto>(order), station.Id.ToString(), order.Id.ToString());
            var oneSignalNotification = new Notifications
            {
                Title = "تم تسليم الطلب",
                Description = "تم تسليم الطلب",
            };
             await OneSignal.SendNoitications(oneSignalNotification, userId.ToString());
             // PusherChannel.Trigger(deliverNotification, station.Id.ToString(), order.Id.ToString());

            if (update == null) return (null, "لا يمكن تسليم الطلب");
            PusherChannel.Trigger(_mapper.Map<OrderDto>(order), order.UserId.ToString(), "rating");
            return ("تم تسليم الطلب", null);
        }

        public async Task<(string? done, string? error)> Cancel(Guid id, Guid userId) {
            var order = await _repositoryWrapper.Order.Get(x => x.Id == id);
            if (order == null) return (null, "الطلب غير موجود");
            if (order.UserId != userId) return (null, "لا يمكن الغاء الطلب");
            order.OrderStatus = OrderStatus.Canceled;
            order.DateOfCanceled= DateTime.UtcNow;
            var update = await _repositoryWrapper.Order.Update(order);
            
            var station = await _repositoryWrapper.Station.Get(x => x.CityId == order.Address.CityId);
            if (station == null) return (null, "لا يوجد محطة لهذه المدينة");
            PusherChannel.Trigger(_mapper.Map<OrderDto>(order), station.Id.ToString(), order.Id.ToString());
            if (update == null) return (null, "لا يمكن الغاء الطلب");
            
            var notificationForm = new Notifications
            {
                Title = "تم الغاء الطلب",
                Description = "تم الغاء الطلب",
                UserId = order.ProviderId,
            };
            
            var cancelNotification = await _repositoryWrapper.Notification.Add(notificationForm);
            var oneSignalNotification = new Notifications
            {
                Title = "تم الغاء الطلب",
                Description = "تم الغاء الطلب",
            };
            await OneSignal.SendNoitications(oneSignalNotification, userId.ToString());
            PusherChannel.Trigger(cancelNotification, station.Id.ToString(), order.Id.ToString());
            return ("تم الغاء الطلب", null);
        }

        public async Task<(string? done, string? error)> Rating(Guid id, Guid userId, RatingOrderForm ratingOrderForm)
        {
            var order = await _repositoryWrapper.Order.Get(x => x.Id == id);
            if (order == null) return (null, "الطلب غير موجود");
            if (order.UserId != userId) return (null, "لا يمكن تقييم الطلب");
            order.Rating = ratingOrderForm.Rating;
            var update = await _repositoryWrapper.Order.Update(order);
            if (update == null) return (null, "لا يمكن تقييم الطلب");
           
            //var notificationForm = new Notifications
            //{
            //    Title = "تم تقييم الطلب",
            //    Description = "تم تقييم الطلب",
            //    UserId = order.ProviderId,
            //};
            //await _repositoryWrapper.Notification.Add(notificationForm);
            //await OneSignal.SendNoitications(notificationForm, userId.ToString());
            
            return ("تم تقييم الطلب", null);
        }
            
        public async Task<(List<OrderDto>? orderDtos, int? totalCount, string? error)> GetOrderByProviderId(Guid id,OrderFilters filters)
        {
            var (orders, totalCount) = await _repositoryWrapper.Order.GetAll(
                (x => (x.ProviderId == id) &&
                      (filters.OrderStatuses == null || filters.OrderStatuses.Contains((OrderStatus)x.OrderStatus)))
                , i => i.Include(a => a.Address)
                    .ThenInclude(g => g.Governorate)
                    .Include(a => a.Address)
                    .ThenInclude(d => d.District)
                    .Include(a => a.Address)
                    .ThenInclude(c => c.City)
                    .Include(p => p.OrderProducts)
                    .ThenInclude(p => p.Product)
                    .ThenInclude(f => f.File)
                    .Include(c => c.User)
                    .Include(p => p.Provider), filters.PageNumber, filters.PageSize);
            var orderDtos = _mapper.Map<List<OrderDto>>(orders);
            return (orderDtos, totalCount, null);
        }
    }
}