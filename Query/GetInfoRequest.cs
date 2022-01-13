using MediatR;

namespace DiFactoryTest.Query
{
    public record GetInfoRequest : IRequest<string> { }
}