using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public interface IMenuManager
    {
        Task<List<MenuDto>> GetMenus(long? userId);
    }
}
