using SCA.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCA.Entity.Dto
{
  public  class SubCategoryDetailDto 
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public long SubCategoryId { get; set; }
        public bool IsActive { get; set; }
    }
}
