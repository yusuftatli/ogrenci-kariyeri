using SCA.BLLServices.Generic;
using SCA.DapperRepository.Generic;
using SCA.Entity.Entities;

namespace SCA.BLLServices
{
    public class CategoryService : GenericService<Entity.Entities.Category>, ICategoryService<Entity.Entities.Category>
    {
        public CategoryService(IGenericRepository<Category> repository) : base(repository)
        {
        }
    }
}
