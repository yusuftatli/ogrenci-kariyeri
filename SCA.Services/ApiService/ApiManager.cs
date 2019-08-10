using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class ApiManager : IApiManager
    {
        public ApiManager()
        {

        }

        public async Task<string> Get(string baseUrl)
        {
            string data = "";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {
                        using (HttpContent content = res.Content)
                        {
                            data = await content.ReadAsStringAsync();

                        }
                    }
                }
            }
            catch (Exception exception)
            {
            }
            return data;
        }
    }
}
