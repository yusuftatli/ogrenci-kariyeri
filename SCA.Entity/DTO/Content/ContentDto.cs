using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class ContentDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Tags { get; set; }
        public long SycnId { get; set; }
        public string Category { get; set; }
        public PlatformType PlatformType { get; set; }
        public PublishState PublishStateType { get; set; }
        public long ReadCount { get; set; }
        public string ImagePath { get; set; }
        public string SeoUrl { get; set; }
        public string Header { get; set; }
        public string Writer { get; set; }
        public long ConfirmUserId { get; set; }
        public string ConfirmUserName { get; set; }
        public string ContentDescription { get; set; }
        public bool IsHeadLine { get; set; }
        public bool IsManset { get; set; }
        public bool IsMainMenu { get; set; }
        public bool IsConstantMainMenu { get; set; }
        public bool IsSendConfirm { get; set; }
        public int EventId { get; set; }
        public int InternId { get; set; }
        public int VisibleId { get; set; }
        public DateTime PublishDate { get; set; }
        public string PlatformTypeDes { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedUserId { get; set; }
    }
}
