using System.Net.Http.Json;

namespace AntiCafe.ConsoleMenu.ApiClients
{
    public abstract class BaseApiClient
    {
        protected readonly HttpClient httpClient;

        protected BaseApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        protected async Task<T> GetAsync<T>(string url)
        {
            var result = await httpClient.GetFromJsonAsync<T>(url);
            return result;
        }

        protected async Task PostAsync<T>(string url, T data)
        {
            var response = await httpClient.PostAsJsonAsync(url, data);

            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());
        }

        protected async Task PutAsync<T>(string url, T data)
        {
            var response = await httpClient.PutAsJsonAsync(url, data);

            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());
        }

        protected async Task DeleteAsync(string url)
        {
            var response = await httpClient.DeleteAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());
        }
    }
}
