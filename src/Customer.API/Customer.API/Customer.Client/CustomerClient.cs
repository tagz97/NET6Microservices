using Customer.Client.Constants;
using Newtonsoft.Json;

namespace Customer.Client
{
    /// <summary>
    /// Consumable client for the Customer Service
    /// </summary>
    public class CustomerClient : ICustomerClient
    {
        private readonly HttpClient _httpClient;
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerClient"/> class with an injected http client
        /// </summary>
        /// <param name="httpClient">The http client</param>
        public CustomerClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Returns the customer document
        /// </summary>
        /// <typeparam name="T">Class to be replaced</typeparam>
        /// <param name="id">Id of the Customer to retrieve</param>
        /// <returns>Customer document</returns>
        public async Task<T> GetCustomerById<T>(string id)
        {
            _httpClient.DefaultRequestHeaders.Add(nameof(id), id);
            var apiresponse = await _httpClient.GetAsync(ClientConstants.GetCustomerByIdentifier);
            var stream = await apiresponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<T>(stream);

            return response;
        }
    }
}