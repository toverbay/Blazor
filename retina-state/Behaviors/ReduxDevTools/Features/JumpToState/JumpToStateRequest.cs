using MediatR;
using Microsoft.JSInterop;
using RetinaState.Features.JavaScriptInterop;

namespace RetinaState.Behaviors.ReduxDevTools.Features.JumpToState
{
    internal sealed class JumpToStateRequest : IRequest, IReduxRequest
    {
        public JumpToStateRequest()
        {
            // Needed for de-serialization
        }

        public JumpToStateRequest(string requestAsJson) : this()
        {
            JsonRequest = Json.Deserialize<JsonRequest<DispatchRequest<JumpToStateRequest>>>(requestAsJson);

            Type = JsonRequest.Payload.Type;
            ActionId = JsonRequest.Payload.Payload.ActionId;
        }

        public int ActionId { get; set; }
        public JsonRequest<DispatchRequest<JumpToStateRequest>> JsonRequest { get; }
        public string Type { get; set; }
    }
}
