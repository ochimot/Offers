// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.State
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System.Net.Sockets;
using System.Text;

namespace AutoLeadGUI
{
  public class State
  {
    public Socket workSocket = (Socket) null;
    public byte[] buffer = new byte[256];
    public StringBuilder sb = new StringBuilder();
    public const int BufferSize = 256;
    public bool done;
  }
}
