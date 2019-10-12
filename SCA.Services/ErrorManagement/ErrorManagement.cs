using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Entity.Model;
using SCA.Repository.Repo;
using SCA.Repository.UoW;
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

        public async Task<string> SaveError(string ex)
        {
            try
            {
                string query = "insert into Errors(UserId,Process,StackTrace) values (@Description,@PlatformType,CURDATE());";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("Description", "");
                filter.Add("PlatformType", 2);
                var result = await _db.ExecuteAsync(query, filter);
            }
            catch (Exception)
            {
            }
            return "";
        }
        public async Task<string> SaveError(string error, long userId, string process, PlatformType platformType)
        {
            try
            {
                string query = "insert into Errors(UserId,Process,Description,PlatformType,ErrorDate) values" +
                    "(@UserId,@Process,@Description,@PlatformType,CURDATE());";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("UserId", userId);
                filter.Add("Process", process);
                filter.Add("Description", error);
                filter.Add("PlatformType", platformType);
                var result = await _db.ExecuteAsync(query, filter);
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            return error;
        }
    }
}
