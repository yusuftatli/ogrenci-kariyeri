﻿using Dapper;
using MySql.Data.MySqlClient;
using SCA.Common;
using SCA.Common.Resource;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class SenderManager : ISender
    {
        IErrorManagement _errorManagement;
        private readonly IDbConnection _db = new MySqlConnection("Server=167.71.46.71;Database=StudentDbTest;Uid=ogrencikariyeri;Pwd=dXog323!s.?;");

        public SenderManager(IErrorManagement errorManagement)
        {
            _errorManagement = errorManagement;
        }

        public async Task<ServiceResult> SendEmail(string to = "", string subject = "", string emailAddress = "")
        {
            return Result.ReturnAsSuccess(null, AlertResource.EmailSendAsSuccessfuly, null);
        }
        public async Task<ServiceResult> SendMessage(string phoneNumber)
        {
            return Result.ReturnAsSuccess(null, AlertResource.MessageSendAsSuccessfuly, null);
        }

        public async Task SendEmail()
        {
            try
            {
                EmailSettings _email = await GetEmailSetting("PSRNW");
                List<EmailsDto> _emails = await GetEmails();
                foreach (var _item in _emails)
                {
                    string toEmail = string.IsNullOrEmpty(_item.ToEmail) ? _email.ToEmail : _item.ToEmail;

                    MailMessage mail = new MailMessage()
                    {
                        From = new MailAddress(_email.UsernameEmail, "Jose Carlos Macoratti")
                    };

                    mail.To.Add(new MailAddress(toEmail));
                    mail.CC.Add(new MailAddress(_email.CcEmail));

                    mail.Subject = "Macoratti .net - " + _item.Subject;
                    mail.Body = _item.Body;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;

                    //outras opções
                    //mail.Attachments.Add(new Attachment(arquivo));
                    //

                    using (SmtpClient smtp = new SmtpClient(_email.PrimaryDomain, _email.PrimaryPort))
                    {
                        smtp.Credentials = new NetworkCredential(_email.UsernameEmail, _email.UsernamePassword);
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "SendEmail", PlatformType.Web);
            }
        }

        public async Task<List<EmailsDto>> GetEmails()
        {
            List<EmailsDto> res = new List<EmailsDto>();
            try
            {
                string query = "select * from Emails where IsSend = 0";
                var resultData = await _db.QueryAsync<EmailsDto>(query) as List<EmailsDto>;
                res = resultData;
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "GetEmails", PlatformType.Web);
            }
            return res;
        }

        public async Task<string> GetEmailTemplate(string code)
        {
            string res = string.Empty;
            try
            {
                string query = "select * from EmailTemplate where Code  = @code";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("code", code);
                var resultData = await _db.QueryFirstAsync<EmailTemplateDto>(query, filter);
                res = resultData.Description;
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "GetEmailTemplate", PlatformType.Web);
            }
            return res;
        }

        public async Task<EmailSettings> GetEmailSetting(string code)
        {
            EmailSettings res = new EmailSettings();
            try
            {
                string query = "select * from EmailSettigns where Code=@Code";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("Code", code);
                var data = await _db.QueryFirstAsync<EmailSettings>(query, filter);
                res = data;
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "GetEmailSetting", PlatformType.Web);
            }
            return res;
        }

        public async Task<ServiceResult> SaveEmails(EmailsDto dto)
        {
            ServiceResult res = new ServiceResult();
            try
            {
               // await SendEmail();
                string query = "insert into Emails (UserId, Subject, Body, FromEmail, ToEmail, ToEmail, CcEmail, IsSend, SendDate,  Process) values (@UserId, @Subject, @Body, @FromEmail, @ToEmail, @CcEmail, 0, NOW(),  @Process);";
                DynamicParameters filter = new DynamicParameters();

                filter.Add("UserId", dto.UserId);
                filter.Add("Subject, ", dto.Subject);
                filter.Add("Body, ", dto.Body);
                filter.Add("FromEmail, ", dto.FromEmail);
                filter.Add("ToEmail, ", dto.ToEmail);
                filter.Add("CcEmail, ", dto.CcEmail);
                filter.Add("Process", dto.Process);

                var result = await _db.ExecuteAsync(query,filter);
                res = Result.ReturnAsSuccess();
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "SaveEmails", PlatformType.Web);
                res = Result.ReturnAsFail(message: "Mail gönderilirken hata meydana geldi.");
            }
            return res;
        }
    }
}
