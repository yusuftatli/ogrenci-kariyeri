using SCA.Entity.Enums;
using SCA.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Model
{
    public class UserCreateAnlitic : BaseEntities
    {
        public MethodType MethodTypeId { get; set; }
        public string Description { get; set; }
        public long WebCount { get; set; }
        public long MobilCount { get; set; }
    }
}
