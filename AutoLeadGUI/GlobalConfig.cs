// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.GlobalConfig
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace AutoLeadGUI
{
  internal class GlobalConfig
  {
    private static Regex regex = new Regex("^\\s*(\\w+)\\s*");
    private static string[] emails = (string[]) null;
    private static string[] firsts = (string[]) null;
    private static string[] names = (string[]) null;
    private static string[] middles = (string[]) null;
    private static string[] addresses = (string[]) null;
    public static Random random = new Random();
    private static string password = (string) null;
    public static string currentPublicIP = (string) null;
    public static string currentPublicIPWithLocationInfo = (string) null;
    public static Dictionary<string, object> currentSSH = (Dictionary<string, object>) null;
    public static bool SSHRefresh = true;
    public static int SSHTimeout;
    public static int SSHThreadCount;

    public static string executableDirectory()
    {
      return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }

    public static bool isPortAvailable(int port)
    {
      bool flag = true;
      foreach (TcpConnectionInformation activeTcpConnection in IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections())
      {
        if (activeTcpConnection.LocalEndPoint.Port == port)
        {
          flag = false;
          break;
        }
      }
      return flag;
    }

    public static void setupLocalConfigWithDeviceSN(string deviceSN, string model, string version)
    {
      LocalConfig.setupCurrentConfig(deviceSN, model, version);
    }

    public static int port()
    {
      return 6799;
    }

    public static string[] stringSplit(string text)
    {
      return text.Split(new string[1]{ " | " }, StringSplitOptions.None);
    }

    public static string[] stringSplit(string text, string splitter)
    {
      return text.Split(new string[1]{ splitter }, StringSplitOptions.None);
    }

    public static string RandomString(string input, int Size)
    {
      if (input == null)
        input = "abcdefghijklmnopqrstuvwxyz0123456789".ToUpper();
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < Size; ++index)
      {
        char ch = input[GlobalConfig.random.Next(0, input.Length)];
        stringBuilder.Append(ch);
      }
      return stringBuilder.ToString();
    }

    public static void parseStart()
    {
      if (GlobalConfig.emails == null)
        GlobalConfig.loadRandom();
      int length = 15;
      char[] chArray = new char[length];
      for (int index = 0; index < length; ++index)
        chArray[index] = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[GlobalConfig.random.Next("ABCDEFGHIJKLMNOPQRSTUVWXYZ".Length)];
      GlobalConfig.password = new string(chArray);
    }

    public static void parseStop()
    {
    }

    public static bool parseScript(string scriptString, out string cmd, out float x1, out float y1, out float x2, out float y2, out string text, out double duration, out string text2, out int count)
    {
      Array script = GlobalConfig._parseScript(scriptString);
      x1 = 0.0f;
      y1 = 0.0f;
      x2 = 0.0f;
      y2 = 0.0f;
      duration = 0.0;
      text = (string) null;
      cmd = (string) null;
      text2 = (string) null;
      count = 0;
      if (script == null || script.Length == 0)
        return false;
      string str1 = script.GetValue(0).ToString();
      if (str1.Equals("exit"))
      {
        if (script.Length != 2)
          return false;
        cmd = str1;
        return true;
      }
      if (str1.Equals("wait"))
      {
        if (script.Length != 2 || !float.TryParse(script.GetValue(1).ToString(), out x1))
          return false;
        cmd = str1;
        return true;
      }
      if (str1.Equals("touch"))
      {
        if (script.Length != 4 || !float.TryParse(script.GetValue(1).ToString(), out x1) || (!float.TryParse(script.GetValue(2).ToString(), out y1) || !double.TryParse(script.GetValue(3).ToString(), out duration)))
          return false;
        cmd = str1;
        return true;
      }
      if (str1.Equals("rand_touch"))
      {
        if (script.Length != 7 || !float.TryParse(script.GetValue(1).ToString(), out x1) || (!float.TryParse(script.GetValue(2).ToString(), out y1) || !float.TryParse(script.GetValue(3).ToString(), out x2)) || (!float.TryParse(script.GetValue(4).ToString(), out y2) || !double.TryParse(script.GetValue(5).ToString(), out duration) || !int.TryParse(script.GetValue(6).ToString(), out count)))
          return false;
        cmd = str1;
        return true;
      }
      if (str1.Equals("swipe"))
      {
        if (script.Length != 6 || !float.TryParse(script.GetValue(1).ToString(), out x1) || (!float.TryParse(script.GetValue(2).ToString(), out y1) || !float.TryParse(script.GetValue(3).ToString(), out x2)) || (!float.TryParse(script.GetValue(4).ToString(), out y2) || !double.TryParse(script.GetValue(5).ToString(), out duration)))
          return false;
        cmd = str1;
        return true;
      }
      if (str1.Equals("backup"))
      {
        if (script.Length == 3)
        {
          cmd = str1;
          text = script.GetValue(1).ToString().Trim();
          text2 = script.GetValue(2).ToString().Trim();
          return true;
        }
      }
      else if (str1.Equals("restore"))
      {
        if (script.Length == 3)
        {
          cmd = str1;
          text = script.GetValue(1).ToString().Trim();
          text2 = script.GetValue(2).ToString().Trim();
          return true;
        }
      }
      else if (str1.Equals("open_url"))
      {
        if (script.Length == 3)
        {
          cmd = str1;
          text = script.GetValue(1).ToString().Trim();
          text2 = script.GetValue(2).ToString().Trim();
          return true;
        }
      }
      else if (str1.Equals("delete"))
      {
        if (script.Length == 3)
        {
          cmd = str1;
          text = script.GetValue(1).ToString().Trim();
          text2 = script.GetValue(2).ToString().Trim();
          return true;
        }
      }
      else if (str1.Equals("wipe"))
      {
        if (script.Length == 2)
        {
          cmd = str1;
          text = script.GetValue(1).ToString().Trim();
          return true;
        }
      }
      else if (str1.Equals("open_app"))
      {
        if (script.Length == 2)
        {
          cmd = str1;
          text = script.GetValue(1).ToString().Trim();
          return true;
        }
      }
      else if (str1.Equals("end") || str1.Equals("bksafari") || str1.Equals("rssafari") || str1.Equals("checkip"))
      {
        if (script.Length == 2)
        {
          cmd = str1;
          if (script.GetValue(1).ToString().Equals(""))
            return true;
        }
      }
      else
      {
        if (str1.Equals(nameof (text)))
        {
          if (script.Length == 2 || script.Length == 3)
          {
            cmd = str1;
            if (script.GetValue(1).ToString().StartsWith("\"") && script.GetValue(1).ToString().EndsWith("\""))
            {
              text = script.GetValue(1).ToString().Trim("\"".ToCharArray());
              return true;
            }
            if (script.GetValue(1).ToString().Equals("rand_email"))
            {
              GlobalConfig.loadRandom();
              int index = GlobalConfig.random.Next(0, GlobalConfig.emails.Length - 1);
              text = GlobalConfig.emails[index];
              text = RandomMail.Getmail();
              return true;
            }
            if (script.GetValue(1).ToString().Equals("rand_onetext"))
            {
              string str2 = "qwertyuiopasdfghjklzxcvbnm";
              text = str2[GlobalConfig.random.Next(str2.Length)].ToString();
              return true;
            }
            if (script.GetValue(1).ToString().Equals("rand_name"))
            {
              GlobalConfig.loadRandom();
              int index1 = GlobalConfig.random.Next(0, GlobalConfig.firsts.Length - 1);
              int index2 = GlobalConfig.random.Next(0, GlobalConfig.middles.Length - 1);
              int index3 = GlobalConfig.random.Next(0, GlobalConfig.names.Length - 1);
              if (GlobalConfig.random.Next(0, 100) % 2 == 0)
                text = GlobalConfig.firsts[index1] + " " + GlobalConfig.middles[index2] + " " + GlobalConfig.names[index3];
              else
                text = GlobalConfig.firsts[index1] + " " + GlobalConfig.names[index3];
              return true;
            }
            if (script.GetValue(1).ToString().Equals("rand_firstname"))
            {
              GlobalConfig.loadRandom();
              do
              {
                int index = GlobalConfig.random.Next(0, GlobalConfig.firsts.Length - 1);
                text = GlobalConfig.firsts[index];
              }
              while (text == null || text == "");
              return true;
            }
            if (script.GetValue(1).ToString().Equals("rand_middlename"))
            {
              GlobalConfig.loadRandom();
              int index = GlobalConfig.random.Next(0, GlobalConfig.middles.Length - 1);
              text = GlobalConfig.middles[index];
              return true;
            }
            if (script.GetValue(1).ToString().Equals("rand_lastname"))
            {
              GlobalConfig.loadRandom();
              int index = GlobalConfig.random.Next(0, GlobalConfig.names.Length - 1);
              text = GlobalConfig.names[index];
              return true;
            }
            if (script.GetValue(1).ToString().Equals("rand_address"))
            {
              GlobalConfig.loadRandom();
              int index = GlobalConfig.random.Next(0, GlobalConfig.addresses.Length - 1);
              text = GlobalConfig.addresses[index];
              return true;
            }
            if (script.GetValue(1).ToString().Equals("rand_password"))
            {
              string str2 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
              int length = GlobalConfig.random.Next(8, 10);
              char[] chArray = new char[length];
              for (int index = 0; index < length; ++index)
              {
                if (index > 0)
                  str2 = str2.ToLower();
                chArray[index] = str2[GlobalConfig.random.Next(str2.Length)];
              }
              GlobalConfig.password = new string(chArray);
              int num = GlobalConfig.random.Next(100, 99999);
              text = GlobalConfig.password + (object) num;
              return true;
            }
            if (script.GetValue(1).ToString().Equals("rand_number"))
            {
              GlobalConfig.loadRandom();
              int length = 10;
              if (script.Length == 2)
                length = GlobalConfig.random.Next() % 9 + 1;
              else if (script.Length == 3)
                length = Convert.ToInt32(script.GetValue(2).ToString());
              text = GlobalConfig.RandomDigits(length);
              return true;
            }
            cmd = (string) null;
          }
          return false;
        }
        if (str1.Equals("rd") && (script.Length == 2 || script.Length == 3))
        {
          cmd = str1;
          if (script.GetValue(1).ToString().StartsWith("\"") && script.GetValue(1).ToString().EndsWith("\""))
          {
            text = script.GetValue(1).ToString().Trim("\"".ToCharArray());
            return true;
          }
          if (script.GetValue(1).ToString().Equals("mail"))
          {
            GlobalConfig.loadRandom();
            int index = GlobalConfig.random.Next(0, GlobalConfig.emails.Length - 1);
            text = GlobalConfig.emails[index];
            text = RandomMail.Getmail();
            return true;
          }
          if (script.GetValue(1).ToString().Equals("pass"))
          {
            string str2 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int length = GlobalConfig.random.Next(8, 10);
            char[] chArray = new char[length];
            for (int index = 0; index < length; ++index)
            {
              if (index > 0)
                str2 = str2.ToLower();
              chArray[index] = str2[GlobalConfig.random.Next(str2.Length)];
            }
            GlobalConfig.password = new string(chArray);
            int num = GlobalConfig.random.Next(100, 99999);
            text = GlobalConfig.password + (object) num;
            return true;
          }
          cmd = (string) null;
          return false;
        }
      }
      return false;
    }

    private static string RandomDigits(int length)
    {
      Random random = new Random();
      string empty = string.Empty;
      for (int index = 0; index < length; ++index)
        empty += random.Next(10).ToString();
      return empty;
    }

    private static void loadRandom()
    {
      try
      {
        GlobalConfig.emails = File.ReadAllLines(GlobalConfig.executableDirectory() + "\\Emails.list");
        GlobalConfig.firsts = File.ReadAllLines(GlobalConfig.executableDirectory() + "\\Firstnames.list");
        GlobalConfig.middles = File.ReadAllLines(GlobalConfig.executableDirectory() + "\\Middlenames.list");
        GlobalConfig.names = File.ReadAllLines(GlobalConfig.executableDirectory() + "\\Names.list");
        GlobalConfig.addresses = File.ReadAllLines(GlobalConfig.executableDirectory() + "\\Addresses.list");
      }
      catch
      {
      }
    }

    public static Array _parseScript(string script)
    {
      Match match = GlobalConfig.regex.Match(script);
      ArrayList arrayList = new ArrayList();
      Console.WriteLine(match.Captures.Count);
      if (match.Captures.Count != 1)
        return (Array) null;
      arrayList.Add((object) match.Captures[0].Value);
      string str = script.Substring(match.Captures[0].Value.Length).Trim("() ".ToCharArray());
      if (str.StartsWith("\""))
      {
        arrayList.Add((object) str);
      }
      else
      {
        string[] strArray = str.Split(',');
        if (strArray == null)
        {
          arrayList.Add((object) str);
        }
        else
        {
          for (int index = 0; index < strArray.Length; ++index)
            arrayList.Add((object) strArray[index].ToString());
        }
      }
      return (Array) arrayList.ToArray();
    }

    public static string[] splitToLines(string str)
    {
      return str.Split(new string[2]{ "\r\n", "\n" }, StringSplitOptions.None);
    }

    public static string randItem(string[] items)
    {
      int index = GlobalConfig.random.Next(items.Length);
      return items[index];
    }
  }
}
