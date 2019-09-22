﻿using SCA.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.BLLServices.Generic
{
    public interface IGenericService<U> where U : class
    {
        Task<ServiceResult> GetByIdAsync<T>(T model, long id) where T : class;

        Task<ServiceResult> GetAllAsync<T>(T model) where T : class;

        Task<ServiceResult> GetByWhereParams<TRequest, TResult>(TResult result, TRequest request, params string[] whereParams) where TRequest : class where TResult : class;

        Task<ServiceResult> InsertAsync<T>(T model) where T : class;

        Task<ServiceResult> UpdateAsync<T>(T model) where T : class;

        Task<ServiceResult> DeleteAsync(long id);

        Task<ServiceResult> DeleteRangeAsync(params long[] ids);

        Task<ServiceResult> SPExecuteAsync<T>(T model) where T : class;

        Task<ServiceResult> SPExecuteScalarAsync<T>(T model) where T : class;

        Task<ServiceResult> SPQueryAsync<TRequest, TResult>(TRequest model) where TRequest : class where TResult : class;

        Task<ServiceResult> SPQueryFirstOrDefaultAsync<TRequest, TResult>(TRequest model) where TRequest : class where TResult : class;
    }
}
