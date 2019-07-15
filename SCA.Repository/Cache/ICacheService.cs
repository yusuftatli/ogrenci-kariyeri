using SCA.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Repository.Cache
{
    public interface ICacheService
    {
        string GetString(string key);
        T Get<T>(string key);
        List<T> GetList<T>(string key);
        void Set(string key, object data, int cacheMinute = 40000);
        void Set(string key, string data, int cacheMinute = 40000);
        bool CacheExist(string key);
        bool Remove(string key);
        void RemoveByPattern(string pattern);
        void RemoveAll();
        ServiceResult CacheList();
    }
}
