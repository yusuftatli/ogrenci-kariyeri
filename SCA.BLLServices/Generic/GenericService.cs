using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SCA.Common.Result;
using SCA.DapperRepository.Generic;

namespace SCA.BLLServices.Generic
{
    public class GenericService<U> : IGenericService<U> where U : class
    {
        private readonly IGenericRepository<U> _repository;

        public GenericService(IGenericRepository<U> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
            return Result.ReturnAsSuccess(message: "Silme işlemi başarılı!");
        }

        public async Task<ServiceResult> DeleteRangeAsync(params long[] ids)
        {
            await _repository.DeleteRangeAsync(ids);
            return Result.ReturnAsSuccess(message: "Silme işlemi başarılı!");
        }

        public async Task<ServiceResult> GetAllAsync<T>(T model) where T : class
        {
            var res = await _repository.GetAllAsync<T>(model);
            return Result.ReturnAsSuccess(data: res);
        }

        public async Task<ServiceResult> GetByIdAsync<T>(long id) where T : class
        {
            var res = await _repository.GetByIdAsync<T>(id);
            return Result.ReturnAsSuccess(data: res);
        }

        public async Task<ServiceResult> GetByWhereParams<TResult>(Expression<Func<U, bool>> predicate) where TResult : class
        {
            var res = await _repository.GetByWhereParams<TResult>(predicate);
            return Result.ReturnAsSuccess(data: res);
        }

        public async Task<ServiceResult> InsertAsync<T>(T model) where T : class
        {
            var res = await _repository.InsertAsync<T>(model);
            return Result.ReturnAsSuccess(data: res);
        }

        public async Task<ServiceResult> SPExecuteAsync<T>(T model) where T : class
        {
            await _repository.SPExecuteAsync<T>(model);
            return Result.ReturnAsSuccess(message: "İşlem başarılı!");
        }

        public async Task<ServiceResult> SPExecuteScalarAsync<T>(T model) where T : class
        {
            var res = await _repository.SPExecuteScalarAsync<T>(model);
            return Result.ReturnAsSuccess(data: res);
        }

        public async Task<ServiceResult> SPQueryAsync<TRequest, TResult>(TRequest model) where TRequest : class where TResult : class
        {
            var res = await _repository.SPQueryAsync<TRequest, TResult>(model);
            return Result.ReturnAsSuccess(data: res);
        }

        public async Task<ServiceResult> SPQueryFirstOrDefaultAsync<TRequest, TResult>(TRequest model) where TRequest : class where TResult : class
        {
            var res = await _repository.SPQueryAsync<TRequest, TResult>(model);
            return Result.ReturnAsSuccess(data: res);
        }

        public async Task<ServiceResult> UpdateAsync<T>(T model) where T : class
        {
            await _repository.UpdateAsync<T>(model);
            return Result.ReturnAsSuccess(message: "Güncelleme işlemi başarılı!");
        }
    }
}
