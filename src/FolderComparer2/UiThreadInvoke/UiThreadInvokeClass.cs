// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UiThreadInvokeClass.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The UI thread invoke class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FolderComparer2.UiThreadInvoke;

/// <summary>
/// The UI thread invoke class.
/// </summary>
public static class UiThreadInvokeClass
{
    /// <summary>
    /// Invokes an UI action from a background thread.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <param name="code">The code.</param>
    public static void UiThreadInvoke(this Control control, Action code)
    {
        if (control.InvokeRequired)
        {
            control.Invoke(code);
            return;
        }

        code.Invoke();
    }
}
