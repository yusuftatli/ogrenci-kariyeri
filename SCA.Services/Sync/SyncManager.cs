using AutoMapper;
using Newtonsoft.Json;
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
using System.IO;
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

        public SyncManager(IUnitofWork unitOfWork, IMapper mapper, IApiManager apiService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _contentRepo = unitOfWork.GetRepository<Content>();
            _commentRepo = unitOfWork.GetRepository<Comments>();
            _apiService = apiService;
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
                    var isData = _contentRepo.Get(x => x.SycnId == Convert.ToInt64(item.ID));
                    if (isData == null)
                    {
                        ContentDto dto = new ContentDto();

                        HttpClient http = new HttpClient();
                        string url = @"https://ogrencikariyeri.com/panel/yusuf.php?act=haber&id="+item.ID;

                        HttpClient client = new HttpClient();
                        var response = await client.GetAsync(url);
                        var pageContents = await response.Content.ReadAsStringAsync();

                        assayDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<SyncDetail>(pageContents);

                        dto.Header = assayDetail.post_title;
                        dto.ContentDescription = assayDetail.post_content;
                        dto.UserId = Convert.ToInt64(assayDetail.post_author);

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

                        Content res = _contentRepo.Add(_mapper.Map<Content>(dto));
                        _unitOfWork.SaveChanges();

                        if (assayDetail.yorumlar.Count > 0)
                        {
                            List<CommentsDto> yorumList = new List<CommentsDto>();
                            foreach (var _yorum in assayDetail.yorumlar)
                            {
                                CommentsDto yorum = new CommentsDto();
                                yorum.Approved = true;
                                yorum.ArticleId = res.Id;
                                yorum.PostDate = Convert.ToDateTime(_yorum.tarih);
                                yorum.UserID = Convert.ToInt64(_yorum.uye);
                                yorum.userName = _yorum.isim;
                                yorum.Comment = _yorum.yorum;
                                yorumList.Add(yorum);
                            }
                            _commentRepo.AddRange(_mapper.Map<List<Comments>>(yorumList));
                        }
                        _unitOfWork.SaveChanges();

                    }
               }
            }
            catch (Exception ex)
            {
                resMessage = ex.ToString();


            }




            return Result.ReturnAsSuccess(message: resMessage, assayDetail);
        }

        public async Task<ServiceResult> SyncDiger()
        {
            string data = "";

            HttpClient http = new HttpClient();
            string url = @"https://ogrencikariyeri.com/panel/yusuf.php?act=sabit";

            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            var pageContents = await response.Content.ReadAsStringAsync();

            RootObject assayDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(pageContents);

            return Result.ReturnAsSuccess(message: " Adet Makalenin Seknronizasyon İşlemi Tamamlanmıştır.", assayDetail);
        }
    }
}
