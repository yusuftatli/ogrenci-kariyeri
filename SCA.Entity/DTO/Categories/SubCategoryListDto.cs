using SCA.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCA.Entity.Dto
{
    public class SubCategoryListDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public long CategoryId { get; set; }
    }
}
