using SCA.Common.Result;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services.Interface.InterfaceUI
{
    public interface IContentManagerUI
    {
        /// <summary>
        /// UI için makaleleri short list olarak kullanıcı bilgileri ile birlikte döner 
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult> ContentShortListForUI();
        Task<ServiceResult> GetContentUI(string url);
    }
}
