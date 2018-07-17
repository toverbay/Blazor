using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace RetinaState
{
    internal sealed class Store : IStore
    {
        public Store(
            IServiceProvider serviceProvider,
            ILogger<Store> logger)
        {
            DebugName = GetType().FullName;
            Logger = logger;

            using (Logger.BeginScope(nameof(Store)))
            {
                Logger.LogInformation($"{DebugName}: ctor: {nameof(Guid)}:{Guid}");
                ServiceProvider = serviceProvider;
                States = new Dictionary<string, IState>();
            }
        }

        public Guid Guid { get; } = Guid.NewGuid();
        private ILogger Logger { get; }
        private IServiceProvider ServiceProvider { get; }
        private IDictionary<string, IState> States { get; }
        private string DebugName { get; }

        public IDictionary<string, object> GetSerializableState()
        {
            var states = (IDictionary<string, object>)new ExpandoObject();

            foreach (var kvp in States.OrderBy(x => x.Key))
            {
                states[kvp.Key] = kvp.Value;
            }

            return states;
        }

        public TState GetState<TState>()
        {
            Type stateType = typeof(TState);

            return (TState)GetState(stateType);
        }

        public object GetState(Type type)
        {
            using (Logger.BeginScope(nameof(GetState)))
            {
                var typeName = type.FullName;
                Logger.LogDebug($"{DebugName}: {nameof(this.GetState)} typeName: {typeName}");

                if (!States.TryGetValue(typeName, out var state))
                {
                    Logger.LogDebug($"{DebugName}: Creating State of type: {typeName}");

                    state = (IState)Activator.CreateInstance(type);
                    States.Add(typeName, state);
                }
                else
                {
                    Logger.LogDebug($"{DebugName}: State exists: {state.Guid}");
                }

                return state;
            }
        }

        public void LoadStatesFromJson(string jsonString)
        {
            Logger.LogDebug($"{DebugName}:{nameof(LoadStatesFromJson)}: {nameof(jsonString)}:{jsonString}");

            var newStates = Json.Deserialize<Dictionary<string, object>>(jsonString);

            foreach (var kvp in newStates)
            {
                var typeName = kvp.Key;

                Logger.LogDebug($"{DebugName}:{nameof(LoadStatesFromJson)}:typeName: {typeName}");

                var stateType = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => !a.IsDynamic)
                    .SelectMany(a => a.GetTypes())
                    .FirstOrDefault(t => string.Equals(typeName, t.FullName));

                // Get the method to hydrate the state
                var hydrateMethodInfo = stateType.GetMethod(nameof(IState<object>.RestoreFromJson));

                // Invoke the hydrate method
                var parameters = new object[] { kvp.Value.ToString() };
                var currentState = GetState(stateType);
                var newState = (IState)hydrateMethodInfo.Invoke(currentState, parameters);

                // Re-assign
                SetState(typeName, newState);
            }
        }

        public void SetState(IState state)
        {
            string typeName = state.GetType().FullName;

            SetState(typeName, state);
        }

        private void SetState(string typeName, object state)
        {
            var newState = (IState)state;

            Logger.LogDebug($"{DebugName}: {nameof(SetState)}: typeName: {typeName}: Guid:{newState.Guid}");

            States[typeName] = newState;
        }
    }
}
