using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class ScreenDoUI
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool IsActive { get; set; }
        public List<ScreenDetailDto> Details { get; set; }
    }
}
