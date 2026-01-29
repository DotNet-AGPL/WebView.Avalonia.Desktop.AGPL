using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;
using WebView.Avalonia.Core.Models;

namespace WebView.Avalonia.Core;

public abstract class WebView : Control, IDisposable
{
    public abstract event EventHandler<NavigationStartingEventArgs>? NavigationStarting;

    public abstract void Dispose();
}
