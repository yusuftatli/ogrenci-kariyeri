using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
  public  class CompanyLogManagementDto
    {
        public long Id { get; set; }
        public long CompanyId{ get; set; }
        public long UserId { get; set; }
        public DateTime DateTime{ get; set; }
    }
}
