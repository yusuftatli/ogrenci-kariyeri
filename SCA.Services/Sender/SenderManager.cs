using SCA.Common.Resource;
using SCA.Common.Result;
using SCA.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class SenderManager : ISender
    {
        public async Task<ServiceResult> SendEmail(string to = "", string subject = "", string emailAddress = "")
        {
            return Result.ReturnAsSuccess(null, AlertResource.EmailSendAsSuccessfuly, null);
        }
        public async Task<ServiceResult> SendMessage(string phoneNumber)
        {
            return Result.ReturnAsSuccess(null, AlertResource.MessageSendAsSuccessfuly, null);
        }
    }
}
