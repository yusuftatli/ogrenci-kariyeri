using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Model
{
   public class UserProfileDto
    {
        public UserProfileMobilDto ProfileInfo { get; set; }
        public List<CategoryPostDto> Categories { get; set; }

    }
}
