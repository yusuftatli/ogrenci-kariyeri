using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.DapperRepository.Generic
{
    public interface IGenericRepository<U> where U : class
    {
        /// <summary>
        /// Get generic model from db by id parameter.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync<T>(T model, long id) where T : class;

        /// <summary>
        /// Get list of generic model from db.
        /// </summary>  
        /// <param name="dormitoryId"></param>
        /// <returns></returns>
        Task<List<T>> GetAllAsync<T>(T model) where T : class;

        /// <summary>
        /// Get list of generic model from db by where parameters includes.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="whereParams"></param>
        /// <returns></returns>
        Task<List<TResult>> GetByWhereParams<TRequest, TResult>(TResult entity, TRequest model, params string[] whereParams) where TRequest : class where TResult : class;

        /// <summary>
        /// Generic insert.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<long> InsertAsync<T>(T model) where T : class;

        /// <summary>
        /// Generic update.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        Task UpdateAsync<T>(T model) where T : class;

        /// <summary>
        /// Delete async.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(long id);

        /// <summary>
        /// Delete range async.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task DeleteRangeAsync(params long[] ids);

        /// <summary>
        /// Execute async for stored procedure.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task SPExecuteAsync<T>(T model) where T : class;

        /// <summary>
        /// Execute scalar async for stored procedure. Returns dynamic.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<dynamic> SPExecuteScalarAsync<T>(T model) where T : class;

        /// <summary>
        /// Execute query async for stored procedure.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<List<TResult>> SPQueryAsync<TRequest, TResult>(TRequest model) where TRequest : class where TResult : class;

        /// <summary>
        /// Execute query first or default async for stored procedure.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<TResult> SPQueryFirstOrDefaultAsync<TRequest, TResult>(TRequest model) where TRequest : class where TResult : class;

    }
}
