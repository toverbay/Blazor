using System.Collections.Generic;

namespace RetinaState.Behaviors.ReduxDevTools
{
    public class ComponentRegistry
    {
        internal List<IDevToolsComponent> DevToolsComponents { get; } = new List<IDevToolsComponent>();

        public void ReRenderAll() => DevToolsComponents.ForEach(c => c.ReRender());
    }
}
