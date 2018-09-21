// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.HttpServer
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace AutoLeadGUI
{
  public abstract class HttpServer
  {
    private bool is_active = true;
    private bool stopped = false;
    public int port;
    private TcpListener listener;
    public frmMain frmMainObj;

    public HttpServer(int port)
    {
      this.port = port;
    }

    public void listen()
    {
      this.is_active = true;
      while (true)
      {
        try
        {
          this.listener = new TcpListener(this.port);
          this.listener.Start();
          break;
        }
        catch
        {
          ++this.port;
        }
      }
      while (this.is_active)
      {
        try
        {
          new Thread(new ThreadStart(new HttpProcessor(this.listener.AcceptTcpClient(), this).process)).Start();
          Thread.Sleep(1);
        }
        catch (Exception ex)
        {
          Console.WriteLine((object) ex);
        }
      }
      this.stopped = true;
    }

    public void stop()
    {
      this.is_active = false;
      this.listener.Stop();
    }

    public abstract void handleGETRequest(HttpProcessor p);

    public abstract void handlePOSTRequest(HttpProcessor p, StreamReader inputData);
  }
}
