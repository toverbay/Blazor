using MediatR;

namespace RetinaState
{
    /// <summary>
    /// Minimum implementation needed for RetinaState to function.
    /// </summary>
    /// <example>
    /// public class RetinaStateComponent : BlazorComponent, IRetinaStateComponent
    /// {
    ///     [Inject] public IMediator Mediator { get; set; }
    ///     [Inject] public IStore Store { get; set; }
    /// }
    /// </example>
    public interface IRetinaStateComponent
    {
        IMediator Mediator { get; set; }
        IStore Store { get; set; }
    }
}
