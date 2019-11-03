using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class CommentsDto
    {
        public long? Id { get; set; }
        public ReadType ReadType { get; set; }
        public string Comment { get; set; }
        public long ArticleId { get; set; }
        public bool Approved { get; set; }

        public long UserID { get; set; }
        public string userName { get; set; }
        public DateTime PostDate { get; set; }
        public string ButtonName { get; set; }
        public string ButtonClass{ get; set; }
    }
}
