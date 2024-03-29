﻿using Application.Wrappers;
using MudBlazor;

namespace Web.Infrastructure.Extensions
{
    public static class SnackbarExtensions
    {
        public static void ShowMessage<T>(this ISnackbar snackbar, Result<T> result)
        {
            if (result.Succeeded && result.HasMessage)
            {
                snackbar.Add(result.Message, Severity.Success);
            }
            else if(result.HasMessage)
            {
                snackbar.Add(result.Message, Severity.Error);
            }
        }
    }
}
