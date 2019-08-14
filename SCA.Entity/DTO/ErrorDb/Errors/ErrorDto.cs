using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO.ErrorDb
{
    public class ErrorDto
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedUserId { get; set; }

        public string Error { get; set; }
    }
}
