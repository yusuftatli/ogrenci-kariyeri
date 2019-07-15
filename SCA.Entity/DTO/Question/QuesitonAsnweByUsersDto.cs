using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
   public class QuesitonAsnweByUsersDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long TestId { get; set; }
        public int Score { get; set; }
    }
}
