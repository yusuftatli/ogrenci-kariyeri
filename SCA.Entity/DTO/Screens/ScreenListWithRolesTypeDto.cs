using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class ScreenListWithRolesTypeDto
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsSuperUser { get; set; }
        public string Icon { get; set; }
        public string description { get; set; }
        public string Url { get; set; }
        public List<ScreenDetailDto> ScreenDetailDto { get; set; }
    }
}
