using OneSignalApi.Model;

namespace Gaz_BackEnd.DATA.DTOs.Reports;

public class ReportForm{
    public string? Description { get; set; }
    public Guid UserId { get; set; }
    public string? Title { get; set; }
}