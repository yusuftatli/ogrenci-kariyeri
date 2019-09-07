using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class FavoriteDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ContentId { get; set; }
        public bool IsActive { get; set; }
    }
}
