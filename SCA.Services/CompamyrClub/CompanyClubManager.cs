using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using SCA.Common.Resource;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Entity.Model;
using SCA.Repository.Repo;
using SCA.Repository.UoW;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        private readonly IDbConnection _db = new MySqlConnection("Server=167.71.46.71;Database=StudentDbTest;Uid=ogrencikariyeri;Pwd=dXog323!s.?;");

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
            ServiceResult _res = new ServiceResult();
            string flag = (companyClupType == CompanyClupType.Club) ? "Şirket" : "Klüp";
            try
            {
                string query = "select * from CompanyClubs where CompanyClupType=@CompanyClupType";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("CompanyClupType", companyClupType);
                var resultData = _db.Query<CompanyClubsDto>(query, filter).ToList();

                if (resultData.Count > 0)
                {
                    _res = Result.ReturnAsSuccess(data: resultData);
                }
                else
                {
                    _res = Result.ReturnAsFail(message: $"{flag} bilgisi yüklenirken hata meydana geldi");
                }
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString());
                _res = Result.ReturnAsFail(message: $"{flag} bilgisi yüklenirken hata meydana geldi");
            }
            return _res;
        }

        public async Task<ServiceResult> GetCompanyId(string seoUrl)
        {
            ServiceResult _res = new ServiceResult();
            try
            {
                string query = "select * from CompanyClubs where SeoUrl=@SeoUrl";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("SeoUrl", seoUrl);

                var result = _db.Query<CompanyClubsDto>(query, filter).FirstOrDefault();
                if (result != null)
                {
                    _res = Result.ReturnAsSuccess(data: result);
                }
                else
                {
                    _res = Result.ReturnAsFail(message: "Aranılan şirket bulunamadı");
                }
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString());
                _res = Result.ReturnAsFail(message: "Şirket bilgisi yüklenirken hata meydana geldi.");
            }


            var data = _companyClubsRepo.Get(x => x.Id == 0);
            return Result.ReturnAsSuccess(null, message: AlertResource.SuccessfulOperation, data);
        }

        public async Task<ServiceResult> CreateCompanyClubs(CompanyClubsDto dto, long userId)
        {
            ServiceResult _res = new ServiceResult();
            string resultMessage = "";
            DynamicParameters filter = new DynamicParameters();
            string query = "";
            if (dto.Equals(null))
            {
                return Result.ReturnAsFail(null, null);
            }

            try
            {
                query = $"insert into CompanyClubs (CompanyClupType, ShortName, SectorType, SeoUrl, HeaderImage, SectorId, UserId, CreateUserName, Description, WebSite, PhoneNumber,EmailAddress,CreatedUserId,CreatedDate) values " +
                    $"insert into CompanyClubs (@CompanyClupType, @ShortName, @SectorType, @SeoUrl, @HeaderImage, @SectorId, @UserId, @CreateUserName, @Description, @WebSite, @PhoneNumber,@EmailAddress,@CreatedUserId,@CreatedDate";
                filter.Add("CompanyClupType", dto.CompanyClupType);
                //filter.Add("ShortName", dto.ShortName);
                //filter.Add("SectorType", dto.sec
                //filter.Add("SeoUrl", 
                //filter.Add("HeaderImage", 
                //filter.Add("SectorId", 
                //filter.Add("UserId", 
                //filter.Add("CreateUserName", 
                //filter.Add("Description", 
                //filter.Add("WebSite", 
                //filter.Add("PhoneNumber",
                //filter.Add("EmailAddress",
                //filter.Add("CreatedUserId",
                //filter.Add("CreatedDate",

            }
            catch (Exception)
            {

                throw;
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
                _res = Result.ReturnAsSuccess(null, message: resultMessage, null);
            }
            return _res;
        }
    }
}
