using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System;

namespace RetinaState.Features.JavaScriptInterop
{
    public class JsonRequestHandler
    {
        public JsonRequestHandler(
            ILogger<JsonRequestHandler> logger,
            IMediator mediator)
        {
            DebugName = GetType().FullName;

            Logger = logger;
            Logger.LogDebug($"{DebugName}: ctor");

            Mediator = mediator;
        }

        private ILogger Logger { get; }
        private IMediator Mediator { get; }
        private string DebugName { get; }

        public async void Handle(string requestAsJson)
        {
            Logger.LogDebug($"{DebugName}: Handling: {requestAsJson}");

            BaseJsonRequest baseRequest = Json.Deserialize<BaseJsonRequest>(requestAsJson);

            Logger.LogDebug($"{DebugName}: RequestType: {baseRequest.RequestType}");

            var requestType = Type.GetType(baseRequest.RequestType);

            if (requestType == null)
            {
                Logger.LogDebug($"{DebugName}: Type not found with name {baseRequest.RequestType}");

                return;
            }

            var request = (IRequest)Activator.CreateInstance(requestType, requestAsJson);

            if (request != null)
            {
                Logger.LogDebug($"{DebugName}: Request created of type {request.GetType().FullName}");

                await Mediator.Send(request);
            }
        }
    }
}
