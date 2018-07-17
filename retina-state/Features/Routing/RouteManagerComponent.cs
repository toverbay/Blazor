using Microsoft.AspNetCore.Blazor.Components;

namespace RetinaState.Features.Routing
{
    /// <summary>
    /// Adds Routing into RetinaState.
    /// </summary>
    /// <remarks>
    /// Implements <see cref="IRetinaStateComponent"/> via Injection.
    /// </remarks>
    public class RouteManagerComponent : BlazorComponent
    {
        [Inject] private RouteManager RouteManager { get; set; }
    }
}
