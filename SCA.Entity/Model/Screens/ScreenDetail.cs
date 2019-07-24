using SCA.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCA.Entity.Model
{
    public class ScreenDetail : BaseEntities
    {
        [ForeignKey("ScreenMasterId")]
        public long? ScreenMasterId { get; set; }
        public ScreenMaster ScreenMaster { get; set; }

        public bool IsActive { get; set; }
        public bool IsSuperUser { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }

    }
}
