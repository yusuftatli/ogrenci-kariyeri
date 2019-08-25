using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class UserSession
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public long RoleTypeId { get; set; }
    }
}
