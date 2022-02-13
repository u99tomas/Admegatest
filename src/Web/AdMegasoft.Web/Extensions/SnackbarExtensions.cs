﻿using Application.Wrappers;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AdMegasoft.Web.Services
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
