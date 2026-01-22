using DnD.Spells.Storage;
using DnDApi.Controllers;
using DnDApi.Extensions;
using static DnDApi.Services.SpellsService;
using System.Net;

namespace DnDApi.Services {
    public class SpellsService : ISPellsService {


        private readonly IHttpClientFactory _httpClientFactory;
        private readonly CosmosService _cosmosService;

        public SpellsService(IHttpClientFactory httpClientFactory, CosmosService cosmosService) {
            _httpClientFactory = httpClientFactory;
            _cosmosService = cosmosService;
        }


        public async Task<SpellResponseDTO?> GetSpellAsync(string id) {

            try {
                var spellcosmos = await _cosmosService.GetItemAsync<SpellResponseDTO>(id);

                if (spellcosmos != null) {
                    return spellcosmos;
                } else {
                    var client = _httpClientFactory.CreateClient("DnDApi");

                    try {

                        var response = await client.GetAsync($"spells/{id}");

                        if (response.IsSuccessStatusCode) {

                            var spell = await response.Content.ReadFromJsonAsync<SpellResponseDTO>();

                            // Sla het spell op in Cosmos DB
                            if (spell != null) {
                                await _cosmosService.AddItemAsync(spell, id);
                            }

                            return spell;
                        } else {

                            return null;
                        }
                    } catch (Exception ex) {
                        Console.WriteLine($"Error occurred while fetching spell: {ex.Message}");
                        return null;
                    }

                }
            } catch (Exception ex) {
                Console.WriteLine($"Failed to fetch spell {id} from Cosmos DB: {ex.Message}");
                return null;
            }



        }
       
        }
    }
    



