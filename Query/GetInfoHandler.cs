using MediatR;

using Microsoft.Extensions.Logging;

namespace DiFactoryTest.Query
{
    public class GetInfoHandler : IRequestHandler<GetInfoRequest, string>
    {
        private readonly IMyService _service;
        private readonly ILogger<GetInfoHandler> _logger;
        public GetInfoHandler(IMyService service, ILogger<GetInfoHandler> logger)
        {
            _service = service;
            _logger = logger;
            _logger.LogThreadInformation("Created");
        }
        public Task<string> Handle(GetInfoRequest request, CancellationToken cancellationToken)
        {
            _logger.LogThreadInformation("Handle request");
            return Task.FromResult(_service.GetInfo());
        }
    }
}