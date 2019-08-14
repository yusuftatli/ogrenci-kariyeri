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
        public string DescriYorumption { get; set; }
        public long ArticleId { get; set; }
        public bool Approved { get; set; }
        public string Comment { get; set; }


        public long UserID { get; set; }
        public string userName { get; set; }
        public DateTime PostDate { get; set; }
    }
}
