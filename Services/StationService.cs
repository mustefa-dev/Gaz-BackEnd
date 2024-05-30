using AutoMapper;
using BackEndStructuer.Repository;
using Gaz_BackEnd.DATA.DTOs.Distric;
using Gaz_BackEnd.DATA.DTOs.Products;
using Gaz_BackEnd.DATA.DTOs.Station;
using Gaz_BackEnd.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gaz_BackEnd.Services
{
    public interface IStationService
    {
        Task<(Station? station, string? error)> add(StationForm stationForm);
        Task<(List<StationDTO> stations, int? totalCount, string? error)> GetAll(StationFilter stationFilter);
        Task<(StationDTO Stations, string? error)> GetById(Guid id);
        Task<(StationDTO? Station, string? error)> update(StationUpdate stationUpdate, Guid id);
        Task<(StationDTO? station, string?)> Delete(Guid id);
    }
    public class StationService:IStationService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public StationService(IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<(Station? station, string? error)> add(StationForm stationForm)
        {
            var station = _mapper.Map<Station>(stationForm);
            var result = await _repositoryWrapper.Station.Add(station);
            return result == null ? (null, "لا يمكن اضافة محطة") : (station, null);
        }
        public async Task<(List<StationDTO> stations, int? totalCount, string? error)> GetAll(StationFilter stationFilter)
        {
            var (stations, totalCount) = await _repositoryWrapper.Station.GetAll(x => (x.Deleted != true)&&
            (stationFilter.Name == null || x.Name.Contains(stationFilter.Name)), i => i.Include(x => x.City).Include(x => x.Governorate).Include(x => x.District).Include(x=>x.AppUser), stationFilter.PageNumber, stationFilter.PageSize);
            var StationDTO = _mapper.Map<List<StationDTO>>(stations);
            return (StationDTO, totalCount, null);
        }
        public async Task<(StationDTO Stations, string? error)> GetById(Guid id)
        {
            var getStation = await _repositoryWrapper.Station.Get(d => d.Id == id && d.Deleted != true, i => i.Include(x => x.City).Include(x => x.Governorate).Include(x => x.District).Include(x => x.AppUser));
            if (getStation == null) return (null, "المحطة غير متوفره");
            var StationDto = _mapper.Map<StationDTO>(getStation);
            return (StationDto, null);
        }
        public async Task<(StationDTO? Station, string? error)> update(StationUpdate stationUpdate, Guid id)
        {
            var station = await _repositoryWrapper.Station.GetById(id);
            if (station == null)
            {
                return (null, "المحطة غير متوفره");
            }
            station = _mapper.Map(stationUpdate, station);
            var response = await _repositoryWrapper.Station.Update(station);
            var StationDTO = _mapper.Map<StationDTO>(response);
            return response == null ? (null, "لا يمكن تحديث المحطة") : (StationDTO, null);
        }
        public async Task<(StationDTO? station, string?)> Delete(Guid id)
        {
            var station = await _repositoryWrapper.Station.GetById(id);
            if (station == null) return (null, "المحطة غير متوفره");
            station.Deleted = true;
            var response = await _repositoryWrapper.Station.Update(station);
            var StationDTO = _mapper.Map<StationDTO>(response);
            return response == null ? (null, "لا يمكن حذف المحطة") : (StationDTO, null);
        }
    }
}