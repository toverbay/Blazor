using Microsoft.AspNetCore.Blazor.Components;

namespace RetinaState.Features.JavaScriptInterop
{
    /// <summary>
    /// Just sets the instance of JsonRequestHandler.
    /// </summary>
    public class JavaScriptInteropComponent : BlazorComponent
    {
        [Inject]
        private JsonRequestHandler JsonRequestHandler { get; set; }

        protected override void OnInit()
        {
            JavaScriptInstanceHelper.JsonRequestHandler = JsonRequestHandler;
        }
    }
}
