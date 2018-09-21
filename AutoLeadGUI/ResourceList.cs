// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.ResourceList
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace AutoLeadGUI
{
  [CompilerGenerated]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  public class ResourceList
  {
    private static ResourceManager resourceMan;


    private static CultureInfo resourceCulture;

    internal ResourceList()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static ResourceManager ResourceManager
    {
      get
      {
        if (ResourceList.resourceMan == null)
          ResourceList.resourceMan = new ResourceManager("AutoLeadGUI.ResourceList", typeof (ResourceList).Assembly);
        return ResourceList.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static CultureInfo Culture
    {
      get
      {
        return ResourceList.resourceCulture;
      }
      set
      {
        ResourceList.resourceCulture = value;
      }
    }

    public static string Codes
    {
      get
      {
        return ResourceList.ResourceManager.GetString(nameof (Codes), ResourceList.resourceCulture);
      }
    }

    public static string Countries
    {
      get
      {
        return ResourceList.ResourceManager.GetString(nameof (Countries), ResourceList.resourceCulture);
      }
    }

    public static string IOS
    {
      get
      {
        return ResourceList.ResourceManager.GetString(nameof (IOS), ResourceList.resourceCulture);
      }
    }

    public static string IOSLanguageCode
    {
      get
      {
        return ResourceList.ResourceManager.GetString(nameof (IOSLanguageCode), ResourceList.resourceCulture);
      }
    }

    public static string LocaleLanguages
    {
      get
      {
        return ResourceList.ResourceManager.GetString(nameof (LocaleLanguages), ResourceList.resourceCulture);
      }
    }

    public static string modelbylee
    {
      get
      {
        return ResourceList.ResourceManager.GetString(nameof (modelbylee), ResourceList.resourceCulture);
      }
    }

    public static string Modembylee
    {
      get
      {
        return ResourceList.ResourceManager.GetString(nameof (Modembylee), ResourceList.resourceCulture);
      }
    }

    public static string Operators
    {
      get
      {
        return ResourceList.ResourceManager.GetString(nameof (Operators), ResourceList.resourceCulture);
      }
    }

    public static string Regions
    {
      get
      {
        return ResourceList.ResourceManager.GetString(nameof (Regions), ResourceList.resourceCulture);
      }
    }

    public static string Timezones
    {
      get
      {
        return ResourceList.ResourceManager.GetString(nameof (Timezones), ResourceList.resourceCulture);
      }
    }

    public static string UA
    {
      get
      {
        return ResourceList.ResourceManager.GetString(nameof (UA), ResourceList.resourceCulture);
      }
    }

    public static string Versions
    {
      get
      {
        return ResourceList.ResourceManager.GetString(nameof (Versions), ResourceList.resourceCulture);
      }
    }
  }
}
