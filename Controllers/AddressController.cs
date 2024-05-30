using BackEndStructuer.Controllers;
using Gaz_BackEnd.DATA.DTOs.Address;
using Gaz_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using BackEndStructuer.Utils;
using Gaz_BackEnd.DATA.DTOs.Products;

namespace Gaz_BackEnd.Controllers
{
    public class AddressController : BaseController
    {
        private readonly IAddressService _AddressServices;

        public AddressController(IAddressService AddressServices)
        {
            _AddressServices = AddressServices;
        }
        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<ActionResult<Respons<AddressDTO>>> GetAddressess([FromQuery]AddressFilter addressFilter) => Ok(await _AddressServices.GetAll(Id,addressFilter), addressFilter.PageNumber);
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAddressById(Guid id) => Ok(await _AddressServices.GetById(id));
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<ActionResult> AddAddress([FromBody] AddressForm addressForm) => Ok(await _AddressServices.add(addressForm,Id));
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAddress([FromBody] AddressUpdate addressUpdate, Guid id)=>  Ok(await _AddressServices.update(addressUpdate, id,Id));
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAddress(Guid id) => Ok(await _AddressServices.Delete(id));
        
        [Authorize]
        [HttpGet("MyAddress")]
        public async Task<ActionResult> GetAllByUserId([FromQuery] AddressFilter filter) => Ok(await _AddressServices.GetAllByUserId(Id),filter.PageNumber);
        
    }
}