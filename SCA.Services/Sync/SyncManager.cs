﻿using AutoMapper;
using Dapper;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using SCA.Common;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.DTO.Sync;
using SCA.Entity.Enums;
using SCA.Entity.Model;
using SCA.Repository.Repo;
using SCA.Repository.UoW;
using SCA.Services.Sync;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class SyncManager : ISyncManager
    {
        private readonly IMapper _mapper;
        private readonly IUnitofWork _unitOfWork;
        private IGenericRepository<Content> _contentRepo;
        private IGenericRepository<Comments> _commentRepo;
        private readonly IApiManager _apiService;
        private readonly IDbConnection _db = new MySqlConnection("Server=167.71.46.71;Database=StudentDbTest;Uid=ogrencikariyeri;Pwd=dXog323!s.?;Convert Zero Datetime=True");

        public SyncManager(IUnitofWork unitOfWork, IMapper mapper, IApiManager apiService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _contentRepo = unitOfWork.GetRepository<Content>();
            _commentRepo = unitOfWork.GetRepository<Comments>();
            _apiService = apiService;
        }

        public async Task<ServiceResult> SyncUser()
        {
            ServiceResult _res = new ServiceResult();
            try
            {
                string query = "select * from ogrencikariyeri.tf_uyeler";
                var result = _db.Query(query).ToList();

            }
            catch (Exception ex)
            {
                _res = Result.ReturnAsFail(message: ex.ToString());

            }
            return _res;
        }

        public async Task<ServiceResult> SyncAssay()
        {
            SyncDetail assayDetail = null;
            string resMessage = "";
            try
            {
                string urlId = @"http://ogrencikariyeri.com/panel/yusuf.php?act=haberler";

                HttpClient clientId = new HttpClient();
                var resId = await clientId.GetAsync(urlId);
                var idcon = await resId.Content.ReadAsStringAsync();
                List<SyncHeader> ids = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SyncHeader>>(idcon);

                List<ContentDto> listContent = new List<ContentDto>();


                foreach (var item in ids)
                {
                    bool isData = ContentControl(Convert.ToInt64(item.ID));
                    if (isData == false)
                    {
                        ContentDto dto = new ContentDto();

                        HttpClient http = new HttpClient();
                        string url = @"https://ogrencikariyeri.com/panel/yusuf.php?act=haber&id=" + item.ID;

                        HttpClient client = new HttpClient();
                        var response = await client.GetAsync(url);
                        var pageContents = await response.Content.ReadAsStringAsync();

                        assayDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<SyncDetail>(pageContents);

                        dto.Header = assayDetail.post_title;
                        dto.ContentDescription = assayDetail.post_content;
                        dto.UserId = Convert.ToInt64(assayDetail.post_author);
                        dto.Category = assayDetail.kategori;
                        dto.SeoUrl = assayDetail.post_title.FriendlyUrl();

                        if (!string.IsNullOrEmpty(assayDetail.appStaj))
                        {
                            dto.InternId = Convert.ToInt32(assayDetail.appStaj);
                        }


                        if (!string.IsNullOrEmpty(assayDetail.appEtkinlik))
                        {
                            dto.EventId = Convert.ToInt32(assayDetail.appEtkinlik);
                        }

                        if (assayDetail.appKat != null)
                        {
                            if (!string.IsNullOrEmpty(assayDetail.appKat.Replace(",", "").Replace("null", "")))
                            {
                                dto.EventId = Convert.ToInt32(assayDetail.appKat.Replace(",", "").Replace("null", ""));
                            }
                        }

                        if (assayDetail.appStaj != null)
                        {
                            if (!string.IsNullOrEmpty(assayDetail.appStaj.Replace(",", "").Replace("null", "")))
                            {
                                dto.InternId = Convert.ToInt32(assayDetail.appStaj.Replace(",", "").Replace("null", ""));

                            }
                        }

                        dto.ImagePath = assayDetail.foto;
                        dto.PublishDate = Convert.ToDateTime(assayDetail.post_date);
                        dto.Writer = assayDetail.yazar;
                        dto.ReadCount = 0;
                        dto.PublishStateType = PublishState.Publish;
                        dto.SycnId = Convert.ToInt64(item.ID);

                        if (assayDetail.app == "1")
                        {
                            dto.PlatformType = PlatformType.Mobil;
                        }
                        else if (assayDetail.app == "0")
                        {
                            dto.PlatformType = PlatformType.Web;
                        }
                        else
                        {
                            dto.PlatformType = PlatformType.WebMobil;
                        }
                        CreateContentSyncData(dto);
                    }
                }
            }
            catch (Exception ex)
            {
                resMessage = ex.ToString();


            }





            return Result.ReturnAsSuccess(null, message: resMessage, assayDetail);
        }

        public async Task<ServiceResult> SyncDiger()
        {
            HttpClient http = new HttpClient();
            string url = @"https://ogrencikariyeri.com/panel/yusuf.php?act=sabit";

            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            var pageContents = await response.Content.ReadAsStringAsync();

            RootObject assayDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(pageContents);

            return Result.ReturnAsSuccess(null, message: " Adet Makalenin Seknronizasyon İşlemi Tamamlanmıştır.", assayDetail);
        }

        public async Task<ServiceResult> CreateContentSyncData(ContentDto dto)
        {
            ServiceResult _res = new ServiceResult();
            try
            {
                UserSession session = new UserSession()
                {
                    Id = 1
                };

                string query = "";
                DynamicParameters filter = new DynamicParameters();
                GetContentQuery(CrudType.Insert, dto, session, ref query, ref filter);

                var result = _db.Execute(query, filter);
            }
            catch (Exception ex)
            {

                _res = Result.ReturnAsFail(message: ex.ToString());
            }
            return _res;
        }


        public bool ContentControl(long id)
        {
            bool res = false;
            string query = "select * from Content where SycnId=" + id;
            var result = _db.Query<DistrictDto>(query).ToList();

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

        public void GetContentQuery(CrudType crudType, ContentDto dto, UserSession session, ref string query, ref DynamicParameters filter)
        {
            query = @"INSERT INTO Content (UserId, PublishStateType, SycnId, ReadCount,ImagePath, SeoUrl, Header, Writer, ConfirmUserId, PublishDate, ConfirmUserName, Category, ContentDescription, PlatformType, IsHeadLine, IsManset, IsMainMenu, IsConstantMainMenu, EventId, InternId, VisibleId, CreatedUserId, CreatedDate, UpdatedUserId, UpdatedDate, DeletedDate, DeletedUserId)
                           VALUES (@UserId, @PublishStateType, @SycnId, @ReadCount, @ImagePath, @SeoUrl, @Header, @Writer, @ConfirmUserId,@PublishDate, @ConfirmUserName, @Category, @ContentDescription, @PlatformType, @IsHeadLine, @IsManset, @IsMainMenu, @IsConstantMainMenu, @EventId, @InternId, @VisibleId, @CreatedUserId, @CreatedDate, @UpdatedUserId, @UpdatedDate, @DeletedDate, @DeletedUserId);";

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
            _filter.Add("PublishDate", dto.PublishDate);
            _filter.Add("IsMainMenu", dto.IsMainMenu);
            _filter.Add("IsConstantMainMenu", dto.IsConstantMainMenu);
            _filter.Add("EventId", dto.EventId);
            _filter.Add("InternId", dto.InternId);
            _filter.Add("VisibleId", dto.VisibleId);
            if (crudType == CrudType.Insert)
            {
                _filter.Add("CreatedUserId", session.Id);
                _filter.Add("CreatedDate", DateTime.Now);
                _filter.Add("UpdatedUserId", "");
                _filter.Add("UpdatedDate", "");
                _filter.Add("DeletedDate", session.Id);
                _filter.Add("DeletedUserId", DateTime.Now);
            }
            else
            {
                _filter.Add("UpdatedUserId", "");
                _filter.Add("UpdatedDate", "");
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

    }
}
