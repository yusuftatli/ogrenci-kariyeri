using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class AnouncementDto
    {
        public long AnnouncementId { get; set; }

        public string ImagePath { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public string SeoUrl { get; set; }
    }
}
