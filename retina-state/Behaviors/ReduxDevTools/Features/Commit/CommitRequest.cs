using RetinaState.Features.JavaScriptInterop;
using MediatR;
using Microsoft.JSInterop;

namespace RetinaState.Behaviors.ReduxDevTools.Features.Commit
{
    internal sealed class CommitRequest : IRequest, IReduxRequest
    {
        public CommitRequest()
        {
            // Needed for de-serialization
        }

        public CommitRequest(string requestAsJson) : this()
        {
            JsonRequest = Json.Deserialize<JsonRequest<DispatchRequest<CommitRequest>>>(requestAsJson);

            Type = JsonRequest.Payload.Type;
        }

        public JsonRequest<DispatchRequest<CommitRequest>> JsonRequest { get; }
        public string Type { get; set; }
    }
}
