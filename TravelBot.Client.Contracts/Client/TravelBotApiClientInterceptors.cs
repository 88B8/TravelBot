using System.Text;

namespace TravelBot.Client.Contracts.Client
{
    public partial class TravelBotApiClient
    {
        private Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, StringBuilder urlBuilder, CancellationToken cancellationToken)
            => Task.CompletedTask;

        private Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, string urlBuilder, CancellationToken cancellationToken)
            => Task.CompletedTask;

        private Task ProcessResponseAsync(HttpClient client, HttpResponseMessage response, CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}