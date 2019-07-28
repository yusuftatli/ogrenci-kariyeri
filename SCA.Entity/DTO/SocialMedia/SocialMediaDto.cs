﻿using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class SocialMediaDto
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public SocialMediaType SocialMediaType { get; set; }
    }
}
