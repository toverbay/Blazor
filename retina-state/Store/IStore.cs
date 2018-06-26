using System;
using System.Collections.Generic;

namespace RetinaState
{
    public interface IStore
    {
        Guid Guid { get; }
        IDictionary<string, object> GetSerializableState();
        TState GetState<TState>();
        object GetState(Type type);
        void LoadStatesFromJson(string jsonString);
        void SetState(IState state);
    }
}
