using Microsoft.Extensions.Options;
using SCA.DapperRepository.Generic;
using SCA.Entity.Entities;
using SCA.Entity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.DapperRepository
{
    public class CategoryRepository : GenericRepository<Entity.Entities.Category>, ICategory<Entity.Entities.Category>
    {
        public CategoryRepository(IOptions<ConnectionStrings> options) : base(options)
        {
        }
    }
}
