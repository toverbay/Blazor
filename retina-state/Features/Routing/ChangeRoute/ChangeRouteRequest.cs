using MediatR;

namespace RetinaState.Features.Routing
{
    internal sealed class ChangeRouteRequest : IRequest<RouteState>
    {
        public string NewRoute { get; set; }
    }
}
