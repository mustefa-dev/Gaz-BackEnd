using BackEndStructuer.Controllers;
using BackEndStructuer.DATA.DTOs.Files;
using Gaz_BackEnd.Interface;
using Gaz_BackEnd.Respository;
using Gaz_BackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gaz_BackEnd.Controllers;

public class FileController:BaseController{
        
    private readonly IFileService _fileService;
    
    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }
    
    [HttpPost]
    public async Task<ActionResult> Add([FromForm] FileForm fileForm) => Ok(await _fileService.add(fileForm));
    
    [HttpGet]
    public async Task<ActionResult> GetProduct(int pageNumber = 1) =>
        Ok(await _fileService.GetAll(pageNumber), pageNumber);
    

    

}