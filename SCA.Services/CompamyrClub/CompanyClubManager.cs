using AutoMapper;
using Microsoft.AspNetCore.Http;
using SCA.Common.Resource;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Entity.Model;
using SCA.Repository.Repo;
using SCA.Repository.UoW;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class CompanyClubManager : ICompanyClubManager
    {
        private readonly IMapper _mapper;
        private readonly IUnitofWork _unitOfWork;
        private readonly IAddressManager _addressManager;
        private IGenericRepository<SocialMedia> _socialMediaRepo;
        private IGenericRepository<CompanyClubs> _companyClubsRepo;
        private readonly IErrorManagement _errorManagement;

        public CompanyClubManager(IUnitofWork unitOfWork, IMapper mapper, IAddressManager addressManager, IErrorManagement errorManagement)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _errorManagement = errorManagement;
            _companyClubsRepo = unitOfWork.GetRepository<CompanyClubs>();
            _socialMediaRepo = _unitOfWork.GetRepository<SocialMedia>();
        }

        public async Task<ServiceResult> GetAllCompaniesClubs(CompanyClupType companyClupType)
        {
            var data = _mapper.Map<List<CompanyClubsDto>>(_companyClubsRepo.GetAll(x => x.CompanyClupType == companyClupType));
            return Result.ReturnAsSuccess(null, data);
        }

        public async Task<ServiceResult> GetCompanyId(long id)
        {
            var data = _companyClubsRepo.Get(x => x.Id == id);
            return Result.ReturnAsSuccess(message: AlertResource.SuccessfulOperation, data);
        }

        public async Task<ServiceResult> CreateCompanyClubs(CompanyClubsDto dto, long userId)
        {
            ServiceResult _res = new ServiceResult();
            string resultMessage = "";
            if (dto.Equals(null))
            {
                return Result.ReturnAsFail();
            }

            dto.UserId = 30;//userId;
            CompanyClubs resData = new CompanyClubs();

            if (dto.Id == 0)
            {
                resData = _companyClubsRepo.Add(_mapper.Map<CompanyClubs>(dto));
                resultMessage = "Kayıt İşlemi Başarılı";
            }
            else
            {
                _companyClubsRepo.Update(_mapper.Map<CompanyClubs>(dto));
                resultMessage = "Güncelleme İşlemi Başarılı";
                var updataSocialData = _socialMediaRepo.GetAll(x => x.CompanyClupId == dto.Id);
                foreach (var item in updataSocialData)
                {
                    _socialMediaRepo.Delete(item);
                }
            }
            var result = _unitOfWork.SaveChanges();

            List<SocialMediaDto> socialData = new List<SocialMediaDto>();
            socialData.Add(new SocialMediaDto { CompanyClupId = resData.Id, IsActive = true, SocialMediaType = SocialMediaType.Facebook, Url = dto.Facebook, UserId = null });
            socialData.Add(new SocialMediaDto { CompanyClupId = resData.Id, IsActive = true, SocialMediaType = SocialMediaType.Linkedin, Url = dto.Linkedin, UserId = null });
            socialData.Add(new SocialMediaDto { CompanyClupId = resData.Id, IsActive = true, SocialMediaType = SocialMediaType.Instagram, Url = dto.Instagram, UserId = null });

            _socialMediaRepo.AddRange(_mapper.Map<List<SocialMedia>>(socialData));
            _unitOfWork.SaveChanges();


            if (result.ResultCode != HttpStatusCode.OK)
            {
                await _errorManagement.SaveError(result.Message);
                _res = Result.ReturnAsFail(message: AlertResource.AnErrorOccurredWhenProcess, null);
            }
            else
            {
                _res = Result.ReturnAsSuccess(message: resultMessage, null);
            }
            return _res;
        }
    }
}
