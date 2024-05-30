using BackEndStructuer.Controllers;
using BackEndStructuer.DATA.DTOs;
using BackEndStructuer.Services;
using BackEndStructuer.Utils;
using Gaz_BackEnd.DATA.DTOs.Governorates;
using Gaz_BackEnd.DATA.DTOs.Products;
using Gaz_BackEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gaz_BackEnd.Controllers
{
    
    public class GovernorateController : BaseController
    {
        private readonly IGovernorateService _GovernoratesServices;

        public GovernorateController(IGovernorateService GovernoratesServices)
        {
            _GovernoratesServices = GovernoratesServices;
        }

        [HttpGet]
        public async Task<ActionResult<Respons<GovernoratesDTO>>> GetGovernorates([FromQuery] GovernorateFilter governorateFilter) => Ok(await _GovernoratesServices.GetAll(governorateFilter), governorateFilter.PageNumber);
        [HttpGet("/api/AllGovernorates")]
        public async Task<ActionResult<Respons<GovernoratesDTO>>> All() => Ok(await _GovernoratesServices.All(),1);
        [HttpGet("{id}")]
        public async Task<ActionResult> GetGovernorate(Guid id) => Ok(await _GovernoratesServices.GetById(id));
        [HttpPost]
        public async Task<ActionResult> AddGovernorates([FromBody] GovernorateForm GovernoratesForm) => Ok(await _GovernoratesServices.add(GovernoratesForm));
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGovernorates([FromBody] GovernorateForm GovernoratesForm, Guid id)
        {
            var result = await _GovernoratesServices.update(GovernoratesForm, id);
            return Ok(result);
        }
    
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGovernorates(Guid id)
        {
            var result = await _GovernoratesServices.Delete(id);
            return Ok(result);
        }
    }
}