using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
public class UriHealthCheck : IHealthCheck {
    private readonly Uri _uri;

    // Pas de constructor aan zodat je de URI direct kunt meegeven
    public UriHealthCheck(Uri uri) {
        _uri = uri;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default) {
        try {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(_uri, cancellationToken);

            if (response.IsSuccessStatusCode) {
                return HealthCheckResult.Healthy($"API {_uri} is reachable");
            }

            return HealthCheckResult.Unhealthy($"API {_uri} returned status code {response.StatusCode}");
        } catch (Exception ex) {
            return HealthCheckResult.Unhealthy($"API {_uri} check failed: {ex.Message}");
        }
    }
}