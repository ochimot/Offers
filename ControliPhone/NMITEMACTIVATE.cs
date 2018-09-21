// Decompiled with JetBrains decompiler
// Type: ControliPhone.NMITEMACTIVATE
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;

namespace ControliPhone
{
  internal struct NMITEMACTIVATE
  {
    public IntPtr hdr;
    public int iItem;
    public int iSubItem;
    public uint uNewState;
    public uint uOldState;
    public uint uChanged;
    public IntPtr ptAction;
    public uint lParam;
    public uint uKeyFlags;
  }
}
