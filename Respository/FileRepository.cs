using BackEndStructuer.DATA;
using BackEndStructuer.Repository;
using Gaz_BackEnd.DATA;
using Gaz_BackEnd.Interface;
using File = Gaz_BackEnd.Entities.File;

namespace Gaz_BackEnd.Respository;

public class FileRepository:GenericRepository<File,Guid>,IFileRepository{
    private readonly DataContext _context;

    public FileRepository(DataContext context) : base(context)
    {
        _context = context;
    }
    
}