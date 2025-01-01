using Modules.DownloadService.Api.Models;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models;
using Swashbuckle.AspNetCore.Filters;

namespace CoreMonolith.Api.Swagger.Examples.SabNzbd;

public class CreateDownloadClientResponseExamples : IExamplesProvider<DownloadClientCreateRequest>
{
    public class SabNzbdClientSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string ApiKey { get; set; }
        public string Category { get; set; }
        public Priority Priority { get; set; }
        public PostProcessingOptions PostPorcesssing { get; set; }
    }

    public DownloadClientCreateRequest GetExamples()
    {
        return new DownloadClientCreateRequest(
                DownloadClientType.SabNzbd,
                "SabNzbd",
                true,
                new SabNzbdClientSettings
                {
                    Host = "http://localhost",
                    Port = 8085,
                    ApiKey = Guid.CreateVersion7().ToString(),
                    Category = "*",
                    Priority = Priority.Normal,
                    PostPorcesssing = PostProcessingOptions.Default
                });
    }
}