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
        public UserManager(IUnitofWork unitOfWork, IMapper mapper, ISender sender, IPictureManager pictureManager)
        {
            _mapper = mapper;
            _sender = sender;
            _unitOfWork = unitOfWork;
            _userRepo = _unitOfWork.GetRepository<Users>();
            _userLogRepo = _unitOfWork.GetRepository<UserLog>();
            _socialMediaRepo = _unitOfWork.GetRepository<SocialMedia>();
            _pictureManager = pictureManager;
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
            string resultMessage = "";
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
                //dto.Password = Guid.NewGuid().ToString();
                _user = _userRepo.Add(_mapper.Map<Users>(dto));
                resultMessage = "Kayıt İşlemi Başarılı";
            }
            else
            {
                _userRepo.Update(_mapper.Map<Users>(dto));
                resultMessage = "Güncelleme İşlemi Başarılı";
            }

            var res = _unitOfWork.SaveChanges();

            //if (dto.SocialMedia.Count > 0)
            //{
            //    var _socialMedia = _mapper.Map<List<SocialMedia>>(dto.SocialMedia);
            //    if (dto.Id == 0)
            //    {
            //        foreach (var item in _socialMedia)
            //        {
            //            item.Id = _user.Id;
            //        }
            //        _socialMediaRepo.AddRange(_socialMedia);
            //    }
            //    else
            //    {
            //        List<SocialMedia> socailData = _socialMediaRepo.GetAll(x => x.Id == dto.Id).ToList();
            //        foreach (var _item in socailData)
            //        {
            //            foreach (var _dto in _socialMedia)
            //            {
            //                if (_item.SocialMediaType == _dto.SocialMediaType)
            //                {
            //                    _item.Id = _dto.Id;
            //                    _item.Url = _dto.Url;
            //                    _item.IsActive = _dto.IsActive;
            //                    _socialMediaRepo.Update(_mapper.Map<SocialMedia>(_item));
            //                }
            //            }
            //            _unitOfWork.SaveChanges();
            //        }
            //    }
            //}

            return Result.ReturnAsSuccess(message: resultMessage, null);
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
