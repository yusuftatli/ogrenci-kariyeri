using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
   public class CommentForUIDto
    {
        public long Id { get; set; }
        public long UserID { get; set; }
        public long ArticleId { get; set; }
        public string Description { get; set; }
        public string userName { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
