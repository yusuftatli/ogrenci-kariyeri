using AutoMapper;
using SCA.Entity.Model;
using SCA.Repository.Repo;
using SCA.Repository.UoW;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class ErrorManagement : IErrorManagement
    {
        private readonly IMapper _mapper;
        private readonly IUnitofWork _unitOfWork;
        private IGenericRepository<Errors> _errorRepo;
        public ErrorManagement(IUnitofWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _errorRepo = unitOfWork.GetRepository<Errors>();
        }

        public async Task<string> SaveError(string value)
        {
            string error = "";
            try
            {
                Errors data = new Errors();
                data.Error = value;

                _errorRepo.Add(data);
                var res = _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            return error;
        }
    }
}
