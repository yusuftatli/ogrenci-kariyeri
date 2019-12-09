using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Model.User
{
    public class UserForgetPassword
    {
        public long Id { get; set; }

        public DateTime ForgetAuthorizationExpiryDate { get; set; }

        public string ForgetAuthorizationKey { get; set; }
    }

}
