using Microsoft.JSInterop;
using System;

namespace RetinaState.State
{
    public abstract class State<TState> : IState<TState>
    {
        public State()
        {
            Initialize();
        }

        public Guid Guid { get; } = Guid.NewGuid();

        TState IState<TState>.State { get; }

        public abstract object Clone();

        public TState RestoreFromJson(string jsonString)
        {
            TState result = Json.Deserialize<TState>(jsonString);

            return result;
        }

        protected abstract void Initialize();
    }
}
