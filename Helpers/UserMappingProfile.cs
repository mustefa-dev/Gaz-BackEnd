using AutoMapper;
using BackEndStructuer.DATA.DTOs;
using BackEndStructuer.DATA.DTOs.Files;
using BackEndStructuer.DATA.DTOs.User;
using BackEndStructuer.Entities;
using Gaz_BackEnd.DATA.DTOs.Address;
using Gaz_BackEnd.DATA.DTOs.City;
using Gaz_BackEnd.DATA.DTOs.Distric;
using Gaz_BackEnd.DATA.DTOs.Governorates;
using Gaz_BackEnd.DATA.DTOs.Station;
using Gaz_BackEnd.DATA.DTOs.Products;
using Gaz_BackEnd.DATA.DTOs.Reports;
using Gaz_BackEnd.DATA.DTOs.User;
using Gaz_BackEnd.Entities;
using OneSignalApi.Model;
using File = Gaz_BackEnd.Entities.File;
using Gaz_BackEnd.DATA.DTOs.Provider;
using Gaz_BackEnd.DATA.DTOs;
using Gaz_BackEnd.DATA.DTOs.cart;
using Gaz_BackEnd.DATA.DTOs.Notifications;
using Gaz_BackEnd.DATA.DTOs.Order;

namespace BackEndStructuer.Helpers{

    public class UserMappingProfile : Profile{
        private readonly string baseUrl;

        public UserMappingProfile() {
            
            baseUrl = "http://164.92.197.198:4376/";
            CreateMap<ArticleForm, Article>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ArticleUpdate, Article>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<AppUser, UserDto>()
                .ForMember(dest => dest.StationId, src => src.MapFrom(src => src.Stations.Select(x=>x.Id)));  
            CreateMap<AppUser, TokenDTO>();
            CreateMap<RegisterForm,AppUser>().ForAllMembers( opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UserForm,AppUser>().ForAllMembers( opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<RegisterForm, App>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<GovernorateForm, Governorate>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            
            CreateMap<GovernorateForm, Governorate>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Governorate, GovernoratesDTO>();

            CreateMap<DistrictForm, District>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<DistrictUpdate, District>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<District, DistrictDTO>();

            CreateMap<CityForm, City>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CityUpdate, City>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<City, CityDTO>()
            .ForMember(d => d.DistrictName, src => src.MapFrom(src => src.District.Name));

            CreateMap<AddressForm, Address>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<AddressUpdate, Address>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Address, AddressDTO>()
                .ForMember(dest => dest.GovernorateName, src => src.MapFrom(src => src.Governorate.Name))
                .ForMember(dest => dest.DistrictName, src => src.MapFrom(src => src.District.Name))
                .ForMember(dest => dest.CityName, src => src.MapFrom(src => src.City.Name));

            CreateMap<StationForm, Station>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<StationUpdate, Station>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Station, StationDTO>()
                .ForMember(dest => dest.GovernorateName, src => src.MapFrom(src => src.Governorate.Name))
                .ForMember(dest => dest.DistrictName, src => src.MapFrom(src => src.District.Name))
                .ForMember(dest => dest.UserName, src => src.MapFrom(src => src.AppUser.Name))
                .ForMember(dest => dest.CityName, src => src.MapFrom(src => src.City.Name));

            CreateMap<ProductForm, Product>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ProductUpdateDto, Product>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Path, opt => 
                    opt.MapFrom(
                        src => 
                            src.File != null ? baseUrl+src.File.Path.Replace("\\","/") : null));

                
            CreateMap<FileForm, File>();
            CreateMap<File, FileDto>();
            CreateMap<Document, DocunentDTO>().ForMember(dest => dest.Id, src => src.MapFrom(src => src.File.Id))
                .ForMember(dest => dest.Path, src => src.MapFrom(src => baseUrl+ src.File.Path));
            
            CreateMap<ReportForm,Report>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<ReportForm, Report>();
            CreateMap<Report, ReportDto>();

            CreateMap<ProviderForm, Provider>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UpdateProvider, Provider>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UpdateMyProfile, Provider>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Provider, ProviderDTO>().ForMember(dest => dest.StationName, src => src.MapFrom(src => src.Station.Name))
                .ForMember(
                                dest => dest.Rating,
                                src => src.MapFrom(
                                    src => (src.Orders != null && src.Orders.Any(x => x.Rating != null))
                                        ? Math.Round(src.Orders.Where(x => x.Rating != null).Average(x => x.Rating.Value), 2)
                                        : (double?)null
                                )
                            );



            CreateMap<Provider, TokenDTO>();

            CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.OrderProducts, src => src.MapFrom(src => src.OrderProducts))
            .ForMember(dest => dest.Client, src => src.MapFrom(src => src.User))
            .ForMember(des => des.Provider, src => src.MapFrom(src => src.Provider));

            CreateMap<OrderProduct, OrderProductDto>()
                .ForMember(dest => dest.Name, src => src.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, src => src.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.Image, src => src.MapFrom(src => src.Product.File != null ? baseUrl+src.Product.File.Path.Replace("\\","/") : null))
                .ForMember(dest => dest.Quantity, src => src.MapFrom(src => src.Quantity));
            
            CreateMap<CartProduct, CartProductDto>()
            .ForMember(des => des.Id, src => src.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Name, src => src.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.Price, src => src.MapFrom(src => src.Product.Price))
            .ForMember(dest => dest.Image, src => src.MapFrom(src => src.Product.File != null ? baseUrl+src.Product.File.Path.Replace("\\","/") : null))
            .ForMember(dest => dest.Quantity, src => src.MapFrom(src => src.Quantity));

            
            CreateMap<OrderFormDto, Order>()
            .ForMember(o => o.OrderProducts, opt => opt.MapFrom(src => src.OrderProducts))
            .ForAllMembers( opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Mapping from OrderProductFormDto to OrderProduct
            CreateMap<OrderProductFormDto, OrderProduct>()
            .ForAllMembers( opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CartForm, Cart>()
            .ForAllMembers( opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Cart, CartDto>()
            .ForMember(o => o.CartProducts, opt => opt.MapFrom(src => src.CartProducts));

            CreateMap<NotificationForm, Notifications>();
            CreateMap<Notifications, NotificationDto>();
        }
    }
}