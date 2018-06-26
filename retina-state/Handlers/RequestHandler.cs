using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace RetinaState
{
    public abstract class RequestHandler<TRequest, TState> : IRequestHandler<TRequest, TState>
        where TRequest : IRequest<TState>
        where TState : IState
    {
        public RequestHandler(
            IStore store)
        {
            Store = store;
        }

        protected IState State => Store.GetState<TState>();
        protected IStore Store { get; set; }

        public abstract Task<TState> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
