namespace RetinaState.Behaviors.ReduxDevTools
{
    /// <summary>
    /// Implementation is required to allow DevTools to ReRender components
    /// when using Time Travel
    /// </summary>
    public interface IDevToolsComponent
    {
        ComponentRegistry ComponentRegistry { get; set; }

        void ReRender();
    }
}
