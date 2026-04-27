using System.Text;

namespace TravelBot.Api.Client.Client;

public partial class TravelBotApiClient
{
    private Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, StringBuilder urlBuilder,
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, string urlBuilder,
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private Task ProcessResponseAsync(HttpClient client, HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}