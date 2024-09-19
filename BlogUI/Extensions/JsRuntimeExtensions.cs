using Microsoft.JSInterop;

namespace EcorpUI.Extensions
{
    public static class JsRuntimeExtensions
    {
        public static ValueTask ToastrSuccess(this IJSRuntime jSRunttime, string message)
        {
            return jSRunttime.InvokeVoidAsync("ShowToastr", "success", message);
        }
        public static ValueTask ToastrError(this IJSRuntime jSRunttime, string message)
        {
            return jSRunttime.InvokeVoidAsync("ShowToastr", "error", message);
        }
        public static ValueTask ToastsWarning(this IJSRuntime jSRunttime, string message)
        {
            return jSRunttime.InvokeVoidAsync("ShowToastr", "warning", message);
        }
    }
}
