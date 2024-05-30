using BackEndStructuer.Interface;
using BackEndStructuer.Interface;
using Gaz_BackEnd.Interface;

namespace BackEndStructuer.Repository
{
    public interface IRepositoryWrapper
    {
   
        IUserRepository User { get; }
        IArticleRespository Article { get; }
        IGovernoratesRepository Governorates { get; }
        
        IProductRepository Product { get; }
        
        IFileRepository File { get; }
        IReportRepository Report { get; }
        IDistrictRepository District { get; }
        ICityRepository City { get; }
        IOtpRepository Otp { get; }
        IAddressRepository Address { get; }
        IStationRepository Station { get; }
        IProviderRepository Provider { get; }
        IDocumentRepository Document { get; }
        IOrderRepository Order { get; }
        
        ICartRepository Cart { get; }
        
        ICartProduct CartProduct { get; }
        INotificationRepository Notification { get; }

        
    }
}