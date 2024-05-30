using BackEndStructuer.Controllers;
using BackEndStructuer.Entities;
using Gaz_BackEnd.DATA.DTOs.Products;
using Gaz_BackEnd.DATA.DTOs.Provider;
using Gaz_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using BackEndStructuer.DATA.DTOs;
using BackEndStructuer.Utils;
using Gaz_BackEnd.DATA.DTOs;

namespace Gaz_BackEnd.Controllers{
    public class ProviderController : BaseController{
        private readonly IProviderService _ProviderServices;

        public ProviderController(IProviderService ProviderServices) {
            _ProviderServices = ProviderServices;
        }

        [HttpGet]
        public async Task<ActionResult<Respons<ProviderDTO>>> GetProviders([FromQuery] ProviderFilter ProviderFilter) =>
            Ok(await _ProviderServices.GetAll(ProviderFilter), ProviderFilter.PageNumber);

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProviderById(Guid id) {
            Guid ProviderId = id;
            if (Role == UserRole.Provider) {
                ProviderId = Id;
            }

            var result = await _ProviderServices.GetById(ProviderId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddProvider([FromBody] ProviderForm ProviderForm) =>
            Ok(await _ProviderServices.add(ProviderForm));

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProvider([FromBody] UpdateProvider ProviderUpdate, Guid id) {
            return Ok(await _ProviderServices.update(ProviderUpdate, id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProvider(Guid id) => Ok(await _ProviderServices.Delete(id));

        [HttpGet("MyProfile")]
        public async Task<ActionResult<ProviderDTO>> GetMyProfile() => Ok(await _ProviderServices.GetMyProfile(Id));

        [HttpPut("MyProfile")]
        public async Task<ActionResult> UpdateMyProfile(UpdateMyProfile updateMyProfile) {
            return Ok(await _ProviderServices.updateMyProfile(updateMyProfile, Id));
        }

        [HttpGet("GetProviderByStationId/{id}")]
        public async Task<ActionResult<Respons<ProviderDTO>>>
            GetProviderByStationId(Guid id, [FromQuery] BaseFilter baseFilter) =>
            Ok(await _ProviderServices.GetProvidersByStationId(id, baseFilter), baseFilter.PageNumber
            );
        
        [HttpGet("GetProviderSells/{id}")]
        public async Task<ActionResult<Respons<ProviderDTO>>>
            GetProviderSells(Guid id, [FromQuery] DateFilter dateFilter) =>
            Ok(await _ProviderServices.GetProviderSells(id ,dateFilter));
            
    
    }
}