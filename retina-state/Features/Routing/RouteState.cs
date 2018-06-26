using RetinaState.State;

namespace RetinaState.Features.Routing
{
    internal class RouteState : State<RouteState>
    {
        public RouteState() { }

        protected RouteState(RouteState state) : this()
        {
            Route = state.Route;
        }

        public string Route { get; set; }

        public override object Clone() => new RouteState(this);

        protected override void Initialize() { }
    }
}
