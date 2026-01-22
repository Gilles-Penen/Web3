using NBomber.Contracts; // To use ScenarioProps.
using NBomber.CSharp; // To use Scenario, Simulation, NBomberRunner.
using NBomber.Http; // To use HttpMetricsPlugin.
using NBomber.Http.CSharp; // To use Http.
using NBomber.Plugins.Network.Ping; // To use PingPlugin.

namespace NBomberApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Maak een HttpClient
            using HttpClient client = new(); // Gebruik een HttpClient.

            LoadSimulation[] loads = {
                // Ramp up to 50 RPS during one minute.
                Simulation.RampingInject(rate: 50,
                                          interval: TimeSpan.FromSeconds(1),
                                          during: TimeSpan.FromMinutes(1)),
                // Maintain 50 RPS for another minute.
                Simulation.Inject(rate: 50,
                                  interval: TimeSpan.FromSeconds(1),
                                  during: TimeSpan.FromMinutes(1)),
                // Ramp down to 0 RPS during one minute.
                Simulation.RampingInject(rate: 0,
                                          interval: TimeSpan.FromSeconds(1),
                                          during: TimeSpan.FromMinutes(1))
            };

            // Maak een scenario
            ScenarioProps scenario = Scenario.Create(
                name: "http_scenario",
                run: async context =>
                {
                    HttpRequestMessage request = Http.CreateRequest(
                    "GET", "https://DNDAPI-GillesPenen.azurewebsites.net/api/spells") // Verander dit naar jouw endpoint!
                     .WithHeader("Accept", "application/json");

                    Response<HttpResponseMessage> response = await Http.Send(client, request);
                    return response;
                })
                .WithoutWarmUp()
                .WithLoadSimulations(loads);

            // Voer NBomber uit
            NBomberRunner
                .RegisterScenarios(scenario)
                .WithWorkerPlugins(
                    new PingPlugin(PingPluginConfig.CreateDefault("nbomber.com")),
                    new HttpMetricsPlugin(new[] { HttpVersion.Version1 })
                )
                .Run();
        }
    }
}