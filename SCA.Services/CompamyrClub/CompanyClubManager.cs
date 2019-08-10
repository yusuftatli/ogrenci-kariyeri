using AutoMapper;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Model;
using SCA.Repository.Repo;
using SCA.Repository.UoW;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class CompanyClubManager : ICompanyClubManager
    {
        private readonly IMapper _mapper;
        private readonly IUnitofWork _unitOfWork;
        private readonly IAddressManager _addressManager;
        private IGenericRepository<CompanyClubs> _companyClubsRepo;

        public CompanyClubManager(IUnitofWork unitOfWork, IMapper mapper, IAddressManager addressManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _companyClubsRepo = unitOfWork.GetRepository<CompanyClubs>();
        }

        public async Task<ServiceResult> GetAllCompanies()
        {
            var data = _companyClubsRepo.GetAll();
            return Result.ReturnAsSuccess(null, data);
        }

        public async Task<ServiceResult> CreateCompanyClubs(CompanyClubsDto dto)
        {
            string resultMessage = "";
            if (dto.Equals(null))
            {
                return Result.ReturnAsFail();
            }

            if (dto.Id == 0)
            {
                _companyClubsRepo.Add(_mapper.Map<CompanyClubs>(dto));
                resultMessage = "Kayıt İşlemi Başarılı";
            }
            else
            {
                _companyClubsRepo.Update(_mapper.Map<CompanyClubs>(dto));
                resultMessage = "Güncelleme İşlemi Başarılı";
            }
            return Result.ReturnAsSuccess(message: resultMessage, null);
        }
    }
}
