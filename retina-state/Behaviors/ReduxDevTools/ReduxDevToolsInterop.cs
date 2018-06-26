using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace RetinaState.Behaviors.ReduxDevTools
{
    internal class ReduxDevToolsInterop
    {
        private string _debugName;
        private const string jsFunctionName = "ReduxDevToolsDispatch";

        public ReduxDevToolsInterop(
            ILogger<ReduxDevToolsInterop> logger)
        {
            Logger = logger;
        }

        public bool IsEnabled { get; set; }
        private ILogger Logger { get; }

        public void Dispatch<TRequest>(in TRequest request, in object state)
        {
            if (IsEnabled)
            {
                Logger.LogDebug($"{DebugName}: {nameof(Dispatch)}");
                Logger.LogDebug($"{DebugName}: request type: {request.GetType().FullName}");

                var reduxAction = new ReduxAction(request);

                RegisteredFunction.Invoke<object>(jsFunctionName, reduxAction, state);
            }
        }

        public async Task Dispatch<TRequest>(TRequest request, object state)
        {
            if (IsEnabled)
            {
                Logger.LogDebug($"{DebugName}: {nameof(Dispatch)}");
                Logger.LogDebug($"{DebugName}: request type: {request.GetType().FullName}");

                var reduxAction = new ReduxAction(request);

                await JSRuntime.Current.InvokeAsync<object>(jsFunctionName, reduxAction, state);
            }
        }

        public void DispatchInit(in object state)
        {
            if (IsEnabled)
            {
                RegisteredFunction.Invoke<object>(jsFunctionName, "init", state);
            }
        }

        public async Task DispatchInit(object state)
        {
            if (IsEnabled)
            {
                await JSRuntime.Current.InvokeAsync<object>(jsFunctionName, "init", state);
            }
        }

        private string DebugName
        {
            get
            {
                if (string.IsNullOrEmpty(_debugName))
                {
                    _debugName = GetType().Name;
                }

                return _debugName;
            }
        }
    }
}