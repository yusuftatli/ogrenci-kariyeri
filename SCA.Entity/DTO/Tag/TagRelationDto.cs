using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class TagRelationDto
    {
        public long Id { get; set; }
        public long TagId { get; set; }
        public long TagContentId { get; set; }
        public ReadType ReadType { get; set; }
    }
}
