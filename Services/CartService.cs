using AutoMapper;
using BackEndStructuer.Repository;
using Gaz_BackEnd.DATA.DTOs.cart;
using Gaz_BackEnd.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gaz_BackEnd.Services
{
    public interface ICartService
    {
        Task<(CartDto? data,String? error)> GetMyCart(Guid userId);
        Task<(string? message, string? error)> AddToCart(Guid userId, CartForm cartForm);
        
        Task<(string? message, string? error)> DeleteFromCart(Guid userId, Guid productId, int quantity);
    }
    
    public class CartService : ICartService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        public CartService(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }
        public async Task<(CartDto? data, string? error)> GetMyCart(Guid userId)
        {
            var cart = await _repositoryWrapper.Cart.Get(x => x.UserId == userId, i => i.Include(x => x.CartProducts).ThenInclude(p => p.Product).ThenInclude(p => p.File));
            // calc total price
            
            cart.TotalPrice = (decimal)cart.CartProducts.Sum(x => x.Product.Price * x.Quantity);

            if (cart == null)
            {
                return (null, "Cart Not Found");
            }
            var cartDto = _mapper.Map<CartDto>(cart);
            return (cartDto, null);
        }
        public async Task<(string? message, string? error)> AddToCart(Guid userId, CartForm cartForm)
        {
            
            var cart = await _repositoryWrapper.Cart.Get(x => x.UserId == userId, i => i.Include(x => x.CartProducts));
            if (cart == null)
            {
                cart = new Cart()
                {
                    UserId = userId,
                    CartProducts = new List<CartProduct>()
                };
                await _repositoryWrapper.Cart.Add(cart);
            }
            
            // check if product is already in cart
            
            foreach (var cartProduct in cartForm.CartProducts)
            {
                var product = await _repositoryWrapper.Product.Get(x => x.Id == cartProduct.ProductId);
                if (product == null)
                {
                    return (null, "Product Not Found");
                }
                
                var cartProductEntity = await _repositoryWrapper.CartProduct.Get(x =>
                    x.CartId == cart.Id && x.ProductId == cartProduct.ProductId);
                
                if (cartProductEntity == null) // add new product to cart
                {
                    var newCartProduct = new CartProduct()
                    {
                        CartId = cart.Id,
                        ProductId = cartProduct.ProductId,
                        Quantity = cartProduct.Quantity
                    };
                    await _repositoryWrapper.CartProduct.Add(newCartProduct);
                }
                else // update product quantity
                {
                    return (null, "المنتج موجود بالفعل في السلة");
                }
                
            }
            
            var result = await _repositoryWrapper.Cart.Update(cart);
            if (result == null)
            {
                return (null, "لا يمكن اضافة المنتجات الى السلة");
            }
            
            return ("تم اضافة المنتجات الى السلة", null);
            
            
        }
        public async Task<(string? message, string? error)> DeleteFromCart(Guid userId, Guid productId, int quantity)
        {
            var cart = await _repositoryWrapper.Cart.Get(x => x.UserId == userId, i => i.Include(x => x.CartProducts));
            if (cart == null)
            {
                return (null, "لم يتم العثور على السلة");
            }
            
            var cartProduct = cart.CartProducts.FirstOrDefault(x => x.ProductId == productId);
            if (cartProduct == null)
            {
                return (null, "لم يتم العثور على المنتج في السلة");
            }
            
            if (cartProduct.Quantity > quantity)
            {
                cartProduct.Quantity -= quantity;
                await _repositoryWrapper.CartProduct.Update(cartProduct);
            }
            else
            {
                await _repositoryWrapper.CartProduct.Delete(cartProduct.Id);
            }
            
            return ("تم حذف المنتج من السلة", null);
        }
    }
}