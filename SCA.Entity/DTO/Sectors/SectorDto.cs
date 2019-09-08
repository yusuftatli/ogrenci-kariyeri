using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class SectorDto
    {
        public long Id { get; set; }
        public SectorType SectorTypeId { get; set; }
        public string Description { get; set; }
    }
}
