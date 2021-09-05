using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MHC.Domain.Requests;
using MHC.Domain.Responses;
using MHC.Internals;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MHC
{
    public class Client
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
        public Client(HttpClient httpClient, string proxyUrl, string torrentUrl)
        {
            _proxyUrl = proxyUrl;
            _torrentUrl = torrentUrl;
            MhcHttpClient = httpClient;
        }

        private static string _proxyUrl = "http://proxy.net-main.metahashnetwork.com:9999/";
        private static string _torrentUrl = "http://tor.net-main.metahashnetwork.com:5795/";

        /// <summary>
        /// Sends a transaction over the MetaHash network with your own signature, use <see cref="Transfer"/> for automatic signature calculation
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
        /// Checks if the transaction exists on the TraceChain and the status is "ok".
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public async Task<bool> VerifyTransaction(string transactionId)
        {
            var txResponse = await GetTransaction(new TransactionRequest
            {
                Params = new TransactionRequestParams
                {
                    Hash = transactionId
                }
            });

            if (txResponse.Result?.Transaction.Status == "ok")
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Verify the transaction if it's part of a certain wallet address (based on the last 100 transactions)
        /// </summary>
        /// <param name="walletAddress">The wallet address to verify</param>
        /// <param name="transactionId">The transaction id to check</param>
        /// <returns></returns>
        public async Task<bool> VerifyTransactionOnWallet(string walletAddress, string transactionId)
        {
            var isTransactionRegistered = await VerifyTransaction(transactionId);

            if (isTransactionRegistered)
            {
                var transactions = await FetchHistory(new FetchHistoryRequest
                {
                    Params = new FetchHistoryRequestParams
                    {
                        Address = walletAddress,
                        BeginTx = 0,
                        CountTx = 100
                    }
                });

                var transactionReceived = transactions.Result.FirstOrDefault(x => x.Transaction == transactionId);
                if (transactionReceived != null)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Transfers MHC over the MetaHash network and manages the signature for you
        /// </summary>
        /// <returns></returns>
        public async Task<TransferResponse> Transfer(TransferRequest request)
        {
            var publicKey = Crypto.GetPublicKey(request.PrivateKey);

            var address = Crypto.GetAddress(publicKey);
            var balance = await FetchBalance(new FetchBalanceRequest
            { Parameters = new FetchBalanceRequestParams { Address = address } });

            var nonce = ++balance.Result.CountSpent;
            var message = Serialization.Encode(request.ToAddress, request.MhcHashAmount, request.Fee, nonce, request.Data);
            var signature = Crypto.Sign(message, request.PrivateKey);

            var sendTransactionRequest = new SendTransactionRequest
            {
                Parameters = new SendTransactionRequestParams
                {
                    Data = request.DataHex,
                    PublicKey = publicKey,
                    Fee = request.Fee.ToString(),
                    Nonce = nonce.ToString(),
                    To = request.ToAddress,
                    Sign = signature,
                    Value = request.MhcHashAmount.ToString(CultureInfo.InvariantCulture)
                }
            };

            var stringPayload = SerializeObject(sendTransactionRequest);
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var response = await MhcHttpClient.PostAsync(_proxyUrl, httpContent);
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync();
            var transferResponse = JsonConvert.DeserializeObject<TransferResponse>(responseJson);

            return transferResponse;
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
        /// Retrieves balance for a wallet
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<FetchBalanceResponse> FetchBalance(FetchBalanceRequest request)
        {
            var stringPayload = SerializeObject(request);
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var response = await MhcHttpClient.PostAsync(_torrentUrl, httpContent);
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync();
            var fetchHistoryResponse = JsonConvert.DeserializeObject<FetchBalanceResponse>(responseJson);
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