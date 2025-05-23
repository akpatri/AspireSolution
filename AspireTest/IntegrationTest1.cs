namespace AspireTest.Tests
{
    public class IntegrationTest1
    {
        // Instructions:
        // 1. Add a project reference to the target AppHost project, e.g.:
        //
        //    <ItemGroup>
        //        <ProjectReference Include="../MyAspireApp.AppHost/MyAspireApp.AppHost.csproj" />
        //    </ItemGroup>
        //
        // 2. Uncomment the following example test and update 'Projects.MyAspireApp_AppHost' to match your AppHost project:
        //
        [Fact]
        public async Task GetWebResourceRootReturnsOkStatusCode()
        {
            Console.WriteLine("Creating appHost...");
            var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.AppHost>();

            appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
            {
                clientBuilder.AddStandardResilienceHandler();
            });

            Console.WriteLine("Building app...");
            await using var app = await appHost.BuildAsync();

            Console.WriteLine("Resolving ResourceNotificationService...");
            var resourceNotificationService = app.Services.GetRequiredService<ResourceNotificationService>();

            Console.WriteLine("Starting app...");
            await app.StartAsync();

            Console.WriteLine("Waiting for resource 'projA'...");
            await resourceNotificationService.WaitForResourceAsync("projA", KnownResourceStates.Running)
                .WaitAsync(TimeSpan.FromSeconds(30));

            Console.WriteLine("Creating HTTP client...");
            var httpClient = app.CreateHttpClient("projA");

            Console.WriteLine("Sending request to '/api'...");
            var response = await httpClient.GetAsync("/api/endpoint2");

            Console.WriteLine($"Status code: {response.StatusCode}");
            Assert.Equal((int)HttpStatusCode.OK, (int)response.StatusCode);
        }

    }
}