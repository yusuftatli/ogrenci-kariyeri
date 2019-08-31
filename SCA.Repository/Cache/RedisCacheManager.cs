using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using SCA.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Repository.Cache
{
    public class RedisCacheManager : CacheHelper, ICacheService
    {
        private static StackExchange.Redis.IDatabase _db;
        private readonly string _host;
        private readonly int _port;

        public RedisCacheManager()
        {
            var connectionString = "localhost";

            var redisConnect = connectionString.Split(',');

            _host = redisConnect[0].Split(':')[1];
            _port = Convert.ToInt32(redisConnect[1].Split(':')[1]);

            var option = new ConfigurationOptions
            {
                Password = redisConnect[2].Split(':')[1] == "" ? null : redisConnect[2].Split(':')[1],
                Ssl = Convert.ToBoolean(redisConnect[3].Split(':')[1]),
                AbortOnConnectFail = Convert.ToBoolean(redisConnect[4].Split(':')[1]),
                SyncTimeout = 100000
            };

            option.EndPoints.Add(_host, _port);
            var connect = ConnectionMultiplexer.Connect(option);
            _db = connect.GetDatabase();
        }
        public void RemoveAll()
        {
            var server = _db.Multiplexer.GetServer(_host, _port);
            foreach (var item in server.Keys())
                _db.KeyDelete(item);
        }

        public string GetString(string key)
        {
            var rValue = _db.SetMembers(key);
            if (rValue.Length == 0)
                return null;

            return rValue[0];
        }

        public T Get<T>(string key)
        {
            var rValue = _db.SetMembers(key);
            if (rValue.Length == 0)
                return default(T);

            var result = Deserialize<T>(rValue.ToStringArray());
            return result;
        }
        public List<T> GetList<T>(string key)
        {
            var rValue = _db.SetMembers(key);
            if (rValue.Length == 0)
                return default(List<T>);

            var result = DeserializeList<T>(rValue.ToStringArray());
            return result;
        }
        public void Set(string key, object data, int cacheMinute = 40000)
        {
            if (data == null)
                return;

            var entryBytes = Serialize(data);
            _db.SetAdd(key, entryBytes);

            var expiresIn = TimeSpan.FromMinutes(cacheMinute);

            if (cacheMinute > 0)
                _db.KeyExpire(key, expiresIn);
        }

        public void Set(string key, string data, int cacheMinute = 40000)
        {
            if (data == null)
                return;

            _db.SetAdd(key, data);

            var expiresIn = TimeSpan.FromMinutes(cacheMinute);

            if (cacheMinute > 0)
                _db.KeyExpire(key, expiresIn);
        }

        public bool CacheExist(string key)
        {
            return _db.KeyExists(key);
        }
        public bool Remove(string key)
        {
            return _db.KeyDelete(key);
        }
        public void RemoveByPattern(string pattern)
        {
            var server = _db.Multiplexer.GetServer(_host, _port);
            foreach (var item in server.Keys(pattern: "*" + pattern + "*"))
                _db.KeyDelete(item);
        }
        public ServiceResult CacheList()
        {
            var cacheList = new List<SystemManagementDto>();

            foreach (var cache in _db.Multiplexer.GetServer(_host, _port).Keys())
            {
                var appCache = new SystemManagementDto
                {
                    CacheList = cache.ToString()
                };

                cacheList.Add(appCache);
            }

            return Result.ReturnAsSuccess(null, "Operation Başarılı", cacheList);
        }
    }
}
