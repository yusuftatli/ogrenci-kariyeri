using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Model.ImageGaleries
{
    public class ImageGaleryInsertModel
    {

        public long CompanyClubId { get; set; }

        public string ImagePath { get; set; }

        public bool IsActive { get; set; }

    }
}
