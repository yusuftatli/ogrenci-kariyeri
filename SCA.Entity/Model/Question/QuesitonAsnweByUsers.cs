using SCA.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCA.Entity.Model
{
    public class QuesitonAsnweByUsers : BaseEntities
    {
        [ForeignKey("UserId")]
        public long UserId { get; set; }
        public Users Users { get; set; }

        [ForeignKey("TestId")]
        public long TestId { get; set; }
        public Tests Tests { get; set; }

        public int Score { get; set; }

    }
}
