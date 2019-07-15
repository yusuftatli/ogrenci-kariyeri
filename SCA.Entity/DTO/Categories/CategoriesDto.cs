using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class CategoriesDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public long? ParentId { get; set; }
    }
}
