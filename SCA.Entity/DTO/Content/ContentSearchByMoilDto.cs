using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class ContentSearchByMoilDto
    {
        public int Type { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string searhCategoryIds { get; set; }
        public int limit { get; set; }

    }
}
