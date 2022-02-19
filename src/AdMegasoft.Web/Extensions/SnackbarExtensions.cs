using Application.Wrappers;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AdMegasoft.Web.Extensions
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

        public static bool CheckIfNull<T>(this ISnackbar snackbar, T? possibleNullValue)
        {
            if(possibleNullValue == null)
            {
                snackbar.Add("Error de referencia nula", Severity.Error);
            }

            return possibleNullValue == null;
        }
    }
}
