using Dapper;
using MySql.Data.MySqlClient;
using SCA.Common;
using SCA.Common.Base;
using SCA.Common.Resource;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Entities;
using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class ActiviyManager : BaseClass, IActivityManager
    {
        private readonly IDbConnection _db = new MySqlConnection(ConnectionString1);
        private readonly ITagManager _tagManager;
        private readonly ICategoryManager _categoryManager;
        private readonly IErrorManagement _errorManagement;

        public ActiviyManager()
        {



        }




        public async Task<ServiceResult> ActivityShortList(ContentSearchDto dto, UserSession session)
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
                    dataList = await _db.QueryAsync<ContentShortListDto>("getActivityShortListById", new { StartDate = startDate.ToString("yyyy-MM-dd"), PublishEndate = endDate.ToString("yyyy-MM-dd"), userId = 0, category = categoryIds, menuSide = dto.menuSide }, commandType: CommandType.StoredProcedure) as List<ContentShortListDto>;
                }
                else
                {
                    dataList = await _db.QueryAsync<ContentShortListDto>("getActivityShortListById", new { StartDate = startDate.ToString("yyyy-MM-dd"), PublishEndate = endDate.ToString("yyyy-MM-dd"), userId = session.Id, category = categoryIds, menuSide = dto.menuSide }, commandType: CommandType.StoredProcedure) as List<ContentShortListDto>;
                }
                if (dataList.Count > 0)
                {
                }

                res= Result.ReturnAsSuccess(session.RoleTypeId.ToString(), message: AlertResource.SuccessfulOperation, dataList);
            }
            catch (Exception _ex)
            {
                //await _errorManagement.SaveError(_ex, 0, "ContentShortList ", Entity.Enums.PlatformType.Web);
                res = Result.ReturnAsFail(message: "Etkinlik Listesi yüklenirken hata meydana geldi.");
            }
            return res;
        }











        public async Task<ServiceResult> ActivityCreate(ContentDto dto, UserSession session)
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
                    _contentId = _db.Query<long>(query, filter).FirstOrDefault();

                    resultMessage = (dto.IsSendConfirm) ? "Makale Yönetici Tarafına Onaya Gönderildi." : "Makale Taslak Olarak Kayıt Edildi.";
                }
                else
                {
                    bool result = await ActivityControl(dto.Id);
                    if (result == true)
                    {
                        res = Result.ReturnAsFail(message: "Yayında olan haber güncellemez.");
                        return res;
                    }
                    string query = "";
                    DynamicParameters filter = new DynamicParameters();
                    GetContentQuery(CrudType.Update, dto, session, ref query, ref filter);
                    var contenId = _db.Execute(query, filter);

                    resultMessage = (dto.IsSendConfirm) ? "Makale Yönetici Tarafına Onaya Gönderildi." : "Makale Taslak Olarak Güncellendi.";
                }

                if (_contentId > 0)
                {
                    res = Result.ReturnAsSuccess(message: resultMessage);
                    string resTag = await _tagManager.CreateTag(dto.Tags, _contentId, ReadType.Content, session);
                    string resCategory = await _categoryManager.CreateCategoryRelation(dto.Category, _contentId, ReadType.Content, session);
                }
                else
                {
                    res = Result.ReturnAsFail(message: resultMessage);
                }
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "ContentCreate", PlatformType.Mobil);
                res = Result.ReturnAsFail(message: resultMessage);
            }
            return res;
        }






        public void GetContentQuery(CrudType crudType, ContentDto dto, UserSession session, ref string query, ref DynamicParameters filter)
        {
            query = @"INSERT INTO Content (UserId, PublishDate, PublishStateType, SycnId, ReadCount,ImagePath, SeoUrl, 
                      Header, Writer, ConfirmUserId, ConfirmUserName, Category, ContentDescription, PlatformType, 
                      IsHeadLine, IsManset, IsMainMenu, IsConstantMainMenu, EventId, InternId, VisibleId, Multiplier, 
                      CreatedUserId, CreatedDate, MenuSide, IsActivicty, IsSownInSlider, ActiviyStartDate, ActivityEndDate)
                           VALUES (@UserId, @PublishDate, @PublishStateType, @SycnId, @ReadCount, @ImagePath, @SeoUrl,
                      @Header, @Writer, @ConfirmUserId, @ConfirmUserName, @Category, @ContentDescription, @PlatformType, 
                      @IsHeadLine, @IsManset, @IsMainMenu, @IsConstantMainMenu, @EventId, @InternId, @VisibleId, 1,
                      @CreatedUserId, @CreatedDate, 0, @IsActivicty, @IsSownInSlider, @ActiviyStartDate, @ActivityEndDate);";

            if (crudType == CrudType.Insert)
            {
                query += "SELECT LAST_INSERT_ID();";
            }

            query = @"UserId = @UserId, PublishDate = @PublishDate, ImagePath = @ImagePath, SeoUrl = @SeoUrl, 
                      Header = @Header, Writer = @Writer, ContentDescription = @ContentDescription, PlatformType = @PlatformType,
                      IsHeadLine = @IsHeadLine, IsManset = @IsManset, IsMainMenu = @IsMainMenu, IsConstantMainMenu = @IsConstantMainMenu,
                      EventId = @EventId, InternId = @InternId, VisibleId = @VisibleId, Multiplier = @Multiplier, UpdatedUserId = @UpdatedUserId,
                      CreatedDate = @UpdatedDate, IsActivicty = @IsActivicty, IsSownInSlider = @IsSownInSlider, 
                      ActiviyStartDate = @ActiviyStartDate, ActivityEndDate = @ActivityEndDate";

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
                _filter.Add("IsActivicty", dto.IsActivicty);
                _filter.Add("IsSownInSlider", dto.IsSownInSlider);
                _filter.Add("ActiviyStartDate", dto.ActiviyStartDate);
                _filter.Add("ActivityEndDate", dto.ActivityEndDate);
            }

            if (crudType == CrudType.Update)
            {
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
                _filter.Add("IsActivicty", dto.IsActivicty);
                _filter.Add("IsSownInSlider", dto.IsSownInSlider);
                _filter.Add("ActiviyStartDate", dto.ActiviyStartDate);
                _filter.Add("ActivityEndDate", dto.ActivityEndDate);
            }

            if (crudType == CrudType.Delete)
            {
                _filter.Add("DeletedDate", session.Id);
                _filter.Add("DeletedUserId", DateTime.Now);
                _filter.Add("IsDeleted", true);
            }

            filter = _filter;
        }

        public async Task<bool> ActivityControl(long id)
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

    }
}
