using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class CommentForUIDto
    {
        private string imagePath;
        public long Id { get; set; }
        public long UserID { get; set; }
        public long ArticleId { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public string ImagePath { get; set; }
        public GenderType GenderId { get; set; }
        public DateTime PostDate { get; set; }
    }
}
