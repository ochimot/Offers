// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.Properties.Resources
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace AutoLeadGUI.Properties
{
  [CompilerGenerated]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (AutoLeadGUI.Properties.Resources.resourceMan == null)
          AutoLeadGUI.Properties.Resources.resourceMan = new ResourceManager("AutoLeadGUI.Properties.Resources", typeof (AutoLeadGUI.Properties.Resources).Assembly);
        return AutoLeadGUI.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return AutoLeadGUI.Properties.Resources.resourceCulture;
      }
      set
      {
        AutoLeadGUI.Properties.Resources.resourceCulture = value;
      }
    }

    internal static Bitmap nav_left_green
    {
      get
      {
        return (Bitmap) AutoLeadGUI.Properties.Resources.ResourceManager.GetObject(nameof (nav_left_green), AutoLeadGUI.Properties.Resources.resourceCulture);
      }
    }

    internal static Bitmap nav_plain_green
    {
      get
      {
        return (Bitmap) AutoLeadGUI.Properties.Resources.ResourceManager.GetObject(nameof (nav_plain_green), AutoLeadGUI.Properties.Resources.resourceCulture);
      }
    }

    internal static Bitmap nav_plain_red
    {
      get
      {
        return (Bitmap) AutoLeadGUI.Properties.Resources.ResourceManager.GetObject(nameof (nav_plain_red), AutoLeadGUI.Properties.Resources.resourceCulture);
      }
    }

    internal static Bitmap nav_right_green
    {
      get
      {
        return (Bitmap) AutoLeadGUI.Properties.Resources.ResourceManager.GetObject(nameof (nav_right_green), AutoLeadGUI.Properties.Resources.resourceCulture);
      }
    }

    internal static Bitmap refresh
    {
      get
      {
        return (Bitmap) AutoLeadGUI.Properties.Resources.ResourceManager.GetObject(nameof (refresh), AutoLeadGUI.Properties.Resources.resourceCulture);
      }
    }
  }
}
