using BackEndStructuer.Controllers;
using BackEndStructuer.Utils;
using Gaz_BackEnd.DATA.DTOs.Products;
using Gaz_BackEnd.DATA.DTOs.Reports;
using Gaz_BackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gaz_BackEnd.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ReportController : BaseController{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService) {
        _reportService = reportService;
    }

    [HttpGet]
    public async Task<ActionResult<Respons<ReportDto>>> GetReports([FromQuery]ReportFilter reportFilter) =>
        Ok(await _reportService.GetAll(reportFilter), reportFilter.PageNumber);



    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> Get(Guid id) => Ok(await _reportService.GetById(id));


    [HttpPost]
    public async Task<ActionResult> AddReport([FromBody] ReportForm reportForm) =>
        Ok(await _reportService.add(reportForm));


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteReport(Guid id) {
        var result = await _reportService.Delete(id);
        return Ok(result);
    }
}