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
        public async Task<ContentDetailForDetailPageDTO> GetContentUI(string seoUrl, long? userId = null)
        {
            ContentDetailForDetailPageDTO _res = new ContentDetailForDetailPageDTO();
            try
            {
                string query = $"select *, IFNULL((select Id from Favorite _f where _f.UserId =@UserId and _f.ContentId = _c.Id and _f.IsActive = 1),0) as IsFavoriteContent " +
                    $"from Content _c where PlatformType <> 1 and seoUrl='@seoUrl'";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("seoUrl", seoUrl);
                filter.Add("UserId", (userId.HasValue ? userId.ToString() : "null"));
                _res = await _db.QueryFirstAsync<ContentDetailForDetailPageDTO>(query, new { SeoUrl = seoUrl });


                query = $"select  * from  Content where  PlatformType <> 1 order by PublishDate desc limit 10;";

                var result2 = await _db.QueryAsync<ContentForHomePageDTO>(query) as List<ContentForHomePageDTO>;
                _res.MostPopularItems = result2;

                query = $"select * from Comments where ArticleId={_res.Id}";
                _res.CommentList = await _db.QueryAsync<CommentForUIDto>(query) as List<CommentForUIDto>;
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
            var contentData = _mapper.Map<ContenUIDto>(_contentRepo.Get(x => x.SeoUrl == seoUrl && x.PlatformType == PlatformType.Mobil));

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
                var res = _unitOfWork.SaveChanges();
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
        public async Task<ServiceResult> ContentCreate(ContentDto dto, UserSession session)
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
                dto.Writer = session.Name + " " + session.Surname;
                dto.PublishStateType = PublishState.Taslak;

                string query = "";
                DynamicParameters filter = new DynamicParameters();
                GetContentQuery(CrudType.Insert, dto, session, ref query, ref filter);
                var contenId = _db.Execute(query, filter);

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

            await _tagManager.CreateTag(dto.Tags, res.Id, ReadType.Content, session);
            //await _categoryManager.CreateCategoryRelation(_categoryManager.GetCategoryRelation(dto.Category, res.Id, ReadType.Content, null));
            return Result.ReturnAsSuccess(null, message: resultMessage, null);
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
            query = @"INSERT INTO Content (UserId, PublishStateType, SycnId, ReadCount,ImagePath, SeoUrl, Header, Writer, ConfirmUserId, ConfirmUserName, Category, ContentDescription, PlatformType, IsHeadLine, IsManset, IsMainMenu, IsConstantMainMenu, EventId, InternId, VisibleId, CreatedUserId, CreatedDate, UpdatedUserId, DeletedDate, DeletedUserId, IsDeleted)
                           VALUES (@UserId, @PublishStateType, @SycnId, @ReadCount, @ImagePath, @SeoUrl, @Header, @Writer, @ConfirmUserId, @ConfirmUserName, @Category, @ContentDescription, @PlatformType, @IsHeadLine, @IsManset, @IsMainMenu, @IsConstantMainMenu, @EventId, @InternId, @VisibleId, @CreatedUserId, @CreatedDate, @UpdatedUserId, @DeletedDate, @DeletedUserId, @IsDeleted);";

            if (crudType == CrudType.Insert)
            {
                query += "SELECT LAST_INSERT_ID();";
            }

            var _filter = new DynamicParameters();

            _filter.Add("UserId", dto.UserId);
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
                _filter.Add("IsDeleted", false);
            }

            if (crudType == CrudType.Update)
            {
                _filter.Add("UpdatedUserId", session.Id);
                _filter.Add("UpdatedDate", DateTime.Now);
                _filter.Add("IsDeleted", false);
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
                string query = "";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("hitTypes", hitTypes);
                filter.Add("count", count);

                if (hitTypes == HitTypes.LastAssay)
                {
                    query = $"select  * from  Content where  PlatformType <> 1 order by PublishDate desc limit {count};";
                }

                else if (hitTypes == HitTypes.Manset)
                {
                    query = $"select  * from  Content where  PlatformType <> 1 order by Id asc limit {count};";
                }

                else if (hitTypes == HitTypes.MostPopuler)
                {
                    query = $"select  * from  Content where  PlatformType <> 1 order by ReadCount asc limit {count};";
                }

                else if (hitTypes == HitTypes.DailyMostPopuler)
                {
                    query = $"select  * from  Content where  PlatformType <> 1 order by Id asc limit {count};";
                }

                else if (hitTypes == HitTypes.HeadLine)
                {
                    query = $"select  * from  Content where  PlatformType <> 1 order by Id asc limit {count};";
                }
                listData = _db.Query<ContentForHomePageDTO>(query, filter).ToList();
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
