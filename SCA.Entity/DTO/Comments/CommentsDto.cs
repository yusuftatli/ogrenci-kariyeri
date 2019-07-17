using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class CommentsDto
    {
        public long Id { get; set; }
        public long ArticleId { get; set; }
        public bool Approved { get; set; }
        public ReadType ReadType { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedUserId { get; set; }
    }
}
