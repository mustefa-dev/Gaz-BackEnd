using BackEndStructuer.DATA;
using BackEndStructuer.Repository;
using Gaz_BackEnd.Entities;
using Gaz_BackEnd.Interface;


namespace Gaz_BackEnd.Respository;

public class NotificationRepository:GenericRepository<Notifications,Guid>,INotificationRepository{
    
    private readonly DataContext _context;

    public NotificationRepository(DataContext context) : base(context)
    {
        _context = context;
    }
    
}