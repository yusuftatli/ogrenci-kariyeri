using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace SCA.Common
{
    public class JwtToken
    {
        public static long GetUserId(string tokenValue)
        {
            var handler = new JwtSecurityTokenHandler();
            var tokens = handler.ReadToken(tokenValue) as JwtSecurityToken;
            var _token = int.Parse(tokens.Claims.First(claim => claim.Type == "unique_name").Value);
            return _token;
        }
    }
}
