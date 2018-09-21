// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.Operation
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using soft;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace AutoLeadGUI
{
  internal class Operation
  {
    public static string __g_var_appName = "";
    public static string __g_var_appId = "";
    public static string __g_var_detectedAppId = "";
    public static string CurrentAppName = "<current_appname>";
    public static string CurrentAppId = "<current_appid>";
    public static JavaScriptSerializer jss = new JavaScriptSerializer();
    public static Dictionary<string, object> dict = new Dictionary<string, object>();
    public static string idapp_open = (string) null;
    public static string nameapp_open = (string) null;
    private bool checkASFlag = false;
    private Thread checkAS = (Thread) null;
    public Dictionary<string, object> checkASResult = (Dictionary<string, object>) null;
    public int timeout = -1;
    private string ipWithLocation = (string) null;
    private Dictionary<string, object> previousSshInfo = (Dictionary<string, object>) null;
    private frmMain frm = (frmMain) null;
    private string tool = (string) null;
    public Operation.OperationType type;
    public Dictionary<string, object> userInfo;
    private bool appstoreXy;
    private float appstoreX;
    private float appstoreY;
    private int appstoreInterval;
    private string fakeAppId;
    private bool fakeSet;
    private string defaultAppId;
    private string defaultAppName;
    private string appScriptOperationAppName;
    private string[] scripts;
    private string _description;

    public string description()
    {
      if (Operation.__g_var_appId.Length > 0 && Operation.__g_var_appName.Length > 0)
        return this._description.Replace(Operation.CurrentAppName, Operation.__g_var_appName).Replace(Operation.CurrentAppId, Operation.__g_var_appId);
      return this._description;
    }

    public static Operation NewGetRunningAppOperation(string _defaultAppId, string _defaultAppName, string _description)
    {
      return new Operation("getrunningapp()", _description)
      {
        type = Operation.OperationType.GetRunningAppOperation,
        defaultAppId = _defaultAppId,
        defaultAppName = _defaultAppName
      };
    }

    public static Operation NewSetupProxyOperation(string _ipWithLocation, string _tool, Dictionary<string, object> dict, frmMain _frm, string _description)
    {
      return new Operation("proxy(" + _ipWithLocation + ")", _description)
      {
        type = Operation.OperationType.SetupProxyOperation,
        ipWithLocation = _ipWithLocation,
        frm = _frm,
        tool = _tool,
        previousSshInfo = dict
      };
    }

    public static Operation NewChangeIPOperation(frmMain _frm, string _description)
    {
      return new Operation("changeip()", _description)
      {
        type = Operation.OperationType.ChangeIPOperation,
        frm = _frm
      };
    }

    public static Operation NewOpenDetectedAppOperation(string _description)
    {
      return new Operation("opendetectedapp()", _description)
      {
        type = Operation.OperationType.OpenDetectedAppOperation
      };
    }

    public static Operation NewFakeInfoOperation(string _appId, bool set, string _description)
    {
      return new Operation("fake(" + _appId + ")", _description)
      {
        type = Operation.OperationType.FakeInfoOperation,
        fakeAppId = _appId,
        _description = _description,
        fakeSet = set
      };
    }

    public static Operation NewWaitOperation(int _timeout, string _description)
    {
      return new Operation(_timeout, _description)
      {
        type = Operation.OperationType.WaitOperation
      };
    }

    public static Operation NewWaitForAppStoreOperation(int _timeout, bool safariXy, float x, float y, int interval, string _description)
    {
      return new Operation(_timeout, _description)
      {
        type = Operation.OperationType.WaitForAppStoreOperation,
        appstoreX = x,
        appstoreY = y,
        appstoreXy = safariXy,
        appstoreInterval = interval
      };
    }

    public static Operation NewBackupOperation(string _name, string _appId, string _description, Dictionary<string, object> _userInfo)
    {
      return new Operation("backup(" + _name + "," + _appId + ")", _description)
      {
        type = Operation.OperationType.BackupOperation,
        userInfo = _userInfo
      };
    }

    public static Operation NewSaveOperation(string _name, string _appId, string _description)
    {
      return new Operation("backuprrs(" + _name + "," + _appId + ")", _description)
      {
        type = Operation.OperationType.SaveOperation
      };
    }

    public static Operation NewScriptOperation(string _script, string _description)
    {
      return new Operation(GlobalConfig.splitToLines(_script), _description)
      {
        type = Operation.OperationType.ScriptOperation
      };
    }

    public static Operation NewCheckIPOperation(string _script, string _description)
    {
      return new Operation(GlobalConfig.splitToLines(_script), _description)
      {
        type = Operation.OperationType.CheckIPOperation
      };
    }

    public static Operation NewScript2Operation(string _appName, string _description)
    {
      return new Operation(GlobalConfig.splitToLines(""), _description)
      {
        type = Operation.OperationType.Script2Operation,
        appScriptOperationAppName = _appName
      };
    }

    public static Operation NewAppScriptOperation(string _app, string _script, string _description)
    {
      return new Operation(GlobalConfig.splitToLines(_script), _description)
      {
        type = Operation.OperationType.AppScriptOperation,
        appScriptOperationAppName = _app
      };
    }

    private Operation(string _script, string __description)
    {
      this.timeout = -1;
      this.scripts = new string[1];
      this.scripts[0] = _script;
      this._description = __description;
    }

    private Operation(string[] _scripts, string __description)
    {
      this.timeout = -1;
      this.scripts = _scripts;
      this._description = __description;
    }

    private Operation(int _timeout, string __description)
    {
      this.timeout = _timeout;
      this.scripts = new string[1];
      this.scripts[0] = "wait(" + (object) this.timeout + ")";
      this._description = __description;
    }

    private void _check_appstore()
    {
      try
      {
        int appstoreInterval = this.appstoreInterval;
        Thread.Sleep(3000);
        while (this.checkASFlag)
        {
          if (appstoreInterval <= 0 && this.appstoreXy)
          {
            appstoreInterval = this.appstoreInterval;
            bool result = false;
            try
            {
              ScriptsCore.executeScript("touch(" + frmMain.Xapp + "," + frmMain.Yapp + "," + (object) 0 + ")", out result);
            }
            catch
            {
            }
          }
          Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
          dictionary1["cmd"] = (object) "topmostappsystem";
          if (AutoLeadClient.send(Operation.jss.Serialize((object) dictionary1)))
          {
            string input1 = AutoLeadClient.receive();
            if (input1 != null)
            {
              Dictionary<string, object> dictionary2 = Operation.jss.Deserialize<Dictionary<string, object>>(input1);
              if (Convert.ToInt32(dictionary2["result"]) != 1)
              {
                bool result;
                ScriptsCore.executeScript("open_url(" + frmMain.linkoff + ",)", out result);
              }
              else if (dictionary2.ContainsKey("id"))
              {
                if (dictionary2["id"].ToString().Equals("com.apple.AppStore") && frmMain.bool_checkRunAppstore && AutoLeadClient.send(Operation.jss.Serialize((object) dictionary1)))
                {
                  Thread.Sleep(5000);
                  string input2 = AutoLeadClient.receive();
                  this.checkASResult = Operation.jss.Deserialize<Dictionary<string, object>>(input2);
                  break;
                }
                if (dictionary2["id"].ToString().Equals(frmMain.Appid) && !frmMain.bool_checkRunAppstore && AutoLeadClient.send(Operation.jss.Serialize((object) dictionary1)))
                {
                  Thread.Sleep(2000);
                  string input2 = AutoLeadClient.receive();
                  this.checkASResult = Operation.jss.Deserialize<Dictionary<string, object>>(input2);
                  break;
                }
                if (dictionary2["id"].ToString().Equals("com.apple.mobilesafari") && frmMain.bool_TouchSF)
                {
                  bool result = false;
                  try
                  {
                    ScriptsCore.executeScript("touch(" + frmMain.Xsf + "," + frmMain.Ysf + "," + (object) 0 + ")", out result);
                    for (int index = 0; index < 5; ++index)
                    {
                      ScriptsCore.executeScript("touch(" + frmMain.Xsf + "," + frmMain.Ysf + "," + (object) 0 + ")", out result);
                      Thread.Sleep(100);
                    }
                  }
                  catch (Exception ex)
                  {
                  }
                }
                else if (dictionary2["id"].ToString().IndexOf("com.apple.mobilesafari") < 0)
                {
                  bool result;
                  ScriptsCore.executeScript("open_url(" + frmMain.linkoff + ",)", out result);
                }
              }
            }
          }
          Thread.Sleep(1000);
          Console.WriteLine("Check => False");
          appstoreInterval -= 2;
        }
      }
      catch (Exception ex)
      {
        bool sttconnect = frmMain.sttconnect;
        Console.WriteLine((object) ex);
      }
    }

    public void execute(out bool end, int countrun, bool setRRS)
    {
      if (this.type == Operation.OperationType.Script2Operation)
      {
        if (File.Exists(LocalConfig.getCurrentConfig().configDirectory() + "/Scripts/" + this.appScriptOperationAppName.Replace(Operation.CurrentAppName, Operation.__g_var_appName)))
        {
          string str = File.ReadAllText(LocalConfig.getCurrentConfig().configDirectory() + "/Scripts/" + this.appScriptOperationAppName.Replace(Operation.CurrentAppName, Operation.__g_var_appName));
          if (str != null)
            this.scripts = GlobalConfig.splitToLines(str);
        }
        else
        {
          Operation.__g_var_appId = frmMain.Appid;
          Operation.__g_var_appName = frmMain.AppnameDefault;
          if (File.Exists(LocalConfig.getCurrentConfig().configDirectory() + "/Scripts/" + this.appScriptOperationAppName.Replace(Operation.CurrentAppName, Operation.__g_var_appName)))
          {
            string str = File.ReadAllText(LocalConfig.getCurrentConfig().configDirectory() + "/Scripts/" + this.appScriptOperationAppName.Replace(Operation.CurrentAppName, Operation.__g_var_appName));
            if (str != null)
              this.scripts = GlobalConfig.splitToLines(str);
          }
        }
        bool result1 = false;
        end = false;
        frmMain.BoolstartTimer1 = true;
        for (int index = 0; index < this.scripts.Length; ++index)
        {
          this.CheckConnectDevice();
          if (AutoLeadClient.send(Operation.jss.Serialize((object) new Dictionary<string, object>()
          {
            ["cmd"] = (object) "topmostappsystem"
          })) && AutoLeadClient.receive().IndexOf(Operation.idapp_open) < 0)
          {
            string script1Line = "open_app(" + Operation.idapp_open + ")";
            try
            {
              bool result2;
              ScriptsCore.executeScript(script1Line, out result2);
            }
            catch
            {
            }
          }
          string script1Line1 = this.scripts[index].Replace(Operation.CurrentAppName, Operation.__g_var_appName).Replace(Operation.CurrentAppId, Operation.__g_var_appId);
          if (script1Line1 != null && script1Line1 != "")
          {
            frmMain.sttScript2 = script1Line1;
            if (script1Line1 == "capcharAli")
            {
              frmMain.FindAli();
              script1Line1 = "text(\"" + Capchar.Getcapchar() + "\")";
            }
            if (script1Line1 == "downChMavrochain")
            {
              frmMain.FindMavrochain();
              script1Line1 = "";
            }
            if (script1Line1 == "capcharMavrochain")
              script1Line1 = "rd(\"" + Capchar.Getcapchar() + "\")";
            this.CheckConnectDevice();
            ScriptsCore.executeScript(script1Line1, out result1, this);
          }
        }
        frmMain.BoolstartTimer1 = false;
      }
      else if (this.type == Operation.OperationType.ScriptOperation && this._description.IndexOf("Script RRS") >= 0)
      {
        bool result1 = false;
        end = false;
        frmMain.BoolstartTimer1 = true;
        for (int index = 0; index < this.scripts.Length; ++index)
        {
          this.CheckConnectDevice();
          if (AutoLeadClient.send(Operation.jss.Serialize((object) new Dictionary<string, object>()
          {
            ["cmd"] = (object) "topmostappsystem"
          })) && AutoLeadClient.receive().IndexOf(frmMain.idappRRS) < 0)
          {
            string script1Line = "open_app(" + frmMain.idappRRS + ")";
            try
            {
              bool result2;
              ScriptsCore.executeScript(script1Line, out result2);
            }
            catch
            {
            }
          }
          string script1Line1 = this.scripts[index].Replace(Operation.CurrentAppName, Operation.__g_var_appName).Replace(Operation.CurrentAppId, Operation.__g_var_appId);
          if (script1Line1 != null && script1Line1 != "")
          {
            frmMain.sttScript2 = script1Line1;
            this.CheckConnectDevice();
            ScriptsCore.executeScript(script1Line1, out result1, this);
          }
        }
        frmMain.BoolstartTimer1 = false;
      }
      else if (this.type == Operation.OperationType.WaitForAppStoreOperation)
      {
        try
        {
          end = false;
          this.checkASFlag = true;
          this.checkAS = new Thread(new ThreadStart(this._check_appstore));
          this.checkAS.Start();
          while (this.timeout > 0)
          {
            if (!frmMain.sttconnect)
            {
              end = true;
              break;
            }
            Thread.Sleep(1000);
            if (this.checkASResult != null)
              break;
          }
          this.checkASFlag = false;
          while (this.checkAS.IsAlive)
          {
            if (!frmMain.sttconnect)
            {
              end = true;
              break;
            }
            Thread.Sleep(1000);
          }
          if (this.checkASResult == null)
            end = true;
          this.checkAS = (Thread) null;
        }
        catch (Exception ex)
        {
          if (this.checkAS != null && this.checkAS.IsAlive)
            this.checkAS.Abort();
          throw ex;
        }
      }
      else if (this.type == Operation.OperationType.OpenDetectedAppOperation)
      {
        end = false;
        if (frmMain.bool_detectapp)
        {
          Dictionary<string, object> dictionary = new Dictionary<string, object>();
          dictionary["cmd"] = (object) "topmostappsystem";
          string url = (string) null;
          if (AutoLeadClient.send(Operation.jss.Serialize((object) dictionary)))
          {
            string input = AutoLeadClient.receive();
            this.checkASResult = Operation.jss.Deserialize<Dictionary<string, object>>(input);
            url = this.checkASResult["url"].ToString();
            if (url.Length > 0)
            {
              Operation.__g_var_detectedAppId = AppURLToAppID.AppIDFromURL(url);
              if (Operation.__g_var_detectedAppId.Length <= 0)
                Operation.__g_var_detectedAppId = AppURLToAppID.AppIDFromSiteLee(url);
            }
          }
          if (Operation.__g_var_detectedAppId.Length > 0)
          {
            List<Listapp> list = frmMain.ListappInstall.Where<Listapp>((Func<Listapp, bool>) (c => c.app_id == Operation.__g_var_detectedAppId)).ToList<Listapp>();
            if (list.Count<Listapp>() > 0)
            {
              Operation.nameapp_open = list[0].app_name;
              if (!frmMain.bool_sml)
              {
                if (frmMain.Appid != Operation.__g_var_detectedAppId)
                {
                  end = true;
                }
                else
                {
                  string script1Line = "open_app(" + Operation.__g_var_detectedAppId + ")";
                  Operation.idapp_open = Operation.__g_var_detectedAppId;
                  try
                  {
                    bool result;
                    ScriptsCore.executeScript(script1Line, out result);
                  }
                  catch
                  {
                  }
                }
              }
              else
              {
                string script1Line = "open_app(" + Operation.__g_var_detectedAppId + ")";
                Operation.idapp_open = Operation.__g_var_detectedAppId;
                try
                {
                  bool result;
                  ScriptsCore.executeScript(script1Line, out result);
                }
                catch
                {
                }
              }
            }
            else
              end = true;
          }
          else
          {
            int num1 = (int) MessageBox.Show(url + " \r\n Nhấn OK để copy link app");
            if (!frmMain.bool_sml)
            {
              int num2 = (int) MessageBox.Show("App này không nhận dạng được, bỏ chọn phần 'Nhận dạng app'");
            }
            end = true;
          }
        }
        else
        {
          Operation.__g_var_detectedAppId = frmMain.Appid;
          string script1Line = "open_app(" + Operation.__g_var_detectedAppId + ")";
          Operation.idapp_open = Operation.__g_var_detectedAppId;
          Operation.nameapp_open = frmMain.AppnameDefault;
          try
          {
            bool result;
            ScriptsCore.executeScript(script1Line, out result);
          }
          catch
          {
          }
        }
      }
      else if (this.type == Operation.OperationType.GetRunningAppOperation)
      {
        Operation.__g_var_appId = Operation.idapp_open;
        Operation.__g_var_appName = Operation.nameapp_open;
        end = false;
      }
      else if (this.type == Operation.OperationType.FakeInfoOperation)
      {
        Dictionary<string, object> dictionary;
        while (true)
        {
          end = false;
          dictionary = new Dictionary<string, object>();
          dictionary["cmd"] = (object) "fake_info";
          dictionary["listapp_id"] = (object) this.fakeAppId;
          if (this.fakeSet)
          {
            if (Operation.dict.Count <= 0)
              Operation.dict = FakeInfoCore.get();
            if (Operation.dict.Count > 0)
            {
              if (!Operation.CheckinfoFake(Operation.dict))
                Operation.dict.Clear();
              else
                break;
            }
            else
              goto label_101;
          }
          else
            goto label_101;
        }
        if (frmMain.bool_fakelangnoIP)
          Operation.dict["lang"] = (object) Split.tachchuoi(frmMain.langnoIP, "|")[0];
        Operation.dict["setfakegeo"] = !frmMain.bool_fakegeo ? (object) "0" : (object) "1";
        Operation.dict["setfakeUa"] = !frmMain.bool_fakeUA ? (object) "0" : (object) "1";
        Operation.dict["setfakescreen"] = !frmMain.bool_fakescreen ? (object) "0" : (object) "1";
        dictionary["info"] = (object) Operation.dict;
label_101:
        if (!AutoLeadClient.send(Operation.jss.Serialize((object) dictionary)))
          return;
        string input = AutoLeadClient.receive();
        if (Convert.ToInt32(Operation.jss.Deserialize<Dictionary<string, object>>(input)["result"]) != 1)
          throw new Exception("ERROR. [" + this.description() + "] return [False]");
        Thread.Sleep(1000);
      }
      else if (this.type == Operation.OperationType.SetupProxyOperation)
      {
        end = false;
        try
        {
          string[] strArray = GlobalConfig.stringSplit(this.ipWithLocation, ",");
          if (strArray.Length >= 3)
          {
            string country = strArray[1].Trim();
            strArray[2].Trim();
            if (country.Equals("TAIWAN"))
              country = "TAIWAN, PROVINCE OF CHINA";
            if (country.Equals("BOLIVIA"))
              country = "BOLIVIA, PLURINATIONAL STATE OF";
            if (country.Equals("IRAN"))
              country = "IRAN, ISLAMIC REPUBLIC OF";
            if (country.Equals("IRAN"))
              country = "IRAN, ISLAMIC REPUBLIC OF";
            if (country.Equals("KOREA"))
              country = "KOREA, REPUBLIC OF";
            if (country.Equals("KOREA"))
              country = "KOREA, REPUBLIC OF";
            if (country.Equals("MACEDONIA"))
              country = "MACEDONIA, THE FORMER YUGOSLAV REPUBLIC OF";
            if (country.Equals("MOLDOVA"))
              country = "MOLDOVA, REPUBLIC OF";
            if (country.Equals("PALESTINE"))
              country = "PALESTINE, STATE OF";
            string str1 = strArray[0].Trim();
            string str2 = this.ipWithLocation.Replace(str1, "").Replace(", " + country, "").Trim();
            string region = str2.Length != 0 ? str2.Remove(0, 1).Trim() : (string) null;
            this.frm.setupProxy(this.tool, str1, country, region, this.previousSshInfo);
          }
        }
        catch (Exception ex)
        {
        }
        if (GlobalConfig.currentPublicIP == null)
          throw new Exception("ERROR. Could not execute [" + this.description() + "]");
      }
      else if (this.type == Operation.OperationType.ChangeIPOperation)
      {
        if (!setRRS)
          Operation.dict.Clear();
        end = false;
        try
        {
          this.frm.changeIP();
        }
        catch (Exception ex)
        {
        }
        if (GlobalConfig.currentPublicIP == null)
          throw new Exception("ERROR. Could not execute [" + this.description() + "]");
      }
      else if (this.type == Operation.OperationType.AppScriptOperation)
      {
        end = false;
        new Dictionary<string, object>()["cmd"] = (object) "topmostappsystem";
        if (!this._isAppRunning(this.appScriptOperationAppName))
          return;
        bool result = false;
        end = false;
        for (int index = 0; index < this.scripts.Length; ++index)
        {
          this.CheckConnectDevice();
          string script1Line = this.scripts[index].Replace(Operation.CurrentAppName, Operation.__g_var_appName).Replace(Operation.CurrentAppId, Operation.__g_var_appId);
          if (this._isAppRunning(this.appScriptOperationAppName))
          {
            if (!ScriptsCore.executeScript(script1Line, out result, this))
            {
              if (!result)
                throw new Exception("ERROR. Could not execute [" + this.description() + "]");
              end = true;
              break;
            }
            if (!result)
              throw new Exception("ERROR. [" + this.description() + "] return [False]");
          }
          else
            break;
        }
      }
      else if (this.type == Operation.OperationType.ScriptOperation && this._description.IndexOf("Restore") >= 0)
      {
        bool result = false;
        end = false;
        for (int index = 0; index < this.scripts.Length; ++index)
        {
          string script1Line = this.scripts[index].Replace(Operation.CurrentAppName, Operation.__g_var_appName).Replace(Operation.CurrentAppId, Operation.__g_var_appId).Replace(" ", "");
          if (script1Line != null && script1Line != "")
          {
            ScriptsCore.executeScript(script1Line, out result, this);
            frmMain.RestoreRRS(Split.tachchuoi(this.scripts[index].Replace("restore(", "").Replace(",<current_appid>)", ""), ",")[0]);
            Thread.Sleep(3000);
            result = true;
            end = false;
          }
        }
      }
      else
      {
        bool result = false;
        end = false;
        for (int index1 = 0; index1 < this.scripts.Length; ++index1)
        {
          string script1Line = this.scripts[index1].Replace(Operation.CurrentAppName, Operation.__g_var_appName).Replace(Operation.CurrentAppId, Operation.__g_var_appId);
          if (script1Line == "exit()")
          {
            script1Line = "";
            Operation.__g_var_appName = "";
            Operation.__g_var_appId = "";
          }
          if (script1Line == "downChMavrochain")
          {
            frmMain.FindMavrochain();
            script1Line = "";
          }
          if (script1Line == "capcharMavrochain")
            script1Line = "rd(\"" + Capchar.Getcapchar() + "\")";
          if (script1Line != null && script1Line != "")
          {
            if (script1Line.IndexOf("backup") >= 0)
            {
              if (script1Line.IndexOf("backuprrs") >= 0)
              {
                string str1 = script1Line.Replace("backuprrs", "backup").Replace(" ", "");
                string str2 = Split.tachchuoi(str1, ",")[0].Replace("backup(", "");
                frmMain.WipeOlRRSlee(str2);
                ScriptsCore.executeScript(str1, out result, this);
                frmMain.BKRRS(str2, true);
                result = true;
                end = false;
                break;
              }
              for (int index2 = 0; index2 < frmMain.ListVitriBk.Count; ++index2)
              {
                if (frmMain.ListVitriBk[index2].vitri == countrun)
                {
                  string str = script1Line.Replace(" ", "");
                  ScriptsCore.executeScript(str, out result, this);
                  frmMain.BKRRS(Split.tachchuoi(str, ",")[0].Replace("backup(", ""), false);
                  frmMain.UpRRSFtp();
                  result = true;
                  end = false;
                  break;
                }
              }
              result = true;
              end = false;
            }
            else if (!ScriptsCore.executeScript(script1Line, out result, this))
            {
              if (!result)
                throw new Exception("ERROR. Could not execute [" + this.description() + "]");
              end = true;
              break;
            }
            if (!result)
              throw new Exception("ERROR. [" + this.description() + "] return [False]");
          }
        }
      }
    }

    public void CheckConnectDevice()
    {
      if (frmMain.sttconnect)
        return;
      int error = 0;
      string serial = (string) null;
      int num = 0;
      while (!AutoLeadClient.connect(frmMain.ipreconnect, frmMain.keyreconnect, out error, out serial))
        ++num;
      frmMain.sttconnect = true;
    }

    public static bool CheckinfoFake(Dictionary<string, object> dict)
    {
      try
      {
        return !(dict["tz"].ToString() == "") && !(dict["locale"].ToString() == "") && (!(dict["lang"].ToString() == "") && !(dict["carrier"].ToString() == "")) && (!(dict["mcc"].ToString() == "") && !(dict["mnc"].ToString() == "") && (!(dict["iso"].ToString() == "") && !(dict["location_x"].ToString() == ""))) && (!(dict["location_y"].ToString() == "") && !(dict["name"].ToString() == "") && (!(dict["systemVersion"].ToString() == "") && !(dict["buildversion"].ToString() == "")) && (!(dict["model"].ToString() == "") && !(dict["machine"].ToString() == "") && (!(dict["ua"].ToString() == "") && !(dict["modelVersion"].ToString() == "")))) && !(dict["ModemVersion"].ToString() == "");
      }
      catch
      {
        return false;
      }
    }

    public bool _isAppRunning(string name)
    {
      Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
      dictionary1["cmd"] = (object) "topmostappsystem";
      bool flag = false;
      if (AutoLeadClient.send(Operation.jss.Serialize((object) dictionary1)))
      {
        string input = AutoLeadClient.receive();
        Dictionary<string, object> dictionary2 = Operation.jss.Deserialize<Dictionary<string, object>>(input);
        if (Convert.ToInt32(dictionary2["result"]) == 1 && dictionary2["id"].ToString().Equals(name))
          flag = true;
      }
      return flag;
    }

    public string getTimeoutStatusText()
    {
      return this.description().Replace("<t>", Convert.ToString(this.timeout));
    }

    internal static void Killapp(string id)
    {
      if (!AutoLeadClient.send(Operation.jss.Serialize((object) new Dictionary<string, object>()
      {
        ["cmd"] = (object) "killapp",
        ["app_id"] = (object) id
      })))
        return;
      AutoLeadClient.receive();
    }

    internal static void Gohome()
    {
      if (!AutoLeadClient.send(Operation.jss.Serialize((object) new Dictionary<string, object>()
      {
        ["cmd"] = (object) "gohome"
      })))
        return;
      AutoLeadClient.receive();
    }

    public enum OperationType
    {
      BackupOperation,
      WaitOperation,
      ScriptOperation,
      SaveOperation,
      SetupProxyOperation,
      FakeInfoOperation,
      GetRunningAppOperation,
      AppScriptOperation,
      Script2Operation,
      ChangeIPOperation,
      CheckIPOperation,
      WaitForAppStoreOperation,
      OpenDetectedAppOperation,
    }
  }
}
