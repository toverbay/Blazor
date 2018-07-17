using System;

namespace RetinaState.Behaviors.ReduxDevTools
{
    internal sealed class ReduxAction
    {
        public ReduxAction(object request)
        {
            Payload = request ?? throw new ArgumentNullException(nameof(request));
            Type = request.GetType().FullName;
        }

        public object Payload { get; set; }
        public string Type { get; set; }
    }
}
