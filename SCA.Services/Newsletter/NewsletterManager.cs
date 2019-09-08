using Dapper;
using MySql.Data.MySqlClient;
using SCA.Common.Result;
using SCA.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class NewsletterManager : INewsletterManager
    {
        private readonly IErrorManagement _errorManagement;
        private readonly IDbConnection _db = new MySqlConnection("Server=167.71.46.71;Database=StudentDbTest;Uid=ogrencikariyeri;Pwd=dXog323!s.?;");

        public NewsletterManager(IErrorManagement errorManagement)
        {
            _errorManagement = errorManagement;
        }

        public async Task<string> CreateNewsletter(NewsletterDto dto)
        {
            string _res = "";
            try
            {
                bool data = await GetNewsletter(dto.EmailAddress);

                if (data == true)
                {
                    _res = "Zaten bültene kaydınız var";
                }
                else
                {
                    string query = @"Insert Into Newsletter (UserId,EmailAddress) values (@UserId,@EmailAddress)";
                    DynamicParameters filter = new DynamicParameters();
                    filter.Add("UserId", dto.UserId);
                    filter.Add("EmailAddress", dto.EmailAddress);

                    var resultData = _db.Execute(query, filter);

                    _res = "Bültene kaydınız başarıyla yapıldı";
                }
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString());
            }
            return _res;
        }

        public async Task<bool> GetNewsletter(string emailAddress)
        {
            bool _res = false;
            try
            {
                string query = "Select * from Newsletter where EmailAddress=@EmailAddress";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("EmailAddress", emailAddress);

                var result = _db.QueryFirstOrDefaultAsync<NewsletterDto>(query, filter);
                if (result != null)
                {
                    _res = true;
                }
                else
                {
                    _res = false;
                }
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString());
            }
            return _res;
        }
    }
}
