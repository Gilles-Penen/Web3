using Azure;
using DnD.Spells.Storage.Contracts;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD.Spells.Storage {
    public class SpellRepository : ISpellRepository {

        private readonly SpellRepositoryOptions _options;

        public SpellRepository(SpellRepositoryOptions options) {
            _options = options;
        }


        public async Task<SpellModel?> GetSpellAsync(string id) {
            CosmosClient client = new(_options.ConnectionString);

            Database database = client.GetDatabase("SpellsDatabase");

            ContainerProperties containerProperties = new() {

                Id = "Spells",
                PartitionKeyPath = "/Level"
            };

            Container container = await database.CreateContainerIfNotExistsAsync(containerProperties);

            try {
                return await container.ReadItemAsync<SpellModel>(
                    id,
                    new PartitionKey(id)
                   );

            } catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound) {
                return null;
            }


        }
    }
}

