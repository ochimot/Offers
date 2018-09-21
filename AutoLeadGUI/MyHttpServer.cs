// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.MyHttpServer
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;
using System.Drawing;
using System.IO;

namespace AutoLeadGUI
{
  public class MyHttpServer : HttpServer
  {
    public MyHttpServer(int port)
      : base(port)
    {
    }

    public override void handleGETRequest(HttpProcessor p)
    {
      Console.WriteLine("request: {0}", (object) p.http_url);
      p.writeSuccess("text/html");
      string stringForKey = LocalConfig.getCurrentConfig().getStringForKey("ProxyTool");
      string ip = LocalConfig.getCurrentConfig().myIP;
      if (p.httpHeaders[(object) "User-Agent"].ToString().Contains("networkd"))
        this.frmMainObj.lbProxyStatus.Invoke(new Action(delegate
        {
          this.frmMainObj.lbProxyStatus.Text = "Proxy configured";
          this.frmMainObj.lbProxyStatus.ForeColor = Color.Green;
        }));
      int sshAndVip72Port = LocalConfig.getCurrentConfig().getSSHAndVip72Port();
      if (stringForKey.Equals("SSH"))
        p.outputStream.WriteLine("function FindProxyForURL(url, host) {\r\nreturn \"SOCKS " + ip + ":" + (object) sshAndVip72Port + "\";\r\n}");
      else
        p.outputStream.WriteLine("function FindProxyForURL(url, host) {\r\nreturn \"SOCKS " + ip + ":" + (object) sshAndVip72Port + "\";\r\n}");
    }

    public override void handlePOSTRequest(HttpProcessor p, StreamReader inputData)
    {
      Console.WriteLine("POST request: {0}", (object) p.http_url);
      inputData.ReadToEnd();
      p.writeSuccess("text/html");
      p.outputStream.WriteLine("");
    }
  }
}
