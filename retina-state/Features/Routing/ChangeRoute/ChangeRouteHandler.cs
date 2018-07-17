using Microsoft.AspNetCore.Blazor.Services;
using System.Threading;
using System.Threading.Tasks;

namespace RetinaState.Features.Routing
{
    internal sealed class ChangeRouteHandler : RequestHandler<ChangeRouteRequest, RouteState>
    {
        public ChangeRouteHandler(
            IStore store,
            IUriHelper uriHelper) : base(store)
        {
            UriHelper = uriHelper;
        }

        private RouteState RouteState => Store.GetState<RouteState>();

        private IUriHelper UriHelper { get; }

        public override Task<RouteState> Handle(ChangeRouteRequest request, CancellationToken cancellationToken)
        {
            if (!string.Equals(RouteState.Route, request.NewRoute))
            {
                RouteState.Route = request.NewRoute;
                UriHelper.NavigateTo(request.NewRoute);
            }

            return Task.FromResult(RouteState);
        }
    }
}
