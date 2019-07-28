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
        Task CreateUserLog(UserLogDto dto);
        Task<ServiceResult> CreateUser(UsersDTO dto);
        Task<ServiceResult> DeleteUser(long userId);
        Task<ServiceResult> CreateUserByMobil(UserMobilDto dto);
        /// <summary>
        /// Kullanıcı bilgilerini döner
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        UsersDTO GetUserInfo(long Id);

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
    }
}
