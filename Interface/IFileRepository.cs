using BackEndStructuer.Entities;
using BackEndStructuer.Interface;
using File = Gaz_BackEnd.Entities.File;

namespace Gaz_BackEnd.Interface;

public interface IFileRepository :IGenericRepository<File,Guid>{
    
}