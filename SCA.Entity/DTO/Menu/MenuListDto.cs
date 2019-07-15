using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class MenuListDto
    {
        public string Description { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public string Icon { get; set; }
        public long? ParentId { get; set; }
    }
}
