// Decompiled with JetBrains decompiler
// Type: ControliPhone.Win32ProcessAccessType
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;

namespace ControliPhone
{
  [Flags]
  public enum Win32ProcessAccessType
  {
    AllAccess = 1050235, // 0x0010067B
    CreateThread = 2,
    DuplicateHandle = 64, // 0x00000040
    QueryInformation = 1024, // 0x00000400
    SetInformation = 512, // 0x00000200
    Terminate = 1,
    VMOperation = 8,
    VMRead = 16, // 0x00000010
    VMWrite = 32, // 0x00000020
    Synchronize = 1048576, // 0x00100000
  }
}
