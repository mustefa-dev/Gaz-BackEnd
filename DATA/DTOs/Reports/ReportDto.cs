using Gaz_BackEnd.DATA.DTOs.BaseDto;
using OneSignalApi.Model;

namespace Gaz_BackEnd.DATA.DTOs.Reports;

public class ReportDto: BaseDto<Guid>{
    public string? Description { get; set; }
    public Guid UserId { get; set; }
    public string? Title { get; set; }
}