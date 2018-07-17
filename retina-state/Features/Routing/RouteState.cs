namespace RetinaState.Features.Routing
{
    internal sealed class RouteState : State<RouteState>
    {
        public RouteState() { }

        private RouteState(RouteState state) : this()
        {
            Route = state.Route;
        }

        public string Route { get; set; }

        public override object Clone() => new RouteState(this);

        protected override void Initialize() { }
    }
}
