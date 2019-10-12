using AutoMapper;
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
            try
            {
                string query = @"insert into Errors (UserId, Process, StackTrace, Source, Message, InnerException, HResult, Data, TargetSite, HelpLink, PlatformType, ErrorDate) values 
                              (@UserId, @Process, @StackTrace, @Source, @Message, @InnerException, @HResult, @Data, @TargetSite, @HelpLink, @PlatformType, CURDATE());";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("UserId", userId);
                filter.Add("Process", process);
                filter.Add("StackTrace", ex.StackTrace);
                filter.Add("Source", ex.Source);
                filter.Add("Message", ex.Message);
                filter.Add("InnerException", ex.InnerException);
                filter.Add("HResult", ex.HResult);
                filter.Add("Data", ex.Data);
                filter.Add("TargetSite", ex.TargetSite);
                filter.Add("HelpLink", ex.HelpLink);
                filter.Add("PlatformType", platformType);

                var result = await _db.ExecuteAsync(query, filter);
            }
            catch (Exception ex1)
            {
                res = ex1.InnerException.ToString();
            }
            return res;
        }
    }
}
