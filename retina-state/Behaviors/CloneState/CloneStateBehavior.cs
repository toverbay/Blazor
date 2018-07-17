using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RetinaState.Behaviors.State
{
    internal sealed class CloneStateBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public CloneStateBehavior(
            ILogger<CloneStateBehavior<TRequest, TResponse>> logger,
            IStore store)
        {
            DebugName = GetType().FullName;

            Logger = logger;
            Logger.LogDebug($"{DebugName}: ctor");

            Store = store;
        }

        private ILogger Logger { get; }
        private IStore Store { get; }
        private string DebugName { get; }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            Logger.LogDebug($"Pipeline Start: {request.GetType().FullName}");
            Logger.LogDebug($"{DebugName}: Start");

            var responseType = typeof(TResponse);

            IState originalState = null;

            // Constrain here if not IState then ignore.
            if (typeof(IState).IsAssignableFrom(responseType))
            {
                Logger.LogDebug($"{DebugName}: Clone State of type {responseType}");

                originalState = (IState)Store.GetState<TResponse>();

                Logger.LogDebug($"{DebugName}: originalState.Guid:{originalState.Guid}");

                var newState = (IState)originalState.Clone();

                Logger.LogDebug($"{DebugName}: newState.Guid:{newState.Guid}");

                Store.SetState(newState);
            }
            else
            {
                Logger.LogDebug($"{DebugName}: Not cloning State because {responseType.Name} is not an IState");
            }
            try
            {
                Logger.LogDebug($"{DebugName}: Call next");

                var response = await next();

                Logger.LogDebug($"{DebugName}: Start Post Processing");
                Logger.LogDebug($"{DebugName}: End");

                return response;
            }
            catch (Exception ex)
            {
                Logger.LogError($"{DebugName}: Error: {ex.Message}");
                Logger.LogError($"{DebugName}: Inner Error: {ex.InnerException?.Message ?? "none"}");
                Logger.LogDebug($"{DebugName}: Restoring State of type: {responseType}");

                // If something fails, we restore system to previous state.
                // One may consider extension point here for error handling.
                // Maybe if error occurs on one action, we want to launch another
                // action to update some error state so the user knows of the failure.
                // But, as a rule, if this is an exception it should be unexpected.
                if (originalState != null)
                {
                    Store.SetState(originalState);
                }

                throw;
            }
        }
    }
}
