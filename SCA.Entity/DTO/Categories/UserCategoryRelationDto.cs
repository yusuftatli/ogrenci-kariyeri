using SCA.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class UserCategoryRelationDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public List<SubCategoryDto> SubCategory { get; set; }
        
    }
}
