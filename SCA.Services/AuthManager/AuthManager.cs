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
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly IUserValidation _userValidation;
        private ISender _sender;
        private readonly IUnitofWork _unitOfWork;
        private IGenericRepository<Users> _userRepo;
       // IUserManager _userManager;

        public AuthManager(IUnitofWork unitOfWork,
            IMapper mapper,
            ISender sender,
          //  IUserManager userManager,
            IAnalysisManager analysisManager,
            IUserValidation userValidation)
        {
            _mapper = mapper;
            _sender = sender;
            _unitOfWork = unitOfWork;
           // _userManager = userManager;
            _userValidation = userValidation;
            _userRepo = _unitOfWork.GetRepository<Users>();
        }

        public async Task<ServiceResult> UserLogin(LoginDto dto)
        {
            ServiceResult _res = new ServiceResult();

            if (dto.Equals(null))
            {
                return Result.ReturnAsFail(null, AlertResource.NoChanges);
            }

            _res = _userValidation.UserLoginValidation(dto);
            if (_res.ResultCode != HttpStatusCode.OK)
            {
                return _res;
            }

            Users userInfo = _mapper.Map<Users>(_userRepo.Get(x => x.EmailAddress.Equals(dto.username) && x.Password.Equals(dto.password)));
            var sessionData = _mapper.Map<UserSession>(userInfo);

            if (_res.ResultCode != HttpStatusCode.OK)
            {
                return _res;
            }

            sessionData.Token = GenerateToken(sessionData);

            UserLogDto dtoLog = new UserLogDto()
            {
                UserId = userInfo.Id,
                PlatformTypeId = PlatformType.Web,
                EnteraceDate = DateTime.Now
            };

           // await _userManager.CreateUserLog(dtoLog);
            _res = Result.ReturnAsSuccess(message: "Giriş Başarılı", sessionData);
            return _res;
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
        public string GenerateToken(UserSession user)
        {
            var someClaims = new Claim[]{
                new Claim(JwtRegisteredClaimNames.UniqueName,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.EmailAddress),
                new Claim(JwtRegisteredClaimNames.GivenName,user.Name+" " +user.Surname),
                new Claim(JwtRegisteredClaimNames.Typ,user.RoleTypeId.ToString())
            };

            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ogrenciKariyerikey"));
            var token = new JwtSecurityToken(
                issuer: "ogrenciKariyeri1",
                audience: "ogrenciKariyeri",
                claims: someClaims,
                expires: DateTime.Now.AddMinutes(3),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
