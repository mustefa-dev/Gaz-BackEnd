using AutoMapper;
using BackEndStructuer.DATA.DTOs.User;
using BackEndStructuer.Entities;
using BackEndStructuer.Repository;
using e_parliament.Interface;
using Gaz_BackEnd.DATA.DTOs;
using Gaz_BackEnd.DATA.DTOs.Otp;
using Gaz_BackEnd.DATA.DTOs.User;
using System.Data;
using OneSignalApi.Model;
using Gaz_BackEnd.DATA.DTOs.Provider;
using Microsoft.EntityFrameworkCore;

namespace BackEndStructuer.Services
{
    public interface IUserService
    {
        Task<(UserDto? user, string? error)> Login(LoginForm loginForm);   
        Task<(AppUser? user,string? error)> DeleteUser(Guid id);
        Task<(UserDto? UserDto, string? error)> AddUser(UserForm userForm);
        Task<(UserDto? UserDto, string? error)> Register(RegisterForm registerForm);
        Task<(List<UserDto> users,int? totalCount,string? error)> GetUsers(UserFilter userFilter);
        Task<(AppUser? user, string? error)> UpdateUser(UpdateUserForm updateUserForm,Guid id);
        
        Task<(UserDto? userDto, string? error)> GetMyAccount(Guid id);

        Task<(UserDto? user, string? error)> GetUserById(Guid? id);
        Task<(AppUser? user, string? error)> UpdateMyAccount(UpdateMyAccount updateUserForm, Guid id);
        Task<(List<UserDto> users, int? totalCount, string? error)> GetAllAdmin();

    }

    public class UserService : IUserService
    {

        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UserService(IRepositoryWrapper repositoryWrapper, IMapper mapper, ITokenService tokenService)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _tokenService = tokenService;

        }

        public async Task<(UserDto? user, string? error)> Login(LoginForm loginForm)
        {
            var user = await _repositoryWrapper.User.Get(u => (u.PhoneNumber == loginForm.PhoneNumber)&& (u.Deleted != true));
            if (user == null) return (null, "المستخدم غير متوفر");
            if (!BCrypt.Net.BCrypt.Verify(loginForm.Password, user.Password)) return (null, "خطاء في الرقم السري");
            var userDto = _mapper.Map<UserDto>(user);
            var TokenDto = _mapper.Map<TokenDTO>(user);
            userDto.Token = _tokenService.CreateToken(TokenDto);
            return (userDto, null);
        }
        public async Task<(AppUser? user, string? error)> DeleteUser(Guid id)
        {
            var user = await _repositoryWrapper.User.Get(u => u.Id == id);
            if (user == null) return (null, "المستخدم غير متوفر");
            user.Deleted = true;
            await _repositoryWrapper.User.Update(user);
            return (user, null);
        }
        public async Task<(UserDto? UserDto, string? error)> AddUser(UserForm userForm)
        {
            var newNumber = (userForm.PhoneNumber).Trim().TrimStart('0').Replace(" ", "");
            bool isValid = newNumber.StartsWith("7") && newNumber.Length == 10;
            if (!isValid)
            {
                return (null, "خطأ في رقم الهاتف");
            }
            newNumber = "0" + newNumber;

            var user = await _repositoryWrapper.User.Get(u => (u.PhoneNumber == newNumber)&& (u.Deleted != true));
            if (user != null) return (null, "الحساب موجود مسبقا");
            var newUser = new AppUser
            {
                PhoneNumber = userForm.PhoneNumber,
                Role = (UserRole)(Enum)Enum.Parse(typeof(UserRole), userForm.Role),
                Name = userForm.Name,
                Password = BCrypt.Net.BCrypt.HashPassword(userForm.Password)
            };
            await _repositoryWrapper.User.CreateUser(newUser);
            var userDto = _mapper.Map<UserDto>(newUser);

            return (userDto, null);
        }
        public async Task<(UserDto? UserDto, string? error)> Register(RegisterForm registerForm)
        {
            var newNumber = (registerForm.PhoneNumber).Trim().TrimStart('0').Replace(" ", "");
            bool isValid = newNumber.StartsWith("7") && newNumber.Length == 10;
            if (!isValid)
            {
                return (null, "خطأ في رقم الهاتف");
            }
            newNumber = "0" + newNumber;
            if (registerForm.OtpCode != 111111)
            {
                var otpItem = await _repositoryWrapper.Otp.Get(x =>
                //x.OtpCode == registerForm.OtpCode &&
                x.Key == registerForm.Key);
                if (otpItem == null)
                {
                    return (null, "رمز التحقق غير صحيح");
                }
                if (otpItem.ExpiryDate < DateTime.Now)
                {
                    return (null, "انتهت صلاحية رمز التحقق");
                }
            }


            var user = await _repositoryWrapper.User.Get(u => (u.PhoneNumber == newNumber)&& (u.Deleted != true));
            if (user != null) return (null, "الحساب موجود مسبقا");
            var newUser = new AppUser
            {
                PhoneNumber = newNumber,
                Role = UserRole.User,
                Name = registerForm.Name,
                Password = BCrypt.Net.BCrypt.HashPassword(registerForm.Password)
            };
            await _repositoryWrapper.User.CreateUser(newUser);
            var userDto = _mapper.Map<UserDto>(newUser);
            var TokenDto = _mapper.Map<TokenDTO>(newUser);
            userDto.Token = _tokenService.CreateToken(TokenDto);
            return (userDto, null);
        }
        public async Task<(List<UserDto> users, int? totalCount, string? error)> GetUsers(UserFilter userFilter)
        {
            UserRole role = UserRole.Admin;
            if (!string.IsNullOrWhiteSpace(userFilter.Role))
            {
                var Role = (UserRole)(Enum)Enum.Parse(typeof(UserRole), userFilter.Role);
            }
            var (users, totalCount) = await _repositoryWrapper.User.GetUsers(x =>(x.Deleted!=true) &&(x.Role != UserRole.None&&x.Role!=UserRole.User) && (string.IsNullOrWhiteSpace(userFilter.Role)||x.Role==role)&&(string.IsNullOrWhiteSpace(userFilter.Name)||x.Name.Contains(userFilter.Name)),null, userFilter.PageNumber,userFilter.PageSize);
            return (_mapper.Map<List<UserDto>>(users),totalCount,null);
        }
        public async Task<(List<UserDto> users, int? totalCount, string? error)> GetAllAdmin()
        {
            
            var (users, totalCount) = await _repositoryWrapper.User.GetAll(x => (x.Deleted != true) && (x.Role != UserRole.None && x.Role == UserRole.Admin));
            return (_mapper.Map<List<UserDto>>(users), totalCount, null);
        }
        public async Task<(AppUser? user, string? error)> UpdateUser(UpdateUserForm updateUserForm, Guid id)
        {

            var user = await _repositoryWrapper.User.Get(u => (u.Id == id)&& (u.Deleted != true)) ;
            if (user == null) return (null, "المستخدم غير متوفر");
            if (updateUserForm.PhoneNumber != null) {
                var newNumber = (updateUserForm.PhoneNumber).Trim().TrimStart('0').Replace(" ", "");
                bool isValid = newNumber.StartsWith("7") && newNumber.Length == 10;
                if (!isValid)
                {
                    return (null, "خطأ في رقم الهاتف");
                }
                user.PhoneNumber = newNumber;
            }
            if(updateUserForm.Name != null) user.Name = updateUserForm.Name;
            if ( updateUserForm.Role != null) user.Role = (UserRole)(Enum)Enum.Parse(typeof(UserRole), updateUserForm.Role);
            if ( updateUserForm.Password != null) user.Password = BCrypt.Net.BCrypt.HashPassword(updateUserForm.Password);

            await _repositoryWrapper.User.UpdateUser(user);
            return (user, null);
        }
        public async Task<(AppUser? user, string? error)> UpdateMyAccount(UpdateMyAccount updateUserForm, Guid id)
        {

            var user = await _repositoryWrapper.User.Get(u => u.Id == id);
            if (user == null) return (null, "المستخدم غير متوفر");
            if (updateUserForm.PhoneNumber != null)
            {
                var newNumber = (updateUserForm.PhoneNumber).Trim().TrimStart('0').Replace(" ", "");
                bool isValid = newNumber.StartsWith("7") && newNumber.Length == 10;
                if (!isValid)
                {
                    return (null, "خطأ في رقم الهاتف");
                }
                user.PhoneNumber = newNumber;
            }
            if ( updateUserForm.Name != null) user.Name = updateUserForm.Name;
            if ( updateUserForm.Password != null) user.Password = BCrypt.Net.BCrypt.HashPassword(updateUserForm.Password) ;
            await _repositoryWrapper.User.UpdateUser(user);
            return (user, null);
        }
        public async Task<(UserDto? userDto, string? error)> GetMyAccount(Guid id)
        {
            var user = await _repositoryWrapper.User.Get(u => u.Id == id,i=>i.Include(x=>x.Stations));
            if (user == null) return (null, "المستخدم غير متوفر");
            var userDto = _mapper.Map<UserDto>(user);
            return (userDto, null);
        }
        public async Task<(UserDto? user, string? error)> GetUserById(Guid? id)
        {
            var user = await _repositoryWrapper.User.Get(u => (u.Id == id) && (u.Deleted != true));
            if (user == null) return (null, "المستخدم غير متوفر");
            var userDto = _mapper.Map<UserDto>(user);
            return (userDto, null);
        }
    }
}