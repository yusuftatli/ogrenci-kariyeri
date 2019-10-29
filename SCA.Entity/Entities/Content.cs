using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Entities
{
    public class Content 
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public PublishState PublishStateType { get; set; }

        public long SyncId { get; set; }

        public long ReadCount { get; set; }

        public string ImagePath { get; set; }

        public string SeoUrl { get; set; }

        public string Header { get; set; }

        public string Writer { get; set; }

        public long ConfirmUserId { get; set; }

        public string ConfirmUserName { get; set; }

        public string Category { get; set; }

        public string ContentDescription { get; set; }

        public PlatformType PlatformType { get; set; }

        public bool IsHeadLine { get; set; }

        public bool IsManset { get; set; }

        public bool IsMainMenu { get; set; }

        public bool IsConstantMainMenu { get; set; }

        public long EventId { get; set; }

        public long InternId { get; set; }

        public long VisibleId { get; set; }

        public long CreatedUserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public long? UpdatedUserId { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long DeletedUserId { get; set; }

        public DateTime? DeletedDate { get; set; }

        public DateTime? PublishDate { get; set; }
    }
}
