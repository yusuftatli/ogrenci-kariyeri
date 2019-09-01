using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class BasicPagesDto
    {
        public long Id { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public string SeoUrl { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public string ActiveDescription { get; set; }
    }
}
