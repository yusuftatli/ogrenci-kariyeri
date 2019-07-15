using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCA.Model
{
    public class BaseEntities
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedUserId { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long UpdatedUserId { get; set; }
        public DateTime DeletedDate { get; set; }
        public long DeletedUserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
