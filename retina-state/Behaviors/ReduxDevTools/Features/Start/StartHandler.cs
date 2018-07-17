using MediatR;
using Microsoft.Extensions.Logging;

namespace RetinaState.Behaviors.ReduxDevTools.Features.Start
{
    internal sealed class StartHandler : RequestHandler<StartRequest>
    {
        public StartHandler(
            ILogger<StartHandler> logger,
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
        private ReduxDevToolsInterop ReduxDevToolsInterop { get; }
        private IStore Store { get; }
        private string DebugName { get; }

        protected override void Handle(StartRequest request)
        {
            // Currently does nothing
        }
    }
}
