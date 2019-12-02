using Dapper;
using MySql.Data.MySqlClient;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
  public  class LogManagementManager: ILogManagementManager
    {

        private readonly IErrorManagement _errorManagement;
        private readonly IDbConnection _db = new MySqlConnection("Server=167.71.46.71;Database=StudentDbTest;Uid=ogrencikariyeri;Pwd=dXog323!s.?;");


        public async Task ComoanyUserLog(CompanyLogManagementDto dto)
        {
            string query = "";
            DynamicParameters filter = new DynamicParameters();
            try
            {
                    query = @"insert into CompanyUserLog(CompanyId, UserId, datetime) values (@CompanyId, @UserId, now());";
                    filter.Add("CompanyId", dto.CompanyId);
                    filter.Add("UserId", dto.UserId);
                var result = _db.Execute(query, filter);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
