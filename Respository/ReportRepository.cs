using BackEndStructuer.DATA;
using BackEndStructuer.Repository;
using Gaz_BackEnd.DATA;
using Gaz_BackEnd.Entities;
using Gaz_BackEnd.Interface;
using Gaz_BackEnd.Services;

namespace Gaz_BackEnd.Respository;

public class ReportRepository: GenericRepository<Report,Guid>,IReportRepository{
    private readonly DataContext _context;

    public ReportRepository(DataContext context) : base(context) {
        _context = context;
    }
}