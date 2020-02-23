using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class CategoriesMobilDto
    {
        public long Id { get; set; }
        public string Icon { get; set; }
        public long ParentId { get; set; }
        public string Description { get; set; }
    }
}
