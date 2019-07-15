using AutoMapper;
using SCA.Common.Resource;
using SCA.Common.Result;
using SCA.Entity.DTO;
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
    public class UserManager : IUserManager
    {
        private readonly IMapper _mapper;
        private ISender _sender;
        private readonly IUnitofWork _unitOfWork;
        private IGenericRepository<Users> _userRepo;
        private IGenericRepository<UserLog> _userLogRepo;
        private IPictureManager _pictureManager;
        public UserManager(IUnitofWork unitOfWork, IMapper mapper, ISender sender, IPictureManager pictureManager)
        {
            _mapper = mapper;
            _sender = sender;
            _unitOfWork = unitOfWork;
            _userRepo = _unitOfWork.GetRepository<Users>();
            _userLogRepo = _unitOfWork.GetRepository<UserLog>();
            _pictureManager = pictureManager;
        }

        public async Task<ServiceResult> CreateUser(UsersDTO dto)
        {
            if (!UserControl(dto.EmailAddress))
            {
                Result.ReturnAsFail(AlertResource.EmailAlreadyExsist, null);
            }
            if (dto.Equals(null))
            {
                Result.ReturnAsFail(AlertResource.NoChanges, null);
            }

            if (dto.IsEmailSend)
            {
                await _sender.SendEmail();
            }

            if (dto.IsPhoneSend)
            {
                await _sender.SendMessage("");
            }

            if (!string.IsNullOrEmpty(dto.ImageData))
            {
                _pictureManager.SaveImage("", "");
            }

            dto.Password = Guid.NewGuid().ToString();
            _userRepo.Add(_mapper.Map<Users>(dto));
            var res = _unitOfWork.SaveChanges();
            return Result.ReturnAsSuccess("Kayır işlemi Başarılı", res);
        }
        public async Task<ServiceResult> UpdateUser(UsersDTO dto)
        {
            if (dto.Equals(null))
            {
                return Result.ReturnAsFail(null, AlertResource.NoChanges);
            }
            _userRepo.Update(_mapper.Map<Users>(dto));
            var result = _unitOfWork.SaveChanges();
            return null;
        }
        public async Task<ServiceResult> DeleteUser(long userId)
        {
            var deleteData = _userRepo.GetAll(x => x.Id == userId).FirstOrDefault();
            _userRepo.Delete(deleteData);
            var result = _unitOfWork.SaveChanges();
            return null;
        }

        public async Task UserLog(long userId)
        {
            UserLogDto data = new UserLogDto();
            data.UserId = userId;
            data.EnteranceDate = DateTime.Now;
            _userLogRepo.Add(_mapper.Map<UserLog>(data));
        }
        public bool UserControl(string emailAddress)
        {
            bool _res = false;
            var userData = _userRepo.Get(x => x.EmailAddress == emailAddress);

            if (userData == null)
            {
                _res = true;
            }
            return _res;
        }
    }
}
