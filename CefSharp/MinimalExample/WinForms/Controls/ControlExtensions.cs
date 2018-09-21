// Decompiled with JetBrains decompiler
// Type: CefSharp.MinimalExample.WinForms.Controls.ControlExtensions
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;
using System.Windows.Forms;

namespace CefSharp.MinimalExample.WinForms.Controls
{
  public static class ControlExtensions
  {
    public static void InvokeOnUiThreadIfRequired(this Control control, Action action)
    {
      if (control.InvokeRequired)
        control.BeginInvoke((Delegate) action);
      else
        action();
    }
  }
}
