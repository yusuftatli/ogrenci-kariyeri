using SCA.Common.Result;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services.Interface
{
    public interface IUserManager
    {
        Task<ServiceResult> CreateUser(UsersDTO dto);
        Task<ServiceResult> UpdateUser(UsersDTO dto);
        Task<ServiceResult> DeleteUser(long userId);
        Task UserLog(long userId);
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
    }
}
