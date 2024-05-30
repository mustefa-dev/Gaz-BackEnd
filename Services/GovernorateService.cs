using AutoMapper;
using BackEndStructuer.DATA;
using BackEndStructuer.DATA.DTOs;
using BackEndStructuer.DATA.DTOs.User;
using BackEndStructuer.Entities;
using BackEndStructuer.Repository;
using Gaz_BackEnd.DATA.DTOs.Distric;
using Gaz_BackEnd.DATA.DTOs.Governorates;
using Gaz_BackEnd.Entities;
using Microsoft.EntityFrameworkCore;
using OneSignalApi.Model;

namespace Gaz_BackEnd.Services
{
    public interface IGovernorateService
    {
        Task<(List<GovernoratesDTO> governorate, int? totalCount, string? error)> All();
      Task<(List<GovernoratesDTO> governorate, int? totalCount, string? error)> GetAll(GovernorateFilter governorateFilter);
        Task<(GovernoratesDTO? governorate, string? error)> GetById(Guid id);
        Task<(GovernoratesDTO? governorate, string? error)> add(GovernorateForm governorateForm);
        Task<(GovernoratesDTO? governorate, string? error)> update(GovernorateForm governorateForm, Guid id);
        Task<(GovernoratesDTO? governorate, string?)> Delete(Guid id);
    }
    public class GovernoratesService:IGovernorateService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public GovernoratesService(IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<(List<GovernoratesDTO> governorate, int? totalCount, string? error)> All()
        {
            var (governorates, totalCount) = await _repositoryWrapper.Governorates.GetAll(x => (x.Deleted != true));
            var result = _mapper.Map<List<GovernoratesDTO>>(governorates);
            return (result, totalCount, null);
        }
        public async Task<(List<GovernoratesDTO> governorate, int? totalCount, string? error)> GetAll(GovernorateFilter governorateFilter)
        {
            var (governorates, totalCount) = await _repositoryWrapper.Governorates.GetAll(x => (x.Deleted!=true)&&
            (governorateFilter.Name == null || x.Name.Contains(governorateFilter.Name)), i => i.Include(x=>x.Districts.Where(x=>x.Deleted!=true)), governorateFilter.PageNumber, governorateFilter.PageSize);

            var result = _mapper.Map<List<GovernoratesDTO>>(governorates);
            return (result, totalCount, null);
        }

        public async Task<(GovernoratesDTO? governorate, string? error)> GetById(Guid id)
        {
            var getGovernorate = await _repositoryWrapper.Governorates.Get(u => u.Id == id && u.Deleted!=true, i => i.Include(x => x.Districts.Where(x => x.Deleted != true)));
            if (getGovernorate == null) return (null, "المحافظة غير متوفرة");
            var GovernorateDto = _mapper.Map<GovernoratesDTO>(getGovernorate);
            return (GovernorateDto, null);
        }
        public async Task<(GovernoratesDTO? governorate, string? error)> add(GovernorateForm governorateForm)
        {
            var get = await _repositoryWrapper.Governorates.Get(x => x.Name == governorateForm.Name);
            if (get != null)
            {
                return (null, "المحافظة موجودة مسبقا");
            }
            var governorate = _mapper.Map<Governorate>(governorateForm);
            var result = await _repositoryWrapper.Governorates.Add(governorate);
            var governorateDTO = _mapper.Map<GovernoratesDTO>(result);
            return result == null ? (null, "لا يمكن اضافة محافظة") : (governorateDTO, null);
        }

        public async Task<(GovernoratesDTO? governorate, string? error)> update(GovernorateForm governorateForm, Guid id)
        {
            var getGovernorate = await _repositoryWrapper.Governorates.GetById(id);
            if (getGovernorate == null)
            {
                return (null, "المحافظة غير متوفرة");
            }
            if (governorateForm.Name != null && governorateForm.Name != getGovernorate.Name)
            {
                var get = await _repositoryWrapper.Governorates.Get(x => x.Name == governorateForm.Name);
                if (get != null)
                {
                    return (null, "المحافظة موجودة مسبقا");
                }
            }
            getGovernorate = _mapper.Map(governorateForm, getGovernorate);
            var response = await _repositoryWrapper.Governorates.Update(getGovernorate);
            var governorateDTO = _mapper.Map<GovernoratesDTO>(response);
            return response == null ? (null, "لا يمكن تحديث المحافظة") : (governorateDTO, null);
        }

        public async Task<(GovernoratesDTO? governorate, string?)> Delete(Guid id)
        {
            var getGovernorate = await _repositoryWrapper.Governorates.GetById(id);
            if (getGovernorate == null) return (null, "المحافظة غير متوفرة");
            getGovernorate.Deleted = true;
            var response = await _repositoryWrapper.Governorates.Update(getGovernorate);
            var governorateDTO = _mapper.Map<GovernoratesDTO>(response);
            return response == null ? (null, "لا يمكن حذف المحافظة") : (governorateDTO, null);
        }
    }
}