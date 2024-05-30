using BackEndStructuer.Controllers;
using Gaz_BackEnd.DATA.DTOs.Station;
using Gaz_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using BackEndStructuer.Utils;
using Gaz_BackEnd.DATA.DTOs.Products;

namespace Gaz_BackEnd.Controllers
{
    
    public class StationController : BaseController
    {
        private readonly IStationService _StationServices;

        public StationController(IStationService StationServices)
        {
            _StationServices = StationServices;
        }

        [HttpGet]
        public async Task<ActionResult<Respons<StationDTO>>> GetStations([FromQuery] StationFilter stationFilter) => Ok(await _StationServices.GetAll(stationFilter), stationFilter.PageNumber);
        [HttpGet("{id}")]
        public async Task<ActionResult> GetStationById(Guid id) => Ok(await _StationServices.GetById(id));
       
        [HttpPost]
        public async Task<ActionResult> AddStation([FromBody] StationForm stationForm) => Ok(await _StationServices.add(stationForm));
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStation([FromBody] StationUpdate stationUpdate, Guid id) => Ok(await _StationServices.update(stationUpdate, id));
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStation(Guid id) => Ok(await _StationServices.Delete(id));
    }
}