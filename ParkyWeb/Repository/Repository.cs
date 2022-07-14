using Amqp.Types;
using Newtonsoft.Json;
using ParkyWeb.Repository.IRepository;
using System.Text;

namespace ParkyWeb.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IHttpClientFactory _clientFactory;
        public Repository(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }




        public async Task<bool> CreateAsync(string url, T objectToCreate)
        {
            if (objectToCreate == null)
                return false;

            // Prepare the request
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            requestMessage.Content = new StringContent(
                JsonConvert.SerializeObject(objectToCreate),
                Encoding.UTF8, "application/json");
            // Prepare the client 
            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(requestMessage);
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
                return true;
            else
                return false;
        }

        public async Task<bool> DeleteAsync(string url, int id)
        {
            var requet = new HttpRequestMessage(HttpMethod.Delete, url + id);
            var client = _clientFactory.CreateClient();
            HttpResponseMessage responseMessage = await client.SendAsync(requet);
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
                return true;
            else return false;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string url)
        {
            var requet = new HttpRequestMessage(HttpMethod.Get, url);
            var client = _clientFactory.CreateClient();
            HttpResponseMessage responseMessage = await client.SendAsync(requet);
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
            }
            return null;


        }

        public async Task<T> GetAsync(string url, int id)
        {
            var requet = new HttpRequestMessage(HttpMethod.Get, url + id);
            var client = _clientFactory.CreateClient();
            HttpResponseMessage responseMessage = await client.SendAsync(requet);
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            return null;

        }

        public async Task<bool> UpdateAsync(string url, T objectToUpdate)
        {
            if (objectToUpdate == null)
                return false;

            // Prepare the request
            var requestMessage = new HttpRequestMessage(HttpMethod.Patch, url);
            requestMessage.Content = new StringContent(
                JsonConvert.SerializeObject(objectToUpdate),
                Encoding.UTF8, "application/json");
            // Prepare the client 
            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(requestMessage);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return true;
            else
                return false;
        }
    }
}
