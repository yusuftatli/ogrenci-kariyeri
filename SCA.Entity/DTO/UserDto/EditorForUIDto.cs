using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class WriterForUIDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int PostCount { get; set; }
        public int CommentCount { get; set; }
        public List<SocialMediaDto> SocialMedias { get; set; }
    }
}
