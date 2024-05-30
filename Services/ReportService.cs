using AutoMapper;
using BackEndStructuer.Repository;
using Gaz_BackEnd.DATA.DTOs.Reports;
using Gaz_BackEnd.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Gaz_BackEnd.Services;

public interface IReportService{
   Task<(ReportDto report, string? error)> add(ReportForm reportForm);
   Task<(List<ReportDto> reportDtos, int? totalCount, string? error)> GetAll(ReportFilter reportFilter);
   Task<(ReportDto report, string? error)> GetById(Guid id);
   Task<(ReportDto? report, string?)> Delete(Guid id);

}
public class ReportService:IReportService{
   
   
   private readonly IMapper _mapper;
   private readonly IRepositoryWrapper _repositoryWrapper;

   public ReportService(IMapper mapper, IRepositoryWrapper repositoryWrapper, IPhotoService photoService) {
      _mapper = mapper;
      _repositoryWrapper = repositoryWrapper;
   }

   public async Task<(ReportDto report, string? error)> add(ReportForm reportForm) {
      var report = _mapper.Map<Report>(reportForm);
      var result = await _repositoryWrapper.Report.Add(report);
      var reportDto = _mapper.Map<ReportDto>(report);
      return result == null ? (null, "Report could not add") : (reportDto, null);



   }

   public  async Task<(List<ReportDto> reportDtos, int? totalCount, string? error)> GetAll(ReportFilter reportFilter) {
      var (reports, totalCount) = await _repositoryWrapper.Report.GetAll(x => x.Deleted != true,reportFilter.PageNumber,reportFilter.PageSize);
      var result = _mapper.Map<List<ReportDto>>(reports);
      return (result, totalCount, null);   }

  
         
   public async Task<(ReportDto report, string? error)> GetById(Guid id) {
      var report = await _repositoryWrapper.Report.GetById(id);
      var reportDto = _mapper.Map<ReportDto>(report);
      return report == null ? (null, "Report not found") : (reportDto, null);
   }
   
   public async Task<(ReportDto? report, string?)> Delete(Guid id) {
      var report = await _repositoryWrapper.Report.GetById(id);
      if (report == null) {
         return (null, "Report not found");
      }
      report.Deleted = true;
      var result = await _repositoryWrapper.Report.Update(report);
      var reportDto = _mapper.Map<ReportDto>(result);
      return (reportDto, null);
   }
   
         
}           