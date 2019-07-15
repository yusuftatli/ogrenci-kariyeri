using SCA.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services.Interface
{
    public interface ISender
    {
        Task<ServiceResult> SendEmail(string to = "", string subject = "", string emailAddress = "");
        Task<ServiceResult> SendMessage(string phoneNumber);
    }
}
