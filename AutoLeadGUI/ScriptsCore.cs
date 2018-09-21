// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.ScriptsCore
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using soft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Script.Serialization;

namespace AutoLeadGUI
{
  internal class ScriptsCore
  {
    private static JavaScriptSerializer jss = new JavaScriptSerializer();

    public static bool executeScript(string script1Line, out bool result)
    {
      return ScriptsCore.executeScript(script1Line, out result, (Operation) null);
    }

    public static double GetRandomNumber(double minimum, double maximum)
    {
      return GlobalConfig.random.NextDouble() * (maximum - minimum) + minimum;
    }

    public static bool executeScript(string script1Line, out bool result, Operation operation)
    {
      script1Line = script1Line.Trim();
      if (script1Line.Length == 0)
      {
        result = true;
        return true;
      }
      if (script1Line.StartsWith(";"))
      {
        result = true;
        return true;
      }
      string str1 = script1Line.Trim();
      string str2;
      if (str1.IndexOf("=") >= 0 && str1.IndexOf("open_url") < 0)
      {
        str2 = Split.tachchuoi(str1, "=")[0];
        str1 = Split.tachchuoi(str1, "=")[1];
      }
      else
        str2 = "";
      string cmd;
      float x1;
      float y1;
      float x2;
      float y2;
      string text;
      double duration;
      string text2;
      int count;
      bool flag1 = GlobalConfig.parseScript(str1, out cmd, out x1, out y1, out x2, out y2, out text, out duration, out text2, out count);
      if (str1.IndexOf("rep") >= 0)
      {
        flag1 = true;
        try
        {
          string str3 = Split.tachchuoi(str1, ":")[1];
          for (int index = 0; index < frmMain.ScriptListRep.Count<ListScript>(); ++index)
          {
            if (frmMain.ScriptListRep[index].bien == str3)
            {
              text = frmMain.ScriptListRep[index].text;
              cmd = "text";
            }
          }
        }
        catch
        {
        }
      }
      string input1 = (string) null;
      frmMain.ScriptListRep.Add(new ListScript()
      {
        bien = str2,
        text = text
      });
      if (!flag1)
      {
        result = false;
      }
      else
      {
        if (cmd.Equals("wait"))
        {
          Thread.Sleep((int) ((double) x1 * 1000.0));
          result = true;
          return true;
        }
        if (cmd.Equals("rd"))
        {
          frmMain.Cmdrequest("sudo " + ScriptsCore.KeyboradGet(text, ""));
          input1 = "{\"result\":\"1\"}";
        }
        else if (cmd.Equals("touch"))
        {
          if (AutoLeadClient.send("{\"cmd\":\"sendUE\", \"type\":1, \"x1\":" + (object) x1 + ", \"y1\":" + (object) y1 + ", \"duration\":" + (object) duration + "}"))
            input1 = AutoLeadClient.receive();
        }
        else if (cmd.Equals("rand_touch"))
        {
          double num = duration / (double) count;
          for (int index = 0; index < count; ++index)
          {
            if (AutoLeadClient.send("{\"cmd\":\"sendUE\", \"type\":1, \"x1\":" + (object) ScriptsCore.GetRandomNumber((double) x1, (double) x2) + ", \"y1\":" + (object) ScriptsCore.GetRandomNumber((double) y1, (double) y2) + ", \"duration\":" + (object) 0 + "}"))
              input1 = AutoLeadClient.receive();
            Thread.Sleep((int) num * 1000);
          }
        }
        else if (cmd.Equals("swipe"))
        {
          if (AutoLeadClient.send("{\"cmd\":\"sendUE\", \"type\":2, \"x1\":" + (object) x1 + ", \"y1\":" + (object) y1 + ", \"x2\":" + (object) x2 + ", \"y2\":" + (object) y2 + ", \"duration\":" + (object) duration + "}"))
            input1 = AutoLeadClient.receive();
        }
        else if (cmd.Equals("text"))
        {
          if (AutoLeadClient.send("{\"cmd\":\"sendUE\", \"type\":3, \"text\":\"" + text + "\"}"))
            input1 = AutoLeadClient.receive();
        }
        else if (cmd.Equals("exit"))
        {
          if (AutoLeadClient.send("{\"cmd\":\"sendUE\", \"type\":4, \"text\":\"" + text + "\"}"))
            input1 = AutoLeadClient.receive();
        }
        else if (cmd.Equals("backup"))
        {
          if (AutoLeadClient.send("{\"cmd\":\"sendUE\", \"type\":5, \"name\":\"" + text + "\",\"app_id\":\"" + text2 + "\"}"))
            input1 = AutoLeadClient.receive();
        }
        else if (cmd.Equals("restore"))
        {
          if (AutoLeadClient.send("{\"cmd\":\"sendUE\", \"type\":6, \"name\":\"" + text + "\",\"app_id\":\"" + text2 + "\"}"))
            input1 = AutoLeadClient.receive();
        }
        else if (cmd.Equals("wipe"))
        {
          Console.WriteLine("{\"cmd\":\"sendUE\", \"type\":7 ,\"app_id\":\"" + text + "\" }");
          if (AutoLeadClient.send("{\"cmd\":\"sendUE\", \"type\":7 ,\"app_id\":\"" + text + "\" }"))
            input1 = AutoLeadClient.receive();
          Thread.Sleep(2000);
        }
        else if (cmd.Equals("delete"))
        {
          if (AutoLeadClient.send("{\"cmd\":\"sendUE\", \"type\":8, \"name\":\"" + text + "\",\"app_id\":\"" + text2 + "\"}"))
            input1 = AutoLeadClient.receive();
        }
        else
        {
          if (cmd.Equals("end"))
          {
            result = true;
            return false;
          }
          if (cmd.Equals("open_url"))
          {
            frmMain.Cmdrequest("killall -9 lsd");
            Thread.Sleep(1000);
            if (AutoLeadClient.send(new JavaScriptSerializer().Serialize((object) new Dictionary<string, object>()
            {
              ["cmd"] = (object) "openURL",
              ["url"] = (object) text
            })))
              input1 = AutoLeadClient.receive();
          }
          else if (cmd.Equals("bksafari"))
          {
            if (AutoLeadClient.send("{\"cmd\":\"sendUE\", \"type\":10 }"))
              input1 = AutoLeadClient.receive();
          }
          else if (cmd.Equals("rssafari"))
          {
            if (AutoLeadClient.send("{\"cmd\":\"sendUE\", \"type\":11 }"))
              input1 = AutoLeadClient.receive();
          }
          else if (cmd.Equals("open_app"))
          {
            if (AutoLeadClient.send("{\"cmd\":\"sendUE\", \"type\":12, \"app_id\":\"" + text + "\"}"))
              input1 = AutoLeadClient.receive();
            if(frmMain.rrsselect == true)
            {
                Thread.Sleep(10000);
                if (AutoLeadClient.send("{\"cmd\":\"sendUE\", \"type\":12, \"app_id\":\"" + text + "\"}"))
                {
                    input1 = AutoLeadClient.receive();
                }
            }
          }
          else if (cmd.Equals("checkip") && AutoLeadClient.send("{\"cmd\":\"get_public_ip\" }"))
          {
            string input2 = AutoLeadClient.receive();
            string str3 = ScriptsCore.jss.Deserialize<Dictionary<string, object>>(input2)[nameof (result)].ToString();
            bool flag2;
            if (str3 == null || GlobalConfig.currentPublicIP == null)
            {
              result = false;
              flag2 = true;
            }
            else
            {
              result = str3.Equals(GlobalConfig.currentPublicIP);
              flag2 = true;
            }
            return flag2;
          }
        }
        if (input1 != null)
        {
          Dictionary<string, object> dictionary = ScriptsCore.jss.Deserialize<Dictionary<string, object>>(input1);
          result = Convert.ToInt32(dictionary[nameof (result)]) == 1;
          flag1 = true;
        }
        else
        {
          result = false;
          flag1 = false;
        }
      }
      return flag1;
    }

    private static string KeyboradGet(string input, string device)
    {
      string str1 = (string) null;
      string str2 = input;
      int length = input.Length;
      for (int index = 0; index < length; ++index)
      {
        switch (str2[index])
        {
          case ' ':
            str1 = str1 != null ? str1 + " && skeyboard 7 44" : "skeyboard 7 44";
            break;
          case '.':
            str1 = str1 != null ? str1 + " && skeyboard 7 55" : "skeyboard 7 55";
            break;
          case '0':
            str1 = str1 != null ? str1 + " && skeyboard 7 39" : "skeyboard 7 39";
            break;
          case '1':
            str1 = str1 != null ? str1 + " && skeyboard 7 30" : "skeyboard 7 30";
            break;
          case '2':
            str1 = str1 != null ? str1 + " && skeyboard 7 31" : "skeyboard 7 31";
            break;
          case '3':
            str1 = str1 != null ? str1 + " && skeyboard 7 32" : "skeyboard 7 32";
            break;
          case '4':
            str1 = str1 != null ? str1 + " && skeyboard 7 33" : "skeyboard 7 33";
            break;
          case '5':
            str1 = str1 != null ? str1 + " && skeyboard 7 34" : "skeyboard 7 34";
            break;
          case '6':
            str1 = str1 != null ? str1 + " && skeyboard 7 35" : "skeyboard 7 35";
            break;
          case '7':
            str1 = str1 != null ? str1 + " && skeyboard 7 36" : "skeyboard 7 36";
            break;
          case '8':
            str1 = str1 != null ? str1 + " && skeyboard 7 37" : "skeyboard 7 37";
            break;
          case '9':
            str1 = str1 != null ? str1 + " && skeyboard 7 38" : "skeyboard 7 38";
            break;
          case '@':
            str1 = str1 != null ? str1 + " && skeyboard 7 225 1 && skeyboard 7 31 && skeyboard 7 225 0" : "skeyboard 7 225 1 && skeyboard 7 31 && skeyboard 7 225 0";
            break;
          case 'A':
            str1 = str1 != null ? str1 + " && skeyboard 7 225 1 && skeyboard 7 4 && skeyboard 7 225 0" : "skeyboard 7 225 1 && skeyboard 7 4 && skeyboard 7 225 0";
            break;
          case 'B':
            str1 = str1 != null ? str1 + " && skeyboard 7 225 1 && skeyboard 7 5 && skeyboard 7 225 0" : "skeyboard 7 225 1 && skeyboard 7 5 && skeyboard 7 225 0";
            break;
          case 'C':
            str1 = str1 != null ? str1 + " && skeyboard 7 225 1 && skeyboard 7 6 && skeyboard 7 225 0" : "skeyboard 7 225 1 && skeyboard 7 6 && skeyboard 7 225 0";
            break;
          case 'D':
            str1 = str1 != null ? str1 + " && skeyboard 7 225 1 && skeyboard 7 7 && skeyboard 7 225 0" : "skeyboard 7 225 1 && skeyboard 7 7 && skeyboard 7 225 0";
            break;
          case 'E':
            str1 = str1 != null ? str1 + " && skeyboard 7 225 1 && skeyboard 7 8 && skeyboard 7 225 0" : "skeyboard 7 225 1 && skeyboard 7 8 && skeyboard 7 225 0";
            break;
          case 'F':
            str1 = str1 != null ? str1 + " && skeyboard 7 225 1 && skeyboard 7 9 && skeyboard 7 225 0" : "skeyboard 7 225 1 && skeyboard 7 9 && skeyboard 7 225 0";
            break;
          case 'G':
            str1 = str1 != null ? str1 + " && skeyboard 7 225 1 && skeyboard 7 10 && skeyboard 7 225 0" : "skeyboard 7 225 1 && skeyboard 7 10 && skeyboard 7 225 0";
            break;
          case 'H':
            str1 = str1 != null ? str1 + " && skeyboard 7 225 1 && skeyboard 7 11 && skeyboard 7 225 0" : "skeyboard 7 225 1 && skeyboard 7 11 && skeyboard 7 225 0";
            break;
          case 'I':
            str1 = str1 != null ? str1 + " && skeyboard 7 225 1 && skeyboard 7 12 && skeyboard 7 225 0" : "skeyboard 7 225 1 && skeyboard 7 12 && skeyboard 7 225 0";
            break;
          case 'J':
            str1 = str1 != null ? str1 + " && skeyboard 7 225 1 && skeyboard 7 13 && skeyboard 7 225 0" : "skeyboard 7 225 1 && skeyboard 7 13 && skeyboard 7 225 0";
            break;
          case 'K':
            str1 = str1 != null ? str1 + " && skeyboard 7 225 1 && skeyboard 7 14 && skeyboard 7 225 0" : "skeyboard 7 225 1 && skeyboard 7 14 && skeyboard 7 225 0";
            break;
          case 'L':
            str1 = str1 != null ? str1 + " && skeyboard 7 225 1 && skeyboard 7 15 && skeyboard 7 225 0" : "skeyboard 7 225 1 && skeyboard 7 15 && skeyboard 7 225 0";
            break;
          case 'M':
            str1 = str1 != null ? str1 + " && skeyboard 7 225 1 && skeyboard 7 16 && skeyboard 7 225 0" : "skeyboard 7 225 1 && skeyboard 7 16 && skeyboard 7 225 0";
            break;
          case 'N':
            str1 = str1 != null ? str1 + " && skeyboard 7 225 1 && skeyboard 7 17 && skeyboard 7 225 0" : "skeyboard 7 225 1 && skeyboard 7 17 && skeyboard 7 225 0";
            break;
          case 'O':
            str1 = str1 != null ? str1 + " && skeyboard 7 225 1 && skeyboard 7 18 && skeyboard 7 225 0" : "skeyboard 7 225 1 && skeyboard 7 18 && skeyboard 7 225 0";
            break;
          case 'P':
            str1 = str1 != null ? str1 + " && skeyboard 7 225 1 && skeyboard 7 19 && skeyboard 7 225 0" : "skeyboard 7 225 1 && skeyboard 7 19 && skeyboard 7 225 0";
            break;
          case 'Q':
            str1 = str1 != null ? str1 + " && skeyboard 7 225 1 && skeyboard 7 20 && skeyboard 7 225 0" : "skeyboard 7 225 1 && skeyboard 7 20 && skeyboard 7 225 0";
            break;
          case 'R':
            str1 = str1 != null ? str1 + " && skeyboard 7 225 1 && skeyboard 7 21 && skeyboard 7 225 0" : "skeyboard 7 225 1 && skeyboard 7 21 && skeyboard 7 225 0";
            break;
          case 'S':
            str1 = str1 != null ? str1 + " && skeyboard 7 225 1 && skeyboard 7 22 && skeyboard 7 225 0" : "skeyboard 7 225 1 && skeyboard 7 22 && skeyboard 7 225 0";
            break;
          case 'T':
            str1 = str1 != null ? str1 + " && skeyboard 7 225 1 && skeyboard 7 23 && skeyboard 7 225 0" : "skeyboard 7 225 1 && skeyboard 7 23 && skeyboard 7 225 0";
            break;
          case 'U':
            str1 = str1 != null ? str1 + " && skeyboard 7 225 1 && skeyboard 7 24 && skeyboard 7 225 0" : "skeyboard 7 225 1 && skeyboard 7 24 && skeyboard 7 225 0";
            break;
          case 'V':
            str1 = str1 != null ? str1 + " && skeyboard 7 225 1 && skeyboard 7 25 && skeyboard 7 225 0" : "skeyboard 7 225 1 && skeyboard 7 25 && skeyboard 7 225 0";
            break;
          case 'W':
            str1 = str1 != null ? str1 + " && skeyboard 7 225 1 && skeyboard 7 26 && skeyboard 7 225 0" : "skeyboard 7 225 1 && skeyboard 7 26 && skeyboard 7 225 0";
            break;
          case 'X':
            str1 = str1 != null ? str1 + " && skeyboard 7 225 1 && skeyboard 7 27 && skeyboard 7 225 0" : "skeyboard 7 225 1 && skeyboard 7 27 && skeyboard 7 225 0";
            break;
          case 'Y':
            str1 = str1 != null ? str1 + " && skeyboard 7 225 1 && skeyboard 7 28 && skeyboard 7 225 0" : "skeyboard 7 225 1 && skeyboard 7 28 && skeyboard 7 225 0";
            break;
          case 'Z':
            str1 = str1 != null ? str1 + " && skeyboard 7 225 1 && skeyboard 7 29 && skeyboard 7 225 0" : "skeyboard 7 225 1 && skeyboard 7 29 && skeyboard 7 225 0";
            break;
          case 'a':
            str1 = str1 != null ? str1 + " && skeyboard 7 4" : "skeyboard 7 4";
            break;
          case 'b':
            str1 = str1 != null ? str1 + " && skeyboard 7 5" : "skeyboard 7 5";
            break;
          case 'c':
            str1 = str1 != null ? str1 + " && skeyboard 7 6" : "skeyboard 7 6";
            break;
          case 'd':
            str1 = str1 != null ? str1 + " && skeyboard 7 7" : "skeyboard 7 7";
            break;
          case 'e':
            str1 = str1 != null ? str1 + " && skeyboard 7 8" : "skeyboard 7 8";
            break;
          case 'f':
            str1 = str1 != null ? str1 + " && skeyboard 7 9" : "skeyboard 7 9";
            break;
          case 'g':
            str1 = str1 != null ? str1 + " && skeyboard 7 10" : "skeyboard 7 10";
            break;
          case 'h':
            str1 = str1 != null ? str1 + " && skeyboard 7 11" : "skeyboard 7 11";
            break;
          case 'i':
            str1 = str1 != null ? str1 + " && skeyboard 7 12" : "skeyboard 7 12";
            break;
          case 'j':
            str1 = str1 != null ? str1 + " && skeyboard 7 13" : "skeyboard 7 13";
            break;
          case 'k':
            str1 = str1 != null ? str1 + " && skeyboard 7 14" : "skeyboard 7 14";
            break;
          case 'l':
            str1 = str1 != null ? str1 + " && skeyboard 7 15" : "skeyboard 7 15";
            break;
          case 'm':
            str1 = str1 != null ? str1 + " && skeyboard 7 16" : "skeyboard 7 16";
            break;
          case 'n':
            str1 = str1 != null ? str1 + " && skeyboard 7 17" : "skeyboard 7 17";
            break;
          case 'o':
            str1 = str1 != null ? str1 + " && skeyboard 7 18" : "skeyboard 7 18";
            break;
          case 'p':
            str1 = str1 != null ? str1 + " && skeyboard 7 19" : "skeyboard 7 19";
            break;
          case 'q':
            str1 = str1 != null ? str1 + " && skeyboard 7 20" : "skeyboard 7 20";
            break;
          case 'r':
            str1 = str1 != null ? str1 + " && skeyboard 7 21" : "skeyboard 7 21";
            break;
          case 's':
            str1 = str1 != null ? str1 + " && skeyboard 7 22" : "skeyboard 7 22";
            break;
          case 't':
            str1 = str1 != null ? str1 + " && skeyboard 7 23" : "skeyboard 7 23";
            break;
          case 'u':
            str1 = str1 != null ? str1 + " && skeyboard 7 24" : "skeyboard 7 24";
            break;
          case 'v':
            str1 = str1 != null ? str1 + " && skeyboard 7 25" : "skeyboard 7 25";
            break;
          case 'w':
            str1 = str1 != null ? str1 + " && skeyboard 7 26" : "skeyboard 7 26";
            break;
          case 'x':
            str1 = str1 != null ? str1 + " && skeyboard 7 27" : "skeyboard 7 27";
            break;
          case 'y':
            str1 = str1 != null ? str1 + " && skeyboard 7 28" : "skeyboard 7 28";
            break;
          case 'z':
            str1 = str1 != null ? str1 + " && skeyboard 7 29" : "skeyboard 7 29";
            break;
        }
      }
      return str1;
    }
  }
}
