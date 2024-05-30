using BackEndStructuer.Entities;
using BackEndStructuer.Interface;
using Gaz_BackEnd.Entities;
using OneSignalApi.Model;

namespace Gaz_BackEnd.Interface;

public interface INotificationRepository : IGenericRepository<Notifications, Guid>{
}