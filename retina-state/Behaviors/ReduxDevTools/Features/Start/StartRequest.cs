using MediatR;
using Microsoft.JSInterop;
using RetinaState.Features.JavaScriptInterop;

namespace RetinaState.Behaviors.ReduxDevTools.Features.Start
{
    /// <summary>
    /// Request received from Redux Dev Tools when one presses the Start Button.
    /// </summary>
    internal sealed class StartRequest : IRequest, IReduxRequest
    {
        public StartRequest()
        {
            // Needed for de-serialization
        }

        public StartRequest(string requestAsJson) : this()
        {
            JsonRequest<StartRequest> jsonRequest =
                Json.Deserialize<JsonRequest<StartRequest>>(requestAsJson);

            Type = jsonRequest.Payload.Type;
            Source = jsonRequest.Payload.Source;
        }

        public string Source { get; set; }
        public string Type { get; set; }
    }
}
