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
        private readonly ISocialMediaManager _socialmanager;
        private readonly IDbConnection _db = new MySqlConnection("Server=167.71.46.71;Database=StudentDbTest;Uid=ogrencikariyeri;Pwd=dXog323!s.?;");

        public CompanyClubManager(IUnitofWork unitOfWork, IMapper mapper, IAddressManager addressManager, IErrorManagement errorManagement, ISocialMediaManager socialManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _errorManagement = errorManagement;
            _socialmanager = socialManager;
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

        public async Task<ServiceResult> CreateCompanyClubs(CompanyClubsDto dto, UserSession session)
        {
            ServiceResult _res = new ServiceResult();
            DynamicParameters filter = new DynamicParameters();
            string query = "";
            if (dto.Equals(null))
            {
                return Result.ReturnAsFail(null, null);
            }

            try
            {
                if (dto.Id == 0)
                {
                    query = $"insert into CompanyClubs (CompanyClupType, ImageDirectory, ShortName, SectorType, SeoUrl, HeaderImage, SectorId, UserId, CreateUserName, Description, WebSite, PhoneNumber,EmailAddress,CreatedUserId,CreatedDate) values " +
                    $"insert into CompanyClubs (@CompanyClupType, @ImageDirectory, @ShortName, @SectorType, @SeoUrl, @HeaderImage, @SectorId, @UserId, @CreateUserName, @Description, @WebSite, @PhoneNumber,@EmailAddress,@CreatedUserId,@CreatedDate; SELECT LAST_INSERT_ID();";
                    filter.Add("CompanyClupType", dto.CompanyClupType);
                    filter.Add("ShortName", dto.ShortName);
                    filter.Add("ImageDirectory", dto.ImageDirectory);
                    filter.Add("SectorType", dto.SectorType);
                    filter.Add("SeoUrl", dto.SeoUrl);
                    filter.Add("SectorId", dto.SectorId);
                    filter.Add("UserId", session.Id);
                    filter.Add("CreateUserName", session.Name);
                    filter.Add("Description", dto.Description);
                    filter.Add("WebSite", dto.WebSite);
                    filter.Add("PhoneNumber", dto.PhoneNumber);
                    filter.Add("EmailAddress", dto.EmailAddress);
                    filter.Add("CreatedUserId", session.Id);
                    filter.Add("CreatedDate", DateTime.Now);
                }
                else
                {
                    query = $"Update CompanyClubs set  CompanyClupType=@CompanyClupType, ImageDirectory=@ImageDirectory, ShortName=@ShortName, SectorType=@SectorType, SeoUrl=@SeoUrl, HeaderImage=@HeaderImage, SectorId=@SectorId, UserId=@UserId, UpdatedUserId=@UpdatedUserId, UpdatedDate=@UpdatedDate, " +
                        $"Description=@Description, WebSite=@WebSite, PhoneNumber=@PhoneNumber,EmailAddress=@EmailAddress where Id=@Id";
                    filter.Add("Id", dto.Id);
                    filter.Add("CompanyClupType", dto.CompanyClupType);
                    filter.Add("ShortName", dto.ShortName);
                    filter.Add("ImageDirectory", dto.ImageDirectory);
                    filter.Add("SectorType", dto.SectorType);
                    filter.Add("SeoUrl", dto.SeoUrl);
                    filter.Add("SectorId", dto.SectorId);
                    filter.Add("UserId", session.Id);
                    filter.Add("CreateUserName", session.Name);
                    filter.Add("Description", dto.Description);
                    filter.Add("WebSite", dto.WebSite);
                    filter.Add("PhoneNumber", dto.PhoneNumber);
                    filter.Add("EmailAddress", dto.EmailAddress);
                    filter.Add("UpdatedUserId", session.Id);
                    filter.Add("UpdatedDate", DateTime.Now);
                }

                var res = _db.Execute(query, filter);

                List<SocialMediaDto> socialData = new List<SocialMediaDto>();
                socialData.Add(new SocialMediaDto { CompanyClupId = res, IsActive = true, SocialMediaType = SocialMediaType.Facebook, Url = dto.Facebook, UserId = null });
                socialData.Add(new SocialMediaDto { CompanyClupId = res, IsActive = true, SocialMediaType = SocialMediaType.Linkedin, Url = dto.Linkedin, UserId = null });
                socialData.Add(new SocialMediaDto { CompanyClupId = res, IsActive = true, SocialMediaType = SocialMediaType.Instagram, Url = dto.Instagram, UserId = null });


                foreach (var item in socialData)
                {
                  await  _socialmanager.CreateSocialMedia(item, session.Id);
                }
                string flag = (dto.CompanyClupType == CompanyClupType.Club) ? "Şirket" : "Klüp";
                _res = Result.ReturnAsSuccess(message: flag+" Başarıyla kaydedildi");
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
            return _res;
        }
    }
}
