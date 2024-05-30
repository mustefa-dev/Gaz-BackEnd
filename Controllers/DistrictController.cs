using BackEndStructuer.Controllers;
using BackEndStructuer.DATA.DTOs;
using BackEndStructuer.Services;
using BackEndStructuer.Utils;
using Gaz_BackEnd.DATA.DTOs.Distric;
using Gaz_BackEnd.DATA.DTOs.Products;
using Gaz_BackEnd.Entities;
using Gaz_BackEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gaz_BackEnd.Controllers
{
    
    public class DistrictController : BaseController
    {
        private readonly IDistrictService _DistrictServices;

        public DistrictController(IDistrictService DistrictServices)
        {
            _DistrictServices = DistrictServices;
        }

        [HttpGet]
        public async Task<ActionResult<Respons<DistrictDTO>>> GetDistricts([FromQuery] DistrictFilter districtFilter) => Ok(await _DistrictServices.GetAll(districtFilter),districtFilter.PageNumber);
        [HttpGet("/api/AllDistricts")]
        public async Task<ActionResult<Respons<DistrictDTO>>> All(Guid? GovernorateID) => Ok(await _DistrictServices.All(GovernorateID),1);
        [HttpGet("{id}")]
        public async Task<ActionResult> GetDistrictById(Guid id) => Ok(await _DistrictServices.GetById(id));
        [HttpPost]
        public async Task<ActionResult> AddDistrict([FromBody] DistrictForm DistrictForm) => Ok(await _DistrictServices.add(DistrictForm));
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDistrict([FromBody] DistrictUpdate DistrictUpdate, Guid id)
        {
            var result = await _DistrictServices.update(DistrictUpdate, id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDistrict(Guid id)
        {
            var result = await _DistrictServices.Delete(id);
            return Ok(result);
        }
    }
}