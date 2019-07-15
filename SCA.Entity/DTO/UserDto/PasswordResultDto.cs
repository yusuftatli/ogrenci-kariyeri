using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class PasswordResultDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public byte BanCount { get; set; }
        public bool IsActive { get; set; }
        public string ImagePath { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public bool NewUser { get; set; }
        public string DefaultUrl { get; set; }
    }
}
