using Azure.Storage.Blobs;
using DnD.Spells.Storage.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace DnDApi.Controllers {



    [Route("api/[controller]")]
    [ApiController]
    public class BlobController : ControllerBase {

        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _containerClient;

        // Constructor: injecteert de configuratie om toegang te krijgen tot de connection string
        public BlobController(IConfiguration configuration) {
            // Haal de connection string op uit de configuratie (appsettings.json)
            var connectionString = configuration.GetConnectionString("BlobStorageConnection");

            // Controleer of de connection string beschikbaar is
            if (string.IsNullOrEmpty(connectionString)) {
                throw new ArgumentNullException("connectionString", "Blob storage connection string is not configured.");
            }

            // Maak een BlobServiceClient aan met de connection string
            _blobServiceClient = new BlobServiceClient(connectionString);

            // Haal de juiste container op. Vervang 'mycontainer' door je eigen containernaam.
            _containerClient = _blobServiceClient.GetBlobContainerClient("dndblobcontainer");
        }


        // POST: api/blob/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadBlob([FromBody] Alingments alignment) {
            if (alignment == null) {
                return BadRequest("Alignment object is null");
            }

            try {
                var blobClient = _containerClient.GetBlobClient(alignment.index);
                var jsonContent = JsonSerializer.Serialize(alignment);
                using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonContent));
                await blobClient.UploadAsync(memoryStream, overwrite: true);

                return Ok("Blob uploaded successfully");
            } catch (Exception ex) {
                Console.WriteLine($"Error while uploading alignment: {ex.Message}");
                return StatusCode(500, "Internal server error while uploading blob");
            }
        }

        // GET: api/blob/{index}
        [HttpGet("{index}")]
        public async Task<IActionResult> GetBlob(string index) {
            try {
                var blobClient = _containerClient.GetBlobClient(index);
                if (await blobClient.ExistsAsync()) {
                    var download = await blobClient.DownloadAsync();
                    using var streamReader = new StreamReader(download.Value.Content);
                    var content = await streamReader.ReadToEndAsync();
                    var alignment = JsonSerializer.Deserialize<Alingments>(content);

                    return Ok(alignment);
                } else {
                    return NotFound("Blob not found");
                }
            } catch (Exception ex) {
                Console.WriteLine($"Error while retrieving blob: {ex.Message}");
                return StatusCode(500, "Internal server error while retrieving blob");
            }
        }
    }
}
