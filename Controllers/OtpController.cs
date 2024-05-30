using BackEndStructuer.Controllers;
using Gaz_BackEnd.DATA.DTOs.City;
using Gaz_BackEnd.DATA.DTOs.Otp;
using Gaz_BackEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gaz_BackEnd.Controllers
{

    public class OtpController : BaseController
    {
        private readonly IOtpService _OtpServices;

        public OtpController(IOtpService OptServices)
        {
            _OtpServices = OptServices;
        }
        [HttpPost("/api/SendOtp")]
        public async Task<ActionResult> SendOtp(SendSmsForm sendSmsForm) => Ok(await _OtpServices.SendOtp(sendSmsForm));
        [HttpPost("/api/VerifyOtp")]
        public async Task<ActionResult> VerifyOtp(VerifyForm verifyForm) => Ok(await _OtpServices.VerifyOtp(verifyForm));
    }
}