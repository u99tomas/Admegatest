using Application.Wrappers;
using MudBlazor;

namespace Web.Extensions
{
    public static class SnackbarExtensions
    {
        public static void ShowMessage<T>(this ISnackbar snackbar, Result<T> result)
        {
            if (result.Succeeded)
            {
                snackbar.Add(result.Message, Severity.Success);
            }
            else
            {
                snackbar.Add(result.Message, Severity.Error);
            }
        }
    }
}
