
using BackEndStructuer.DATA;
using BackEndStructuer.Interface;
using BackEndStructuer.Interface;
using Gaz_BackEnd.DATA;
using Gaz_BackEnd.Interface;
using Gaz_BackEnd.Respository;

namespace BackEndStructuer.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly DataContext _context;
    
        private IUserRepository _user;
        private IArticleRespository _articles;
        private IGovernoratesRepository _governorates;
        private IFileRepository _file;
        private IProductRepository _product;
        private IReportRepository _report;
        private IDistrictRepository _district;
        private ICityRepository _city;
        private IOtpRepository _otp;
        private IAddressRepository _address;
        private IStationRepository _station;
        private IProviderRepository _provider;
        private IDocumentRepository _document;
        private IOrderRepository _order;
        private ICartRepository _cart;
        private ICartProduct _cartProduct;
        private INotificationRepository _notification;
        
        
        public RepositoryWrapper(DataContext context) {
            _context = context;
        }


        public ICartProduct CartProduct
        {
            get
            {
                if (_cartProduct == null)
                {
                    _cartProduct = new CartProductRepository(_context);
                }
                return _cartProduct;
            }
        }
        
        public ICartRepository Cart
        {
            get
            {
                if (_cart == null)
                {
                    _cart = new CartRepository(_context);
                }
                return _cart;
            }
        }
        public IArticleRespository Article {  get {
            if(_articles == null)
            {
                _articles = new ArticleRepository(_context);
            }
            return _articles;
        } }
        
        public IOrderRepository Order {  get {
            if(_order == null)
            {
                _order = new OrderRepository(_context);
            }
            return _order;
        } }

        
        public IUserRepository User {  get {
            if(_user == null)
            {
                _user = new UserRepository(_context);
            }
            return _user;
        } }

        public IGovernoratesRepository Governorates
        {
            get
            {
                if (_governorates== null)
                {
                    _governorates = new GovernoratesRepository(_context);
                }
                return _governorates;
            }
        }

        public IDistrictRepository District
        {
            get
            {
                if (_district == null)
                {
                    _district = new DistrictRepository(_context);
                }
                return _district;
            }
        }
        public ICityRepository City
        {
            get
            {
                if (_city == null)
                {
                    _city = new CityRepository(_context);
                }
                return _city;
            }
        }
        public IOtpRepository Otp
        {
            get
            {
                if (_otp == null)
                {
                    _otp = new OtpRepository(_context);
                }
                return _otp;
            }
        }
        public IAddressRepository Address
        {
            get
            {
                if (_address == null)
                {
                    _address = new AddressRepository(_context);
                }
                return _address;
            }
        }public IStationRepository Station
        {
            get
            {
                if (_station == null)
                {
                    _station = new StationRepository(_context);
                }
                return _station;
            }
        }
            
        
        public IFileRepository File
        {
            get
            {
                if (_file== null)
                {
                    _file = new FileRepository(_context);
                }
                return _file;
            }
        }
        public IReportRepository Report {  get {
            if(_report == null)
            {
                _report = new ReportRepository(_context);
            }
            return _report;
        } }
        public IProductRepository Product
        {
            get
            {
                if (_product == null)
                {
                    _product = new ProductRepository(_context);
                }
                return _product;
            }
        }
        public IProviderRepository Provider
        {
            get
            {
                if (_provider == null)
                {
                    _provider = new ProviderRepository(_context);
                }
                return _provider;
            }
        }
        public IDocumentRepository Document
        {
            get
            {
                if (_document == null)
                {
                    _document = new DocumentRepository(_context);
                }
                return _document;
            }
        }
        public INotificationRepository Notification
        {
            get
            {
                if (_notification == null)
                {
                    _notification = new NotificationRepository(_context);
                }
                return _notification;
            }
        }
    }
}