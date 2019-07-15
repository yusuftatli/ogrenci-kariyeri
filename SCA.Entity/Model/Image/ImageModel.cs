using SCA.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Model
{
    public class ImageModel : BaseEntities
    {
        //resim kullanım sayısı
        public string ImageFolder { get; set; }
        public string ImageName { get; set; }
    }
}
