using BackEndStructuer.Controllers;
using BackEndStructuer.Utils;
using Gaz_BackEnd.DATA.DTOs.Order;
using Gaz_BackEnd.DATA.DTOs.Products;
using Gaz_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gaz_BackEnd.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        
        [HttpGet]
        public async Task<ActionResult<Respons<OrderDto>>> GetAll([FromQuery]OrderFilters filters) => Ok(await _orderService.GetAll(filters,Id,Role), filters.PageNumber);

        
        [HttpPost]
        public async Task<ActionResult> Add([FromBody]OrderFormDto orderForm) => Ok(await _orderService.Add(orderForm, Id));
        
        [HttpPut("{id}/Approve")]
        public async Task<ActionResult> Approve(Guid id) => Ok(await _orderService.Approve(id, Id));
        
        [HttpPut("{id}/Delivered")]
        public async Task<ActionResult> Delivered(Guid id) => Ok(await _orderService.Delivered(id, Id));
        
        [HttpPut("{id}/Cancel")]
        public async Task<ActionResult> Cancel(Guid id) => Ok(await _orderService.Cancel(id, Id));
        [HttpPut("{id}/Rating")]
        public async Task<ActionResult> Rating(Guid id,RatingOrderForm ratingOrderForm) => Ok(await _orderService.Rating(id, Id, ratingOrderForm));
        //get order by provider id
        [HttpGet("GetOrderByProviderId/{id}")]
        public async Task<IActionResult> GetOrderByProviderId(Guid id,[FromQuery]OrderFilters filters) => Ok(await _orderService.GetOrderByProviderId(id,filters), filters.PageNumber);
    }
}