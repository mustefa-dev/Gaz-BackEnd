using AutoMapper;
using BackEndStructuer.DATA.DTOs.Files;
using BackEndStructuer.Repository;
using Gaz_BackEnd.DATA.DTOs.Governorates;
using Gaz_BackEnd.DATA.DTOs.Products;
using Gaz_BackEnd.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gaz_BackEnd.Services;

public interface IProductServices{
    Task<(ProductDto? productDto, string? error)> add(ProductForm productForm);
    Task<(List<ProductDto> productDto, int? totalCount, string? error)> GetAll(ProductFilter productFilter);
    Task<(ProductDto productDto, int?totalCount, string?error)> GetById(Guid id);
    Task<(ProductDto? productDto, string?error)> update(ProductUpdateDto productUpdate, Guid id);
    Task<(ProductDto? productDto, string?)> Delete(Guid id);
}

public class ProductSerivce : IProductServices{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public ProductSerivce(IMapper mapper, IRepositoryWrapper repositoryWrapper) {
        _mapper = mapper;
        _repositoryWrapper = repositoryWrapper;
    } 

    public async Task<(ProductDto? productDto, string? error)> add(ProductForm productForm) {
        var product = _mapper.Map<Product>(productForm);


        var result = await _repositoryWrapper.Product.Add(product);
        var productDto = _mapper.Map<ProductDto>(result);
        return result == null ? (null, "product could not add") : (productDto, null);
    }

    public async Task<(List<ProductDto> productDto, int? totalCount, string? error)> GetAll(ProductFilter productFilter) {
        var (products, totalCount) = await _repositoryWrapper.Product
            .GetAll(x => (x.Deleted != true)&&(productFilter.Name==null|| x.Name.Contains(productFilter.Name)) && (productFilter.Type==null|| x.Type == productFilter.Type), include: product => 
                product.Include(p => p.File)!,productFilter.PageNumber,productFilter.PageSize);
        var result = _mapper.Map<List<ProductDto>>(products);

        return (result, totalCount, null);
    }


    public async Task<(ProductDto productDto, int? totalCount, string? error)> GetById(Guid id) {
        var product = await _repositoryWrapper.Product.Get(p=>p.Id==id, include: product => 
            product.Include(p => p.File));
        var productDto = _mapper.Map<ProductDto>(product);
        return product == null ? (null, null, "product not found") : (productDto, null, null);
    }

    public async Task<(ProductDto? productDto, string? error)> update(ProductUpdateDto productUpdate, Guid id) {
        var product = await _repositoryWrapper.Product.GetById(id);
        if (product == null) {
            return (null, "product not found");
        }

        product = _mapper.Map(productUpdate, product);
        var response = await _repositoryWrapper.Product.Update(product);
        var productDto = _mapper.Map<ProductDto>(response);
        return response == null ? (null, "product could not be updated") : (productDto, null);
    }

    public async Task<(ProductDto? productDto, string?)> Delete(Guid id) {
        var product = await _repositoryWrapper.Product.GetById(id);
        if (product == null) {
            return (null, "product not found");
        }

        product.Deleted = true;
        var response = await _repositoryWrapper.Product.Update(product);
        var productDto = _mapper.Map<ProductDto>(response);
        return response == null ? (null, "product could not be deleted") : (productDto, null);
    }
}