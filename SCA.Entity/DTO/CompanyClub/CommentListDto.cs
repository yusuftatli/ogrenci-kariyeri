using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class CommentListDto
    {
        public string Description { get; set; }
        public DateTime PostDate { get; set; }
        public string UserName { get; set; }
        public string ImagePath { get; set; }
    }
}
