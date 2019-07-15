using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class CategoryRelationDto
    {
        public long Id { get; set; }
        public long CategoryId { get; set; }
        public long TagContentId { get; set; }
        public ReadType ReadType { get; set; }
    }
}
