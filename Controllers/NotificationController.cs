using BackEndStructuer.Controllers;
using Gaz_BackEnd.DATA.DTOs.Notifications;
using Gaz_BackEnd.DATA.DTOs.Station;
using Gaz_BackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gaz_BackEnd.Controllers;

public class NotificationController : BaseController{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService) {
        _notificationService = notificationService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetNotification(Guid id) => Ok(await _notificationService.GetById(id));

    [HttpGet]
    public async Task<ActionResult> GetAll([FromQuery] StationFilter stationFilter) =>
        Ok(await _notificationService.GetAll(stationFilter, Id));

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] NotificationForm notificationForm) =>
        Ok(await _notificationService.add(notificationForm));

    [HttpDelete]
    public async Task<ActionResult> Delete(Guid id) => Ok(await _notificationService.Delete(id));

    //GET UNREAD NOTIFICATIONS
    [HttpGet("unread")]
    public async Task<ActionResult> GetUnreadNotifications([FromQuery] StationFilter stationFilter) =>
        Ok(await _notificationService.GetUnreadNotifications(stationFilter));
}