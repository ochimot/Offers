// Decompiled with JetBrains decompiler
// Type: ControliPhone.Win32AllocationTypes
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;

namespace ControliPhone
{
  [Flags]
  public enum Win32AllocationTypes
  {
    MEM_COMMIT = 4096, // 0x00001000
    MEM_RESERVE = 8192, // 0x00002000
    MEM_DECOMMIT = 16384, // 0x00004000
    MEM_RELEASE = 32768, // 0x00008000
    MEM_RESET = 524288, // 0x00080000
    MEM_PHYSICAL = 4194304, // 0x00400000
    MEM_TOP_DOWN = 1048576, // 0x00100000
    WriteWatch = 2097152, // 0x00200000
    MEM_LARGE_PAGES = 536870912, // 0x20000000
  }
}
