using AutoMapper;
using Dapper;
using MySql.Data.MySqlClient;
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
        private readonly IMapper _mapper;
        private readonly IUnitofWork _unitOfWork;
        private readonly IDbConnection _db = new MySqlConnection("Server=167.71.46.71;Database=ErrorDbTest;Uid=ogrencikariyeri;Pwd=dXog323!s.?;");


        public ErrorManagement(IUnitofWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> SaveError(string value)
        {
            string error = "";
            try
            {
                string query = "insert into Errors(Description,UserId,ErrorDate) values(@Description,1,CURDATE());";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("Description", value);
                var result = _db.Execute(query, filter);
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            return error;
        }
    }
}
