using AutoMapper;
using BackEndStructuer.DATA;
using BackEndStructuer.DATA.DTOs;
using BackEndStructuer.Entities;
using BackEndStructuer.Repository;
using BackEndStructuer.Utils;
using Gaz_BackEnd.DATA.DTOs.City;
using Gaz_BackEnd.DATA.DTOs.Distric;
using Gaz_BackEnd.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gaz_BackEnd.Services
{
    public interface IDistrictService
    {
        Task<(List<DistrictDTO> Districts, int? totalCount, string? error)> All(Guid? GovernorateID);
        Task<(List<DistrictDTO> Districts, int? totalCount, string? error)> GetAll(DistrictFilter districtFilter);
        Task<(DistrictDTO Districts, string? error)> GetById(Guid id);
        Task<(District? District, string? error)> add(DistrictForm DistrictForm);
        Task<(DistrictDTO? District, string? error)> update(DistrictUpdate DistrictUpdate, Guid id);
        Task<(DistrictDTO? District, string?)> Delete(Guid id);
    }
    public class DistrictService:IDistrictService
    {

        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public DistrictService(IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<(District? District, string? error)> add(DistrictForm DistrictForm)
        {
            var get = await _repositoryWrapper.District.Get(x => x.Name == DistrictForm.Name);
            if (get != null)
            {
                return (null, "القضاء موجود مسبقا");
            }
            var District = _mapper.Map<District>(DistrictForm);
            var result = await _repositoryWrapper.District.Add(District);
            return result == null ? (null, "لا يمكن اضافة قضاء") : (District, null);
        }
        public async Task<(List<DistrictDTO> Districts, int? totalCount, string? error)> All(Guid? GovernorateID)
        {
            var (Districts, totalCount) = await _repositoryWrapper.District.GetAll(x => (x.Deleted != true)&&(GovernorateID==null||x.GovernorateId==GovernorateID));
            var districtDTO = _mapper.Map<List<DistrictDTO>>(Districts);
            return (districtDTO, totalCount, null);
        }
        public async Task<(List<DistrictDTO> Districts, int? totalCount, string? error)> GetAll(DistrictFilter districtFilter)
        {
            var (Districts, totalCount) = await _repositoryWrapper.District.GetAll(x => (x.Deleted != true)&&
            (districtFilter.Name == null || x.Name.Contains(districtFilter.Name))&& (districtFilter.GovernorateId == null || x.GovernorateId==districtFilter.GovernorateId), i=>i.Include(x=>x.Cities.Where(x => x.Deleted != true)), districtFilter.PageNumber, districtFilter.PageSize);
            var districtDTO= _mapper.Map<List<DistrictDTO>>(Districts);
            return (districtDTO, totalCount, null);
        }
        public async Task<(DistrictDTO Districts, string? error)> GetById(Guid id)
        {
            var getDistrict = await _repositoryWrapper.District.Get(d => d.Id == id && d.Deleted != true,i=> i.Include(x => x.Cities.Where(x=>x.Deleted!=true)));
            if (getDistrict == null) return (null, "القضاء غير متوفر");
            var DistrictDto = _mapper.Map<DistrictDTO>(getDistrict);
            return (DistrictDto, null);
        }
        public async Task<(DistrictDTO? District, string? error)> update(DistrictUpdate DistrictUpdate, Guid id)
        {
            var district = await _repositoryWrapper.District.GetById(id);
            if (district == null)
            {
                return (null, "القضاء غير متوفر");
            }
            if (DistrictUpdate.Name != null && DistrictUpdate.Name != district.Name)
            {
                var get = await _repositoryWrapper.District.Get(x => x.Name == DistrictUpdate.Name);
                if (get != null)
                {
                    return (null, "القضاء موجود مسبقا");
                }
            }
            district = _mapper.Map(DistrictUpdate, district);
            var response = await _repositoryWrapper.District.Update(district);
            var districtDTO = _mapper.Map<DistrictDTO>(response);
            return response == null ? (null, "لا يمكن تحديث القضاء") : (districtDTO, null);
        }
        public async Task<(DistrictDTO? District, string?)> Delete(Guid id)
        {
            var district = await _repositoryWrapper.District.GetById(id);
            if (district == null) return (null, "القضاء غير متوفر");
            district.Deleted=true;
            var response = await _repositoryWrapper.District.Update(district);
            var districtDTO = _mapper.Map<DistrictDTO>(response);
            return response == null ? (null, "لا يمكن حذف القضاء") : (districtDTO, null);
        }
    }
}