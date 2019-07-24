using SCA.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Model
{
    public class ScreenMaster : BaseEntities
    {
        public bool IsActive { get; set; }
        public bool IsSuperUser { get; set; }
        public string Icon { get; set; }
        public string description { get; set; }
        public string Url { get; set; }

        public ICollection<ScreenDetail> ScreenDetail { get; set; }
    }
}
