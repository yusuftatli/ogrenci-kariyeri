using SCA.Entity.Enums;
using SCA.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Model
{
    public class Sector : BaseEntities
    {
        public SectorType SectorTypeId { get; set; }
        public string Description { get; set; }

        public ICollection<CompanyClubs> CompanyClubs { get; set; }
    }
}
