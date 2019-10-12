using SCA.Common.Result;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public interface IUserManager
    {
        Task<ServiceResult> PasswordRenew(string emailAddress,string token);
        Task CreateUserLog(UserLogDto dto);
        Task<ServiceResult> CheckUserForLogin(string email, string password);
        Task<ServiceResult> RegisterUser(UserRegisterDto dto);
        Task<ServiceResult> CreateUserByMobil(UserMobilDto dto);
        Task<List<UserModelList>> GetUserList();
        /// <summary>
        /// Kullanıcı bilgilerini döner
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<UsersDTO> GetUserInfo(long Id);

        /// <summary>
        /// verilen idlere göre kullanıcıları listeler
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<UserShortInforDto> GetShortUserInfo(List<long> ids);

        /// <summary>
        /// kullanıcı durumunu aktif veya pasif yapar 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<ServiceResult> UpdateUserStatu(long id, bool value);
        Task<ServiceResult> UserLoginByMobil(MobilUserLoginDto dto);
        Task<ServiceResult> UpdateUserRoleType(UserRoleTypeDto dto, UserSession session);

        /// <summary>
        /// Kullanıcı kategori günceleme
        /// </summary>
        /// <param name="userId">Kullanıcı id</param>
        /// <param name="category">kategori</param>
        /// <returns></returns>
        Task<ServiceResult> UpdateUserCategory(long userId, string category);
        Task<ServiceResult> Dashboard(UserSession session);
    }
}
