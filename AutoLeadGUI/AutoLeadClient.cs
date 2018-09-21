// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.AutoLeadClient
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace AutoLeadGUI
{
  internal class AutoLeadClient
  {
    private static string response = string.Empty;
    private static ManualResetEvent receiveDone = new ManualResetEvent(false);
    private static ManualResetEvent connectDone = new ManualResetEvent(false);
    private static ManualResetEvent sendDone = new ManualResetEvent(false);
    private static Socket client;
    private static bool sent;
    public static bool connected;
    private static string _host;
    private static string _key;
    private static string _serial;
    private static string _license;

    public static void close()
    {
      try
      {
        AutoLeadClient.client.Shutdown(SocketShutdown.Both);
        AutoLeadClient.client.Close();
        AutoLeadClient.connected = false;
      }
      catch (Exception ex)
      {
      }
    }

    public static bool reconnect()
    {
      int num = 0;
      try
      {
        AutoLeadClient.connected = false;
        num = 0;
        AutoLeadClientHelper.setHostAndPort(AutoLeadClient._host, 6800);
        int port = AutoLeadClientHelper.reconnect(AutoLeadClient._key, AutoLeadClient._license);
        if (port > 0)
        {
          AutoLeadClient.connectDone.Reset();
          IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(AutoLeadClient._host), port);
          AutoLeadClient.client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
          AutoLeadClient.client.BeginConnect((EndPoint) ipEndPoint, new AsyncCallback(AutoLeadClient.ConnectCallback), (object) AutoLeadClient.client);
          AutoLeadClient.connectDone.WaitOne();
          return true;
        }
        num = port;
        return false;
      }
      catch (Exception ex)
      {
        num = -1;
        Console.WriteLine(ex.ToString());
        return false;
      }
    }

    public static bool connect(string host, string key, out int error, out string serial)
    {
      try
      {
        AutoLeadClient.connected = false;
        error = 0;
        AutoLeadClientHelper.setHostAndPort(host, 6800);
        int port = AutoLeadClientHelper.reset(key, out serial, out AutoLeadClient._license);
        if (port > 0)
        {
          AutoLeadClient.connectDone.Reset();
          IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(host), port);
          AutoLeadClient.client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
          AutoLeadClient.client.BeginConnect((EndPoint) ipEndPoint, new AsyncCallback(AutoLeadClient.ConnectCallback), (object) AutoLeadClient.client);
          AutoLeadClient.connectDone.WaitOne();
          AutoLeadClient._host = host;
          AutoLeadClient._key = key;
          AutoLeadClient._serial = serial;
          return true;
        }
        error = port;
        return false;
      }
      catch (Exception ex)
      {
        error = -1;
        serial = "";
        Console.WriteLine(ex.ToString());
        return false;
      }
    }

    private static void ConnectCallback(IAsyncResult ar)
    {
      try
      {
        Socket asyncState = (Socket) ar.AsyncState;
        asyncState.EndConnect(ar);
        Console.WriteLine("Socket connected to {0}", (object) asyncState.RemoteEndPoint.ToString());
        AutoLeadClient.connected = true;
        AutoLeadClient.connectDone.Set();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
        AutoLeadClient.connected = false;
        AutoLeadClient.connectDone.Set();
      }
    }

    public static string receive()
    {
      try
      {
        AutoLeadClient.receiveDone.Reset();
        State state = new State();
        state.workSocket = AutoLeadClient.client;
        state.done = false;
        AutoLeadClient.client.BeginReceive(state.buffer, 0, 256, SocketFlags.None, new AsyncCallback(AutoLeadClient.ReceiveCallback), (object) state);
        AutoLeadClient.client.ReceiveTimeout = 5000;
        if (!AutoLeadClient.receiveDone.WaitOne(60000))
          frmMain.sttconnect = false;
        return AutoLeadClient.response;
      }
      catch (Exception ex)
      {
        frmMain.sttconnect = false;
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
            workSocket.BeginReceive(asyncState.buffer, 0, 256, SocketFlags.None, new AsyncCallback(AutoLeadClient.ReceiveCallback), (object) asyncState);
          }
        }
        if (!asyncState.done)
          return;
        AutoLeadClient.response = asyncState.sb.Length <= 1 ? (string) null : asyncState.sb.ToString();
        AutoLeadClient.receiveDone.Set();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
        AutoLeadClient.response = (string) null;
        AutoLeadClient.receiveDone.Set();
      }
    }

    public static bool send(string data)
    {
      try
      {
        AutoLeadClient.sendDone.Reset();
        byte[] bytes = Encoding.ASCII.GetBytes(data);
        byte[] buffer = new byte[bytes.Length + 1];
        bytes.CopyTo((Array) buffer, 0);
        buffer[bytes.Length] = (byte) 0;
        AutoLeadClient.sent = false;
        AutoLeadClient.client.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(AutoLeadClient.SendCallback), (object) AutoLeadClient.client);
        AutoLeadClient.sendDone.WaitOne();
      }
      catch
      {
        frmMain.sttconnect = false;
      }
      return AutoLeadClient.sent;
    }

    private static void SendCallback(IAsyncResult ar)
    {
      try
      {
        Console.WriteLine("Sent {0} bytes to server.", (object) ((Socket) ar.AsyncState).EndSend(ar));
        AutoLeadClient.sent = true;
        AutoLeadClient.sendDone.Set();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
        AutoLeadClient.sent = false;
        AutoLeadClient.sendDone.Set();
      }
    }
  }
}
