using SCA.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Model.BasicPages
{
    public class BasicPages : BaseEntities
    {
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public string SeoUrl { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
    }
}
