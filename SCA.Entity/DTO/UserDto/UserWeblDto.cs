using SCA.Entity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class UserWeblDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string Biography { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ImaageData { get; set; }
        public int GenderId { get; set; }
        public int RoleTypeId { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RoleExpiresDate { get; set; }
        public string ReferanceCode { get; set; }

        public string Facebook { get; set; }
        public string Linkedin { get; set; }
        public string Instagram { get; set; }
    }
}
