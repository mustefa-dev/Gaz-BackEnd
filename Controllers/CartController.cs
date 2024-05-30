using BackEndStructuer.Controllers;
using Gaz_BackEnd.DATA.DTOs.cart;
using Gaz_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gaz_BackEnd.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartService _service;
        public CartController(ICartService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<CartDto>> Get() => Ok(await _service.GetMyCart(Id));
        
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddToCart(CartForm cartForm) => Ok(await _service.AddToCart(Id, cartForm));
        
        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteFromCart([FromQuery] Guid ProductId, int Quantity) => Ok(await _service.DeleteFromCart(Id, ProductId, Quantity));
    }
}