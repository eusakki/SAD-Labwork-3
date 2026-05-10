using AntiCafe.Contracts.DTOs;

namespace AntiCafe.ConsoleMenu.ApiClients
{
    public class RoomApiClient : BaseApiClient
    {
        public RoomApiClient(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<List<RoomDto>> GetAllRoomsAsync()
        {
            return await GetAsync<List<RoomDto>>("rooms");
        }
    }
}
