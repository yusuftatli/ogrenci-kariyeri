﻿using SCA.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public interface INewsletterManager
    {
        Task<string> CreateNewsletter(NewsletterDto dto);
    }
}
