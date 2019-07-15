using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services.Interface.InterfaceUI
{
    public interface IContentManagerUI
    {
        Task<List<ContentShortListDto>> ContentShortListForUI();
    }
}
