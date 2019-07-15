using SCA.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Dto
{
    public class MainCategoryDto 
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public long? ParentId { get; set; }
        public bool IsActive { get; set; }
    }
}
