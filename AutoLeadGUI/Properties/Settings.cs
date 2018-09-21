// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.Properties.Settings
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AutoLeadGUI.Properties
{
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
  [CompilerGenerated]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        Settings defaultInstance = Settings.defaultInstance;
        return defaultInstance;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("False")]
    public bool Reconnect
    {
      get
      {
        return (bool) this[nameof (Reconnect)];
      }
      set
      {
        this[nameof (Reconnect)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("192.168.0.105")]
    [UserScopedSetting]
    public string DeviceIP
    {
      get
      {
        return (string) this[nameof (DeviceIP)];
      }
      set
      {
        this[nameof (DeviceIP)] = (object) value;
      }
    }
  }
}
