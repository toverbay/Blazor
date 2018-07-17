using MediatR;

namespace RetinaState.Handlers
{
    /// <summary>
    /// This class can be used if you don't use the
    /// <see cref="RetinaState.Behaviors.State.CloneStateBehavior">Clone</see> behavior
    /// (Not sure why you wouldn't just use the behavior).
    /// </summary>
    internal abstract class CloneStateRequestHandler<TRequest, TState> : RequestHandler<TRequest, TState>
        where TRequest : IRequest<TState>
        where TState : IState
    {
        protected CloneStateRequestHandler(
            IStore store) : base(store)
        {
            IState state = Store.GetState<TState>();
            Store = store;

            var newState = (TState)state.Clone();

            Store.SetState(newState);
        }
    }
}
