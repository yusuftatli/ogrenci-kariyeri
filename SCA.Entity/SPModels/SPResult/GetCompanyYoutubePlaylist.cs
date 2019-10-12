using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.SPModels.SPResult
{
    public class GetCompanyYoutubePlaylist
    {
        public long Id { get; set; }

        public string VideoLink { get; set; }

        public string ImagePath { get; set; }

        public string Title { get; set; }

        public long CompanyId { get; set; }
    }
}
