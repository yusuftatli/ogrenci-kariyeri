using SCA.DapperRepository.Generic;

namespace SCA.DapperRepository
{
    public interface IAnnounsment<U> : IGenericRepository<U> where U : class
    {
    }
}
