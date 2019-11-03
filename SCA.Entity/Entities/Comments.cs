using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Entities
{
    public class Comments
    {
        public long Id { get; set; }

        public int ReadType { get; set; }

        public string Description { get; set; }

        public long ArticleId { get; set; }

        public bool Approved { get; set; }

        public long UserID { get; set; }

        public string userName { get; set; }

        public DateTime PostDate { get; set; }
    }
}
