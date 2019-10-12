using SCA.Common.Result;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public interface ISender
    {
        Task<ServiceResult> SendEmail(string to = "", string subject = "", string emailAddress = "");
        Task<ServiceResult> SendMessage(string phoneNumber);
        Task<ServiceResult> SaveEmails(EmailsDto dto);
        Task<string> GetEmailTemplate(string code);
        Task<EmailSettings> GetEmailSetting(string code);
    }
}
