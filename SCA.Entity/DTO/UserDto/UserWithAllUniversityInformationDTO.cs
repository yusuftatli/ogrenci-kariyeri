using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class UserWithAllUniversityInformationDTO
    {
        public SCA.Entity.Entities.Users User { get; set; }

        public AllUniversityInformationDto Definitions { get; set; }
    }
}
