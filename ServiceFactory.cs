using DiFactoryTest.Impl;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DiFactoryTest
{
    public class ServiceFactory
    {
        private readonly IServiceProvider _provider;
        private readonly IOptions<ModeOptions> _options;
        public ServiceFactory (IServiceProvider provider, IOptions<ModeOptions> options)
        {
            _options = options;
            _provider = provider;

        }
        public IMyService Create()
        {
            return _options.Value.IsPrimary
                ? _provider.GetRequiredService<MyPrimaryService>()
                : _provider.GetRequiredService<MySecondaryService>();
        }
    }
}