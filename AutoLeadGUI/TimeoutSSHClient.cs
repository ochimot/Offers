// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.TimeoutSSHClient
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using Renci.SshNet;
using Renci.SshNet.Common;
using System;
using System.Threading;

namespace AutoLeadGUI
{
  internal class TimeoutSSHClient
  {
    public string fingerPrint = (string) null;
    private SshClient client = (SshClient) null;
    private Thread thread = (Thread) null;
    private bool result = false;
    private ManualResetEvent checkDone = (ManualResetEvent) null;
    private string errorMsg = (string) null;
    private int timeout;
    private string host;
    private string username;
    private string password;
    private int port;

    public TimeoutSSHClient(int _timeout, string _host, string _username, string _password, int _port)
    {
      this.timeout = _timeout;
      this.username = _username;
      this.password = _password;
      this.host = _host;
      this.port = _port;
    }

    public bool isLive()
    {
      try
      {
        this.client = new SshClient(this.host, this.port, this.username, this.password);
        this.client.ConnectionInfo.Timeout = TimeSpan.FromSeconds((double) this.timeout);
        this.client.HostKeyReceived += (EventHandler<HostKeyEventArgs>) ((sender, e) =>
        {
          e.CanTrust = true;
          this.fingerPrint = BitConverter.ToString(e.FingerPrint).Replace("-", ":").ToLower();
        });
        this.client.Connect();
        if (this.client.IsConnected)
        {
          this.client.Disconnect();
          return true;
        }
        this.errorMsg = "*** [" + (object) this + "]: Dead: " + this.host;
        return false;
      }
      catch
      {
        this.errorMsg = "*** [" + (object) this + "]: Dead: " + this.host;
        return false;
      }
    }
  }
}
