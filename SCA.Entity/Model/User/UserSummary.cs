using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Model.User
{
    public class UserSummary
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string EmailAddress { get; set; }

        public string ForgetAuthorizationKey { get; set; }
    }
}
