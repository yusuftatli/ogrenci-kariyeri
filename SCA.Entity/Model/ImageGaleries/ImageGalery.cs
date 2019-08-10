using SCA.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCA.Entity.Model
{
    public class ImageGalery : BaseEntities
    {
        [ForeignKey("CompanyClubId")]
        public long CompanyClubId { get; set; }
        public CompanyClubs CompanyClubs { get; set; }

        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
    }
}
