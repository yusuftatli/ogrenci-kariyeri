using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class UserModelList
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ImagePath { get; set; }
        public string EmailAddress { get; set; }
        public long RoleTypeId { get; set; }
        public string RoleDescription { get; set; }
        public bool IsActive { get; set; }
        public string Durum { get; set; }
        public GenderType GenderId { get; set; }
        public string GenderDescription { get; set; }
        public EducationType EducationStatusId { get; set; }
        public string EducationDescription { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime LastEntrance { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
