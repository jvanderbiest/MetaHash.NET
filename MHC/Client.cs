using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MHC.Domain.Requests;
using MHC.Domain.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MHC
{
    public interface IClient
    {
        /// <summary>
        /// Sends a transaction over the MetaHash network
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> SendTransaction(SendTransactionRequest request);

        /// <summary>
        /// Gets the history for a walletAddress
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<FetchHistoryResponse> FetchHistory(FetchHistoryRequest request);

        /// <summary>
        /// Retrieves a single transaction
        /// </summary>
        /// <returns></returns>
        Task<TransactionResponse> GetTransaction(TransactionRequest request);
    }

    public class Client : IClient
    {
        private HttpClient MhcHttpClient { get; }

        /// <summary>
        /// Initializes the MetaHash client with a new HttpClient
        /// </summary>
        public Client() : this(new HttpClient()) { }

        /// <summary>
        /// Creates a MetaHash client with a custom client
        /// </summary>
        /// <param name="mhcHttpClient"></param>
        public Client(HttpClient mhcHttpClient) : this(mhcHttpClient ?? new HttpClient(), _proxyUrl, _torrentUrl) { }

        /// <summary>
        /// Creates a MetaHash client with a custom endpoint url
        /// </summary>
        /// <param name="proxyUrl">MetaHash proxy endpoint url</param>
        /// <param name="torrentUrl">MetaHash torrent endpoint url</param>
        public Client(string proxyUrl, string torrentUrl) : this(new HttpClient(), proxyUrl, torrentUrl) { }

        /// <summary>
        /// Creates a MetaHash client with a custom client and endpoint url
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="proxyUrl">MetaHash proxy endpoint url</param>
        /// <param name="torrentUrl">MetaHash torrent endpoint url</param>
        public Client(HttpClient httpClient, string proxyUrl, string torrentUrl) {
            _proxyUrl = proxyUrl;
            _torrentUrl = torrentUrl;
            MhcHttpClient = httpClient;
        }

        private static string _proxyUrl = "http://proxy.net-main.metahashnetwork.com:9999/";
        private static string _torrentUrl = "http://tor.net-main.metahashnetwork.com:5795/";

        /// <summary>
        /// Sends a transaction over the MetaHash network
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendTransaction(SendTransactionRequest request)
        {
            var stringPayload = SerializeObject(request);
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var response = await MhcHttpClient.PostAsync(_proxyUrl, httpContent);
            return response;
        }

        /// <summary>
        /// Gets the history for a walletAddress
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<FetchHistoryResponse> FetchHistory(FetchHistoryRequest request)
        {
            var stringPayload = SerializeObject(request);
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var response = await MhcHttpClient.PostAsync(_torrentUrl, httpContent);
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync();
            var fetchHistoryResponse = JsonConvert.DeserializeObject<FetchHistoryResponse>(responseJson);
            return fetchHistoryResponse;
        }

        /// <summary>
        /// Retrieves a single transaction
        /// </summary>
        /// <returns></returns>
        public async Task<TransactionResponse> GetTransaction(TransactionRequest request)
        {
            var stringPayload = SerializeObject(request);
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var response = await MhcHttpClient.PostAsync(_torrentUrl, httpContent);
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync();
            var transactionResponse = JsonConvert.DeserializeObject<TransactionResponse>(responseJson);
            return transactionResponse;
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