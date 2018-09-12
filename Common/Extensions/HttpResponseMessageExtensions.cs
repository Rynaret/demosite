using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> ToModel<T>(this Task<HttpResponseMessage> httpResponseMessageTask)
        {
            var content = await (await httpResponseMessageTask).Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
