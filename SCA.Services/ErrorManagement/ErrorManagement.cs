using Dapper;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Entity.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class ErrorManagement : IErrorManagement
    {
        private readonly IDbConnection _db = new MySqlConnection("Server = 167.71.46.71; Database = ErrorDbTest; Uid = ogrencikariyeri; Pwd = dXog323!s.?;");


        public ErrorManagement()
        {
        }

        public async Task<string> SaveError(Exception ex, long? userId, string process, PlatformType platformType)
        {
            string res = string.Empty;
            string query = string.Empty;
            long? _userId = userId == null ? 0 : userId;
            try
            {
                if (ex.InnerException != null)
                {
                    query = $"insert into Errors (UserId, Process, StackTrace, PlatformType, ErrorDate) values " +
                                       $"({_userId}, '{process}', '{ex.InnerException.StackTrace.ToString()}', '{platformType}', CURDATE());";
                }
                else
                {
                    query = $"insert into Errors (UserId, Process, StackTrace, PlatformType, ErrorDate) values " +
                   $"({_userId}, '{process}', '{ex.Message}', '{platformType}', CURDATE());";
                }

                var data = _db.Execute(query);
            }
            catch (Exception ex1)
            {
                res = ex1.InnerException.ToString();
            }
            return res;
        }
    }
}
