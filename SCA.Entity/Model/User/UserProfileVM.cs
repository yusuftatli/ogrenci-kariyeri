using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Model.User
{
    public class UserProfileVM
    {
        public UserProfile UserProfile { get; set; }

        public List<SocialMedias.SocialMediaVM> SocialMedias { get; set; }

    }

    
}
