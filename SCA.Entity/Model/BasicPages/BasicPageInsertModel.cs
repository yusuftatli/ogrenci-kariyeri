using SCA.Entity.Enums;
using System;

namespace SCA.Entity.Model
{
    public class BasicPageInsertModel
    {
        public string ImagePath { get; set; }

        public string Description { get; set; }

        public string SeoUrl { get; set; }

        public string Title { get; set; }

        public bool IsActive { get; set; }

        public byte OrderNo { get; set; }

        public bool IsDeleted { get; set; }

        public PageType TypeOfPage { get; set; }

        public long CreatedUserId { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
