using SCA.Entity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class UserWithAllUniversityInformationDTO
    {
        public UserProfile User { get; set; }

        public List<SocialMediaDto> SocialMedias { get; set; }

        public AllUniversityInformationDto Definitions { get; set; }
    }
}
