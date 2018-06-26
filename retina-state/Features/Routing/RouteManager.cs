using MediatR;
using Microsoft.AspNetCore.Blazor.Services;

namespace RetinaState.Features.Routing
{
    internal class RouteManager
    {
        public RouteManager(
            IUriHelper uriHelper,
            IMediator mediator,
            IStore store)
        {
            UriHelper = uriHelper;
            Mediator = mediator;
            Store = store;

            UriHelper.OnLocationChanged += OnLocationChanged;
            Mediator.Send(new InitializeRouteRequest());
        }

        private IMediator Mediator { get; }
        private RouteState RouteState => Store.GetState<RouteState>();
        private IStore Store { get; }
        private IUriHelper UriHelper { get; }

        private void OnLocationChanged(object sender, string e)
        {
            string absoluteUri = UriHelper.ToAbsoluteUri(e).ToString();

            if (!string.Equals(RouteState.Route, absoluteUri))
            {
                Mediator.Send(new ChangeRouteRequest { NewRoute = absoluteUri });
            }
        }
    }
}
