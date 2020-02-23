using Dapper;
using MySql.Data.MySqlClient;
using Npgsql;
using SCA.Common;
using SCA.Common.Base;
using SCA.Common.Resource;
using SCA.Common.Result;
using SCA.Entity.Dto;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Entity.Model;
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
    public class ContentManager : BaseClass, IContentManager
    {
        private readonly ITagManager _tagManager;
        private readonly ICategoryManager _categoryManager;
        private readonly IUserManager _userManager;
        private readonly IErrorManagement _errorManagement;
        private readonly IDbConnection _db = new MySqlConnection(ConnectionString1);
        public ContentManager(ITagManager tagManager, ICategoryManager categoryManager, IUserManager userManager, IErrorManagement errorManagement)
        {
            _tagManager = tagManager;
            _userManager = userManager;
            _categoryManager = categoryManager;
            _errorManagement = errorManagement;
        }

        public async Task<ServiceResult> GetSearch(string seacrh, long count, string token)
        {
            ServiceResult res = new ServiceResult();
            try
            {
                
                string query = string.Empty;
                query = $"select Id, ImagePath, Header from Content where ContentDescription like '%{seacrh}%' limit {count};";
                var dataList = await _db.QueryAsync<ContentSeacrhListDto>(query) as List<ContentSeacrhListDto>;
                res = Result.ReturnAsSuccess(data: dataList);
            }
            catch (Exception)
            {

            }
            return res;
        }


        /// <summary>
        /// makale kısa açıklamalarını listeler
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult> ContentShortListByMobil(ContentSearchByMoilDto dto, string token)
        {
            ServiceResult res = new ServiceResult();
            long userId = JwtToken.GetUserId(token);
            try
            {

                var listData = await _db.QueryAsync<ContentForHomePageDTO>("Content_ListAllByMobil", new { _Type = dto.Type, _StartDate = dto.StartDate, _EndDate = dto.EndDate, _searhCategoryIds = dto.searhCategoryIds, _limit = dto.limit }, commandType: CommandType.StoredProcedure) as List<ContentForHomePageDTO>;

                res = Result.ReturnAsSuccess(data: listData);

            }
            catch (Exception _ex)
            {
                await _errorManagement.SaveError(_ex, userId, "ContentShortListByMobil", PlatformType.Mobil);
                res = Result.ReturnAsFail(message: "Mobil Content bilgileri yüklenirken hata meydana geldi");
            }
            return res;
        }

        /// <summary>
        /// makale kısa açıklamalarını listeler
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult> ContentShortListFavoriByMobil(int count, string token)
        {
            ServiceResult res = new ServiceResult();
            long userId = JwtToken.GetUserId(token);
            try
            {
                string query = "select * from Favorite f left join Content c on f.ContentId = c.Id where f.UserId=@UserId limit @count";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("UserId", userId);
                filter.Add("count", count);

                var dataList = await _db.QueryAsync<ContentForHomePageDTO>(query, filter) as List<ContentForHomePageDTO>;

                res = Result.ReturnAsSuccess(data: dataList);

            }
            catch (Exception _ex)
            {
                await _errorManagement.SaveError(_ex, userId, "ContentShortListByMobil", PlatformType.Mobil);
                res = Result.ReturnAsFail(message: "Mobil Content bilgileri yüklenirken hata meydana geldi");
            }
            return res;
        }


        /// <summary>
        /// makale kısa açıklamalarını listeler
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult> ContentShortList(ContentSearchDto dto, UserSession session)
        {
            ServiceResult res = new ServiceResult();
            try
            {
                string categoryIds = dto.searhCategoryIds.Replace('[', ' ').Replace(']', ' ');
                List<ContentShortListDto> dataList = new List<ContentShortListDto>();

                DateTime startDate = string.IsNullOrEmpty(dto.StartDate) ? DateTime.Now.AddDays(-30) : Convert.ToDateTime(dto.StartDate);
                DateTime endDate = string.IsNullOrEmpty(dto.EndDate) ? DateTime.Now.AddDays(30) : Convert.ToDateTime(dto.EndDate);

                if (session.RoleTypeId == 1 || session.RoleTypeId == 2)
                {
                    dataList = await _db.QueryAsync<ContentShortListDto>("getContentShortListById", new { StartDate = startDate.ToString("yyyy-MM-dd"), PublishEndate = endDate.ToString("yyyy-MM-dd"), userId = 0, category = categoryIds, menuSide = dto.menuSide }, commandType: CommandType.StoredProcedure) as List<ContentShortListDto>;
                }
                else
                {
                    dataList = await _db.QueryAsync<ContentShortListDto>("getContentShortListById", new { StartDate = startDate.ToString("yyyy-MM-dd"), PublishEndate = endDate.ToString("yyyy-MM-dd"), userId = session.Id, category = categoryIds, menuSide = dto.menuSide }, commandType: CommandType.StoredProcedure) as List<ContentShortListDto>;
                }
                if (dataList.Count > 0)
                {
                }

                res = Result.ReturnAsSuccess(session.RoleTypeId.ToString(), message: AlertResource.SuccessfulOperation, dataList);
            }
            catch (Exception _ex)
            {
                //await _errorManagement.SaveError(_ex, 0, "ContentShortList ", Entity.Enums.PlatformType.Web);
                res = Result.ReturnAsFail(message: "Haberler yüklenirken hata meydana geldi.");
            }
            return res;
        }

        /// <summary>
        /// UI için makaleleri short list olarak kullanıcı bilgileri ile birlikte döner 
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult> ContentShortListForUI()
        {
            ServiceResult res = new ServiceResult();
            await Task.Run(() =>
            {
                var dataList = 0;//_mapper.Map<List<ContentShortListUIDto>>(_contentRepo.GetAll(x => x.IsDeleted.Equals(false)).ToList());
                List<long> ids = new List<long>();

                //foreach (ContentShortListUIDto item in dataList)
                //{
                //    ids.Add(item.Id);
                //}

                var users = _userManager.GetShortUserInfo(ids);

                //dataList.ForEach(x =>
                //{
                //    x.Writer = users.Where(a => a.Id == x.CreatedUserId).Select(s => s.Name + " " + s.Surname).FirstOrDefault();
                //    x.WriterImagePath = users.Where(a => a.Id == x.CreatedUserId).Select(s => s.ImagePath).FirstOrDefault();
                //});
                res = Result.ReturnAsSuccess(data: dataList);
            });
            return res;
        }

        public async Task<ServiceResult> GetContentByMobil(long contentId, string token)
        {
            ServiceResult res = new ServiceResult();
          
            long userId = JwtToken.GetUserId(token);
            ContentDetailForDetailPageDTO result = new ContentDetailForDetailPageDTO();
            try
            {
                using (var multi = await _db.QueryMultipleAsync("ContentListByMobil", new { _Id = contentId, _UserId = userId }, commandType: CommandType.StoredProcedure))
                {
                    result = await multi.ReadFirstOrDefaultAsync<ContentDetailForDetailPageDTO>();
                    result.MostPopularItems = await multi.ReadAsync<ContentForHomePageDTO>() as List<ContentForHomePageDTO>;
                    result.CommentList = await multi.ReadAsync<SocialMediaListDto>() as List<CommentForUIDto>;
                    result.Taglist = await multi.ReadAsync<TagDto>() as List<TagDto>;

                    return Result.ReturnAsSuccess(data: res);
                }
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, 0, "GetCompanyDetail ", Entity.Enums.PlatformType.Web);
                res = Result.ReturnAsFail(message: "Hata");
            }
            return res;
        }

        public async Task<ContentDetailForDetailPageDTO> GetContentUI(string seoUrl, long? userId = null, string ip = "")
        {
            ContentDetailForDetailPageDTO res = new ContentDetailForDetailPageDTO();
            try
            {
                using (var multi = await _db.QueryMultipleAsync("ContentListBySeoUrl", new { _SeoUrl = seoUrl, _UserId = userId == null ? 0 : userId, _Ip = ip }, commandType: CommandType.StoredProcedure))
                {
                    res = await multi.ReadFirstOrDefaultAsync<ContentDetailForDetailPageDTO>();
                    res.MostPopularItems = await multi.ReadAsync<ContentForHomePageDTO>() as List<ContentForHomePageDTO>;
                    res.CommentList = await multi.ReadAsync<SocialMediaListDto>() as List<CommentForUIDto>;
                    res.Taglist = await multi.ReadAsync<TagDto>() as List<TagDto>;

                }
            }
            catch (Exception ex)
            {
                //await _errorManagement.SaveError(ex, 0, "GetContentUI ", Entity.Enums.PlatformType.Web);
            }
            return res;
        }

        public async Task<long> GetContentId(string seoUrl)
        {
            string query = $"select top 1 ContentId from content where seoUrl={seoUrl}";
            var data = await _db.ExecuteScalarAsync<long>(query);
            return data;
        }
        public async Task<ServiceResult> GetContentForMobil(string seoUrl)
        {
            ServiceResult res = new ServiceResult();
            await Task.Run(() =>
            {
                ContentForUIDto result = new ContentForUIDto();
                //var contentData = _mapper.Map<ContenUIDto>(_contentRepo.Get(x => x.SeoUrl == seoUrl && x.PlatformType == PlatformType.Mobil));

                //var userData = _userManager.GetUserInfo(contentData.CreatedUserId);

                //contentData.WriterName = userData.Name + " " + userData.Surname;
                //contentData.WriterImagePath = userData.ImagePath;

                res = Result.ReturnAsSuccess(data: null);
            });
            return res;
        }

        public async Task<ServiceResult> Dashboard()
        {
            ServiceResult res = new ServiceResult();
            try
            {
                List<ContentDashboardDto> data = new List<ContentDashboardDto>();

                string query = @"select count(Id) as Count,PublishStateType from Content group by PublishStateType";
                var result = await _db.QueryAsync<ContentDashboardDto>(query);
                long total = 0;
                foreach (var item in result)
                {
                    if (item.PublishStateType == 1)
                    {
                        data.Add(new ContentDashboardDto()
                        {
                            Description = "Taslak",
                            Count = item.Count
                        });
                        total += item.Count;
                    }
                    if (item.PublishStateType == 2)
                    {
                        data.Add(new ContentDashboardDto()
                        {
                            Description = "Yayın Aşamasında",
                            Count = item.Count
                        });
                        total += item.Count;
                    }
                    if (item.PublishStateType == 3)
                    {
                        data.Add(new ContentDashboardDto()
                        {
                            Description = "Yayında Değil",
                            Count = item.Count
                        });
                        total += item.Count;
                    }
                    if (item.PublishStateType == 4)
                    {
                        data.Add(new ContentDashboardDto()
                        {
                            Description = "Yayında",
                            Count = item.Count
                        });
                        total += item.Count;
                    }
                }
                data.Add(new ContentDashboardDto()
                {
                    Description = "Toplam",
                    Count = total
                });

                res = Result.ReturnAsSuccess(data: data);
            }
            catch (Exception ex)
            {
                res = res = Result.ReturnAsFail(message: "Kullanıcı dashboard bilgisi yüklenemedi");
                await _errorManagement.SaveError(ex, null, "User/Dashboard", PlatformType.Web);
            }
            return res;
        }

        public async Task<ServiceResult> GetContent(long id)
        {
            ServiceResult res = new ServiceResult();
            try
            {
                string query = "Select *From Content where Id=@Id";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("Id", id);

                var resultData = await _db.QueryFirstAsync<ContentDto>(query, filter);
                resultData.CategoryDes = await _categoryManager.GetCategoryById(resultData.Id);
                resultData.TagDes = await _tagManager.GetTagById(resultData.Id);
                resultData.TagIdList = await _tagManager.GetTagIdByContentId(resultData.Id);

                if (resultData != null)
                {
                    res = Result.ReturnAsSuccess(data: resultData);
                }
                else
                {
                    res = Result.ReturnAsFail(message: "Yüklenecek herhangi bir makale detayı bulunamadı");
                }
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "GetContent", PlatformType.Mobil);
                res = Result.ReturnAsFail(message: "Makale detay bilgisi yüklenirken hata meydana geldi");
            }
            return res;
        }
        public async Task<ServiceResult> GetContent(string url)
        {
            ServiceResult res = new ServiceResult();
            await Task.Run(() =>
            {
                //var listData = _mapper.Map<ContentDto>(_contentRepo.Get(x => x.SeoUrl == url));
                res = Result.ReturnAsSuccess(data: null);
            });
            return res;
        }
        public async Task<ServiceResult> UpdateContentPublish(publishStateDto dto, UserSession session)
        {
            ServiceResult res = new ServiceResult();
            try
            {
                string query = "Update Content set PublishStateType = @PublishStateType where Id=@Id";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("Id", dto.Id);
                filter.Add("PublishStateType", dto.publishState);

                var resultData = _db.Execute(query, filter);
                res = Result.ReturnAsSuccess();
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "UpdateContentPublish", PlatformType.Mobil);
                res = Result.ReturnAsFail();
            }
            return res;
        }
        /// <summary>
        /// Makaleleri içerikleri ile birlikte listeler
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult> ContentList()
        {
            ServiceResult res = new ServiceResult();
            await Task.Run(() =>
            {
                res = Result.ReturnAsSuccess(data: null);
            });
            return res;
        }
        /// <summary>
        /// makale ekler
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ServiceResult> ContentCreate(ContentDto dto, UserSession session)
        {
            ServiceResult res = new ServiceResult();
            long _contentId = 0;
            if (dto == null)
            {
                res = Result.ReturnAsFail(AlertResource.NoChanges, null);
            }
            string resultMessage = "";
            try
            {

                if (dto.IsSendConfirm == true)
                {
                    dto.PublishStateType = (dto.IsSendConfirm == true) ? 2 : 1;
                }

                if (dto.Id == 0)
                {
                    dto.UserId = session.Id;
                    dto.ReadCount = 0;
                    dto.Writer = session.Name + " " + session.Surname;
                    dto.PublishStateType = 1;

                    string query = "";
                    DynamicParameters filter = new DynamicParameters();
                    GetContentQuery(CrudType.Insert, dto, session, ref query, ref filter);
                    _contentId = await _db.QueryFirstAsync<long>(query, filter);

                    resultMessage = (dto.IsSendConfirm) ? "Haber editör Tarafına Onaya Gönderildi." : "Haber başarılı bir şekilde kaydedildi fakat editör onayına gönderilmek üzere beklemede";
                }
                else
                {
                    bool result = await ContentControl(dto.Id);
                    if (result == true)
                    {
                        res = Result.ReturnAsFail(message: "Yayında olan haber güncellemez.");
                        return res;
                    }
                    string query = "";
                    DynamicParameters filter = new DynamicParameters();
                    GetContentQuery(CrudType.Update, dto, session, ref query, ref filter);
                    await _db.ExecuteAsync(query, filter);

                    resultMessage = (dto.IsSendConfirm) ? "Haber editör Tarafına Onaya Gönderildi." : "Haber başarılı bir şekilde güncellendi fakat editör onayına gönderilmek üzere beklemede";
                }

                
                    res = Result.ReturnAsSuccess(message: resultMessage, data: _contentId> 0?_contentId :dto.Id);
                    string resTag = await _tagManager.CreateTag(dto.Tags, _contentId > 0 ? _contentId : dto.Id, ReadType.Content, session);
                    string resCategory = await _categoryManager.CreateCategoryRelation(dto.Category, _contentId > 0 ? _contentId : dto.Id, ReadType.Content, session);
              
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "ContentCreate", PlatformType.Mobil);
                res = Result.ReturnAsFail(message: resultMessage);
            }
            return res;
        }

        public async Task<bool> ContentControl(long id)
        {
            bool res = new bool();
            try
            {
                string query = string.Empty;
                query = "select * from Content where Id =@Id";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("Id", id);

                var data = await _db.QueryFirstAsync<Content>(query, filter);
                PublishState result = data.PublishStateType;
                if (result == PublishState.Publish)
                {
                    res = true;
                }
                else
                {
                    res = false;
                }
            }
            catch (Exception ex)
            {
            }
            return res;
        }

        public async Task<List<CommentForUIDto>> GetCommentsByContentId(long contentId)
        {
            List<CommentForUIDto> res = new List<CommentForUIDto>();
            await Task.Run(() =>
            {
                string query = $"Select * Comments where ArticleId ={contentId}";
                var listData = _db.Query<CommentForUIDto>(query) as List<CommentForUIDto>;
                res = listData;
            });
            return res;
        }

        public async Task<ServiceResult> CreateContent(ContentDto dto, UserSession session)
        {
            ServiceResult res = new ServiceResult();
            await Task.Run(() =>
            {
                string query = "";
                DynamicParameters filter = null;
                GetContentQuery(CrudType.Insert, dto, session, ref query, ref filter);

                var result = _db.Execute(query, filter);
                res = Result.ReturnAsSuccess();
            });
            return res;

        }

        public async Task<ServiceResult> CreateContentSyncData(ContentDto dto)
        {
            ServiceResult res = new ServiceResult();
            await Task.Run(() =>
            {
                UserSession session = new UserSession()
                {
                    Id = 1
                };

                string query = "";
                DynamicParameters filter = null;
                GetContentQuery(CrudType.Insert, dto, session, ref query, ref filter);

                var dataList = _db.Query<ContentShortListDto>(query, filter);

                res = Result.ReturnAsSuccess(); ;
            });
            return res;
        }

        public void GetContentQuery(CrudType crudType, ContentDto dto, UserSession session, ref string query, ref DynamicParameters filter)
        {
            if (crudType == CrudType.Insert)
            {
                query = @"INSERT INTO Content (UserId, PublishDate, PublishStateType, SycnId, ReadCount,ImagePath, SeoUrl, Header, Writer, ConfirmUserId, ConfirmUserName, Category, ContentDescription, PlatformType, IsHeadLine, IsManset, IsMainMenu, IsConstantMainMenu, EventId, InternId, VisibleId, Multiplier, CreatedUserId, CreatedDate, MenuSide)
                           VALUES (@UserId, @PublishDate, @PublishStateType, @SycnId, @ReadCount, @ImagePath, @SeoUrl, @Header, @Writer, @ConfirmUserId, @ConfirmUserName, @Category, @ContentDescription, @PlatformType, @IsHeadLine, @IsManset, @IsMainMenu, @IsConstantMainMenu, @EventId, @InternId, @VisibleId, 1,@CreatedUserId, @CreatedDate, 0);";
                query += "SELECT LAST_INSERT_ID();";
            }

            if (crudType == CrudType.Update)
            {
                query = @"update Content set UserId = @UserId, PublishDate = @PublishDate, ImagePath = @ImagePath, SeoUrl = @SeoUrl, 
                      Header = @Header, Writer = @Writer, ContentDescription = @ContentDescription, PlatformType = @PlatformType,
                      IsHeadLine = @IsHeadLine, IsManset = @IsManset, IsMainMenu = @IsMainMenu, IsConstantMainMenu = @IsConstantMainMenu,
                      EventId = @EventId, InternId = @InternId, VisibleId = @VisibleId, Multiplier = 1, UpdatedUserId = @UpdatedUserId,
                      CreatedDate = @UpdatedDate where Id = @Id";
            }
            var _filter = new DynamicParameters();


            if (crudType == CrudType.Insert)
            {
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
                _filter.Add("CreatedUserId", session.Id);
                _filter.Add("CreatedDate", DateTime.Now);
            }

            if (crudType == CrudType.Update)
            {
                _filter.Add("Id", dto.Id);
                _filter.Add("UserId", dto.UserId);
                _filter.Add("PublishDate", dto.PublishDate);
                _filter.Add("ImagePath", dto.ImagePath);
                _filter.Add("SeoUrl", dto.SeoUrl);
                _filter.Add("Header", dto.Header);
                _filter.Add("ContentDescription", dto.ContentDescription);
                _filter.Add("Writer", dto.Writer);
                _filter.Add("PlatformType", dto.PlatformType);
                _filter.Add("IsHeadLine", dto.IsHeadLine);
                _filter.Add("IsManset", dto.IsManset);
                _filter.Add("IsMainMenu", dto.IsMainMenu);
                _filter.Add("IsConstantMainMenu", dto.IsConstantMainMenu);
                _filter.Add("EventId", dto.EventId);
                _filter.Add("InternId", dto.InternId);
                _filter.Add("VisibleId", dto.VisibleId);
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
        /// makalelerin yayınlanma durumunu günceller
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="publishState"></param>
        /// <returns></returns>
        public async Task<ServiceResult> UpdateAssayState(long Id, PublishState publishState)
        {
            ServiceResult res = new ServiceResult();

            await Task.Run(() =>
            {
                string errorMessage = "Güncelleme işlemi başarılı";
                if (Id == 0)
                {
                    Result.ReturnAsFail(AlertResource.NoChanges, null);
                }

            });
            return res;
        }

        public async Task<List<ContentForHomePageDTO>> GetContentForHomePage(HitTypes hitTypes, int count, int offset = 0)
        {
            List<ContentForHomePageDTO> listData = new List<ContentForHomePageDTO>();
            try
            {
                listData = await _db.QueryAsync<ContentForHomePageDTO>("Content_ListAll", new { hitType = 1, count = count, pageNumber = offset }, commandType: CommandType.StoredProcedure) as List<ContentForHomePageDTO>;

                if (listData.Count > 0)
                {
                    List<long> ids = new List<long>();
                    foreach (ContentForHomePageDTO item in listData)
                    {
                        ids.Add(item.Id);
                    }
                    List<CategoriesDto> categoryList = await _categoryManager.GetCategoryListById(ids);

                    foreach (ContentForHomePageDTO item in listData)
                    {
                        string value = string.Empty;
                        var dataList = categoryList.Where(x => x.Id == item.Id).ToList();
                        if (dataList != null)
                        {
                            for (int i = 0; i < dataList.Count; i++)
                            {
                                if (i + 1 == dataList.Count)
                                {
                                    value += dataList[i].Description;
                                }
                                else
                                {
                                    value += dataList[i].Description + ", ";
                                }
                            }
                        }
                        else
                        {
                            item.Category = "";
                        }
                        item.Category = value;
                    }
                }
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "GetContentForHomePage", PlatformType.Mobil);
            }
            return listData;
        }

        public async Task<List<ContentForHomePageDTO>> GetContentForTopAndBottomSlide()
        {
            List<ContentForHomePageDTO> listData = new List<ContentForHomePageDTO>();
            try
            {
                string query = @"select 
		                            Id,
                                    SeoUrl,
                                    ImagePath,
                                    Header, 		
                                    PublishDate,
                                    Category,
                                    IsHeadLine, 	
                                    IsManset, 		
                                    Writer,	
                                    ReadCount,
                                    MenuSide 		
                                    from Content where  MenuSide in (1,2)";
                listData = await _db.QueryAsync<ContentForHomePageDTO>(query) as List<ContentForHomePageDTO>;
                return listData;
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "GetContentForHomePage", PlatformType.Mobil);
            }
            return listData;
        }


        public async Task<List<FavoriteDto>> GetFavoriteContents(int count)
        {
            List<FavoriteDto> res = new List<FavoriteDto>();
            await Task.Run(() =>
            {
                string query = $"select * from Favorite limit {count}";
                var listData = _db.Query<FavoriteDto>(query).ToList();
                res = listData;
            });
            return res;
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

        public async Task<ServiceResult> GetFavoriteContents(int count, string token)
        {
            ServiceResult res = new ServiceResult();
            long userId = JwtToken.GetUserId(token);
            try
            {
                int _count = count == 0 ? 10 : count;
                string query = $"select * from Favorite where UserId=@UserId limit @count";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("count", count);
                filter.Add("UserId", userId);
                var listData = await _db.QueryAsync<FavoriteDto>(query, filter) as List<FavoriteDto>;
                res = Result.ReturnAsSuccess(data: listData);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, userId, "GetFavoriteContents", PlatformType.Mobil);
                res = Result.ReturnAsFail(message: "Favori listesi yüklenirken hata meydana geldi");
            }
            return res;
        }
        public async Task<ServiceResult> CreateFavorite(FavoriteMobilDto dto, string token)
        {
            ServiceResult res = new ServiceResult();

            if (dto.ContentId == 0)
            {
                return Result.ReturnAsFail(message: "İçerik boş geçilemez");
            }
            long userId = JwtToken.GetUserId(token);
            try
            {
                string query = "";
                if (await FavoriteControl(userId, dto.ContentId) == false)
                {
                    query = $"Insert Into Favorite (UserId,ContentId,IsActive) values ({userId},{dto.ContentId},{dto.IsActive})";
                }
                else
                {
                    query = $"Update Favorite  set IsActive={dto.IsActive} where UserId={userId} and ContentId={dto.ContentId}";
                }

                var result = await _db.ExecuteAsync(query);
                res = Result.ReturnAsSuccess(message: "Favorilerinize eklendi");
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, userId, "CreateFavorite", PlatformType.Mobil);
                res = Result.ReturnAsSuccess(message: "İçerik favoriye alınırken hata meydana geldi.");
            }
            return res;
        }

        public async Task<bool> FavoriteControl(long userId, long cotentId)
        {
            bool res = false;
            string query = $"Select * from Favorite where UserId={userId} and ContentId ={cotentId}";
            var result = await _db.QueryAsync<FavoriteDto>(query) as List<FavoriteDto>;

            if (result.Count > 0)
            {
                res = true;
            }
            else
            {
                res = false;
            }
            return res;
        }

        public async Task<ServiceResult> GetSearch(string value)
        {
            ServiceResult res = new ServiceResult();
            try
            {
                string query = string.Empty;
                query = $"select SeoUrl from Content where SeoUrl like '%{value}%' union ALL " +
                    $"select SeoUrl from Content where Writer like '%{value}%' " +
                    $"UNION ALL " +
                    $"select SeoUrl from Content where ContentDescription like '%{value}'";

                var result = await _db.QueryAsync<SearchDto>(query) as List<SearchDto>;
                if (result.Count > 0)
                {
                    res = Result.ReturnAsSuccess(data: result);
                }
                else
                {
                    res = Result.ReturnAsSuccess(message: "Herhangi bir sonuç bulunamadı...");
                }
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "GetSearch", PlatformType.Mobil);
                res = Result.ReturnAsSuccess(message: "Herhangi bir sonuç bulunamadı...");
            }
            return res;
        }

        public async Task<List<TagDto>> GetAllTagsUI()
        {
            List<TagDto> res = new List<TagDto>();
            try
            {
                string query = "select Id, Description from Tags";
                var listData = await _db.QueryAsync<TagDto>(query) as List<TagDto>;
                res = listData;

            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "GetAllTagsUI", PlatformType.Mobil);
            }
            return res;
        }

        public async Task<ServiceResult> GetAllTags()
        {
            ServiceResult res = new ServiceResult();
            try
            {
                string query = "select Id, Description from Tags";
                var listData = await _db.QueryAsync<TagDto>(query) as List<TagDto>;
                res = Result.ReturnAsSuccess(data: listData);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "GetAllTags", PlatformType.Mobil);
            }
            return res;
        }

        public async Task<ServiceResult> UpdateMenuSide(long contentId, int state)
        {
            ServiceResult res = new ServiceResult();
            string query = string.Empty;
            try
            {
                query = "Select * from Content where Id = @Id";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("Id", contentId);
                var contentData = await _db.QueryFirstAsync<ContentDto>(query, filter);

                if (contentData.PlatformType == 1)
                {
                    res = Result.ReturnAsFail(message: "Haber platform türü mobil dir bu sebeble sabit menü ayarı yapılamaz.");
                    return res;
                }

                query = "Update Content set MenuSide = 0 where Id = @Id;" +
                        "Update Content set MenuSide = @MenuSide where Id = @Id;";
                filter = new DynamicParameters();
                filter.Add("Id", contentId);
                filter.Add("MenuSide", state);
                await _db.QueryAsync(query, filter);
                res = Result.ReturnAsSuccess(message: "Haber slider başlık pozisyonu güncellendi");
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "UpdateMenuSide", PlatformType.Web);
            }
            return res;
        }

        public async Task<ServiceResult> UpdatePlatformType(long contentId, int type)
        {
            ServiceResult res = new ServiceResult();
            string query = string.Empty;
            try
            {

                query = "update Content set  PlatformType = @PlatformType where Id = @Id";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("Id", contentId);
                filter.Add("PlatformType", type);
                await _db.QueryAsync(query, filter);

                res = Result.ReturnAsSuccess(message: "Haber Platform türü başarıyla güncellendi");
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "UpdateMenuSide", PlatformType.Web);
            }
            return res;
        }

        public async Task<ServiceResult> GetMenuSideState(long contentId)
        {
            ServiceResult res = new ServiceResult();
            string query = string.Empty;
            try
            {

                query = "Select * from Content where Id = @Id";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("Id", contentId);
                var resultData = await _db.QueryFirstAsync<ContentDto>(query, filter);


                res = Result.ReturnAsSuccess(data: resultData.MenuSide);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "UpdateMenuSide", PlatformType.Web);
            }
            return res;
        }
    }
}
