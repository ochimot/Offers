// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.LocalConfig
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;

namespace AutoLeadGUI
{
  internal class LocalConfig
  {
    public static string currentDeviceSNLog = (string) null;
    private static ManualResetEvent _checkLiveDone = new ManualResetEvent(false);
    private static ManualResetEvent _checkLiveThreadDone = new ManualResetEvent(false);
    private static LocalConfig currentConfig = (LocalConfig) null;
    private static string fileLogs = (string) null;
    public string currentDeviceSN = (string) null;
    public string currentModel = (string) null;
    public string currentDeviceVersion = (string) null;
    public string myIP = (string) null;
    public string mySockPort = (string) null;
    private int mySSHAndVip72Port = 0;
    private int myVip72Port = 0;
    private JavaScriptSerializer serialzer = new JavaScriptSerializer();
    private ArrayList _allOffers = (ArrayList) null;
    private ArrayList _allSSHs = (ArrayList) null;
    private ArrayList _allRSSs = (ArrayList) null;
    private ArrayList _allVip72s = (ArrayList) null;
    private Dictionary<string, SynchronizedCollection<Dictionary<string, object>>> _allLiveSSHs = new Dictionary<string, SynchronizedCollection<Dictionary<string, object>>>();
    private Thread _checkLiveThread = (Thread) null;
    private string _countryToCheck = (string) null;
    private ArrayList _allLogs = (ArrayList) null;


    public int getSSHAndVip72Port()
    {
      if ((uint) this.mySSHAndVip72Port > 0U)
        return this.mySSHAndVip72Port;
      for (int port = 5010 + new Random().Next() % 2990; port < 8000; ++port)
      {
        if (GlobalConfig.isPortAvailable(port))
        {
          this.mySSHAndVip72Port = port;
          return port;
        }
      }
      return 8989;
    }



    public int changeSSHPort()
    {
      for (int port = 5010 + new Random().Next() % 2990; port < 8000; ++port)
      {
        if (GlobalConfig.isPortAvailable(port))
        {
          this.mySSHAndVip72Port = port;
          return port;
        }
      }
      return 8989;
    }

    public int changeVip72Port()
    {
      if (this.myVip72Port > 0)
      {
        this.mySSHAndVip72Port = this.myVip72Port;
        return this.myVip72Port;
      }
      for (int port = 5010 + new Random().Next() % 2990; port < 8000; ++port)
      {
        if (GlobalConfig.isPortAvailable(port))
        {
          this.mySSHAndVip72Port = port;
          this.myVip72Port = port;
          return port;
        }
      }
      return 8989;
    }

    public LocalConfig(string deviceSN, string model, string version)
    {
      LocalConfig.currentDeviceSNLog = deviceSN;
      this.currentDeviceSN = deviceSN;
      this.currentModel = model;
      this.currentDeviceVersion = version;
    }

    public string configDirectory()
    {
      return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + this.currentDeviceSN;
    }

    public string configDirectoryAli()
    {
      return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }

    public object getObjectForKey(string key)
    {
      string path = this.configDirectory() + "\\settings.list";
      string input = File.Exists(path) ? File.ReadAllText(path) : (string) null;
      if (input == null)
        return (object) null;
      Dictionary<string, object> dictionary = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(input);
      if (dictionary.ContainsKey(key))
        return dictionary[key];
      return (object) null;
    }

    public List<string> allScripts()
    {
      List<string> stringList = new List<string>();
      string path = LocalConfig.getCurrentConfig().configDirectory() + "\\Scripts";
      if (Directory.Exists(path))
      {
        string[] files = Directory.GetFiles(path);
        if (files != null)
        {
          Array.Sort<string>(files, (IComparer<string>) StringComparer.InvariantCultureIgnoreCase);
          for (int index = 0; index < files.Length; ++index)
          {
            string fileName = Path.GetFileName(files[index]);
            stringList.Add(fileName);
          }
        }
      }
      return stringList;
    }

    public bool getBooleanForKey(string key)
    {
      object objectForKey = this.getObjectForKey(key);
      if (objectForKey == null)
        return false;
      return Convert.ToBoolean(objectForKey);
    }

    public int getInt32ForKey(string key)
    {
      object objectForKey = this.getObjectForKey(key);
      if (objectForKey == null)
        return 0;
      return Convert.ToInt32(objectForKey);
    }

    public string getStringForKey(string key)
    {
      object objectForKey = this.getObjectForKey(key);
      if (objectForKey == null)
        return "";
      return objectForKey.ToString();
    }

    public void setObjectForKey(object obj, string key)
    {
      string path = this.configDirectory() + "\\settings.list";
      string input = File.Exists(path) ? File.ReadAllText(path) : (string) null;
      if (input != null)
      {
        Dictionary<string, object> dictionary = this.serialzer.Deserialize<Dictionary<string, object>>(input);
        dictionary[key] = obj;
        string contents = this.serialzer.Serialize((object) dictionary);
        File.WriteAllText(path, contents);
      }
      else
      {
        string contents = this.serialzer.Serialize((object) new Dictionary<string, object>()
        {
          [key] = obj
        });
        File.WriteAllText(path, contents);
      }
    }

    public ArrayList allOffers()
    {
      if (this._allOffers == null)
      {
        string path = this.configDirectory() + "\\offers.list";
        string input = File.Exists(path) ? File.ReadAllText(path) : (string) null;
        if (input != null)
        {
          ArrayList arrayList = this.serialzer.Deserialize<ArrayList>(input);
          this._allOffers = arrayList;
          if (arrayList == null)
            this._allOffers = new ArrayList();
        }
        else
          this._allOffers = new ArrayList();
      }
      return this._allOffers;
    }

    public ArrayList allRSSs()
    {
      this.serialzer.MaxJsonLength = 2147483644;
      if (this._allRSSs == null)
      {
        string path = this.configDirectory() + "\\rss.list";
        string input = File.Exists(path) ? File.ReadAllText(path) : (string) null;
        if (input != null)
        {
          ArrayList arrayList = this.serialzer.Deserialize<ArrayList>(input);
          this._allRSSs = arrayList;
          if (arrayList == null)
            this._allRSSs = new ArrayList();
        }
        else
          this._allRSSs = new ArrayList();
      }
      return this._allRSSs;
    }

    private string getRSSsListPath()
    {
      return this.configDirectory() + "\\rss.list";
    }

    private string getOfferListPath()
    {
      return this.configDirectory() + "\\offers.list";
    }

    public ArrayList allVip72s()
    {
      if (this._allVip72s == null)
      {
        string vip72ListPath = this.getVip72ListPath();
        string input = File.Exists(vip72ListPath) ? File.ReadAllText(vip72ListPath) : (string) null;
        if (input != null)
        {
          ArrayList arrayList = this.serialzer.Deserialize<ArrayList>(input);
          this._allVip72s = arrayList;
          if (arrayList == null)
            this._allVip72s = new ArrayList();
        }
        else
          this._allVip72s = new ArrayList();
      }
      return this._allVip72s;
    }

    public void updateBitviseProfileWithCurrentIP()
    {
      string ip = this.myIP;
      byte[] numArray1 = new byte[1305]
      {
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 14,
        (byte) 84,
        (byte) 117,
        (byte) 110,
        (byte) 110,
        (byte) 101,
        (byte) 108,
        (byte) 105,
        (byte) 101,
        (byte) 114,
        (byte) 32,
        (byte) 54,
        (byte) 46,
        (byte) 50,
        (byte) 52,
        (byte) 0,
        (byte) 0,
        (byte) 5,
        (byte) 119,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 104,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 26,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 4,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 4,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 13,
        (byte) 52,
        (byte) 54,
        (byte) 46,
        (byte) 49,
        (byte) 53,
        (byte) 51,
        (byte) 46,
        (byte) 50,
        (byte) 51,
        (byte) 53,
        (byte) 46,
        (byte) 55,
        (byte) 56,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 22,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 4,
        (byte) 117,
        (byte) 115,
        (byte) 101,
        (byte) 114,
        (byte) 1,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 11,
        (byte) 98,
        (byte) 115,
        (byte) 100,
        (byte) 97,
        (byte) 117,
        (byte) 116,
        (byte) 104,
        (byte) 44,
        (byte) 112,
        (byte) 97,
        (byte) 109,
        (byte) 1,
        (byte) 1,
        (byte) 0,
        (byte) 1,
        (byte) 2,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 2,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 5,
        (byte) 120,
        (byte) 116,
        (byte) 101,
        (byte) 114,
        (byte) 109,
        (byte) 1,
        (byte) 0,
        (byte) 0,
        (byte) 253,
        (byte) 233,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 80,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 25,
        (byte) 0,
        (byte) 0,
        (byte) 1,
        (byte) 44,
        (byte) 7,
        (byte) 1,
        (byte) 2,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 13,
        (byte) 49,
        (byte) 50,
        (byte) 55,
        (byte) 46,
        (byte) 48,
        (byte) 46,
        (byte) 48,
        (byte) 46,
        (byte) 49,
        (byte) 58,
        (byte) 48,
        (byte) 46,
        (byte) 48,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 205,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 205,
        (byte) 0,
        (byte) 0,
        (byte) 205,
        (byte) 205,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 238,
        (byte) 0,
        (byte) 205,
        (byte) 0,
        (byte) 205,
        (byte) 0,
        (byte) 0,
        (byte) 205,
        (byte) 205,
        (byte) 0,
        (byte) 229,
        (byte) 229,
        (byte) 229,
        (byte) 0,
        (byte) 127,
        (byte) 127,
        (byte) 127,
        (byte) 0,
        byte.MaxValue,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        byte.MaxValue,
        (byte) 0,
        (byte) 0,
        byte.MaxValue,
        byte.MaxValue,
        (byte) 0,
        (byte) 0,
        (byte) 92,
        (byte) 92,
        byte.MaxValue,
        (byte) 0,
        byte.MaxValue,
        (byte) 0,
        byte.MaxValue,
        (byte) 0,
        (byte) 0,
        byte.MaxValue,
        byte.MaxValue,
        (byte) 0,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 9,
        (byte) 49,
        (byte) 50,
        (byte) 55,
        (byte) 46,
        (byte) 48,
        (byte) 46,
        (byte) 48,
        (byte) 46,
        (byte) 49,
        (byte) 0,
        (byte) 0,
        (byte) 13,
        (byte) 61,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 2,
        (byte) 1,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 1,
        (byte) 0,
        (byte) 1,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 206,
        (byte) 101,
        (byte) 99,
        (byte) 100,
        (byte) 104,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 50,
        (byte) 45,
        (byte) 49,
        (byte) 46,
        (byte) 51,
        (byte) 46,
        (byte) 49,
        (byte) 51,
        (byte) 50,
        (byte) 46,
        (byte) 48,
        (byte) 46,
        (byte) 49,
        (byte) 48,
        (byte) 44,
        (byte) 101,
        (byte) 99,
        (byte) 100,
        (byte) 104,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 50,
        (byte) 45,
        (byte) 110,
        (byte) 105,
        (byte) 115,
        (byte) 116,
        (byte) 112,
        (byte) 53,
        (byte) 50,
        (byte) 49,
        (byte) 44,
        (byte) 101,
        (byte) 99,
        (byte) 100,
        (byte) 104,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 50,
        (byte) 45,
        (byte) 110,
        (byte) 105,
        (byte) 115,
        (byte) 116,
        (byte) 112,
        (byte) 51,
        (byte) 56,
        (byte) 52,
        (byte) 44,
        (byte) 101,
        (byte) 99,
        (byte) 100,
        (byte) 104,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 50,
        (byte) 45,
        (byte) 110,
        (byte) 105,
        (byte) 115,
        (byte) 116,
        (byte) 112,
        (byte) 50,
        (byte) 53,
        (byte) 54,
        (byte) 44,
        (byte) 100,
        (byte) 105,
        (byte) 102,
        (byte) 102,
        (byte) 105,
        (byte) 101,
        (byte) 45,
        (byte) 104,
        (byte) 101,
        (byte) 108,
        (byte) 108,
        (byte) 109,
        (byte) 97,
        (byte) 110,
        (byte) 45,
        (byte) 103,
        (byte) 114,
        (byte) 111,
        (byte) 117,
        (byte) 112,
        (byte) 45,
        (byte) 101,
        (byte) 120,
        (byte) 99,
        (byte) 104,
        (byte) 97,
        (byte) 110,
        (byte) 103,
        (byte) 101,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 50,
        (byte) 53,
        (byte) 54,
        (byte) 44,
        (byte) 100,
        (byte) 105,
        (byte) 102,
        (byte) 102,
        (byte) 105,
        (byte) 101,
        (byte) 45,
        (byte) 104,
        (byte) 101,
        (byte) 108,
        (byte) 108,
        (byte) 109,
        (byte) 97,
        (byte) 110,
        (byte) 45,
        (byte) 103,
        (byte) 114,
        (byte) 111,
        (byte) 117,
        (byte) 112,
        (byte) 45,
        (byte) 101,
        (byte) 120,
        (byte) 99,
        (byte) 104,
        (byte) 97,
        (byte) 110,
        (byte) 103,
        (byte) 101,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 49,
        (byte) 44,
        (byte) 100,
        (byte) 105,
        (byte) 102,
        (byte) 102,
        (byte) 105,
        (byte) 101,
        (byte) 45,
        (byte) 104,
        (byte) 101,
        (byte) 108,
        (byte) 108,
        (byte) 109,
        (byte) 97,
        (byte) 110,
        (byte) 45,
        (byte) 103,
        (byte) 114,
        (byte) 111,
        (byte) 117,
        (byte) 112,
        (byte) 49,
        (byte) 52,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 49,
        (byte) 44,
        (byte) 100,
        (byte) 105,
        (byte) 102,
        (byte) 102,
        (byte) 105,
        (byte) 101,
        (byte) 45,
        (byte) 104,
        (byte) 101,
        (byte) 108,
        (byte) 108,
        (byte) 109,
        (byte) 97,
        (byte) 110,
        (byte) 45,
        (byte) 103,
        (byte) 114,
        (byte) 111,
        (byte) 117,
        (byte) 112,
        (byte) 49,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 49,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 206,
        (byte) 101,
        (byte) 99,
        (byte) 100,
        (byte) 104,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 50,
        (byte) 45,
        (byte) 49,
        (byte) 46,
        (byte) 51,
        (byte) 46,
        (byte) 49,
        (byte) 51,
        (byte) 50,
        (byte) 46,
        (byte) 48,
        (byte) 46,
        (byte) 49,
        (byte) 48,
        (byte) 44,
        (byte) 101,
        (byte) 99,
        (byte) 100,
        (byte) 104,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 50,
        (byte) 45,
        (byte) 110,
        (byte) 105,
        (byte) 115,
        (byte) 116,
        (byte) 112,
        (byte) 53,
        (byte) 50,
        (byte) 49,
        (byte) 44,
        (byte) 101,
        (byte) 99,
        (byte) 100,
        (byte) 104,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 50,
        (byte) 45,
        (byte) 110,
        (byte) 105,
        (byte) 115,
        (byte) 116,
        (byte) 112,
        (byte) 51,
        (byte) 56,
        (byte) 52,
        (byte) 44,
        (byte) 101,
        (byte) 99,
        (byte) 100,
        (byte) 104,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 50,
        (byte) 45,
        (byte) 110,
        (byte) 105,
        (byte) 115,
        (byte) 116,
        (byte) 112,
        (byte) 50,
        (byte) 53,
        (byte) 54,
        (byte) 44,
        (byte) 100,
        (byte) 105,
        (byte) 102,
        (byte) 102,
        (byte) 105,
        (byte) 101,
        (byte) 45,
        (byte) 104,
        (byte) 101,
        (byte) 108,
        (byte) 108,
        (byte) 109,
        (byte) 97,
        (byte) 110,
        (byte) 45,
        (byte) 103,
        (byte) 114,
        (byte) 111,
        (byte) 117,
        (byte) 112,
        (byte) 45,
        (byte) 101,
        (byte) 120,
        (byte) 99,
        (byte) 104,
        (byte) 97,
        (byte) 110,
        (byte) 103,
        (byte) 101,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 50,
        (byte) 53,
        (byte) 54,
        (byte) 44,
        (byte) 100,
        (byte) 105,
        (byte) 102,
        (byte) 102,
        (byte) 105,
        (byte) 101,
        (byte) 45,
        (byte) 104,
        (byte) 101,
        (byte) 108,
        (byte) 108,
        (byte) 109,
        (byte) 97,
        (byte) 110,
        (byte) 45,
        (byte) 103,
        (byte) 114,
        (byte) 111,
        (byte) 117,
        (byte) 112,
        (byte) 45,
        (byte) 101,
        (byte) 120,
        (byte) 99,
        (byte) 104,
        (byte) 97,
        (byte) 110,
        (byte) 103,
        (byte) 101,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 49,
        (byte) 44,
        (byte) 100,
        (byte) 105,
        (byte) 102,
        (byte) 102,
        (byte) 105,
        (byte) 101,
        (byte) 45,
        (byte) 104,
        (byte) 101,
        (byte) 108,
        (byte) 108,
        (byte) 109,
        (byte) 97,
        (byte) 110,
        (byte) 45,
        (byte) 103,
        (byte) 114,
        (byte) 111,
        (byte) 117,
        (byte) 112,
        (byte) 49,
        (byte) 52,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 49,
        (byte) 44,
        (byte) 100,
        (byte) 105,
        (byte) 102,
        (byte) 102,
        (byte) 105,
        (byte) 101,
        (byte) 45,
        (byte) 104,
        (byte) 101,
        (byte) 108,
        (byte) 108,
        (byte) 109,
        (byte) 97,
        (byte) 110,
        (byte) 45,
        (byte) 103,
        (byte) 114,
        (byte) 111,
        (byte) 117,
        (byte) 112,
        (byte) 49,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 49,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 99,
        (byte) 101,
        (byte) 99,
        (byte) 100,
        (byte) 115,
        (byte) 97,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 50,
        (byte) 45,
        (byte) 49,
        (byte) 46,
        (byte) 51,
        (byte) 46,
        (byte) 49,
        (byte) 51,
        (byte) 50,
        (byte) 46,
        (byte) 48,
        (byte) 46,
        (byte) 49,
        (byte) 48,
        (byte) 44,
        (byte) 101,
        (byte) 99,
        (byte) 100,
        (byte) 115,
        (byte) 97,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 50,
        (byte) 45,
        (byte) 110,
        (byte) 105,
        (byte) 115,
        (byte) 116,
        (byte) 112,
        (byte) 53,
        (byte) 50,
        (byte) 49,
        (byte) 44,
        (byte) 101,
        (byte) 99,
        (byte) 100,
        (byte) 115,
        (byte) 97,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 50,
        (byte) 45,
        (byte) 110,
        (byte) 105,
        (byte) 115,
        (byte) 116,
        (byte) 112,
        (byte) 51,
        (byte) 56,
        (byte) 52,
        (byte) 44,
        (byte) 101,
        (byte) 99,
        (byte) 100,
        (byte) 115,
        (byte) 97,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 50,
        (byte) 45,
        (byte) 110,
        (byte) 105,
        (byte) 115,
        (byte) 116,
        (byte) 112,
        (byte) 50,
        (byte) 53,
        (byte) 54,
        (byte) 44,
        (byte) 115,
        (byte) 115,
        (byte) 104,
        (byte) 45,
        (byte) 114,
        (byte) 115,
        (byte) 97,
        (byte) 44,
        (byte) 115,
        (byte) 115,
        (byte) 104,
        (byte) 45,
        (byte) 100,
        (byte) 115,
        (byte) 115,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 99,
        (byte) 101,
        (byte) 99,
        (byte) 100,
        (byte) 115,
        (byte) 97,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 50,
        (byte) 45,
        (byte) 49,
        (byte) 46,
        (byte) 51,
        (byte) 46,
        (byte) 49,
        (byte) 51,
        (byte) 50,
        (byte) 46,
        (byte) 48,
        (byte) 46,
        (byte) 49,
        (byte) 48,
        (byte) 44,
        (byte) 101,
        (byte) 99,
        (byte) 100,
        (byte) 115,
        (byte) 97,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 50,
        (byte) 45,
        (byte) 110,
        (byte) 105,
        (byte) 115,
        (byte) 116,
        (byte) 112,
        (byte) 53,
        (byte) 50,
        (byte) 49,
        (byte) 44,
        (byte) 101,
        (byte) 99,
        (byte) 100,
        (byte) 115,
        (byte) 97,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 50,
        (byte) 45,
        (byte) 110,
        (byte) 105,
        (byte) 115,
        (byte) 116,
        (byte) 112,
        (byte) 51,
        (byte) 56,
        (byte) 52,
        (byte) 44,
        (byte) 101,
        (byte) 99,
        (byte) 100,
        (byte) 115,
        (byte) 97,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 50,
        (byte) 45,
        (byte) 110,
        (byte) 105,
        (byte) 115,
        (byte) 116,
        (byte) 112,
        (byte) 50,
        (byte) 53,
        (byte) 54,
        (byte) 44,
        (byte) 115,
        (byte) 115,
        (byte) 104,
        (byte) 45,
        (byte) 114,
        (byte) 115,
        (byte) 97,
        (byte) 44,
        (byte) 115,
        (byte) 115,
        (byte) 104,
        (byte) 45,
        (byte) 100,
        (byte) 115,
        (byte) 115,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 83,
        (byte) 97,
        (byte) 101,
        (byte) 115,
        (byte) 50,
        (byte) 53,
        (byte) 54,
        (byte) 45,
        (byte) 99,
        (byte) 116,
        (byte) 114,
        (byte) 44,
        (byte) 97,
        (byte) 101,
        (byte) 115,
        (byte) 49,
        (byte) 57,
        (byte) 50,
        (byte) 45,
        (byte) 99,
        (byte) 116,
        (byte) 114,
        (byte) 44,
        (byte) 97,
        (byte) 101,
        (byte) 115,
        (byte) 49,
        (byte) 50,
        (byte) 56,
        (byte) 45,
        (byte) 99,
        (byte) 116,
        (byte) 114,
        (byte) 44,
        (byte) 51,
        (byte) 100,
        (byte) 101,
        (byte) 115,
        (byte) 45,
        (byte) 99,
        (byte) 116,
        (byte) 114,
        (byte) 44,
        (byte) 97,
        (byte) 101,
        (byte) 115,
        (byte) 50,
        (byte) 53,
        (byte) 54,
        (byte) 45,
        (byte) 99,
        (byte) 98,
        (byte) 99,
        (byte) 44,
        (byte) 97,
        (byte) 101,
        (byte) 115,
        (byte) 49,
        (byte) 57,
        (byte) 50,
        (byte) 45,
        (byte) 99,
        (byte) 98,
        (byte) 99,
        (byte) 44,
        (byte) 97,
        (byte) 101,
        (byte) 115,
        (byte) 49,
        (byte) 50,
        (byte) 56,
        (byte) 45,
        (byte) 99,
        (byte) 98,
        (byte) 99,
        (byte) 44,
        (byte) 51,
        (byte) 100,
        (byte) 101,
        (byte) 115,
        (byte) 45,
        (byte) 99,
        (byte) 98,
        (byte) 99,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 88,
        (byte) 97,
        (byte) 101,
        (byte) 115,
        (byte) 50,
        (byte) 53,
        (byte) 54,
        (byte) 45,
        (byte) 99,
        (byte) 116,
        (byte) 114,
        (byte) 44,
        (byte) 97,
        (byte) 101,
        (byte) 115,
        (byte) 49,
        (byte) 57,
        (byte) 50,
        (byte) 45,
        (byte) 99,
        (byte) 116,
        (byte) 114,
        (byte) 44,
        (byte) 97,
        (byte) 101,
        (byte) 115,
        (byte) 49,
        (byte) 50,
        (byte) 56,
        (byte) 45,
        (byte) 99,
        (byte) 116,
        (byte) 114,
        (byte) 44,
        (byte) 51,
        (byte) 100,
        (byte) 101,
        (byte) 115,
        (byte) 45,
        (byte) 99,
        (byte) 116,
        (byte) 114,
        (byte) 44,
        (byte) 97,
        (byte) 101,
        (byte) 115,
        (byte) 50,
        (byte) 53,
        (byte) 54,
        (byte) 45,
        (byte) 99,
        (byte) 98,
        (byte) 99,
        (byte) 44,
        (byte) 97,
        (byte) 101,
        (byte) 115,
        (byte) 49,
        (byte) 57,
        (byte) 50,
        (byte) 45,
        (byte) 99,
        (byte) 98,
        (byte) 99,
        (byte) 44,
        (byte) 97,
        (byte) 101,
        (byte) 115,
        (byte) 49,
        (byte) 50,
        (byte) 56,
        (byte) 45,
        (byte) 99,
        (byte) 98,
        (byte) 99,
        (byte) 44,
        (byte) 51,
        (byte) 100,
        (byte) 101,
        (byte) 115,
        (byte) 45,
        (byte) 99,
        (byte) 98,
        (byte) 99,
        (byte) 44,
        (byte) 110,
        (byte) 111,
        (byte) 110,
        (byte) 101,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 74,
        (byte) 104,
        (byte) 109,
        (byte) 97,
        (byte) 99,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 50,
        (byte) 45,
        (byte) 50,
        (byte) 53,
        (byte) 54,
        (byte) 44,
        (byte) 104,
        (byte) 109,
        (byte) 97,
        (byte) 99,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 49,
        (byte) 44,
        (byte) 104,
        (byte) 109,
        (byte) 97,
        (byte) 99,
        (byte) 45,
        (byte) 109,
        (byte) 100,
        (byte) 53,
        (byte) 44,
        (byte) 104,
        (byte) 109,
        (byte) 97,
        (byte) 99,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 50,
        (byte) 45,
        (byte) 50,
        (byte) 53,
        (byte) 54,
        (byte) 45,
        (byte) 57,
        (byte) 54,
        (byte) 44,
        (byte) 104,
        (byte) 109,
        (byte) 97,
        (byte) 99,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 49,
        (byte) 45,
        (byte) 57,
        (byte) 54,
        (byte) 44,
        (byte) 104,
        (byte) 109,
        (byte) 97,
        (byte) 99,
        (byte) 45,
        (byte) 109,
        (byte) 100,
        (byte) 53,
        (byte) 45,
        (byte) 57,
        (byte) 54,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 79,
        (byte) 104,
        (byte) 109,
        (byte) 97,
        (byte) 99,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 50,
        (byte) 45,
        (byte) 50,
        (byte) 53,
        (byte) 54,
        (byte) 44,
        (byte) 104,
        (byte) 109,
        (byte) 97,
        (byte) 99,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 49,
        (byte) 44,
        (byte) 104,
        (byte) 109,
        (byte) 97,
        (byte) 99,
        (byte) 45,
        (byte) 109,
        (byte) 100,
        (byte) 53,
        (byte) 44,
        (byte) 104,
        (byte) 109,
        (byte) 97,
        (byte) 99,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 50,
        (byte) 45,
        (byte) 50,
        (byte) 53,
        (byte) 54,
        (byte) 45,
        (byte) 57,
        (byte) 54,
        (byte) 44,
        (byte) 104,
        (byte) 109,
        (byte) 97,
        (byte) 99,
        (byte) 45,
        (byte) 115,
        (byte) 104,
        (byte) 97,
        (byte) 49,
        (byte) 45,
        (byte) 57,
        (byte) 54,
        (byte) 44,
        (byte) 104,
        (byte) 109,
        (byte) 97,
        (byte) 99,
        (byte) 45,
        (byte) 109,
        (byte) 100,
        (byte) 53,
        (byte) 45,
        (byte) 57,
        (byte) 54,
        (byte) 44,
        (byte) 110,
        (byte) 111,
        (byte) 110,
        (byte) 101,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 9,
        (byte) 110,
        (byte) 111,
        (byte) 110,
        (byte) 101,
        (byte) 44,
        (byte) 122,
        (byte) 108,
        (byte) 105,
        (byte) 98,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 9,
        (byte) 110,
        (byte) 111,
        (byte) 110,
        (byte) 101,
        (byte) 44,
        (byte) 122,
        (byte) 108,
        (byte) 105,
        (byte) 98,
        (byte) 0,
        (byte) 0,
        (byte) 1,
        (byte) 44,
        (byte) 1,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 1
      };
      byte[] numArray2 = new byte[245]
      {
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 7,
        (byte) 48,
        (byte) 46,
        (byte) 48,
        (byte) 46,
        (byte) 48,
        (byte) 46,
        (byte) 48,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 2,
        (byte) 58,
        (byte) 58,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 7,
        (byte) 48,
        (byte) 46,
        (byte) 48,
        (byte) 46,
        (byte) 48,
        (byte) 46,
        (byte) 48,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 2,
        (byte) 58,
        (byte) 58,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 9,
        (byte) 49,
        (byte) 50,
        (byte) 55,
        (byte) 46,
        (byte) 48,
        (byte) 46,
        (byte) 48,
        (byte) 46,
        (byte) 49,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 2,
        (byte) 50,
        (byte) 49,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 1,
        (byte) 48,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 1,
        (byte) 48,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 1,
        (byte) 1,
        (byte) 1,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 1,
        (byte) 1,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 1,
        (byte) 1,
        (byte) 0,
        (byte) 0,
        (byte) 2,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 1,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 22,
        (byte) 116,
        (byte) 101,
        (byte) 115,
        (byte) 116,
        (byte) 64,
        (byte) 49,
        (byte) 48,
        (byte) 48,
        (byte) 46,
        (byte) 49,
        (byte) 48,
        (byte) 48,
        (byte) 46,
        (byte) 49,
        (byte) 48,
        (byte) 48,
        (byte) 46,
        (byte) 56,
        (byte) 56,
        (byte) 58,
        (byte) 50,
        (byte) 50,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 21,
        (byte) 67,
        (byte) 58,
        (byte) 92,
        (byte) 85,
        (byte) 115,
        (byte) 101,
        (byte) 114,
        (byte) 115,
        (byte) 92,
        (byte) 116,
        (byte) 101,
        (byte) 115,
        (byte) 116,
        (byte) 92,
        (byte) 68,
        (byte) 101,
        (byte) 115,
        (byte) 107,
        (byte) 116,
        (byte) 111,
        (byte) 112,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 10,
        (byte) 47,
        (byte) 104,
        (byte) 111,
        (byte) 109,
        (byte) 101,
        (byte) 47,
        (byte) 116,
        (byte) 101,
        (byte) 115,
        (byte) 116,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 1,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 17,
        (byte) 49,
        (byte) 52,
        (byte) 54,
        (byte) 46,
        (byte) 49,
        (byte) 56,
        (byte) 53,
        (byte) 46,
        (byte) 49,
        (byte) 52,
        (byte) 56,
        (byte) 46,
        (byte) 54,
        (byte) 54,
        (byte) 58,
        (byte) 50,
        (byte) 50,
        (byte) 0,
        (byte) 0,
        (byte) 1,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 4,
        (byte) 56,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0
      };
      byte[] numArray3 = new byte[4];
      byte[] bytes1 = Encoding.ASCII.GetBytes(ip);
      numArray3[3] = (byte) bytes1.Length;
      numArray1[21] += (byte) (bytes1.Length - Encoding.ASCII.GetBytes("192.168.0.110").Length);
      byte[] numArray4 = new byte[4];
      byte[] bytes2 = Encoding.ASCII.GetBytes(this.getSSHAndVip72Port().ToString());
      numArray4[3] = (byte) bytes2.Length;
      byte[] bytes3 = new byte[numArray1.Length + numArray3.Length + bytes1.Length + numArray2.Length + numArray4.Length + bytes2.Length];
      Buffer.BlockCopy((Array) numArray1, 0, (Array) bytes3, 0, numArray1.Length);
      Buffer.BlockCopy((Array) numArray3, 0, (Array) bytes3, numArray1.Length, numArray3.Length);
      Buffer.BlockCopy((Array) bytes1, 0, (Array) bytes3, numArray1.Length + numArray3.Length, bytes1.Length);
      Buffer.BlockCopy((Array) numArray4, 0, (Array) bytes3, numArray1.Length + bytes1.Length + numArray3.Length, numArray4.Length);
      Buffer.BlockCopy((Array) bytes2, 0, (Array) bytes3, numArray1.Length + numArray3.Length + bytes1.Length + numArray4.Length, bytes2.Length);
      Buffer.BlockCopy((Array) numArray2, 0, (Array) bytes3, numArray1.Length + numArray3.Length + bytes1.Length + bytes2.Length + numArray4.Length, numArray2.Length);
      File.WriteAllBytes(this.configDirectory() + "\\ldefault.bscp", bytes3);
    }

    public ArrayList allSSHs()
    {
      this.serialzer.MaxJsonLength = 2147483644;
      if (this._allSSHs == null)
      {
        string sshListPath = this.getSSHListPath();
        string input = File.Exists(sshListPath) ? File.ReadAllText(sshListPath) : (string) null;
        if (input != null)
        {
          ArrayList arrayList = this.serialzer.Deserialize<ArrayList>(input);
          this._allSSHs = arrayList;
          if (arrayList == null)
            this._allSSHs = new ArrayList();
        }
        else
          this._allSSHs = new ArrayList();
      }
      return this._allSSHs;
    }

    public void refreshSSHToUnused(string country)
    {
      for (int index = 0; index < this.allSSHs().Count; ++index)
      {
        Dictionary<string, object> allSsH = (Dictionary<string, object>) this.allSSHs()[index];
        if (allSsH[nameof (country)].ToString().Equals(country) && allSsH.ContainsKey("used"))
          allSsH.Remove("used");
      }
      this._allLiveSSHs.Remove(country);
      this.saveSSHsList();
    }

    public Dictionary<string, object> getLiveSSH(string country, bool delete, bool clearUnused = true)
    {
      if (!this._allLiveSSHs.ContainsKey(country))
      {
        this._allLiveSSHs[country] = new SynchronizedCollection<Dictionary<string, object>>();
        for (int index = 0; index < this.allSSHs().Count; ++index)
        {
          Dictionary<string, object> allSsH = (Dictionary<string, object>) this.allSSHs()[index];
          if (allSsH[nameof (country)].ToString().Equals(country) && !allSsH.ContainsKey("used") && (allSsH.ContainsKey("status") && allSsH.ContainsKey("fp")))
            this._allLiveSSHs[country].Add(allSsH);
        }
      }
      if (this._countryToCheck == null)
      {
        this._countryToCheck = country;
        this._checkLiveThread = new Thread(new ThreadStart(this.__checkLive));
      }
      else if (!this._countryToCheck.Equals(country))
      {
        this._checkLiveThread.Abort();
        this._checkLiveThread = (Thread) null;
        this._countryToCheck = country;
        this._checkLiveThread = new Thread(new ThreadStart(this.__checkLive));
      }
      SynchronizedCollection<Dictionary<string, object>> allLiveSsH = this._allLiveSSHs[country];
      if (allLiveSsH.Count == 0 && this._checkLiveThread == null)
      {
        if (!clearUnused)
          return (Dictionary<string, object>) null;
        this.refreshSSHToUnused(country);
        return this.getLiveSSH(country, delete, false);
      }
      if (allLiveSsH.Count == 0)
      {
        if (!this._checkLiveThread.IsAlive)
        {
          this._checkLiveThread = new Thread(new ThreadStart(this.__checkLive));
          this._checkLiveThread.Start();
        }
        LocalConfig._checkLiveDone.Reset();
        LocalConfig._checkLiveDone.WaitOne();
        if (allLiveSsH.Count > 0)
        {
          Dictionary<string, object> dictionary = allLiveSsH[0];
          allLiveSsH.RemoveAt(0);
          if (delete)
          {
            dictionary["used"] = (object) true;
            this.saveSSHsList();
          }
          return dictionary;
        }
        if (!clearUnused)
          return (Dictionary<string, object>) null;
        this.refreshSSHToUnused(country);
        return this.getLiveSSH(country, delete, false);
      }
      if (this._checkLiveThread != null && allLiveSsH.Count < GlobalConfig.SSHThreadCount / 2 && !this._checkLiveThread.IsAlive)
      {
        this._checkLiveThread = new Thread(new ThreadStart(this.__checkLive));
        this._checkLiveThread.Start();
      }
      Dictionary<string, object> dictionary1 = allLiveSsH[0];
      allLiveSsH.RemoveAt(0);
      if (delete)
      {
        dictionary1["used"] = (object) true;
        this.saveSSHsList();
      }
      return dictionary1;
    }

    private void __checkLive()
    {
      try
      {
        ArrayList list = new ArrayList();
        for (int index = 0; index < this.allSSHs().Count; ++index)
        {
          Dictionary<string, object> allSsH = (Dictionary<string, object>) this.allSSHs()[index];
          if (allSsH["country"].ToString().Equals(this._countryToCheck))
          {
            if (!allSsH.ContainsKey("status"))
              list.Add((object) allSsH);
            else if (!allSsH.ContainsKey("fp") && Convert.ToBoolean(allSsH["status"]))
              list.Add((object) allSsH);
          }
        }
        if (list.Count > 0)
        {
          LocalConfig._checkLiveThreadDone = new ManualResetEvent(false);
          int count = 0;
                    SSHTest.doTestSSHConnections(list.ToArray(), new Action<Dictionary<string, object>, int, bool, string>(delegate (Dictionary<string, object> dict, int index, bool result, string fingerPrint)
                    {
                        bool flag2 = result && fingerPrint != null;
                        if (flag2)
                        {
                            Dictionary<string, object> dictionary = (Dictionary<string, object>)list[index];
                            dictionary["status"] = (result ? 1 : 0);
                            bool flag3 = fingerPrint != null;
                            if (flag3)
                            {
                                dictionary["fp"] = fingerPrint;
                            }
                            LocalConfig.getCurrentConfig().saveSSHsList();
                            string text2 = dictionary["country"].ToString().ToUpper();
                            this._allLiveSSHs[text2].Add(dictionary);
                            bool flag4 = text2.Equals(this._countryToCheck);
                            if (flag4)
                            {
                                LocalConfig._checkLiveDone.Set();

                                count++;
                                bool flag5 = count >= GlobalConfig.SSHThreadCount;
                                if (flag5)
                                {
                                    SSHTest.stopTestSSHConnections();
                                    LocalConfig._checkLiveThreadDone.Set();
                                }
                            }
                        }
                        else
                        {
                            Dictionary<string, object> dictionary2 = (Dictionary<string, object>)list[index];
                            dictionary2["status"] = 0;
                            LocalConfig.getCurrentConfig().saveSSHsList();
                        }
                        Console.WriteLine(string.Concat(new object[]
                        {
                            dict["host"],
                            "[",
                            index,
                            "] : ",
                            result.ToString()
                        }));
                    }), GlobalConfig.SSHThreadCount);
                    LocalConfig._checkLiveThreadDone.Reset();
          LocalConfig._checkLiveThreadDone.WaitOne();
          if (count < GlobalConfig.SSHThreadCount)
          {
            this._checkLiveThread = (Thread) null;
            Console.WriteLine("*** [Check live] Done");
          }
          else
            Console.WriteLine("*** [Check live] Stop due to limitation");
        }
        else
        {
          LocalConfig._checkLiveDone.Set();
          this._checkLiveThread = (Thread) null;
          Console.WriteLine("*** [Check live] Done");
        }
      }
      catch
      {
        LocalConfig._checkLiveDone.Set();
        SSHTest.stopTestSSHConnections();
      }
    }

    public void refreshLiveSSHs()
    {
      LocalConfig._checkLiveDone.Set();
      if (this._checkLiveThread != null)
      {
        this._checkLiveThread.Abort();
        this._checkLiveThread = (Thread) null;
      }
      this._allLiveSSHs.Clear();
      this._countryToCheck = (string) null;
    }

    public void refreshLiveSSHs(string country)
    {
      if (this._countryToCheck == null)
      {
        if (!this._allLiveSSHs.ContainsKey(country))
          return;
        this._allLiveSSHs.Remove(country);
      }
      else if (this._countryToCheck.Equals(country))
      {
        this._countryToCheck = (string) null;
        LocalConfig._checkLiveDone.Set();
        if (this._checkLiveThread != null)
        {
          this._checkLiveThread.Abort();
          this._checkLiveThread = (Thread) null;
        }
        if (!this._allLiveSSHs.ContainsKey(country))
          return;
        this._allLiveSSHs.Remove(country);
      }
      else if (this._allLiveSSHs.ContainsKey(country))
        this._allLiveSSHs.Remove(country);
    }

    public void saveSSHsList()
    {
      File.WriteAllText(this.getSSHListPath(), this.serialzer.Serialize((object) this._allSSHs));
    }

    public void saveRSSsList()
    {
      this.serialzer.MaxJsonLength = 2147483644;
      File.WriteAllText(this.getRSSsListPath(), this.serialzer.Serialize((object) this._allRSSs));
    }

    public void saveOffersList()
    {
      File.WriteAllText(this.getOfferListPath(), this.serialzer.Serialize((object) this._allOffers));
    }

    public void saveLogsList()
    {
      File.WriteAllText(this.getLogsListPath(), this.serialzer.Serialize((object) this._allLogs));
    }

    private string getLogsListPath()
    {
      return this.configDirectory() + "\\" + LocalConfig.fileLogs;
    }

    public void saveVip72sList()
    {
      File.WriteAllText(this.getVip72ListPath(), this.serialzer.Serialize((object) this._allVip72s));
    }

    private string getSSHListPath()
    {
      return this.configDirectory() + "\\sshaccounts.list";
    }

    private string getVip72ListPath()
    {
      return this.configDirectory() + "\\vip72accounts.list";
    }

    public static LocalConfig getCurrentConfig()
    {
      return LocalConfig.currentConfig;
    }

    public static void setupCurrentConfig(string deviceSN, string model, string version)
    {
      LocalConfig.currentConfig = new LocalConfig(deviceSN, model, version);
    }

    internal static void SetLog(string deviceSN)
    {
      DateTime now = DateTime.Now;
      string str = now.Year.ToString() + "-" + (object) now.Month + "-" + (object) now.Day + "-Logs.list";
      string path = LocalConfig.configDirectorylog() + deviceSN + "\\" + str;
      if (!File.Exists(path))
      {
        try
        {
          File.CreateText(path);
        }
        catch
        {
        }
      }
      LocalConfig.fileLogs = str;
    }

    public static string configDirectorylog()
    {
      return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + LocalConfig.currentDeviceSNLog;
    }

    internal static bool Checkip(string ip, string linkoff)
    {
      Array array = (Array) LocalConfig.getCurrentConfig().AllLogs().ToArray();
      for (int index = 0; index < array.Length; ++index)
      {
        Dictionary<string, object> dictionary = (Dictionary<string, object>) LocalConfig.getCurrentConfig().AllLogs().ToArray().GetValue(index);
        string str1 = Convert.ToString(dictionary[nameof (linkoff)]);
        string str2 = Convert.ToString(dictionary["ipfake"]);
        if (linkoff == str1 && ip == str2)
          return false;
      }
      LocalConfig.getCurrentConfig().AllLogs().Add((object) new Dictionary<string, object>()
      {
        [nameof (linkoff)] = (object) linkoff,
        ["ipfake"] = (object) ip
      });
      LocalConfig.getCurrentConfig().saveLogsList();
      return true;
    }

    public ArrayList AllLogs()
    {
      string path = this.configDirectory() + "\\" + LocalConfig.fileLogs;
      string input = File.Exists(path) ? File.ReadAllText(path) : (string) null;
      if (input != null)
      {
        ArrayList arrayList = this.serialzer.Deserialize<ArrayList>(input);
        this._allLogs = arrayList;
        if (arrayList == null)
          this._allLogs = new ArrayList();
      }
      else
        this._allLogs = new ArrayList();
      return this._allLogs;
    }
  }
}
