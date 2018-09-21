// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.IndexItem
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System.Collections.Generic;

namespace AutoLeadGUI
{
  internal class IndexItem
  {
    public int tableIndex;
    public Dictionary<string, object> info;
    public string hostKeyFp;

    public IndexItem(int index, Dictionary<string, object> _info)
    {
      this.tableIndex = index;
      this.info = _info;
    }
  }
}
