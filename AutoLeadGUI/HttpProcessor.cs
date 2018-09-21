// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.HttpProcessor
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace AutoLeadGUI
{
  public class HttpProcessor
  {
    private static int MAX_POST_SIZE = 10485760;
    public Hashtable httpHeaders = new Hashtable();
    public TcpClient socket;
    public HttpServer srv;
    private Stream inputStream;
    public StreamWriter outputStream;
    public string http_method;
    public string http_url;
    public string http_protocol_versionstring;
    private const int BUF_SIZE = 4096;

    public HttpProcessor(TcpClient s, HttpServer srv)
    {
      this.socket = s;
      this.srv = srv;
    }

    private string streamReadLine(Stream inputStream)
    {
      string str = "";
      while (true)
      {
        int num;
        do
        {
          num = inputStream.ReadByte();
          if (num == 10)
            goto label_6;
        }
        while (num == 13);
        if (num == -1)
          Thread.Sleep(1);
        else
          str += Convert.ToChar(num).ToString();
      }
label_6:
      return str;
    }

    public void process()
    {
      this.inputStream = (Stream) new BufferedStream((Stream) this.socket.GetStream());
      this.outputStream = new StreamWriter((Stream) new BufferedStream((Stream) this.socket.GetStream()));
      try
      {
        this.parseRequest();
        this.readHeaders();
        if (this.http_method.Equals("GET"))
          this.handleGETRequest();
        else if (this.http_method.Equals("POST"))
          this.handlePOSTRequest();
      }
      catch (Exception ex)
      {
        Console.WriteLine("Exception: " + ex.ToString());
        this.writeFailure();
      }
      this.outputStream.Flush();
      this.inputStream = (Stream) null;
      this.outputStream = (StreamWriter) null;
      this.socket.Close();
    }

    public void parseRequest()
    {
      string str = this.streamReadLine(this.inputStream);
      string[] strArray = str.Split(' ');
      if (strArray.Length != 3)
        throw new Exception("invalid http request line");
      this.http_method = strArray[0].ToUpper();
      this.http_url = strArray[1];
      this.http_protocol_versionstring = strArray[2];
      Console.WriteLine("starting: " + str);
    }

    public void readHeaders()
    {
      Console.WriteLine("readHeaders()");
      string str1;
      while ((str1 = this.streamReadLine(this.inputStream)) != null)
      {
        if (str1.Equals(""))
        {
          Console.WriteLine("got headers");
          break;
        }
        int length = str1.IndexOf(':');
        if (length == -1)
          throw new Exception("invalid http header line: " + str1);
        string str2 = str1.Substring(0, length);
        int startIndex = length + 1;
        while (startIndex < str1.Length && str1[startIndex] == ' ')
          ++startIndex;
        string str3 = str1.Substring(startIndex, str1.Length - startIndex);
        Console.WriteLine("header: {0}:{1}", (object) str2, (object) str3);
        this.httpHeaders[(object) str2] = (object) str3;
      }
    }

    public void handleGETRequest()
    {
      this.srv.handleGETRequest(this);
    }

    public void handlePOSTRequest()
    {
      Console.WriteLine("get post data start");
      MemoryStream memoryStream = new MemoryStream();
      if (this.httpHeaders.ContainsKey((object) "Content-Length"))
      {
        int int32 = Convert.ToInt32(this.httpHeaders[(object) "Content-Length"]);
        if (int32 > HttpProcessor.MAX_POST_SIZE)
          throw new Exception(string.Format("POST Content-Length({0}) too big for this simple server", (object) int32));
        byte[] buffer = new byte[4096];
        int val2 = int32;
        while (val2 > 0)
        {
          Console.WriteLine("starting Read, to_read={0}", (object) val2);
          int count = this.inputStream.Read(buffer, 0, Math.Min(4096, val2));
          Console.WriteLine("read finished, numread={0}", (object) count);
          if (count == 0)
          {
            if (val2 != 0)
              throw new Exception("client disconnected during post");
            break;
          }
          val2 -= count;
          memoryStream.Write(buffer, 0, count);
        }
        memoryStream.Seek(0L, SeekOrigin.Begin);
      }
      Console.WriteLine("get post data end");
      this.srv.handlePOSTRequest(this, new StreamReader((Stream) memoryStream));
    }

    public void writeSuccess(string content_type = "text/html")
    {
      this.outputStream.WriteLine("HTTP/1.0 200 OK");
      this.outputStream.WriteLine("Content-Type: " + content_type);
      this.outputStream.WriteLine("Connection: close");
      this.outputStream.WriteLine("");
    }

    public void writeFailure()
    {
      this.outputStream.WriteLine("HTTP/1.0 404 File not found");
      this.outputStream.WriteLine("Connection: close");
      this.outputStream.WriteLine("");
    }
  }
}
