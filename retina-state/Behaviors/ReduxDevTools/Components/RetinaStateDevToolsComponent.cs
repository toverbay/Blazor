using System;
using Microsoft.AspNetCore.Blazor.Components;

namespace RetinaState.Behaviors.ReduxDevTools
{
    /// <summary>
    /// Base implementation of <see cref="IDevToolsComponent"/>. Required for
    /// TimeTravel in ReduxDevTools.
    /// </summary>
    /// <remarks>
    /// See Peter Morris Issue on Blazor
    /// https://github.com/aspnet/Blazor/issues/704
    /// <para>
    /// If one implements their own base class with these interfaces
    /// They won't be forced to use this one.
    /// C# 8 with default implementations of interfaces will be quite tempting
    /// </para>
    /// </remarks>
    public class RetinaStateDevToolsComponent : RetinaStateComponent, IDevToolsComponent, IDisposable
    {
        [Inject]
        public ComponentRegistry ComponentRegistry { get; set; }

        public void Dispose() => ComponentRegistry.DevToolsComponents.Remove(this);

        /// <summary>
        /// Exposes StateHasChanged.
        /// </summary>
        public void ReRender() => StateHasChanged();

        protected override void OnInit()
        {
            base.OnInit();

            ComponentRegistry.DevToolsComponents.Add(this);
        }
    }
}
