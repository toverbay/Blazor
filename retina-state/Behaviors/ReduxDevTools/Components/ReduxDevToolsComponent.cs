using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace RetinaState.Behaviors.ReduxDevTools
{
    /// <summary>
    /// Add this component to Client App to use ReduxDevTools.
    /// </summary>
    public class ReduxDevToolsComponent : BlazorComponent
    {
        [Inject]
        private ReduxDevToolsInterop ReduxDevToolsInterop { get; set; }

        protected override async Task OnInitAsync()
        {
            await base.OnInitAsync();

            ReduxDevToolsInterop.IsEnabled = await JSRuntime.Current.InvokeAsync<bool>("RetinaState.ReduxDevTools.create");

            // We could send in the Store.GetSerializedState, but it will be empty.
            if (ReduxDevToolsInterop.IsEnabled)
            {
                await ReduxDevToolsInterop.DispatchInit("");
            }
        }
    }
}
