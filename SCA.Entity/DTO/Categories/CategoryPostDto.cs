using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class CategoryPostDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Categories { get; set; }
        public long CategoryId { get; set; }
    }
}
