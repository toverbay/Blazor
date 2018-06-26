using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Services;

namespace RetinaState.Features.Routing
{
    internal class InitializeRouteHandler : RequestHandler<InitializeRouteRequest, RouteState>
    {
        public InitializeRouteHandler(
            IStore store,
            IUriHelper uriHelper) : base(store)
        {
            UriHelper = uriHelper;
        }

        private RouteState RouteState => Store.GetState<RouteState>();

        private IUriHelper UriHelper { get; }

        public override Task<RouteState> Handle(InitializeRouteRequest request, CancellationToken cancellationToken)
        {
            RouteState.Route = UriHelper.GetAbsoluteUri().ToString();

            return Task.FromResult(RouteState);
        }
    }
}
