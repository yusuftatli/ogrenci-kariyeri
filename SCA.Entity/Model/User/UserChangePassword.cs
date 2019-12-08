using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Model.User
{
    public class UserChangePassword
    {
        public long Id { get; set; }

        public string ForgetAuthorizationKey { get; set; }

        public string Password { get; set; }

    }
}
