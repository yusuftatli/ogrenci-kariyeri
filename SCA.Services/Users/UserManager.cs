using AutoMapper;
using SCA.Common.Resource;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Entity.Model;
using SCA.Repository.Repo;
using SCA.Repository.UoW;
using SCA.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private IGenericRepository<SocialMedia> _socialMediaRepo;
        private IPictureManager _pictureManager;
        private IUserValidation _userValidation;
        private readonly IErrorManagement _errorManagement;
        public UserManager(IUnitofWork unitOfWork, IMapper mapper, ISender sender, IPictureManager pictureManager, IErrorManagement errorManagement, IUserValidation userValidation)
        {
            _mapper = mapper;
            _sender = sender;
            _unitOfWork = unitOfWork;
            _userRepo = _unitOfWork.GetRepository<Users>();
            _userLogRepo = _unitOfWork.GetRepository<UserLog>();
            _socialMediaRepo = _unitOfWork.GetRepository<SocialMedia>();
            _errorManagement = errorManagement;
            _pictureManager = pictureManager;
            _userValidation = userValidation;
        }

        public async Task<ServiceResult> CreateUserByMobil(UserMobilDto dto)
        {
            if (dto.Equals(null))
            {
                Result.ReturnAsFail();
            }

            var user = _mapper.Map<Users>(dto);
            user.IsActive = true;
            user.IsStudent = true;

            _userRepo.Add(user);
            return _unitOfWork.SaveChanges();

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


        public async Task<ServiceResult> RegisterUser(UserRegisterDto dto)
        {
            if (dto.Equals(null))
            {
                Result.ReturnAsFail(AlertResource.NoChanges, null);
            }

            ServiceResult _res = new ServiceResult();
            string resultMessage = "";

            _res = _userValidation.UserRegisterValidation(dto);
            if (HttpStatusCode.OK != _res.ResultCode)
            {
                return _res;
            }

            var userData = _mapper.Map<Users>(dto);
            userData.IsStudent = true;
            userData.RoleExpiresDate = DateTime.Now.AddYears(20);
            userData.RoleType = null;
            userData.IsActive = false;

            _userRepo.Add(_mapper.Map<Users>(dto));

            _unitOfWork.SaveChanges();
            return Result.ReturnAsFail(null, null);
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

        public async Task CreateUserLog(UserLogDto dto)
        {
            await Task.Run(() =>
            {
                _userLogRepo.Add(_mapper.Map<UserLog>(dto));
                _unitOfWork.SaveChanges();
            });

        }

        /// <summary>
        /// Kullanıcı kayıt (editör,admin vb)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ServiceResult> CreateUser(UsersDTO dto)
        {
            if (dto.Equals(null))
            {
                Result.ReturnAsFail(AlertResource.NoChanges, null);
            }


            ServiceResult _res = new ServiceResult();
            string resultMessage = "";

            _res = _userValidation.CreateUserValidation(dto);
            if (HttpStatusCode.OK != _res.ResultCode)
            {
                return _res;
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
                _pictureManager.SaveImage(dto.ImageData, dto.Name + "-" + dto.Surname);
            }

            Users _user = null;
            if (dto.Id == 0)
            {
                dto.IsActive = true;
                dto.BanCount = 0;
                dto.EnrollPlatformTypeId = PlatformType.Web;
                dto.HighSchoolTypeId = (dto.HighSchoolTypeId == 0) ? null : dto.HighSchoolTypeId;
                dto.UniversityId = (dto.UniversityId == 0) ? null : dto.UniversityId;
                dto.FacultyId = (dto.FacultyId == 0) ? null : dto.FacultyId;
                dto.DepartmentId = (dto.DepartmentId == 0) ? null : dto.DepartmentId;
                dto.CityId = (dto.CityId == 0) ? null : dto.CityId;
                dto.EducationStatusId = (dto.EducationStatusId == 0) ? null : dto.EducationStatusId;
                _user = _userRepo.Add(_mapper.Map<Users>(dto));
                resultMessage = "Kayıt İşlemi Başarılı";
            }
            else
            {
                _userRepo.Update(_mapper.Map<Users>(dto));
                resultMessage = "Güncelleme İşlemi Başarılı";
            }

            var result = _unitOfWork.SaveChanges();
            long _userId = _user.Id;

            if (dto.Id != 0)
            {
                var sData = _socialMediaRepo.GetAll(x => x.UserId == _userId).ToList();
                _socialMediaRepo.Delete(_mapper.Map<SocialMedia>(sData));
                _unitOfWork.SaveChanges();
            }

            List<SocialMediaDto> socialData = new List<SocialMediaDto>();
            socialData.Add(new SocialMediaDto { CompanyClupId = null, IsActive = true, SocialMediaType = SocialMediaType.Facebook, Url = dto.Facebook, UserId = _userId });
            socialData.Add(new SocialMediaDto { CompanyClupId = null, IsActive = true, SocialMediaType = SocialMediaType.Linkedin, Url = dto.Linkedin, UserId = _userId });
            socialData.Add(new SocialMediaDto { CompanyClupId = null, IsActive = true, SocialMediaType = SocialMediaType.Instagram, Url = dto.Instagram, UserId = _userId });

            _socialMediaRepo.AddRange(_mapper.Map<List<SocialMedia>>(socialData));
            _unitOfWork.SaveChanges();

            if (result.ResultCode != HttpStatusCode.OK)
            {
                await _errorManagement.SaveError(result.Message);
                _res = Result.ReturnAsFail(message: AlertResource.AnErrorOccurredWhenProcess, null);
            }
            else
            {
                _res = Result.ReturnAsSuccess(message: resultMessage, _userId);
            }

            return _res;
        }
        public async Task<ServiceResult> DeleteUser(long userId)
        {
            var deleteData = _userRepo.GetAll(x => x.Id == userId).FirstOrDefault();
            _userRepo.Delete(deleteData);
            var result = _unitOfWork.SaveChanges();
            return null;
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

        /// <summary>
        /// üye durumunu Aktif/pasif yapar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResult> UpdateUserStatu(long id, bool value)
        {
            var data = _userRepo.Get(x => x.Id == id);
            data.IsActive = value;
            _userRepo.Update(_mapper.Map<Users>(data));
            _unitOfWork.SaveChanges();
            return Result.ReturnAsSuccess();
        }

    }
}
