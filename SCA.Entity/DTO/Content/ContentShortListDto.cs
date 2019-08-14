using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class ContentShortListDto
    {
        public long Id { get; set; }
        public string Header { get; set; }
        public string SeoUrl { get; set; }
        public string ImagePath { get; set; }
        public string Writer { get; set; }
        public long ReadCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime PublishDate { get; set; }
        public PublishState PublishStateType { get; set; }
        public string PublishStateTypeDes { get; set; }
        public PlatformType PlatformType { get; set; }
        public string PlatformTypeDes { get; set; }
        public long ConfirmUserId { get; set; }
        public string ConfirmUserName { get; set; }
        public string Tags { get; set; }
    }
}
