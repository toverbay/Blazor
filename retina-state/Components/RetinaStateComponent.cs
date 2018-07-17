using MediatR;
using Microsoft.AspNetCore.Blazor.Components;

namespace RetinaState
{
    /// <summary>
    /// A simple optional base class that just injects Mediator and Store.
    /// </summary>
    /// <remarks>
    /// Implements IRetinaStateComponent by using [Inject].
    /// </remarks>
    public class RetinaStateComponent : BlazorComponent, IRetinaStateComponent
    {
        [Inject]
        public IMediator Mediator { get; set; }

        [Inject]
        public IStore Store { get; set; }
    }
}
