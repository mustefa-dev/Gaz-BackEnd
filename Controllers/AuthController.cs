using BackEndStructuer.DATA.DTOs.User;
using BackEndStructuer.Entities;
using BackEndStructuer.Services;
using Gaz_BackEnd.DATA.DTOs.Address;
using Gaz_BackEnd.DATA.DTOs.Provider;
using Gaz_BackEnd.DATA.DTOs.User;
using Gaz_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndStructuer.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IProviderService _providerService;
        public AuthController(IUserService userService,IProviderService providerService)
        {
            _userService = userService;
            _providerService = providerService;
        }
        
        [HttpPost("/api/Login")]
        public async Task<ActionResult> Login(LoginForm loginForm) => Ok(await _userService.Login(loginForm));
        [HttpPost("/api/ProviderLogin")]
        public async Task<ActionResult> ProviderLogin(ProviderLogin providerLogin) => Ok(await _providerService.ProviderLogin(providerLogin));
        [HttpPost("/api/Register")]
        public async Task<ActionResult> Register(RegisterForm registerForm) => Ok(await _userService.Register(registerForm));
        [HttpPost("/api/User")]
        public async Task<ActionResult> AddUser(UserForm userForm) => Ok(await _userService.AddUser(userForm));
        [HttpGet("/api/User")]
        public async Task<ActionResult> GetUsers([FromQuery] UserFilter userFilter) => Ok(await _userService.GetUsers(userFilter), userFilter.PageNumber);
        [HttpGet("/api/Admin")]
        public async Task<ActionResult> GetAllAdmin() => Ok(await _userService.GetAllAdmin(),1);
        [HttpGet("/api/User/{id}")]
        public async Task<ActionResult> GetUser(Guid id)
        {
            var result = await _userService.GetUserById(id);
            return Ok(result);
        }
        
        [HttpGet("/api/MyAccount")]
        public async Task<ActionResult<UserDto>> GetMyAccount() => Ok(await _userService.GetMyAccount(Id));
        [HttpPut("/api/User/{id}")]
        public async Task<ActionResult> UpdateUser(UpdateUserForm updateUserForm, Guid id)
        {
            var result = await _userService.UpdateUser(updateUserForm, id);
            return Ok(result);
        }
        [HttpPut("/api/MyAccount")]
        public async Task<ActionResult> UpdateMyAccount(UpdateMyAccount updateMyAcountForm)
        {
            var result = await _userService.UpdateMyAccount(updateMyAcountForm, Id);
            return Ok(result);
        }
        [HttpDelete("/api/User/{id}")]
        public async Task<ActionResult> DeleteUser(Guid id) => Ok(await _userService.DeleteUser(id));
        

    }
}