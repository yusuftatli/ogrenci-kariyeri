using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.SPModels.SPResult
{
    public class GetCompanyAnnouncements
    {
        public long AnnouncementId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }
    }
}
