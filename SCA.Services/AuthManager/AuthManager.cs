using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using SCA.Common.Resource;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Entity.Model;
using SCA.Repository.Repo;
using SCA.Repository.UoW;
using SCA.Services.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private ISender _sender;
        private readonly IUnitofWork _unitOfWork;
        private IGenericRepository<Users> _userRepo;
        IUserManager _userManager;
        IAnalysisManager _analysisManager;

        public AuthManager(IUnitofWork unitOfWork, IMapper mapper, ISender sender, IUserManager userManager, IAnalysisManager analysisManager)
        {
            _mapper = mapper;
            _sender = sender;
            _unitOfWork = unitOfWork;
            _analysisManager = analysisManager;
            _userManager = userManager;
            _userRepo = _unitOfWork.GetRepository<SCA.Entity.Model.Users>();
        }

        public async Task<ServiceResult> UserLogin(LoginModel dto)
        {
            if (dto.Equals(null))
            {
                return Result.ReturnAsFail(null, AlertResource.NoChanges);
            }
            var loginData = _userRepo.Get(x => x.EmailAddress == dto.username && x.Password == dto.password);

            SCA.Entity.Model.Users data = _mapper.Map<SCA.Entity.Model.Users>(loginData);

            if (loginData != null)
            {
                var requestAt = DateTime.Now;
                var expiresIn = requestAt.Add(TimeSpan.FromMinutes(30));
                var token = GenerateToken(data, expiresIn);
                await _userManager.UserLog(loginData.Id);
                await _analysisManager.LogUserCreateanalitic(PlatformType.Web);
                return Result.ReturnAsFail(AlertResource.SuccessfulOperation, loginData);
            }
            else
            {
                return Result.ReturnAsUnAuth(AlertResource.UserNameOrPasswordIsInCorrect, null);
            }
        }
        public async Task<ServiceResult> PasswordForget(string emailAddress)
        {
            var userData = _userRepo.GetAll(x => x.EmailAddress == emailAddress);
            if (userData == null)
            {
                return Result.ReturnAsFail(null, null);
            }
            else
            {
                return await _sender.SendEmail(to: "test", subject: "", emailAddress: "");
            }
        }
        public async Task<ServiceResult> ReNewPassword(string guidValue)
        {
            var loginData = _userRepo.GetAll(x => x.Password == guidValue).FirstOrDefault();

            if (loginData == null)
            {
                return Result.ReturnAsFail();
            }
            else
            {
                PasswordResultDto newData = new PasswordResultDto();
                newData.Name = loginData.Name;
                newData.Surname = loginData.Surname;
                return Result.ReturnAsSuccess(null, newData);
            }
        }
        private string GenerateToken(SCA.Entity.Model.Users user, DateTime expires)
        {
            var handler = new JwtSecurityTokenHandler();

            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.EmailAddress, "token"),
                new[] {
                 new Claim("Id", user.EmailAddress.ToString())
                }
            );

            var keybit = Encoding.ASCII.GetBytes("55D9BF4F187EF3F961FC87C0435ADBBC314A5AEA9841E5B0C5090BE25414016A");
            var signkey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(keybit);

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {

                Issuer = "Issuer",
                Audience = "Audience",
                SigningCredentials = new SigningCredentials(signkey, SecurityAlgorithms.HmacSha256),
                Subject = identity,
                Expires = expires,
                NotBefore = DateTime.Now.Subtract(TimeSpan.FromMinutes(30))
            });
            return handler.WriteToken(securityToken);
        }
    }
}
