using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace RetinaState
{
    internal static class RegisteredFunction
    {
        public static T Invoke<T>(string identifier, params object[] args)
        {
            return Task.Run(() => JSRuntime.Current.InvokeAsync<T>(identifier, args)).Result;
        }
    }
}
