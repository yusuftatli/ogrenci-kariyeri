﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SCA.Entity.DTO;

namespace SCA.Services.Interface.InterfaceUI
{
    public interface IB2CManagerUI
    {
        Task<List<ContentForHomePageDTO>> GetContentsForHomePage();
    }
}
