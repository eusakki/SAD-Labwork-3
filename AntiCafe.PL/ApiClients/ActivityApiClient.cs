using AntiCafe.Contracts.DTOs;

namespace AntiCafe.ConsoleMenu.ApiClients
{
    public class ActivityApiClient : BaseApiClient
    {
        public ActivityApiClient(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<List<ActivityDto>> GetAllActivitiesAsync()
        {
            return await GetAsync<List<ActivityDto>>("activities");
        }
    }
}
