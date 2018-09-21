// Decompiled with JetBrains decompiler
// Type: ControliPhone.Win32MemoryProtection
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;

namespace ControliPhone
{
  [Flags]
  public enum Win32MemoryProtection
  {
    PAGE_EXECUTE = 16, // 0x00000010
    PAGE_EXECUTE_READ = 32, // 0x00000020
    PAGE_EXECUTE_READWRITE = 64, // 0x00000040
    PAGE_EXECUTE_WRITECOPY = 128, // 0x00000080
    PAGE_NOACCESS = 1,
    PAGE_READONLY = 2,
    PAGE_READWRITE = 4,
    PAGE_WRITECOPY = 8,
    PAGE_GUARD = 256, // 0x00000100
    PAGE_NOCACHE = 512, // 0x00000200
    PAGE_WRITECOMBINE = 1024, // 0x00000400
  }
}
