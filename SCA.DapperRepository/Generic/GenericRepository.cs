using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using SCA.Entity.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SCA.DapperRepository.Generic
{
    public class GenericRepository<U> : IGenericRepository<U> where U : class
    {
        private readonly string _connString;
        private readonly string _tableName;

        public GenericRepository(IOptions<ConnectionStrings> options)
        {
            _connString = options.Value.MySqlConnection;
            _tableName = typeof(U).Name;
        }

        /// <inheritdoc cref="IGenericRepository{T}.SPExecuteAsync(T)"/>
        public async Task SPExecuteAsync<T>(T model) where T : class
        {
            using (IDbConnection conn = new MySqlConnection(_connString))
            {
                await conn.ExecuteAsync(typeof(T).Name, model, commandType: CommandType.StoredProcedure);
            }
        }

        /// <inheritdoc cref="IGenericRepository{T}.SPQueryAsync(T)"/>
        public async Task<List<TResult>> SPQueryAsync<TRequest, TResult>(TRequest model) where TRequest : class where TResult : class
        {
            using (IDbConnection conn = new MySqlConnection(_connString))
            {
                var res = await conn.QueryAsync<TResult>(typeof(TResult).Name, model, commandType: CommandType.StoredProcedure);
                return res as List<TResult>;
            }
        }

        /// <inheritdoc cref="IGenericRepository{T}.SPQueryFirstOrDefaultAsync(T)"/>
        public async Task<TResult> SPQueryFirstOrDefaultAsync<TRequest, TResult>(TRequest model) where TRequest : class where TResult : class
        {
            using (IDbConnection conn = new MySqlConnection(_connString))
            {
                var res = await conn.QueryFirstOrDefaultAsync<TResult>(typeof(TResult).Name, model, commandType: CommandType.StoredProcedure);
                return res;
            }
        }

        /// <inheritdoc cref="IGenericRepository{T}.SPExecuteScalarAsync(T)"/>
        public async Task<dynamic> SPExecuteScalarAsync<T>(T model) where T : class
        {
            using (IDbConnection conn = new MySqlConnection(_connString))
            {
                var res = await conn.ExecuteScalarAsync<dynamic>(typeof(T).Name, model, commandType: CommandType.StoredProcedure);
                return res;
            }
        }

        /// <inheritdoc cref="IGenericRepository.DeleteAsync(long)"/>
        public async Task DeleteAsync(long id)
        {
            using (IDbConnection conn = new MySqlConnection(_connString))
            {
                var query = _tableName.GenerateDeleteQuery();
                await conn.ExecuteAsync(query, new { Id = id });
            }
        }

        /// <inheritdoc cref="IGenericRepository.DeleteRangeAsync(long[])"/>
        public async Task DeleteRangeAsync(params long[] ids)
        {
            using (IDbConnection conn = new MySqlConnection(_connString))
            {
                var query = _tableName.GenerateDeleteRangeQuery();
                await conn.ExecuteAsync(query, ids);
            }
        }

        /// <inheritdoc cref="IGenericRepository{T}.GetAllAsync(T, long)"/>
        public async Task<List<T>> GetAllAsync<T>(T model) where T : class
        {
            using (IDbConnection conn = new MySqlConnection(_connString))
            {
                var query = model.GenerateSelectQuery<T,U>(_tableName);
                var res = await conn.QueryAsync<T>(query) as List<T>;
                return res;
            }
        }

        /// <inheritdoc cref="IGenericRepository{T}.GetByIdAsync(T, long)"/>
        public async Task<T> GetByIdAsync<T>(long id) where T : class
        {
            using (IDbConnection conn = new MySqlConnection(_connString))
            {
                var query = ((T)Activator.CreateInstance(typeof(T), null)).GenerateSelectQuery<T,U>(_tableName);
                var res = await conn.QueryFirstOrDefaultAsync<T>(query, new { Id = id });
                return res;
            }
        }

        /// <inheritdoc cref="IGenericRepository{T}.GetByWhereParams(T, string[])"/>
        public async Task<List<TResult>> GetByWhereParams<TResult>(Expression<Func<U, bool>> predicate) where TResult : class
        {
            using (IDbConnection conn = new MySqlConnection(_connString))
            {
                var query = ((TResult)Activator.CreateInstance(typeof(TResult), null)).GenerateSelectQuery(_tableName, predicate);
                var res = await conn.QueryAsync<TResult>(query) as List<TResult>;
                return res;
            }
        }

        /// <inheritdoc cref="IGenericRepository{T}.InsertAsync(T)"/>
        public async Task<long> InsertAsync<T>(T model) where T : class
        {
            using (IDbConnection conn = new MySqlConnection(_connString))
            {
                var query = model.GenerateInsertQuery(_tableName);
                var res = await conn.ExecuteScalarAsync<long>(query, model);
                return res;
            }
        }

        /// <inheritdoc cref="IGenericRepository{T}.UpdateAsync(T)"/>
        public async Task UpdateAsync<T>(T model) where T : class
        {
            using (IDbConnection conn = new MySqlConnection(_connString))
            {
                var query = model.GenerateUpdateQuery(_tableName);
                await conn.ExecuteAsync(query, model);
            }
        }
    }
}
