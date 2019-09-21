using AutoMapper;
using Dapper;
using MySql.Data.MySqlClient;
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
using System.Net;
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
        private readonly IDbConnection _db = new MySqlConnection("Server=167.71.46.71;Database=StudentDbTest;Uid=ogrencikariyeri;Pwd=dXog323!s.?;");
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
        public async Task<ServiceResult> ContentShortListByMobil(ContentSearchByMoilDto dto, string token)
        {
            ServiceResult _res = new ServiceResult();
            long userId = JwtToken.GetUserId(token);
            try
            {
                string query = "";

                var listData = await _db.QueryAsync<ContentForHomePageDTO>("Content_ListAllByMobil", new { Type = dto.Type, StartDate = dto.StartDate, EndDate = dto.EndDate, searhCategoryIds = dto.searhCategoryIds, limit = dto.limit }, commandType: CommandType.StoredProcedure) as List<ContentForHomePageDTO>;

                _res = Result.ReturnAsSuccess(data: listData);
             
            }
            catch (Exception _ex)
            {
                 await _errorManagement.SaveError(_ex.ToString(), userId, "ContentShortListByMobil",PlatformType.Mobil);
                _res = Result.ReturnAsFail(message: "Mobil Content bilgileri yüklenirken hata meydana geldi");
            }
            return _res;
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
                string query = "";
                DynamicParameters filter = new DynamicParameters();

                DateTime startDate = string.IsNullOrEmpty(dto.StartDate) ? DateTime.Now.AddDays(-30) : Convert.ToDateTime(dto.StartDate);
                DateTime endDate = string.IsNullOrEmpty(dto.EndDate) ? DateTime.Now.AddDays(30) : Convert.ToDateTime(dto.EndDate);

                if (session.RoleTypeId == 1 || session.RoleTypeId == 2)
                {
                    query = ContentQuery.ContentListQuery;
                    filter.Add("PublishStartDate", startDate.ToString("yyyy-MM-dd"));
                    filter.Add("PublishEndate", endDate.ToString("yyyy-MM-dd"));

                }
                else
                {
                    query = ContentQuery.ContentListQueryWithUser;
                    filter.Add("PublishStartDate", startDate.ToString("yyyy-MM-dd"));
                    filter.Add("PublishEndate", endDate.ToString("yyyy-MM-dd"));
                    filter.Add("UserId", session.Id);
                }

                var dataList = _db.Query<ContentShortListDto>(query, filter).ToList();
                if (dataList.Count > 0)
                {
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
        public async Task<ContentDetailForDetailPageDTO> GetContentUI(string seoUrl, long? userId = null, string ip = "")
        {
            ContentDetailForDetailPageDTO _res = new ContentDetailForDetailPageDTO();
            try
            {
                _res = await _db.QueryFirstAsync<ContentDetailForDetailPageDTO>("Content_ListBySeoUrl", new { type = 1, _SeoUrl = seoUrl, _Ip = ip, _UserId = (userId == null) ? 0 : userId, ContentId = 0, count = 10 }, commandType: CommandType.StoredProcedure);
                _res.MostPopularItems = _db.Query<ContentForHomePageDTO>("Content_ListBySeoUrl", new { type = 2, _SeoUrl = seoUrl, _Ip = ip, _UserId = userId, ContentId = _res.Id, count = 10 }, commandType: CommandType.StoredProcedure).ToList();
                _res.CommentList = _db.Query<CommentForUIDto>("Content_ListBySeoUrl", new { type = 3, _SeoUrl = seoUrl, _Ip = ip, _UserId = userId, ContentId = _res.Id, count = 10 }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString());
            }
            return _res;
        }

        public async Task<long> GetContentId(string seoUrl)
        {
            string query = $"select top 1 ContentId from content where seoUrl={seoUrl}";
            var data = await _db.ExecuteScalarAsync<long>(query);
            return data;
        }
        public async Task<ServiceResult> GetContentForMobil(string seoUrl)
        {
            ContentForUIDto result = new ContentForUIDto();
            //var contentData = _mapper.Map<ContenUIDto>(_contentRepo.Get(x => x.SeoUrl == seoUrl && x.PlatformType == PlatformType.Mobil));

            //var userData = _userManager.GetUserInfo(contentData.CreatedUserId);

            //contentData.WriterName = userData.Name + " " + userData.Surname;
            //contentData.WriterImagePath = userData.ImagePath;

            return Result.ReturnAsSuccess(null, null, null);
        }
        public async Task<ServiceResult> GetContent(long id)
        {
            ServiceResult _res = new ServiceResult();
            try
            {
                string query = "Select *From Content where Id=@Id";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("Id", id);

                var resultData = await _db.QueryFirstAsync<ContentDto>(query, filter);

                if (resultData != null)
                {
                    _res = Result.ReturnAsSuccess(data: resultData);
                }
                else
                {
                    _res = Result.ReturnAsFail(message: "Yüklenecek herhangi bir makale detayı bulunamadı");
                }
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString());
                _res = Result.ReturnAsFail(message: "Makale detay bilgisi yüklenirken hata meydana geldi");
            }
            return _res;
        }
        public async Task<ServiceResult> GetContent(string url)
        {
            var listData = _mapper.Map<ContentDto>(_contentRepo.Get(x => x.SeoUrl == url));
            return Result.ReturnAsSuccess(null, null, listData);
        }
        public async Task<ServiceResult> UpdateContentPublish(publishStateDto dto, UserSession session)
        {
            ServiceResult _res = new ServiceResult();
            try
            {
                string query = "Update Content set PublishStateType = @PublishStateType where Id=@Id";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("Id", dto.Id);
                filter.Add("PublishStateType", dto.publishState);

                var resultData = _db.Execute(query, filter);
                _res = Result.ReturnAsSuccess();
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString());
                _res = Result.ReturnAsFail();
            }
            return _res;
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
        public async Task<ServiceResult> ContentCreate(ContentDto dto, UserSession session)
        {
            ServiceResult _res = new ServiceResult();
            long _contentId = 0;
            if (dto == null)
            {
                _res = Result.ReturnAsFail(AlertResource.NoChanges, null);
            }
            string resultMessage = "";
            try
            {

                if (dto.IsSendConfirm == true)
                {
                    dto.PublishStateType = (dto.IsSendConfirm == true) ? PublishState.PublishProcess : PublishState.Taslak;
                }

                Content res = null;
                if (dto.Id == 0)
                {
                    dto.ReadCount = 0;
                    dto.Writer = session.Name + " " + session.Surname;
                    dto.PublishStateType = PublishState.Taslak;

                    string query = "";
                    DynamicParameters filter = new DynamicParameters();
                    GetContentQuery(CrudType.Insert, dto, session, ref query, ref filter);
                    _contentId = _db.Query<long>(query, filter).FirstOrDefault();

                    resultMessage = (dto.IsSendConfirm) ? "Makale Yönetici Tarafına Onaya Gönderildi." : "Makale Taslak Olarak Kayıt Edildi.";
                }
                else
                {
                    string query = "";
                    DynamicParameters filter = new DynamicParameters();
                    GetContentQuery(CrudType.Update, dto, session, ref query, ref filter);
                    var contenId = _db.Execute(query, filter);

                    resultMessage = (dto.IsSendConfirm) ? "Makale Yönetici Tarafına Onaya Gönderildi." : "Makale Taslak Olarak Güncellendi.";
                }

                if (_contentId > 0)
                {
                    _res = Result.ReturnAsSuccess(message: resultMessage);
                    ServiceResult _resTag = await _tagManager.CreateTag(dto.Tags, _contentId, ReadType.Content, session);
                    if (_resTag.ResultCode != HttpStatusCode.OK)
                    {
                        _res = Result.ReturnAsFail("Makale kayıt edildi fakat etiket bilgileri kayıt edilirken hata meydana geldi, lütfen tekrar deneyiniz.");
                    }
                    ServiceResult _resCategory = await _categoryManager.CreateCategoryRelation(dto.Category, _contentId, ReadType.Content, session);
                    if (_resCategory.ResultCode != HttpStatusCode.OK)
                    {
                        _res = Result.ReturnAsFail("Makale kayıt edildi fakat categori bilgileri kayıt edilirken hata meydana geldi, lütfen daha sonra tekrar deneyiniz.");
                    }
                }
                else
                {
                    _res = Result.ReturnAsFail(message: resultMessage);
                }
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString());
                _res = Result.ReturnAsFail(message: resultMessage);
            }
            return _res;
        }

        public async Task<List<CommentForUIDto>> GetCommentsByContentId(long contentId)
        {
            string query = $"Select * Comments where ArticleId ={contentId}";
            var listData = _db.Query<CommentForUIDto>(query) as List<CommentForUIDto>;
            return listData;
        }

        public async Task<ServiceResult> CreateContent(ContentDto dto, UserSession session)
        {
            ServiceResult _res = new ServiceResult();

            string query = "";
            DynamicParameters filter = null;
            GetContentQuery(CrudType.Insert, dto, session, ref query, ref filter);

            var res = _db.Execute(query, filter);

            return Result.ReturnAsSuccess();

        }

        public async Task<ServiceResult> CreateContentSyncData(ContentDto dto)
        {
            ServiceResult _res = new ServiceResult();

            UserSession session = new UserSession()
            {
                Id = 1
            };

            string query = "";
            DynamicParameters filter = null;
            GetContentQuery(CrudType.Insert, dto, session, ref query, ref filter);

            var dataList = _db.Query<ContentShortListDto>(query, filter);

            return Result.ReturnAsSuccess();

        }



        public void GetContentQuery(CrudType crudType, ContentDto dto, UserSession session, ref string query, ref DynamicParameters filter)
        {
            query = @"INSERT INTO Content (UserId, PublishDate, PublishStateType, SycnId, ReadCount,ImagePath, SeoUrl, Header, Writer, ConfirmUserId, ConfirmUserName, Category, ContentDescription, PlatformType, IsHeadLine, IsManset, IsMainMenu, IsConstantMainMenu, EventId, InternId, VisibleId, CreatedUserId, CreatedDate)
                           VALUES (@UserId, @PublishDate, @PublishStateType, @SycnId, @ReadCount, @ImagePath, @SeoUrl, @Header, @Writer, @ConfirmUserId, @ConfirmUserName, @Category, @ContentDescription, @PlatformType, @IsHeadLine, @IsManset, @IsMainMenu, @IsConstantMainMenu, @EventId, @InternId, @VisibleId, @CreatedUserId, @CreatedDate);";

            if (crudType == CrudType.Insert)
            {
                query += "SELECT LAST_INSERT_ID();";
            }

            var _filter = new DynamicParameters();

            _filter.Add("UserId", dto.UserId);
            _filter.Add("PublishDate", dto.PublishDate);
            _filter.Add("PublishStateType", dto.PublishStateType);
            _filter.Add("SycnId", dto.SycnId);
            _filter.Add("ReadCount", dto.ReadCount);
            _filter.Add("ImagePath", dto.ImagePath);
            _filter.Add("SeoUrl", dto.SeoUrl);
            _filter.Add("Header", dto.Header);
            _filter.Add("Writer", dto.Writer);
            _filter.Add("ConfirmUserId", dto.ConfirmUserId);
            _filter.Add("ConfirmUserName", dto.ConfirmUserName);
            _filter.Add("Category", dto.Category);
            _filter.Add("ContentDescription", dto.ContentDescription);
            _filter.Add("PlatformType", dto.PlatformType);
            _filter.Add("IsHeadLine", dto.IsHeadLine);
            _filter.Add("IsManset", dto.IsManset);
            _filter.Add("IsMainMenu", dto.IsMainMenu);
            _filter.Add("IsConstantMainMenu", dto.IsConstantMainMenu);
            _filter.Add("EventId", dto.EventId);
            _filter.Add("InternId", dto.InternId);
            _filter.Add("VisibleId", dto.VisibleId);
            if (crudType == CrudType.Insert)
            {
                _filter.Add("CreatedUserId", session.Id);
                _filter.Add("CreatedDate", DateTime.Now);
            }

            if (crudType == CrudType.Update)
            {
                _filter.Add("UpdatedUserId", session.Id);
                _filter.Add("UpdatedDate", DateTime.Now);
            }

            if (crudType == CrudType.Delete)
            {
                _filter.Add("DeletedDate", session.Id);
                _filter.Add("DeletedUserId", DateTime.Now);
                _filter.Add("IsDeleted", true);
            }

            filter = _filter;
        }


        /// <summary>
        /// makale siler
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ServiceResult> ContentDelete(long Id)
        {
            ServiceResult _res = new ServiceResult();

            await Task.Run(() =>
            {
                if (Id == 0)
                {
                    Result.ReturnAsFail(AlertResource.NoChanges, null);
                }
                var deleteData = _contentRepo.Get(x => x.Id == Id);
                var result = _unitOfWork.SaveChanges();

                if (result.ResultCode == HttpStatusCode.OK)
                {
                    _res = Result.ReturnAsSuccess(message: "Silme işlemi başarılı");
                }
                else
                {
                    _res = Result.ReturnAsFail(message: "Silme işlemi sırasında hata meydana geldi.");
                    _errorManagement.SaveError(result.Message);
                }
            });

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
            ServiceResult _res = new ServiceResult();

            await Task.Run(() =>
            {
                string errorMessage = "Güncelleme işlemi başarılı";
                if (Id == 0)
                {
                    Result.ReturnAsFail(AlertResource.NoChanges, null);
                }
                var data = _contentRepo.Get(x => x.Id.Equals(Id) && x.IsDeleted.Equals(false));
                data.PublishStateType = publishState;
                _contentRepo.Update(_mapper.Map<Content>(data));
                var result = _unitOfWork.SaveChanges();
                if (result.ResultCode == HttpStatusCode.OK)
                {
                    _res = Result.ReturnAsSuccess(message: errorMessage);
                }
                else
                {
                    errorMessage = "Güncelleme işlemi yapılırken hata meydana geldi.";
                    _res = Result.ReturnAsFail(message: errorMessage);
                    _errorManagement.SaveError(_res.Message);
                }
            });
            return _res;
        }

        public async Task<List<ContentForHomePageDTO>> GetContentForHomePage(HitTypes hitTypes, int count)
        {
            List<ContentForHomePageDTO> listData = new List<ContentForHomePageDTO>();
            try
            {
                listData = await _db.QueryAsync<ContentForHomePageDTO>("Content_ListAll", new { hitType = 1, count = count }, commandType: CommandType.StoredProcedure) as List<ContentForHomePageDTO>;
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString());
            }
            return listData;
        }


        public async Task<List<FavoriteDto>> GetFavoriteContents(int count)
        {
            string query = $"select * from Favorite limit {count}";
            var listData = _db.Query<FavoriteDto>(query).ToList();
            return listData;
        }

        public async Task<List<ContentForHomePageDTO>> GetUsersFavoriteContents(long userId, int count)
        {
            string query = $"select * from Content where Id in (select ContentId from Favorite where UserId = {userId} and IsActive = 1) order by PublishDate desc  limit {count}";
            var listData = await _db.QueryAsync<ContentForHomePageDTO>(query) as List<ContentForHomePageDTO>;
            return listData;
        }

        public async Task<bool> CreateFavorite(FavoriteDto dto)
        {
            string query = "";
            if (await FavoriteControl(dto.UserId, dto.ContentId) == false)
            {
                query = $"Insert Into Favorite (UserId,ContentId,IsActive) values ({dto.UserId},{dto.ContentId},{dto.IsActive})";
            }
            else
            {
                query = $"Update Favorite  set IsActive={dto.IsActive} where UserId={dto.UserId} and ContentId={dto.ContentId}";
            }

            var res = await _db.ExecuteAsync(query);
            return (res != -1) ? true : false;
        }

        public async Task<bool> FavoriteControl(long userId, long cotentId)
        {
            bool _res = false;
            string query = $"Select * from Favorite where UserId={userId} and ContentId ={cotentId}";
            var result = await _db.QueryAsync<FavoriteDto>(query) as List<FavoriteDto>;

            if (result.Count > 0)
            {
                _res = true;
            }
            else
            {
                _res = false;
            }
            return _res;
        }
    }
}
