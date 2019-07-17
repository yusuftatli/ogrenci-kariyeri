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


        /// <summary>
        /// Kullanıcı bilgilerini döner
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public UsersDTO GetUserInfo(long Id)
        {
            var dataResult = _mapper.Map<UsersDTO>((_userRepo.Get(x => x.Id == Id)));
            return dataResult;
        }

        /// <summary>
        /// verilen idlere göre kullanıcıları listeler
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<UserShortInforDto> GetShortUserInfo(List<long> ids)
        {
            var listData = _mapper.Map<List<UserShortInforDto>>(_userRepo.GetAll(x => ids.Contains(x.Id)).ToList());
            return listData;
        }

        public async Task<ServiceResult> CreateUser(UsersDTO dto)
        {
            if (!UserControl(dto.EmailAddress))
            {
                Result.ReturnAsFail(AlertResource.EmailAlreadyExsist, null);
            }

            //if (dto.EmailAddress.Equals(null) && dto.EmailAddress == "")
            //{
            //    Result.ReturnAsFail("Ad Boş Geçilemez", null);
            //}

            if (dto.Equals(null))
            {
                Result.ReturnAsFail(AlertResource.NoChanges, null);
            }

            //if (dto.Name.Equals(null) && dto.Name == "")
            //{
            //    Result.ReturnAsFail("Ad Boş Geçilemez", null);
            //}

            //if (dto.Surname.Equals(null) && dto.Surname == "")
            //{
            //    Result.ReturnAsFail("Ad Boş Geçilemez", null);
            //}

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
                //_pictureManager.SaveImage(dto.ImageData, dto.Name + "-" + dto.Surname);
            }

            dto.HighSchoolTypeId = (dto.HighSchoolTypeId == 0) ? null : dto.HighSchoolTypeId;
            dto.UniversityId = (dto.UniversityId == 0) ? null : dto.UniversityId;
            dto.FacultyId = (dto.FacultyId == 0) ? null : dto.FacultyId;
            dto.DepartmentId = (dto.DepartmentId == 0) ? null : dto.DepartmentId;
            //dto.Password = Guid.NewGuid().ToString();
            _userRepo.Add(_mapper.Map<Users>(dto));
            var res = _unitOfWork.SaveChanges();
            return Result.ReturnAsSuccess("Kayıt işlemi Başarılı", res);
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
