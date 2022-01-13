using DiFactoryTest.Query;

using MediatR;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DiFactoryTest
{
    public class InfoReader
    {
        private readonly IMediator _mediator;
        private readonly IServiceProvider _provider;
        private readonly ILogger<InfoReader> _logger;
        public InfoReader(IMediator mediator, ILogger<InfoReader> logger, IServiceProvider provider)
        {
            _logger = logger;
            _provider = provider;
            _mediator = mediator;
            _logger.LogThreadInformation("Created");

        }
        public async Task<string> GetInfoAsync()
        {
            _logger.LogThreadInformation("GetInfoAsync");
            // Mimicing a web controller, will have it's own scope
            using var scope = _provider.CreateScope();

            // Force run on another task
            return await Task.Run(async () => await _mediator.Send(new GetInfoRequest()));
        }
    }
}