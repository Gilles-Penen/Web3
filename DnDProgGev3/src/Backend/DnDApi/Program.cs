using System.Drawing.Drawing2D;
using System.Threading.RateLimiting;
using Azure.Storage.Blobs;
using DnD.Spells.Storage;
using DnDApi.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
namespace  DnDApi
{
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Lezen voor BLOB storage (Binary large object)
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // Voeg Blob Storage Dependency Injection toe
            builder.Services.AddSingleton<BlobServiceClient>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("BlobStorageConnection");
                return new BlobServiceClient(connectionString);
            });

            // Voeg Health Checks toe
            builder.Services.AddHealthChecks()
                .AddCheck("API", new UriHealthCheck(new Uri("https://www.dnd5eapi.co/api/health")), tags: new[] { "api" });

            // RateLimiter configuratie
            builder.Services.AddRateLimiter(_ =>
                _.AddFixedWindowLimiter(policyName: "DnDApiFixed", options => {
                    options.PermitLimit = 1000;
                    options.Window = TimeSpan.FromSeconds(10);
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = 50;
                }));

            // CORS configuratie
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins("http://localhost:5173", "http://localhost:3000") // React-applicatie en dan ook docker API
                          .AllowAnyMethod()                    // Toestaan van GET, POST, etc.
                          .AllowAnyHeader()                    // Toestaan van headers zoals JSON
                          .AllowCredentials();                 // Nodig als je cookies gebruikt
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // COSMOS DB DEPENDENCY INJECTION
            builder.Services.AddSingleton<CosmosService>();

            builder.Services.AddHttpClient("DnDApi", client =>
            {
                client.BaseAddress = new Uri("http://localhost:3000/api/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            builder.Services.AddScoped<ISPellsService, SpellsService>();
            builder.Services.AddScoped<IMonsterService, MonsterService>();

            // Voeg HSTS configuratie toe via ConfigureServices
            builder.Services.Configure<HstsOptions>(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // **Gebruik de UseHsts middleware zonder configuratie**
            if (app.Environment.IsProduction()) {
                app.UseHsts(); // Geen configuratie in deze regel nodig
            }

            // Forceer HTTPS-omleiding
            app.UseHttpsRedirection();

            // Gebruik CORS policy
            app.UseCors("AllowReactApp");

            // Voeg de Health Checks endpoints toe
            app.MapHealthChecks("/health");

            app.MapControllers();

            // RateLimiter zodat iemand niet 10000000 requests kan sturen per second
            app.UseRateLimiter();

            // Headers toevoegen voor extra beveiliging
            app.Use(async (context, next) =>
            {
                // X-Frame-Options header toevoegen (ervoor zorgen dat de site niet in een iframe kan worden geladen)
                context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");

                // X-XSS-Protection header toevoegen (Activeert een browserfunctie om te voorkomen
                // dat scripts worden uitgevoerd bij Cross-Site Scripting (XSS)-aanvallen)
                context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");

                // X-Content-Type-Options header toevoegen (voorkomt dat de browser MIME-sniffing uitvoert)
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");

                // Referrer-Policy header toevoegen (bepaalt welke informatie wordt doorgestuurd naar de server)
                context.Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");

                // Content-Security-Policy header toevoegen (bepaalt welke bronnen mogen worden geladen)
                context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");

                // Permissions-Policy header toevoegen (bepaalt welke functies mogen worden gebruikt)
                context.Response.Headers.Add("Permissions-Policy", "geolocation=(), camera=(), microphone=()");
                await next();
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.Urls.Add("http://localhost:7166"); // Zorg dat dit klopt

            app.Run();
        }
    }
    
}