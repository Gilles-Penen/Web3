using Azure.Storage.Blobs;

using DnD.Spells.Storage.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DnD.Spells.Storage {
    public class AlingmentsRepository : IAlignmentsRepository {
        
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public AlingmentsRepository(string connectionString, string containerName) {
            _blobServiceClient = new BlobServiceClient(connectionString);
            _containerName = containerName;
        }

        public async Task UploadAlignmentAsync(Alingments alignment) {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

            // Blob naam op basis van de Index van de alignment
            string blobName = $"{alignment.index}.json"; // Of een andere naamstrategie
            var blobClient = containerClient.GetBlobClient(blobName);

            // Converteer Alignment object naar JSON
            var alignmentJson = JsonSerializer.Serialize(alignment);

            using (MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(alignmentJson))) {
                // Upload de JSON naar de blob
                await blobClient.UploadAsync(ms, overwrite: true);
            }

            Console.WriteLine($"Alignment {alignment.name} succesvol geüpload.");
        }

        public async Task<Alingments> GetAlignmentAsync(string index) {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            string blobName = $"{index}.json"; // Blob naam op basis van de Index

            var blobClient = containerClient.GetBlobClient(blobName);

            // Download de blob naar een MemoryStream
            var memoryStream = new MemoryStream();
            await blobClient.DownloadToAsync(memoryStream);
            memoryStream.Position = 0;

            // Converteer de JSON terug naar een Alignment object
            var alignment = await JsonSerializer.DeserializeAsync<Alingments>(memoryStream);

            return alignment;
        }

       
    }
}
