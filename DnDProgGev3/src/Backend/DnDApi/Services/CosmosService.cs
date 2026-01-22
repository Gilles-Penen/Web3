using Microsoft.Azure.Cosmos;
using System.Net;

public class CosmosService
{
    private readonly CosmosClient _cosmosClient;
    private readonly Container _container;

    public CosmosService(IConfiguration configuration)
    {
        var endpoint = configuration["CosmosDb:AccountEndpoint"];
        var key = configuration["CosmosDb:AccountKey"];
        var databaseName = configuration["CosmosDb:DatabaseName"];
        var containerName = configuration["CosmosDb:ContainerName"];

        _cosmosClient = new CosmosClient(endpoint, key);
        _container = _cosmosClient.GetContainer(databaseName, containerName);
    }

    public async Task AddItemAsync<T>(T item, string id) {
        try {
            
            await _container.CreateItemAsync(item, new PartitionKey(id));
        } catch (CosmosException ex) {
            Console.WriteLine($"Fout bij het toevoegen van item: {ex.Message}");
            throw;
        }
    }

    public async Task<T> GetItemAsync<T>(string id) {
        try {
            // Lees het item met id en partition key
            var response = await _container.ReadItemAsync<T>(
                id,              // De id van het item
                new PartitionKey(id) // De partition key moet overeenkomen
            );

            // Geef het item terug als het gevonden is
            return response.Resource;
        } catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound) {
            // Return default als het item niet gevonden is
            Console.WriteLine($"Item with id {id} not found.");
            return default; // Geen item gevonden
        } catch (CosmosException ex) {
            // Log andere fouten
            Console.WriteLine($"Cosmos DB error: {ex.Message}");
            throw;
        } catch (Exception ex) {
            // Algemene fout afhandeling
            Console.WriteLine($"Error occurred while fetching item: {ex.Message}");
            throw;
        }
    }

    internal async Task<IEnumerable<object>> GetSpellsByIdAsync(string id) {
        throw new NotImplementedException();
    }
}