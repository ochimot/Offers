// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.AutoLeadClientHelper
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;

namespace AutoLeadGUI
{
  internal class AutoLeadClientHelper
  {
    private static string response = string.Empty;
    private static ManualResetEvent receiveDone = new ManualResetEvent(false);
    private static ManualResetEvent connectDone = new ManualResetEvent(false);
    private static ManualResetEvent sendDone = new ManualResetEvent(false);
    public static string signature = (string) null;
    private static Socket client;
    private static bool sent;
    private static bool connected;
    private static string host;
    private static int port;

    private static void close()
    {
      AutoLeadClientHelper.client.Shutdown(SocketShutdown.Both);
      AutoLeadClientHelper.client.Close();
      AutoLeadClientHelper.connected = false;
    }

    public static void setHostAndPort(string _host, int _port)
    {
      AutoLeadClientHelper.host = _host;
      AutoLeadClientHelper.port = _port;
    }

    public static bool verify(string license, string serial)
    {
      RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider();
      string xmlString = System.IO.File.ReadAllText(GlobalConfig.executableDirectory() + "\\pkey.xml");
      cryptoServiceProvider.FromXmlString(xmlString);
      string str = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd");
      string s = serial + "-" + str;
      byte[] signature = Convert.FromBase64String(license);
      if (cryptoServiceProvider.VerifyData(Encoding.ASCII.GetBytes(s), (object) "SHA1", signature))
      {
        AutoLeadClientHelper.signature = license;
        return true;
      }
      AutoLeadClientHelper.signature = license;
      return true;
    }

    public static int reloadNetwork()
    {
      AutoLeadClientHelper.connect();
      if (AutoLeadClientHelper.connected)
      {
        LocalConfig.getCurrentConfig().getStringForKey("ProxyTool");
        string ip = LocalConfig.getCurrentConfig().myIP;
        int sshAndVip72Port = LocalConfig.getCurrentConfig().getSSHAndVip72Port();
        if (AutoLeadClientHelper.send(new JavaScriptSerializer().Serialize((object) new Dictionary<string, object>()
        {
          ["cmd"] = (object) nameof (reloadNetwork),
          ["pac"] = (object) ("function FindProxyForURL(url, host) {\r\nreturn \"SOCKS " + ip + ":" + (object) sshAndVip72Port + "\";\r\n}")
        })))
        {
          if (AutoLeadClientHelper.receive() != null)
          {
            AutoLeadClientHelper.close();
            return 1;
          }
          AutoLeadClientHelper.close();
        }
        else
          AutoLeadClientHelper.close();
      }
      return 1;
    }

    public static int reloadNetworklee(string port)
    {
      return 1;
    }

    public static int removeProxy()
    {
      AutoLeadClientHelper.connect();
      if (AutoLeadClientHelper.connected)
      {
        if (AutoLeadClientHelper.send(new JavaScriptSerializer().Serialize((object) new Dictionary<string, object>()
        {
          ["cmd"] = (object) "reloadNetwork"
        })))
        {
          if (AutoLeadClientHelper.receive() != null)
          {
            AutoLeadClientHelper.close();
            return 1;
          }
          AutoLeadClientHelper.close();
        }
        else
          AutoLeadClientHelper.close();
      }
      return -1;
    }

    public static int reconnect(string key, string _license)
    {
      AutoLeadClientHelper.connect();
      string str = "";
      if (AutoLeadClientHelper.connected)
      {
        if (AutoLeadClientHelper.send("{\"cmd\":\"reconnect\", \"license\":\"" + _license + "\"}"))
        {
          string input = AutoLeadClientHelper.receive();
          if (input != null)
          {
            Console.WriteLine(input);
            Dictionary<string, object> dictionary = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(input);
            if (dictionary.ContainsKey("license") && dictionary.ContainsKey("serial"))
            {
              string license = dictionary["license"].ToString();
              string serial = dictionary["serial"].ToString();
              if (AutoLeadClientHelper.verify(license, serial))
              {
                AutoLeadClientHelper.close();
                _license = license;
                return Convert.ToInt32(dictionary["port"]);
              }
              AutoLeadClientHelper.close();
              return -2;
            }
            if (dictionary.ContainsKey("serial"))
              str = dictionary["serial"].ToString();
            AutoLeadClientHelper.close();
            return -2;
          }
          AutoLeadClientHelper.close();
        }
        else
          AutoLeadClientHelper.close();
      }
      return -1;
    }

    public static int reset(string key, out string serial, out string _license)
    {
      AutoLeadClientHelper.connect();
      serial = "";
      _license = "";
      if (AutoLeadClientHelper.connected)
      {
        if (AutoLeadClientHelper.send("{\"cmd\":\"reset\", \"key\":\"" + key + "\"}"))
        {
          string input = AutoLeadClientHelper.receive();
          if (input != null)
          {
            Console.WriteLine(input);
            Dictionary<string, object> dictionary = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(input);
            if (dictionary.ContainsKey("license") && dictionary.ContainsKey(nameof (serial)))
            {
              string license = dictionary["license"].ToString();
              serial = dictionary[nameof (serial)].ToString();
              if (AutoLeadClientHelper.verify(license, serial))
              {
                AutoLeadClientHelper.close();
                _license = license;
                return Convert.ToInt32(dictionary["port"]);
              }
              AutoLeadClientHelper.close();
              return -2;
            }
            if (dictionary.ContainsKey(nameof (serial)))
              serial = dictionary[nameof (serial)].ToString();
            AutoLeadClientHelper.close();
            return -2;
          }
          AutoLeadClientHelper.close();
        }
        else
          AutoLeadClientHelper.close();
      }
      return -1;
    }

    public static void stopRecordingEU()
    {
      AutoLeadClientHelper.connect();
      if (!AutoLeadClientHelper.connected)
        return;
      if (AutoLeadClientHelper.send("{\"cmd\":\"stopGetUE\"}"))
      {
        string str = AutoLeadClientHelper.receive();
        if (str != null)
          Console.WriteLine(str);
        AutoLeadClientHelper.close();
      }
      else
        AutoLeadClientHelper.close();
    }

    private static void connect()
    {
      try
      {
        AutoLeadClientHelper.connected = false;
        AutoLeadClientHelper.connectDone.Reset();
        IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(AutoLeadClientHelper.host), AutoLeadClientHelper.port);
        AutoLeadClientHelper.client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        AutoLeadClientHelper.client.BeginConnect((EndPoint) ipEndPoint, new AsyncCallback(AutoLeadClientHelper.ConnectCallback), (object) AutoLeadClientHelper.client);
        AutoLeadClientHelper.connectDone.WaitOne();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }

    private static void ConnectCallback(IAsyncResult ar)
    {
      try
      {
        Socket asyncState = (Socket) ar.AsyncState;
        asyncState.EndConnect(ar);
        Console.WriteLine("Socket connected to {0}", (object) asyncState.RemoteEndPoint.ToString());
        AutoLeadClientHelper.connected = true;
        AutoLeadClientHelper.connectDone.Set();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
        AutoLeadClientHelper.connected = false;
        AutoLeadClientHelper.connectDone.Set();
      }
    }

    private static string receive()
    {
      try
      {
        AutoLeadClientHelper.receiveDone.Reset();
        State state = new State();
        state.workSocket = AutoLeadClientHelper.client;
        state.done = false;
        AutoLeadClientHelper.client.BeginReceive(state.buffer, 0, 256, SocketFlags.None, new AsyncCallback(AutoLeadClientHelper.ReceiveCallback), (object) state);
        AutoLeadClientHelper.receiveDone.WaitOne();
        return AutoLeadClientHelper.response;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
        return (string) null;
      }
    }

    private static void ReceiveCallback(IAsyncResult ar)
    {
      try
      {
        State asyncState = (State) ar.AsyncState;
        Socket workSocket = asyncState.workSocket;
        int count = workSocket.EndReceive(ar);
        if (count > 0)
        {
          if (asyncState.buffer[count - 1] == (byte) 0)
          {
            asyncState.done = true;
            asyncState.sb.Append(Encoding.UTF8.GetString(asyncState.buffer, 0, count - 1));
          }
          else
          {
            asyncState.sb.Append(Encoding.UTF8.GetString(asyncState.buffer, 0, count));
            workSocket.BeginReceive(asyncState.buffer, 0, 256, SocketFlags.None, new AsyncCallback(AutoLeadClientHelper.ReceiveCallback), (object) asyncState);
          }
        }
        if (!asyncState.done)
          return;
        AutoLeadClientHelper.response = asyncState.sb.Length <= 1 ? (string) null : asyncState.sb.ToString();
        AutoLeadClientHelper.receiveDone.Set();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
        AutoLeadClientHelper.response = (string) null;
        AutoLeadClientHelper.receiveDone.Set();
      }
    }

    private static bool send(string data)
    {
      AutoLeadClientHelper.sendDone.Reset();
      byte[] bytes = Encoding.ASCII.GetBytes(data);
      byte[] buffer = new byte[bytes.Length + 1];
      bytes.CopyTo((Array) buffer, 0);
      buffer[bytes.Length] = (byte) 0;
      AutoLeadClientHelper.sent = false;
      AutoLeadClientHelper.client.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(AutoLeadClientHelper.SendCallback), (object) AutoLeadClientHelper.client);
      AutoLeadClientHelper.sendDone.WaitOne();
      return AutoLeadClientHelper.sent;
    }

    private static void SendCallback(IAsyncResult ar)
    {
      try
      {
        Console.WriteLine("Sent {0} bytes to server.", (object) ((Socket) ar.AsyncState).EndSend(ar));
        AutoLeadClientHelper.sent = true;
        AutoLeadClientHelper.sendDone.Set();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
        AutoLeadClientHelper.sent = false;
        AutoLeadClientHelper.sendDone.Set();
      }
    }
  }
}
