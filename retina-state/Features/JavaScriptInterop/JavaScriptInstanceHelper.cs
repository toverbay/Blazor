using System;

namespace RetinaState.Features.JavaScriptInterop
{
    /// <summary>
    /// Maintains a static reference to the JsonRequestHandler
    /// </summary>
    /// <remarks>
    /// Yes, this is a service locator anti-pattern. But it is the
    /// cleanest option for now.
    /// </remarks>
    public static class JavaScriptInstanceHelper
    {
        public static JsonRequestHandler JsonRequestHandler { get; set; }

        public static void Handle(string requestAsJson)
        {
            if (JsonRequestHandler == null)
            {
                throw new ArgumentNullException(nameof(JsonRequestHandler));
            }

            JsonRequestHandler.Handle(requestAsJson);
        }
    }
}
