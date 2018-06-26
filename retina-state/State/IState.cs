using System;

namespace RetinaState
{
    public interface IState : ICloneable
    {
        Guid Guid { get; }
    }

    public interface IState<TState> : IState
    {
        TState State { get; }
        TState RestoreFromJson(string jsonString);
    }
}
