using Microsoft.IdentityModel.Tokens;
using SCA.Common.Resource;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Entity.Model;
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
        private readonly IUserValidation _userValidation;
        private ISender _sender;
       // IUserManager _userManager;

        public AuthManager(
            ISender sender,
            IUserValidation userValidation)
        {
            _sender = sender;
            _userValidation = userValidation;
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
                expires: DateTime.Now.AddDays(10),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
