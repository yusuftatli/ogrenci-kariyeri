using AutoMapper;
using Dapper;
using Npgsql;
using SCA.Common;
using SCA.Common.Resource;
using SCA.Common.Result;
using SCA.Entity.Dto;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Entity.Model;
using SCA.Repository.Repo;
using SCA.Repository.UoW;
using SCA.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class ContentManager : IContentManager
    {
        private readonly IMapper _mapper;
        private readonly ITagManager _tagManager;
        private readonly IUnitofWork _unitOfWork;
        private readonly ICategoryManager _categoryManager;
        private readonly IUserManager _userManager;
        private readonly IErrorManagement _errorManagement;
        private IGenericRepository<Content> _contentRepo;
        private readonly IDbConnection _db = new NpgsqlConnection("Host=167.71.46.71;Database=StudentDb;Username=postgres;Password=og123456;Port=5432");
        public ContentManager(IUnitofWork unitOfWork, IMapper mapper, ITagManager tagManager, ICategoryManager categoryManager, IUserManager userManager, IErrorManagement errorManagement)
        {
            _mapper = mapper;
            _tagManager = tagManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _categoryManager = categoryManager;
            _errorManagement = errorManagement;
            _contentRepo = unitOfWork.GetRepository<Content>();
        }
        /// <summary>
        /// makale kısa açıklamalarını listeler
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult> ContentShortList(ContentSearchDto dto, UserSession session)
        {
            ServiceResult _res = new ServiceResult();
            try
            {
                DateTime startDate = string.IsNullOrEmpty(dto.StartDate) ? DateTime.Now.AddDays(-30) : Convert.ToDateTime(dto.StartDate);

                DateTime endDate = string.IsNullOrEmpty(dto.EndDate) ? DateTime.Now.AddDays(30) : Convert.ToDateTime(dto.EndDate);


                string query = "SELECT \"Id\",\"Header\",\"Writer\",\"ReadCount\", \"Category\",\"CreatedDate\",\"PublishDate\",\"PublishStateType\",\"PlatformType\",\"ConfirmUserName\" " +
                               "FROM public.\"Content\"  where \"PublishDate\" >= '" + startDate.ToString("yyyy-MM-dd") + "' and \"PublishDate\" <= '" + endDate.ToString("yyyy-MM-dd") + "'";


                var dataList = _db.Query<ContentShortListDto>(query).ToList();
                if (dataList.Count > 0)
                {
                    dataList.ForEach(x =>
                    {
                        x.PublishStateTypeDes = x.PublishStateType.GetDescription();
                        x.PlatformTypeDes = x.PlatformType.GetDescription();
                    });
                }

                return Result.ReturnAsSuccess(session.RoleTypeId.ToString(), message: AlertResource.SuccessfulOperation, dataList);
            }
            catch (Exception _ex)
            {
                string res = await _errorManagement.SaveError(_ex.ToString());
                _res = Result.ReturnAsFail(message: res, null);
            }
            return _res;
        }
        /// <summary>
        /// UI için makaleleri short list olarak kullanıcı bilgileri ile birlikte döner 
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult> ContentShortListForUI()
        {
            var dataList = _mapper.Map<List<ContentShortListUIDto>>(_contentRepo.GetAll(x => x.IsDeleted.Equals(false)).ToList());
            List<long> ids = new List<long>();

            foreach (ContentShortListUIDto item in dataList)
            {
                ids.Add(item.Id);
            }

            var users = _userManager.GetShortUserInfo(ids);

            dataList.ForEach(x =>
            {
                x.Writer = users.Where(a => a.Id == x.CreatedUserId).Select(s => s.Name + " " + s.Surname).FirstOrDefault();
                x.WriterImagePath = users.Where(a => a.Id == x.CreatedUserId).Select(s => s.ImagePath).FirstOrDefault();
            });


            return Result.ReturnAsSuccess(null, null, dataList);
        }
        public async Task<ServiceResult> GetContentUI(string url)
        {
            ContentForUIDto result = new ContentForUIDto();
            var contentData = _mapper.Map<ContenUIDto>(_contentRepo.Get(x => x.SeoUrl == url));

            var userData = _userManager.GetUserInfo(contentData.CreatedUserId);

            contentData.WriterName = userData.Name + " " + userData.Surname;
            contentData.WriterImagePath = userData.ImagePath;

            return Result.ReturnAsSuccess(null, null, contentData);
        }
        public async Task<ServiceResult> GetContent(long id)
        {
            var listData = _mapper.Map<ContentDto>(_contentRepo.Get(x => x.Id == id));
            return Result.ReturnAsSuccess(null, null, listData);
        }
        public async Task<ServiceResult> GetContent(string url)
        {
            var listData = _mapper.Map<ContentDto>(_contentRepo.Get(x => x.SeoUrl == url));
            return Result.ReturnAsSuccess(null, null, listData);
        }
        public async Task<ServiceResult> UpdateContentPublish(publishStateDto dto, UserSession session)
        {
            if (dto.publishState.Equals(PublishState.Publish))
            {
                var data = _contentRepo.Get(x => x.Id == dto.id);
                data.PublishStateType = (PublishState)dto.publishState;
                data.ConfirmUserId = 1;
                data.ConfirmUserName = session.Name + " " + session.Surname;
                _contentRepo.Update(data);
                _unitOfWork.SaveChanges();
            }
            else
            {
                var data = _contentRepo.Get(x => x.Id == dto.id);
                data.PublishStateType = (PublishState)dto.publishState;
                _contentRepo.Update(data);
              var res=  _unitOfWork.SaveChanges();
            }
            return Result.ReturnAsSuccess(null, null, null);
        }
        /// <summary>
        /// Makaleleri içerikleri ile birlikte listeler
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult> ContentList()
        {
            return Result.ReturnAsSuccess(null, null, _mapper.Map<List<ContentDto>>(_contentRepo.GetAll(x => x.IsDeleted.Equals(false)).ToList()));
        }
        /// <summary>
        /// makale ekler
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ServiceResult> ContentCreate(ContentDto dto)
        {
            string resultMessage = "";
            if (dto == null)
            {
                Result.ReturnAsFail(AlertResource.NoChanges, null);
            }
            if (dto.IsSendConfirm == true)
            {
                dto.PublishStateType = (dto.IsSendConfirm == true) ? PublishState.PublishProcess : PublishState.Taslak;
            }

            Content res = null;
            if (dto.Id == 0)
            {
                dto.ReadCount = 0;
                dto.Writer = "Yusuf Can TATLI";
                dto.PublishStateType = PublishState.Taslak;
                var data = _mapper.Map<Content>(dto);
                res = _contentRepo.Add(data);
                resultMessage = (dto.IsSendConfirm) ? "Makale Yönetici Tarafına Onaya Gönderildi." : "Makale Taslak Olarak Kayıt Edildi.";
            }
            else
            {
                var updateData = _mapper.Map<Content>(dto);
                _contentRepo.Update(updateData);
                resultMessage = (dto.IsSendConfirm) ? "Makale Yönetici Tarafına Onaya Gönderildi." : "Makale Taslak Olarak Güncellendi.";
            }
            _unitOfWork.SaveChanges();
            await _tagManager.CreateTag(dto.Tags, res.Id, ReadType.Content);
            await _categoryManager.CreateCategoryRelation(_categoryManager.GetCategoryRelation(dto.Category, res.Id, ReadType.Content));
            return Result.ReturnAsSuccess(null, message: resultMessage, null);
        }
        /// <summary>
        /// makale siler
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ServiceResult> ContentDelete(long Id)
        {
            if (Id == null)
            {
                Result.ReturnAsFail(AlertResource.NoChanges, null);
            }
            var deleteData = _contentRepo.Get(x => x.Id == Id);
            return _unitOfWork.SaveChanges();
        }
        /// <summary>
        /// makalelerin yayınlanma durumunu günceller
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="publishState"></param>
        /// <returns></returns>
        public async Task<ServiceResult> UpdateAssayState(long Id, PublishState publishState)
        {
            if (Id == 0)
            {
                Result.ReturnAsFail(AlertResource.NoChanges, null);
            }
            var data = _contentRepo.Get(x => x.Id.Equals(Id) && x.IsDeleted.Equals(false));
            data.PublishStateType = publishState;
            _contentRepo.Update(_mapper.Map<Content>(data));
            return _unitOfWork.SaveChanges();
        }
        public async Task<List<ContentForHomePageDTO>> GetContentForHomePage(HitTypes hitTypes, int count)
        {
            return ComtentFake.FakeContentList();
        }
    }
}
