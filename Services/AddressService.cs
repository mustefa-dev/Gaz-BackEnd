using AutoMapper;
using BackEndStructuer.Repository;
using Gaz_BackEnd.DATA.DTOs.Address;
using Gaz_BackEnd.DATA.DTOs.Distric;
using Gaz_BackEnd.Entities;
using Gaz_BackEnd.Helpers;
using Microsoft.EntityFrameworkCore;
using OneSignalApi.Model;
using System.Net;

namespace Gaz_BackEnd.Services
{
    public interface IAddressService
    {
        Task<(Address? Address, string? error)> add(AddressForm addressForm,Guid userId);
        Task<(List<AddressDTO> Addresss, int? totalCount, string? error)> GetAll(Guid userId,AddressFilter addressFilter);
        Task<(AddressDTO Addresss, string? error)> GetById(Guid id);
        Task<(AddressDTO? Address, string? error)> update(AddressUpdate AddressUpdate, Guid id,Guid userId);
        Task<(AddressDTO? Address, string?)> Delete(Guid id);
        
        Task<(List<AddressDTO>? addressDtos,int? totalCount, string error)> GetAllByUserId(Guid userId);
    }
    public class AddressService:IAddressService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        public AddressService(IMapper mapper, IRepositoryWrapper repositoryWrapper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }


        public async Task<(Address? Address, string? error)> add(AddressForm addressForm,Guid userId)
        {
            var address = _mapper.Map<Address>(addressForm);
            address.AppUserId = userId;
            if(address.IsMain==true)
            {
                var getAddress = await _repositoryWrapper.Address.GetAll(x => x.AppUserId == userId, 1,200);
                if(getAddress.data!=null) {
                    foreach (var item in getAddress.data)
                    {
                        item.IsMain = false;
                        await _repositoryWrapper.Address.Update(item);
                    }
                }
            }
            var result = await _repositoryWrapper.Address.Add(address);
            return result == null ? (null, "لا يمكن اضافة عنوان") : (address, null);
        }
        public async Task<(List<AddressDTO> Addresss, int? totalCount, string? error)> GetAll(Guid userId,AddressFilter addressFilter)
        {
            var (Addresss, totalCount) = await _repositoryWrapper.Address.GetAll((x =>
                    (x.AppUserId == userId) && 
                    ( addressFilter.Address==null||x.FullAddress.Contains(addressFilter.Address))),i=>
                i.Include(x=>
                    x.City).Include(x=>
                    x.Governorate).Include(x=>
                    x.District)!, addressFilter.PageNumber,
                addressFilter.PageSize);
            var AddressDTO = _mapper.Map<List<AddressDTO>>(Addresss);
            return (AddressDTO, totalCount, null);
        }
        public async Task<(AddressDTO Addresss, string? error)> GetById(Guid id)
        {
            var getAddress = await _repositoryWrapper.Address.Get(d => d.Id == id, i=>i.Include(x => x.City).Include(x => x.Governorate).Include(x => x.District));
            if (getAddress == null) return (null, "العنوان غير متوفر");
            var AddressDto = _mapper.Map<AddressDTO>(getAddress);
            return (AddressDto, null);
        }
        public async Task<(AddressDTO? Address, string? error)> update(AddressUpdate AddressUpdate, Guid id, Guid userId )
        {
            var Address = await _repositoryWrapper.Address.GetById(id);
            if (Address == null)
            {
                return (null, "العنوان غير متوفر");
            }
            
            if (AddressUpdate.IsMain == true)
            {
                var getAddress = await _repositoryWrapper.Address.GetAll(x => x.AppUserId == userId, 1, 200);
                if (getAddress.data != null)
                {
                    foreach (var item in getAddress.data)
                    {
                        item.IsMain = false;
                        await _repositoryWrapper.Address.Update(item);
                    }
                }
            }
            Address = _mapper.Map(AddressUpdate, Address);
            var response = await _repositoryWrapper.Address.Update(Address);
            var AddressDTO = _mapper.Map<AddressDTO>(response);
            return response == null ? (null, "لا يمكن تحديث العنوان") : (AddressDTO, null);
        }
        public async Task<(AddressDTO? Address, string?)> Delete(Guid id)
        {
            var address = await _repositoryWrapper.Address.Get(x=>x.Id==id);
            if (address == null) return (null, "العنوان غير متوفر");
            
            var response = await _repositoryWrapper.Address.Delete(id);
            var AddressDTO = _mapper.Map<AddressDTO>(response);
            return response == null ? (null, "لا يمكن حذف العنوان") : (AddressDTO, null);
        }
        public async Task<(List<AddressDTO>? addressDtos,int? totalCount, string error)> GetAllByUserId(Guid userId)
        {
            var address = await _repositoryWrapper.Address.GetAll(x => x.AppUserId == userId, i => i.Include(c => c.City).Include(g => g.Governorate).Include(d => d.District));
            var addressDtos = _mapper.Map<List<AddressDTO>>(address.data);
            return (addressDtos,address.totalCount, null);
        }
    }
}