using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MHC.Domain.Requests;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MHC
{
    public class MetaHashClient
    {
        private HttpClient MhcHttpClient { get; }

        public MetaHashClient()
        {
            MhcHttpClient = new HttpClient();
        }

        public MetaHashClient(HttpClient mhcHttpClient)
        {
            MhcHttpClient = mhcHttpClient ?? new HttpClient();
        }

        const int ProxyNodePort = 9999;
        const int TorNodePort = 5795;

        private const string ProxyUrl = "https://proxy.metahash.dev";

        public async Task<HttpResponseMessage> SendTransaction(SendTransactionRequest request)
        {
            var stringPayload = SerializeObject(request);
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var response = await MhcHttpClient.PostAsync(ProxyUrl, httpContent);
            return response;
        }

        private string SerializeObject(object obj)
        {
            var json = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            });

            return json;
        }
    }
}