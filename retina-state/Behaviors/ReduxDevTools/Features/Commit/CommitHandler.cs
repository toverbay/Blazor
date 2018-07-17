using MediatR;
using Microsoft.Extensions.Logging;

namespace RetinaState.Behaviors.ReduxDevTools.Features.Commit
{
    internal sealed class CommitHandler : RequestHandler<CommitRequest>
    {
        public CommitHandler(
            ILogger<CommitHandler> logger,
            IStore store,
            ReduxDevToolsInterop reduxDevToolsInterop)
        {
            DebugName = GetType().FullName;

            Logger = logger;
            Logger.LogDebug($"{DebugName}: ctor");

            Store = store;
            ReduxDevToolsInterop = reduxDevToolsInterop;
        }

        private ILogger Logger { get; }
        private IStore Store { get; }
        private ReduxDevToolsInterop ReduxDevToolsInterop { get; }
        private string DebugName { get; }

        protected override void Handle(CommitRequest request)
        {
            Logger.LogDebug($"{DebugName}: Handling request: {request.GetType()}");

            object state = Store.GetSerializableState();

            ReduxDevToolsInterop.DispatchInit(in state);
        }
    }
}
