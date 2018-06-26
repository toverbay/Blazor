using MediatR;

namespace RetinaState.Features.Routing
{
    internal class ChangeRouteRequest : IRequest<RouteState>
    {
        public string NewRoute { get; set; }
    }
}
