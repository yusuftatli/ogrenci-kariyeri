using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Repository.Cache
{
    public class CacheHelper
    {
        protected virtual byte[] Serialize(object item)
        {
            var jsonString = JsonConvert.SerializeObject(item);
            return Encoding.UTF8.GetBytes(jsonString);
        }
        protected virtual T Deserialize<T>(string[] serializedObject)
        {
            if (serializedObject == null)
                return default(T);

            return JsonConvert.DeserializeObject<T>(serializedObject[0]);
        }

        protected virtual List<T> DeserializeList<T>(string[] serializedObject)
        {
            if (serializedObject == null)
                return default(List<T>);

            return JsonConvert.DeserializeObject<List<T>>(serializedObject[0]);
        }
    }
}
