using AutoMapper;
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
        private IGenericRepository<Content> _contentRepo;
        public ContentManager(IUnitofWork unitOfWork, IMapper mapper, ITagManager tagManager, ICategoryManager categoryManager, IUserManager userManager)
        {
            _mapper = mapper;
            _tagManager = tagManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _categoryManager = categoryManager;
            _contentRepo = unitOfWork.GetRepository<Content>();
        }



        /// <summary>
        /// makale kısa açıklamalarını listeler
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult> ContentShortList(ContentSearchDto dto)
        {
            return Result.ReturnAsSuccess(null, _mapper.Map<List<ContentShortListDto>>(_contentRepo.GetAll(x => x.IsDeleted.Equals(false)).ToList()));
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


            return Result.ReturnAsSuccess(null, dataList);
        }

        public async Task<ServiceResult> GetContentUI(string url)
        {
            ContentForUIDto result = new ContentForUIDto();
            var contentData = _mapper.Map<ContenUIDto>(_contentRepo.Get(x => x.SeoUrl == url));

            var userData = _userManager.GetUserInfo(contentData.CreatedUserId);

            contentData.WriterName = userData.Name + " " + userData.Surname;
            contentData.WriterImagePath = userData.ImagePath;

            return Result.ReturnAsSuccess(null, contentData);
        }

        public async Task<ServiceResult> GetContent(string url)
        {
            var listData = _mapper.Map<ContentDto>(_contentRepo.Get(x => x.SeoUrl == url));
            return Result.ReturnAsSuccess(null, listData);
        }

        public async Task<ServiceResult> UpdateContentPublish(long id, PublishState publishState)
        {
            if (publishState.Equals(PublishState.Publish))
            {
                var data = _contentRepo.Get(x => x.Id == id);
                data.PublishStateType = publishState;
                data.ConfirmUserId = 1;
                data.ConfirmUserName = "tets";
                _contentRepo.Update(data);
                _unitOfWork.SaveChanges();
            }
            else
            {
                var data = _contentRepo.Get(x => x.Id == id);
                data.PublishStateType = publishState;
                _contentRepo.Update(data);
                _unitOfWork.SaveChanges();
            }
            return Result.ReturnAsSuccess();
        }

        /// <summary>
        /// Makaleleri içerikleri ile birlikte listeler
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult> ContentList()
        {
            return Result.ReturnAsSuccess(null, _mapper.Map<List<ContentDto>>(_contentRepo.GetAll(x => x.IsDeleted.Equals(false)).ToList()));
        }

        /// <summary>
        /// makale ekler
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ServiceResult> ContentCreate(ContentDto dto)
        {
            if (dto == null)
            {
                Result.ReturnAsFail(AlertResource.NoChanges, null);
            }
            dto.ReadCount = 0;
            dto.Writer = "yusuf";
            if (dto.IsSendConfirm == true)
            {
                dto.PublishStateType = (dto.IsSendConfirm == true) ? PublishState.PublishProcess : PublishState.Taslak;
            }

            var res = _contentRepo.Add(_mapper.Map<Content>(dto));
            _unitOfWork.SaveChanges();

            await _tagManager.CreateTag(dto.Tags, res.Id, ReadType.Content);
            await _categoryManager.CreateCategoryRelation(_categoryManager.GetCategoryRelation(dto.Category, res.Id, ReadType.Content));

            return Result.ReturnAsSuccess(null, null);
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
