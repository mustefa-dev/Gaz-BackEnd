using AutoMapper;
using BackEndStructuer.Repository;
using BackEndStructuer.Utils;
using Gaz_BackEnd.DATA.DTOs.Otp;
using Gaz_BackEnd.Entities;
using Gaz_BackEnd.Helpers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Gaz_BackEnd.Services
{
    public interface IOtpService
    {
       Task<(OtpDTO? Otp, string? error)> SendOtp(SendSmsForm sendSmsForm);
        Task<(Otp? Otp, string? error)> VerifyOtp(VerifyForm verifyForm);
    }
    public class OtpService:IOtpService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public OtpService(IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<(OtpDTO? Otp, string? error)> SendOtp( SendSmsForm sendSmsForm)
        {
            var newNumber = (sendSmsForm.PhoneNumber).Trim().TrimStart('0').Replace(" ", "");
            bool isValid = newNumber.StartsWith("7") && newNumber.Length == 10;
            if (!isValid)
            {
                return (null, "خطأ في رقم الهاتف");
            }

            var time = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
            var random = new Random();
            var otpCode = random.Next(100000, 999999);
            var areaDate = time.AddHours(3);
            Otp otp = new Otp
            {

                CreationDate = areaDate,
                ExpiryDate = areaDate.AddMinutes(3),
                OtpCode = otpCode,
                PhoneNumber = "0" + newNumber,
                Key= Guid.NewGuid().ToString("N")
            };
            var result = await _repositoryWrapper.Otp.Add(otp);
            if(result == null)
            {
                return (null, "لا يمكن ارسال رمز التحقق");
            }
           
            var message = "رمز التحقق الخاص بك هو  : " + otp;
            var isSend = new SendSms().SendMessage(newNumber, message);
            var otpDto = new OtpDTO
            {
                Key = otp.Key
            };

            if (isSend)
            {
                return (otpDto, null);
            }
            else { 
                return (null, "حدث خطأ اثناء ارسال الرمز"); 
            }
        }

        public async Task<(Otp? Otp, string? error)> VerifyOtp(VerifyForm verifyForm)
        {
            var otpItem = await _repositoryWrapper.Otp.Get(x => x.OtpCode == verifyForm.OtpCode&&x.Key==verifyForm.Key);
            if (otpItem == null)
            {
                return (null, "رمز التحقق غير صحيح");
            }
            if (otpItem.ExpiryDate < DateTime.Now)
            {
                return (null, "انتهت صلاحية رمز التحقق");
            }
            return (otpItem, null);
        }
    }
}
