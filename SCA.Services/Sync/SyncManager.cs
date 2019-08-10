using AutoMapper;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Model;
using SCA.Repository.Repo;
using SCA.Repository.UoW;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class SyncManager : ISyncManager
    {
        private readonly IMapper _mapper;
        private readonly IUnitofWork _unitOfWork;
        private IGenericRepository<Content> _contentRepo;
        private readonly IApiManager _apiService;

        public SyncManager(IUnitofWork unitOfWork, IMapper mapper, IApiManager apiService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _contentRepo = unitOfWork.GetRepository<Content>();
            _apiService= apiService;
        }

        public async Task<ServiceResult> SyncAssay()
        {
            string data = await _apiService.Get("http://ogrencikariyeri.com/panel/json.php?act=haberler");
            List<SyncHeader> assayHeader = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SyncHeader>>(data);
            
            return Result.ReturnAsSuccess(message: assayHeader.Count+" Adet Makalenin Seknronizasyon İşlemi Tamamlanmıştır.",null);
        }
    }
}
