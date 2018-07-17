using MediatR;
using Microsoft.Extensions.Logging;

namespace RetinaState.Behaviors.ReduxDevTools.Features.JumpToState
{
    internal sealed class JumpToStateHandler : RequestHandler<JumpToStateRequest>
    {
        public JumpToStateHandler(
            ILogger<JumpToStateHandler> logger,
            IStore store,
            ReduxDevToolsInterop reduxDevToolsInterop,
            ComponentRegistry componentRegistry)
        {
            DebugName = GetType().FullName;

            Logger = logger;
            Logger.LogDebug($"{DebugName}: ctor");

            Store = store;
            ReduxDevToolsInterop = reduxDevToolsInterop;
            ComponentRegistry = componentRegistry;
        }

        private ComponentRegistry ComponentRegistry { get; }
        private ILogger Logger { get; }
        private ReduxDevToolsInterop ReduxDevToolsInterop { get; }
        private IStore Store { get; }
        private string DebugName { get; }

        protected override void Handle(JumpToStateRequest request)
        {
            Logger.LogDebug($"{DebugName}: Handling request: {request.GetType()}");
            Logger.LogDebug($"{DebugName}: Loading state: {request.JsonRequest.Payload.State}");

            Store.LoadStatesFromJson(request.JsonRequest.Payload.State);

            ComponentRegistry.ReRenderAll();
        }
    }
}
