using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Model
{
    public class BasicPageUpdateModel
    {
        public long Id { get; set; }

        public string ImagePath { get; set; }

        public string Description { get; set; }

        public string SeoUrl { get; set; }

        public string Title { get; set; }

        public bool IsActive { get; set; }

        public byte OrderNo { get; set; }

        public PageType TypeOfPage { get; set; }

        public long UpdatedUserID { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
