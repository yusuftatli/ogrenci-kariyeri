using SCA.Common.Result;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SCA.BLLServices.Generic
{
    public interface IGenericService<U> where U : class
    {
        Task<ServiceResult<T>> GetByIdAsync<T>(long id) where T : class;

        Task<ServiceResult<List<T>>> GetAllAsync<T>(T model) where T : class;

        Task<ServiceResult<List<TResult>>> GetByWhereParams<TResult>(Expression<Func<U, bool>> predicate) where TResult : class;

        Task<ServiceResult<long>> InsertAsync<T>(T model) where T : class;

        Task<ServiceResult> UpdateAsync<T>(T model) where T : class;

        Task<ServiceResult> DeleteAsync(long id);

        Task<ServiceResult> DeleteRangeAsync(params long[] ids);
            
        Task<ServiceResult> SPExecuteAsync<T>(T model) where T : class;

        Task<ServiceResult<dynamic>> SPExecuteScalarAsync<T>(T model) where T : class;

        Task<ServiceResult<List<TResult>>> SPQueryAsync<TRequest, TResult>(TRequest model, string procedureName = null) where TRequest : class where TResult : class;

        Task<ServiceResult<List<TResult>>> SPQueryFirstOrDefaultAsync<TRequest, TResult>(TRequest model) where TRequest : class where TResult : class;
    }
}
