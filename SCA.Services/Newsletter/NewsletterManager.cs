using Dapper;
using MySql.Data.MySqlClient;
using SCA.Common.Base;
using SCA.Common.Result;
using SCA.Entity;
using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class NewsletterManager :BaseClass, INewsletterManager
    {
        private readonly IErrorManagement _errorManagement;
        private readonly IDbConnection _db = new MySqlConnection(ConnectionString1);

        public NewsletterManager(IErrorManagement errorManagement)
        {
            _errorManagement = errorManagement;
        }

        public async Task<string> CreateNewsletter(NewsletterDto dto)
        {
            string res = "";
            try
            {
                bool data = await GetNewsletter(dto.EmailAddress);

                if (data == true)
                {
                    res = "Zaten bültene kaydınız var";
                }
                else
                {
                    string query = @"Insert Into Newsletter (UserId,EmailAddress) values (@UserId,@EmailAddress)";
                    DynamicParameters filter = new DynamicParameters();
                    filter.Add("UserId", dto.UserId);
                    filter.Add("EmailAddress", dto.EmailAddress);

                    var resultData = _db.Execute(query, filter);

                    res = "Bültene kaydınız başarıyla yapıldı";
                }
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "CreateNewsletter", PlatformType.Mobil);
            }
            return res;
        }

        public async Task<bool> GetNewsletter(string emailAddress)
        {
            bool res = false;
            try
            {
                string query = "Select * from Newsletter where EmailAddress=@EmailAddress";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("EmailAddress", emailAddress);

                var result = _db.QueryFirstOrDefaultAsync<NewsletterDto>(query, filter);
                if (result != null)
                {
                    res = true;
                }
                else
                {
                    res = false;
                }
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "GetNewsletter", PlatformType.Mobil);
            }
            return res;
        }
    }
}
