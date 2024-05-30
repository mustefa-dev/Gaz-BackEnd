using BackEndStructuer.Controllers;
using BackEndStructuer.Utils;
using Gaz_BackEnd.DATA.DTOs.City;
using Gaz_BackEnd.DATA.DTOs.Products;
using Gaz_BackEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gaz_BackEnd.Controllers
{
    public class CityController : BaseController
    {
        private readonly ICityService _CityServices;

        public CityController(ICityService CityServices)
        {
            _CityServices = CityServices;
        }

        [HttpGet("/api/AllCities")]
        public async Task<ActionResult<Respons<CityDTO>>> All(Guid? DistrictId) => Ok(await _CityServices.All( DistrictId),1);
        [HttpGet]
        public async Task<ActionResult<Respons<CityDTO>>> GetCities([FromQuery] CityFilter cityFilter) => Ok(await _CityServices.GetAll(cityFilter),cityFilter.PageNumber);
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCityById(Guid id) => Ok(await _CityServices.GetById(id));
        [HttpPost]
        public async Task<ActionResult> AddCity([FromBody] CityForm CityForm) => Ok(await _CityServices.add(CityForm));
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCity([FromBody] CityUpdate CityUpdate, Guid id)
        {
            var result = await _CityServices.update(CityUpdate, id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCity(Guid id)
        {
            var result = await _CityServices.Delete(id);
            return Ok(result);
        }
    }
}