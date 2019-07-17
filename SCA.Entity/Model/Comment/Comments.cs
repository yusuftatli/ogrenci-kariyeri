using SCA.Entity.Enums;
using SCA.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCA.Entity.Model
{
    public class Comments : BaseEntities
    {
        public ReadType ReadType { get; set; }
        public string Description { get; set; }
        public long ArticleId { get; set; }
        public bool Approved { get; set; }

        [ForeignKey("UserID")]
        public long UserID { get; set; }
        public Users Users { get; set; }
    }
}
