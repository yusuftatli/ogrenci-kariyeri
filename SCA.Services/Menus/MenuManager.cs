using AutoMapper;
using Newtonsoft.Json;
using SCA.Common.Resource;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Model;
using SCA.Repository.Repo;
using SCA.Repository.UoW;
using SCA.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class MenuManager : IMenuManager
    {
        private readonly IMapper _mapper;
        private readonly IUnitofWork _unitOfWork;
        private readonly IRoleManager _roleManager;
        private IGenericRepository<ScreenMaster> _screenMasterRepo;
        private IGenericRepository<ScreenDetail> _screenDetailRepo;

        public MenuManager(IUnitofWork unitOfWork, IMapper mapper, IRoleManager roleManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _screenMasterRepo = unitOfWork.GetRepository<ScreenMaster>();
            _screenDetailRepo = unitOfWork.GetRepository<ScreenDetail>();
        }

        //public async Task<ServiceResult> GetSuperUser(long roleId)
        //{
        //    var roleData = _roleManager.GetRoleTypeDataRow(roleId);
        //    ScreenListWithRolesTypeDto menuData = JsonConvert.DeserializeObject<ScreenListWithRolesTypeDto>(roleData.Menus);

        //}

        //public async Task<ServiceResult> GetScreensForSuperUser()
        //{

        //}




        #region ScreenMaster CRUD

        public async Task<ServiceResult> CreateScreenMaster(ScreenMaster dto)
        {
            string resultMessage = "";
            if (dto.Equals(null))
            {
                Result.ReturnAsFail();
            }

            dto.IsActive = false;
            dto.IsSuperUser = false;

            if (dto.Id == 0)
            {
                _screenMasterRepo.Add(_mapper.Map<ScreenMaster>(dto));
                resultMessage = AlertResource.CreateIsOk;
            }
            else
            {
                _screenMasterRepo.Update(_mapper.Map<ScreenMaster>(dto));
                resultMessage = AlertResource.UpdateIsOk;
            }
            _unitOfWork.SaveChanges();
            return Result.ReturnAsSuccess(message: resultMessage, null);
        }

        public async Task<ServiceResult> ScreenMasterState(long id, bool state)
        {
            var data = _screenMasterRepo.Get(x => x.Id == id);
            data.IsActive = state;
            _screenMasterRepo.Update(_mapper.Map<ScreenMaster>(data));
            return Result.ReturnAsSuccess(message: "Veri Güncelleme İşlemi Başarılı", null);
        }

        #endregion


        #region ScreenDetail CRUD

        public async Task<ServiceResult> CreateScreenDetail(ScreenDetailDto dto)
        {
            string resultMessage = "";
            if (dto.Equals(null))
            {
                Result.ReturnAsFail();
            }

            dto.IsActive = false;
            dto.IsSuperUser = false;

            if (dto.Id == 0)
            {
                _screenDetailRepo.Add(_mapper.Map<ScreenDetail>(dto));
                resultMessage = AlertResource.CreateIsOk;
            }
            else
            {
                _screenDetailRepo.Update(_mapper.Map<ScreenDetail>(dto));
                resultMessage = AlertResource.UpdateIsOk;
            }
            _unitOfWork.SaveChanges();
            return Result.ReturnAsSuccess(message: resultMessage, null);
        }

        public async Task<ServiceResult> ScreenDetailState(long id, bool state)
        {
            var data = _screenDetailRepo.Get(x => x.Id == id);
            data.IsActive = state;
            _screenDetailRepo.Update(_mapper.Map<ScreenDetail>(data));
            return Result.ReturnAsSuccess(message: "Veri Güncelleme İşlemi Başarılı", null);
        }

        #endregion

    }
}
