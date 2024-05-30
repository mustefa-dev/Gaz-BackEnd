using AutoMapper;
using BackEndStructuer.Repository;
using Gaz_BackEnd.DATA.DTOs.Address;
using Gaz_BackEnd.DATA.DTOs.City;
using Gaz_BackEnd.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gaz_BackEnd.Services
{
    public interface ICityService
    {
        Task<(City? City, string? error)> add(CityForm CityForm);
        Task<(List<CityDTO> Cities, int? totalCount, string? error)> GetAll(CityFilter cityFilter);
        Task<(List<CityDTO> Cities, int? totalCount, string? error)> All(Guid? DistrictID);
        Task<(CityDTO city, string? error)> GetById(Guid id);
        Task<(CityDTO? City, string? error)> update(CityUpdate CityUpdate, Guid id);
        Task<(CityDTO? City, string?)> Delete(Guid id);
    }
    public class CityService:ICityService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CityService(IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<(City? City, string? error)> add(CityForm CityForm)
        {
            var get = await _repositoryWrapper.City.Get(x=>x.Name==CityForm.Name);
            if (get != null)
            {
                return (null, "المدينة موجودة مسبقا");
            }
            var City = _mapper.Map<City>(CityForm);
            var result = await _repositoryWrapper.City.Add(City);
            return result == null ? (null, "لا يمكن اضافة مدينة") : (City, null);
        }
        public async Task<(List<CityDTO> Cities, int? totalCount, string? error)> GetAll(CityFilter cityFilter)
        {
            var (citirs, totalCount) = await _repositoryWrapper.City.GetAll(x => (x.Deleted != true)&&
                (cityFilter.Name == null || x.Name.Contains(cityFilter.Name)) && (cityFilter.DistrictId == null || x.DistrictId==cityFilter.DistrictId), i => i.Include(d => d.District), cityFilter.PageNumber,cityFilter.PageSize);
            var CityDTO = _mapper.Map<List<CityDTO>>(citirs);
            return (CityDTO, totalCount, null);
        }
        public async Task<(List<CityDTO> Cities, int? totalCount, string? error)> All(Guid? DistrictID)
        {
            var (citirs, totalCount) = await _repositoryWrapper.City.GetAll(x => (x.Deleted != true)&&(DistrictID==null||x.DistrictId==DistrictID));
            var CityDTO = _mapper.Map<List<CityDTO>>(citirs);
            return (CityDTO, totalCount, null);
        }
        public async Task<(CityDTO city, string? error)> GetById(Guid id)
        {
            var getCity = await _repositoryWrapper.City.Get(d => d.Id == id && d.Deleted != true);
            if (getCity == null) return (null, "المدينة غير متوفر");
            var CityDto = _mapper.Map<CityDTO>(getCity);
            return (CityDto, null);
        }
        public async Task<(CityDTO? City, string? error)> update(CityUpdate CityUpdate, Guid id)
        {
            var City = await _repositoryWrapper.City.GetById(id);
            if (City == null)
            {
                return (null, "المدينة غير متوفر");
            }
            if (CityUpdate.Name!=null&&CityUpdate.Name!=City.Name) 
            {
                var get = await _repositoryWrapper.City.Get(x => x.Name == CityUpdate.Name);
                if (get != null)
                {
                    return (null, "المدينة موجودة مسبقا");
                }
            }
            
            City = _mapper.Map(CityUpdate, City);
            var response = await _repositoryWrapper.City.Update(City);
            var CityDTO = _mapper.Map<CityDTO>(response);
            return response == null ? (null, "لا يمكن تحديث المدينة") : (CityDTO, null);
        }
        public async Task<(CityDTO? City, string?)> Delete(Guid id)
        {
            var City = await _repositoryWrapper.City.GetById(id);
            if (City == null) return (null, "المدينة غير متوفر");
            City.Deleted = true;
            var response = await _repositoryWrapper.City.Update(City);
            var CityDTO = _mapper.Map<CityDTO>(response);
            return response == null ? (null, "لا يمكن حذف المدينة") : (CityDTO, null);
        }
    }
}