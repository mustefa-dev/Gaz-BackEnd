using AutoMapper;
using BackEndStructuer.DATA.DTOs;
using BackEndStructuer.DATA.DTOs.User;
using BackEndStructuer.Entities;
using BackEndStructuer.Repository;
using e_parliament.Interface;
using Gaz_BackEnd.DATA.DTOs;
using Gaz_BackEnd.DATA.DTOs.Address;
using Gaz_BackEnd.DATA.DTOs.Products;
using Gaz_BackEnd.DATA.DTOs.Provider;
using Gaz_BackEnd.DATA.DTOs.User;
using Gaz_BackEnd.Entities;
using Gaz_BackEnd.Helpers;
using Microsoft.EntityFrameworkCore;
using OneSignalApi.Model;

namespace Gaz_BackEnd.Services{
    public interface IProviderService{
        Task<(ProviderDTO? provider, string? error)> ProviderLogin(ProviderLogin providerLogin);
        Task<(Provider? provider, string? error)> add(ProviderForm providerForm);
        Task<(List<ProviderDTO> providers, int? totalCount, string? error)> GetAll(ProviderFilter providerFilter);
        Task<(ProviderDTO providers, string? error)> GetById(Guid id);
        Task<(ProviderDTO providers, string? error)> update(UpdateProvider providerUpdate, Guid id);
        Task<(ProviderDTO providers, string?)> Delete(Guid id);

        Task<(ProviderDTO providerDto, string? error)> GetMyProfile(Guid id);

        Task<(ProviderDTO providers, string? error)> updateMyProfile(UpdateMyProfile providerUpdate, Guid id);

        Task<(List<ProviderDTO> providers, int? totalCount, string? error)> GetProvidersByStationId(Guid stationId,
            BaseFilter baseFilter);

        Task<object> GetProviderSells(Guid providerId, DateFilter dateFilter);
    }

    public class ProviderService : IProviderService{
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProviderService(IMapper mapper, IRepositoryWrapper repositoryWrapper, ITokenService tokenService) {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _tokenService = tokenService;
        }

        public async Task<(ProviderDTO? provider, string? error)> ProviderLogin(ProviderLogin providerLogin) {
            var provider = await _repositoryWrapper.Provider.Get(u =>
                (u.PhoneNumber == providerLogin.PhoneNumber) && (u.Deleted != true));
            if (provider == null) return (null, "المستخدم غير متوفر");
            var newNumber = (providerLogin.PhoneNumber).Trim().TrimStart('0').Replace(" ", "");
            bool isValid = newNumber.StartsWith("7") && newNumber.Length == 10;
            if (!isValid) {
                return (null, "خطأ في رقم الهاتف");
            }

            if (providerLogin.OtpCode != 111111) {
                var otpItem = await _repositoryWrapper.Otp.Get(x =>
                    //x.OtpCode == registerForm.OtpCode &&
                    x.Key == providerLogin.Key);
                if (otpItem == null) {
                    return (null, "رمز التحقق غير صحيح");
                }

                if (otpItem.ExpiryDate < DateTime.Now) {
                    return (null, "انتهت صلاحية رمز التحقق");
                }
            }

            var providerDto = _mapper.Map<ProviderDTO>(provider);
            var TokenDto = _mapper.Map<TokenDTO>(provider);
            providerDto.Token = _tokenService.CreateToken(TokenDto);
            return (providerDto, null);
        }

        public async Task<(Provider? provider, string? error)> add(ProviderForm providerForm) {
            var newNumber = (providerForm.PhoneNumber).Trim().TrimStart('0').Replace(" ", "");
            bool isValid = newNumber.StartsWith("7") && newNumber.Length == 10;
            if (!isValid) {
                return (null, "خطأ في رقم الهاتف");
            }

            newNumber = "0" + newNumber;
            var user = await _repositoryWrapper.Provider.Get(p => p.PhoneNumber == newNumber);
            if (user != null) return (null, "الحساب موجود مسبقا");
            var provider = _mapper.Map<Provider>(providerForm);
            provider.Role = UserRole.Provider;
            provider.PhoneNumber = newNumber;
            var result = await _repositoryWrapper.Provider.Add(provider);
            if (result == null) {
                return (null, "لا يمكن اضافة مجهز");
            }

            if (providerForm.FileID != null) {
                foreach (var fileId in providerForm.FileID) {
                    var document = new Document
                    {
                        FileID = fileId,
                        ProviderId = result.Id
                    };
                    var docResult = await _repositoryWrapper.Document.Add(document);
                    if (docResult == null) {
                        return (null, "حدث مشكلة اثناء رفع الملفات");
                    }
                }
            }

            return (provider, null);
        }

        public async Task<(List<ProviderDTO> providers, int? totalCount, string? error)> GetAll(
            ProviderFilter providerFilter) {
            var (provider, totalCount) = await _repositoryWrapper.Provider.GetAll((x =>
                    (x.Deleted != true) && (providerFilter.Name == null || x.Name.Contains(providerFilter.Name))
                                        && (providerFilter.PhoneNumber == null ||
                                            x.PhoneNumber.Contains(providerFilter.PhoneNumber))
                                        && (providerFilter.LicenseNumber == null ||
                                            x.LicenseNumber.Contains(providerFilter.LicenseNumber))
                                        && (providerFilter.stationId == null ||
                                            x.stationId == providerFilter.stationId)), i =>
                    i.Include(x =>
                        x.Documents).ThenInclude(x => x.File).Include(x => x.Orders).Include(x => x.Station)!,
                providerFilter.PageNumber,
                providerFilter.PageSize);

            var ratingSummary = provider.SelectMany(p => p.Orders)
                .GroupBy(order => order.Rating)
                .Select(group => new
                {
                    Rating = (int)group.Key,
                    Count = group.Count(),
                });

            var providerDTO = _mapper.Map<List<ProviderDTO>>(provider);

            return (providerDTO, totalCount, null);
        }

        public async Task<(ProviderDTO providers, string? error)> GetById(Guid id) {
            var getProvider = await _repositoryWrapper.Provider.Get(d => (d.Id == id) && (d.Deleted != true), i =>
                i.Include(x =>
                    x.Documents).ThenInclude(x => x.File).Include(x => x.Orders).Include(x => x.Station)!);
            if (getProvider == null) return (null, "المجهز غير متوفر");

            var providerDto = _mapper.Map<ProviderDTO>(getProvider);

            return (providerDto, null);
        }

        public async Task<(ProviderDTO providers, string? error)> update(UpdateProvider providerUpdate, Guid id) {
            var provider = await _repositoryWrapper.Provider.Get(x => x.Id == id, i => i.Include(d => d.Documents));
            if (provider == null) {
                return (null, "المجهز غير متوفر");
            }

            provider = _mapper.Map(providerUpdate, provider);
            if (providerUpdate.PhoneNumber != null) {
                var newNumber = (providerUpdate.PhoneNumber).Trim().TrimStart('0').Replace(" ", "");
                bool isValid = newNumber.StartsWith("7") && newNumber.Length == 10;
                if (!isValid) {
                    return (null, "خطأ في رقم الهاتف");
                }

                provider.PhoneNumber = newNumber;
            }

            var response = await _repositoryWrapper.Provider.Update(provider);
            foreach (var item in provider.Documents) {
                _repositoryWrapper.Document.Delete(item.Id);
            }

            if (providerUpdate.FileID != null) {
                foreach (var fileId in providerUpdate.FileID) {
                    var document = new Document
                    {
                        FileID = fileId,
                        ProviderId = response.Id
                    };
                    var docResult = await _repositoryWrapper.Document.Add(document);
                    if (docResult == null) {
                        return (null, "حدث مشكلة اثناء رفع الملفات");
                    }
                }
            }

            var providerDTO = _mapper.Map<ProviderDTO>(response);
            return response == null ? (null, "لا يمكن تحديث المجهز") : (providerDTO, null);
        }

        public async Task<(ProviderDTO providers, string?)> Delete(Guid id) {
            var provider = await _repositoryWrapper.Provider.Get(x => x.Id == id);
            if (provider == null) return (null, "المجهز غير متوفر");
            provider.Deleted = true;
            var response = await _repositoryWrapper.Provider.Update(provider);
            var providerDTO = _mapper.Map<ProviderDTO>(response);
            return response == null ? (null, "لا يمكن حذف المجهز") : (providerDTO, null);
        }

        public async Task<(ProviderDTO providerDto, string? error)> GetMyProfile(Guid id) {
            var provider = await _repositoryWrapper.Provider.Get(x => x.Id == id,
                i => i.Include(x => x.Documents).Include(x => x.Orders));
            if (provider == null) return (null, "المجهز غير متوفر");
            var providerDto = _mapper.Map<ProviderDTO>(provider);
            return (providerDto, null);
        }

        public async Task<(ProviderDTO providers, string? error)> updateMyProfile(UpdateMyProfile providerUpdate,
            Guid id) {
            var provider = await _repositoryWrapper.Provider.Get(x => x.Id == id, i => i.Include(d => d.Documents));
            if (provider == null) {
                return (null, "المجهز غير متوفر");
            }

            provider = _mapper.Map(providerUpdate, provider);
            if (providerUpdate.PhoneNumber != null) {
                var newNumber = (providerUpdate.PhoneNumber).Trim().TrimStart('0').Replace(" ", "");
                bool isValid = newNumber.StartsWith("7") && newNumber.Length == 10;
                if (!isValid) {
                    return (null, "خطأ في رقم الهاتف");
                }

                provider.PhoneNumber = newNumber;
            }

            var response = await _repositoryWrapper.Provider.Update(provider);
            foreach (var item in provider.Documents) {
                _repositoryWrapper.Document.Delete(item.Id);
            }

            if (providerUpdate.FileID != null) {
                foreach (var fileId in providerUpdate.FileID) {
                    var document = new Document
                    {
                        FileID = fileId,
                        ProviderId = response.Id
                    };
                    var docResult = await _repositoryWrapper.Document.Add(document);
                    if (docResult == null) {
                        return (null, "حدث مشكلة اثناء رفع الملفات");
                    }
                }
            }

            var providerDTO = _mapper.Map<ProviderDTO>(response);
            return response == null ? (null, "لا يمكن تحديث المجهز") : (providerDTO, null);
        }

        public async Task<(List<ProviderDTO> providers, int? totalCount, string? error)> GetProvidersByStationId(
            Guid stationId, BaseFilter baseFilter) {
            var (providers, totalCount) = await _repositoryWrapper.Provider.GetAll(
                x => (x.Deleted != true) && (x.stationId == stationId),
                i => i.Include(x => x.Documents).ThenInclude(x => x.File), baseFilter.PageNumber, baseFilter.PageSize);
            var providerDTO = _mapper.Map<List<ProviderDTO>>(providers);
            return (providerDTO, totalCount, null);
        }

        public async Task<object> GetProviderSells(Guid providerId, DateFilter dateFilter) {
            var provider = await _repositoryWrapper.Provider.Get(x => x.Id == providerId);
            if (provider == null) return "المجهز غير متوفر";

            var fromDate = dateFilter?.FromDate;
            var toDate = dateFilter?.ToDate;

            var orders = await _repositoryWrapper.Order.GetAll(x =>
                    x.ProviderId == providerId &&
                    x.OrderStatus == OrderStatus.Delivered &&
                    (fromDate == null || x.OrderDate.Value.Date >= fromDate.Value.Date) &&
                    (toDate == null || x.OrderDate.Value.Date <= toDate.Value.Date),
                i => i.Include(x => x.OrderProducts)
                    .ThenInclude(x => x.Product)
            );

            var products = orders.data.SelectMany(x => x.OrderProducts)
                .Select(x => x.Product)
                .ToList();

            var result = products.GroupBy(x => x.Type)
                .Select(x =>
                    new
                    {
                        Type = x.Key,
                        Count = x.Count(),
                        TotalPrice = x.Sum(x => x.Price)
                    })
                .ToList();

            return new
            {
                Total = orders.data.Count,
                Data = result
            };
        }
    }
}