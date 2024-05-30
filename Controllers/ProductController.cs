using BackEndStructuer.Controllers;
using BackEndStructuer.Utils;
using Gaz_BackEnd.DATA.DTOs.Products;
using Gaz_BackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gaz_BackEnd.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductController : BaseController{
    private readonly IProductServices _productServices;

    public ProductController(IProductServices productServices) {
        _productServices = productServices;
    }

    [HttpGet]
    public async Task<ActionResult<Respons<ProductDto>>> GetProduct([FromQuery] ProductFilter productFilter) =>
        Ok(await _productServices.GetAll(productFilter), productFilter.PageNumber);

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> Get(Guid id) => Ok(await _productServices.GetById(id));


    [HttpPost]
    public async Task<ActionResult> AddProduct([FromBody] ProductForm productForm) =>
        Ok(await _productServices.add(productForm));


    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProduct([FromBody] ProductUpdateDto productUpdate, Guid id) {
        var result = await _productServices.update(productUpdate, id);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(Guid id) {
        var result = await _productServices.Delete(id);
        return Ok(result);
    }
}