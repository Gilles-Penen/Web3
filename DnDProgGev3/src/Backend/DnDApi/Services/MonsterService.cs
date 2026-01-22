using DnD.Spells.Api.Contracts.DTO;

namespace DnDApi.Services {
    public class MonsterService : IMonsterService {


        private readonly IHttpClientFactory _httpClientFactory;

        public MonsterService(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }



        public async Task<MonsterResponseDTO?> GetMonsterAsync(string id) {
            var client = _httpClientFactory.CreateClient("DnDApi");
            try {
                Console.WriteLine($"Making request to: monsters/{id}");
                var response = await client.GetAsync($"monsters/{id}");
                if (response.IsSuccessStatusCode) {
                    Console.WriteLine($"Request successful for: monsters/{id}");
                    return await response.Content.ReadFromJsonAsync<MonsterResponseDTO>();
                } else {
                    Console.WriteLine($"Request failed for: monsters/{id} with status code: {response.StatusCode}");
                    return null;
                }
            } catch (Exception ex) {
                Console.WriteLine($"Error occurred while fetching monster: {ex.Message}");
                return null;
            }
        }


    }
}
