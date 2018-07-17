using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using RetinaState.Behaviors.ReduxDevTools.Features;
using System;
using System.Threading.Tasks;

namespace RetinaState.Behaviors.ReduxDevTools
{
    internal sealed class ReduxDevToolsBehavior<TRequest, Unit> : IRequestPostProcessor<TRequest, Unit>
    {
        public ReduxDevToolsBehavior(
            ILogger<ReduxDevToolsBehavior<TRequest, Unit>> logger,
            ReduxDevToolsInterop reduxDevToolsInterop,
            IStore store)
        {
            DebugName = GetType().FullName;

            Logger = logger;
            Logger.LogDebug($"{ DebugName}: ctor");

            Store = store;
            ReduxDevToolsInterop = reduxDevToolsInterop;
        }

        private ILogger Logger { get; }
        private ReduxDevToolsInterop ReduxDevToolsInterop { get; }
        private IStore Store { get; }
        private string DebugName { get; set; }

        public async Task Process(TRequest request, Unit response)
        {
            try
            {
                Logger.LogDebug($"{DebugName}: Start Post-processing");

                if (!(request is IReduxRequest))
                {
                    await ReduxDevToolsInterop.Dispatch(request, Store.GetSerializableState());
                }

                Logger.LogDebug($"{DebugName}: End Post-processing");
            }
            catch (Exception ex)
            {
                Logger.LogError($"{DebugName}: Error: {ex.Message}");
                Logger.LogError($"{DebugName}: Inner Error: {ex.InnerException?.Message ?? "none"}");
                Logger.LogDebug($"{DebugName}: StackTrace: {ex.StackTrace}");
            }
        }
    }
}
