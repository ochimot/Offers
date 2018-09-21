// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.FakeInfoCore
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using soft;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace AutoLeadGUI
{
  internal class FakeInfoCore
  {
    public static string iosVersion = "";
    private static string buildversion = (string) null;
    private static string regioncode = "en_US";
    private static string langcode = "en";
    private static string countryISO = (string) null;
    private static string countryCode = (string) null;
    private static string timezone = (string) null;
    private static PointF location = PointF.Empty;
    private static string carrier = (string) null;
    private static string mcc = (string) null;
    private static string mnc = (string) null;
    private static string iso = (string) null;
    private static string locale = (string) null;
    private static string model = (string) null;
    private static string modemVersion = (string) null;
    private static string systemVersion = (string) null;
    private static string name = (string) null;
    private static string[] _modelbylee = (string[]) null;
    private static string[] _IMEbylee = (string[]) null;
    private static string[] _modemlee = (string[]) null;
    private static string[] _timezones = (string[]) null;
    private static string[] _carriers = (string[]) null;
    private static string[] _langs = (string[]) null;
    private static string[] _regions = (string[]) null;
    public static string[] _versions_ = (string[]) null;
    public static string[] _models_ = (string[]) null;
    private static string[] _names = (string[]) null;
    private static string[] _codes = (string[]) null;
    private static string[] _ccodes = (string[]) null;
    private static string[] _LocaleLanguages = (string[]) null;
    private static string[] _ua = (string[]) null;
    private static JavaScriptSerializer jss = new JavaScriptSerializer();
    public static List<countrycodeiOS> listcountrycodeiOS = new List<countrycodeiOS>();
    public static string country = (string) null;
    private static Random getrandom;
    private static object syncLock;

    private static bool _e(string p)
    {
      return LocalConfig.getCurrentConfig().getBooleanForKey(p);
    }

    public static Dictionary<string, object> get()
    {
label_1:
      while (true)
      {
        try
        {
          FakeInfoCore.buildversion = (string) null;
          FakeInfoCore.regioncode = "en_US";
          FakeInfoCore.langcode = "en";
          FakeInfoCore.countryISO = (string) null;
          FakeInfoCore.countryCode = (string) null;
          FakeInfoCore.timezone = (string) null;
          FakeInfoCore.location = PointF.Empty;
          FakeInfoCore.carrier = (string) null;
          FakeInfoCore.locale = (string) null;
          FakeInfoCore.model = (string) null;
          FakeInfoCore.modemVersion = (string) null;
          FakeInfoCore.systemVersion = (string) null;
          FakeInfoCore.name = (string) null;
          if (FakeInfoCore._timezones == null)
          {
            FakeInfoCore._modemlee = Split.tachchuoi(ResourceList.Modembylee, "\r\n");
            FakeInfoCore._IMEbylee = System.IO.File.ReadAllLines(GlobalConfig.executableDirectory() + "\\IMEbylee.list");
            FakeInfoCore._modelbylee = Split.tachchuoi(ResourceList.modelbylee, "\r\n");
            FakeInfoCore._timezones = Split.tachchuoi(ResourceList.Timezones, "\r\n");
            FakeInfoCore._carriers = Split.tachchuoi(ResourceList.Operators, "\r\n");
            FakeInfoCore._regions = Split.tachchuoi(ResourceList.Regions, "\r\n");
            FakeInfoCore._langs = FakeInfoCore._regions;
            FakeInfoCore._ua = Split.tachchuoi(ResourceList.UA, "\r\n");
            FakeInfoCore._names = System.IO.File.ReadAllLines(GlobalConfig.executableDirectory() + "\\Names.list");
            FakeInfoCore._codes = Split.tachchuoi(ResourceList.Countries, "\r\n");
            FakeInfoCore._ccodes = Split.tachchuoi(ResourceList.Codes, "\r\n");
            FakeInfoCore._LocaleLanguages = Split.tachchuoi(ResourceList.LocaleLanguages, "\r\n");
          }
          string[] strArray1 = Split.tachchuoi(ResourceList.IOSLanguageCode, "\r\n");
          Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
          if (LocalConfig.getCurrentConfig().getBooleanForKey("FakeLocationFromIP"))
          {
            string requestUriString = "http://pro.ip-api.com/json/" + GlobalConfig.currentPublicIP + "?key=kISXZimHUJwHgV7";
            string input = (string) null;
            using (HttpWebResponse response = (HttpWebResponse) WebRequest.Create(requestUriString).GetResponse())
            {
              using (Stream responseStream = response.GetResponseStream())
              {
                using (StreamReader streamReader = new StreamReader(responseStream))
                  input = streamReader.ReadToEnd();
              }
            }
            if (input != null)
            {
              Dictionary<string, object> dictionary2 = FakeInfoCore.jss.Deserialize<Dictionary<string, object>>(input);
              FakeInfoCore.country = dictionary2["country"].ToString();
              string str1 = dictionary2["countryCode"].ToString();
              dictionary2["region"].ToString();
              string str2 = dictionary2["timezone"].ToString();
              double num1 = Convert.ToDouble(dictionary2["lat"].ToString());
              double num2 = Convert.ToDouble(dictionary2["lon"].ToString());
              double num3 = (double) FakeInfoCore.GetRandomNumber(-10000, 10000) / 100000.0;
              double num4 = (double) FakeInfoCore.GetRandomNumber(-10000, 10000) / 100000.0;
              FakeInfoCore.location = new PointF((float) (num1 + num3), (float) (num2 + num4));
              FakeInfoCore.timezone = !((IEnumerable<string>) FakeInfoCore._timezones).Contains<string>(str2) ? GlobalConfig.randItem(FakeInfoCore._timezones) : str2;
              for (int index = 0; index < ((IEnumerable<string>) strArray1).Count<string>(); ++index)
              {
                if (strArray1[index].IndexOf(FakeInfoCore.country) >= 0)
                {
                  FakeInfoCore.langcode = Split.tachchuoi(strArray1[index], "|")[0];
                  break;
                }
              }
              List<region> source1 = new List<region>();
              foreach (string region in FakeInfoCore._regions)
              {
                string str3 = Split.tachchuoi(region, "_")[1];
                if (str1.ToLower() == str3.ToLower())
                  source1.Add(new region() { code = region });
                try
                {
                  string str4 = Split.tachchuoi(region, "_")[2];
                  if (str1.ToLower() == str4.ToLower())
                    source1.Add(new region()
                    {
                      code = region
                    });
                }
                catch
                {
                }
              }
              try
              {
                string langcode = FakeInfoCore.langcode;
                if (langcode.IndexOf("_") >= 0)
                  langcode = Split.tachchuoi(langcode, "_")[0];
                for (int index = 0; index < source1.Count<region>(); ++index)
                {
                  if (source1[index].code.IndexOf(langcode) >= 0)
                  {
                    FakeInfoCore.regioncode = source1[index].code;
                    break;
                  }
                }
              }
              catch
              {
              }
              FakeInfoCore.countryISO = str1.ToLower();
              string upper = dictionary2["country"].ToString().ToUpper();
              foreach (string code in FakeInfoCore._codes)
              {
                if (code.Contains(upper))
                {
                  FakeInfoCore.countryCode = GlobalConfig.stringSplit(code)[1];
                  break;
                }
              }
              if (FakeInfoCore.countryCode == null || FakeInfoCore.countryCode == "")
              {
                int num5 = (int) MessageBox.Show("set countryCode", upper);
              }
              List<Carrier> source2 = new List<Carrier>();
              foreach (string carrier in FakeInfoCore._carriers)
              {
                if (carrier.ToUpper().Contains(upper))
                {
                  string str3 = GlobalConfig.stringSplit(carrier)[1];
                  string str4 = GlobalConfig.stringSplit(carrier)[2];
                  string str5 = GlobalConfig.stringSplit(carrier)[3];
                  string str6 = GlobalConfig.stringSplit(carrier)[4];
                  if (str3 != null && str3 != "")
                    source2.Add(new Carrier()
                    {
                      namecarrier = str3,
                      mcc = str4,
                      mnc = str5,
                      iso = str6
                    });
                }
              }
              if (source2.Count == 0)
              {
                int index = new Random().Next(0, ((IEnumerable<string>) FakeInfoCore._carriers).Count<string>());
                FakeInfoCore.carrier = GlobalConfig.stringSplit(FakeInfoCore._carriers[index])[1];
                FakeInfoCore.mcc = GlobalConfig.stringSplit(FakeInfoCore._carriers[index])[2];
                FakeInfoCore.mnc = GlobalConfig.stringSplit(FakeInfoCore._carriers[index])[3];
                FakeInfoCore.iso = GlobalConfig.stringSplit(FakeInfoCore._carriers[index])[4];
              }
              else
              {
                int index = new Random().Next(0, source2.Count<Carrier>());
                FakeInfoCore.carrier = source2[index].namecarrier;
                FakeInfoCore.mcc = source2[index].mcc;
                FakeInfoCore.mnc = source2[index].mnc;
                FakeInfoCore.iso = source2[index].iso;
              }
            }
            else
            {
              int num = (int) MessageBox.Show("Lol");
            }
          }
          else
          {
            int int32ForKey1 = LocalConfig.getCurrentConfig().getInt32ForKey("fakeRegion");
            FakeInfoCore.regioncode = FakeInfoCore._e("fakeRegionEnable") ? FakeInfoCore._regions[int32ForKey1] : (string) null;
            int int32ForKey2 = LocalConfig.getCurrentConfig().getInt32ForKey("fakeLang");
            FakeInfoCore.langcode = FakeInfoCore._e("fakelangEnable") ? FakeInfoCore._langs[int32ForKey2] : (string) null;
            int int32ForKey3 = LocalConfig.getCurrentConfig().getInt32ForKey("fakeTz");
            FakeInfoCore.timezone = FakeInfoCore._e("fakeTzEnable") ? FakeInfoCore._timezones[int32ForKey3] : (string) null;
            int int32ForKey4 = LocalConfig.getCurrentConfig().getInt32ForKey("fakeCa");
            FakeInfoCore.carrier = FakeInfoCore._e("fakeCaEnable") ? FakeInfoCore._carriers[int32ForKey4] : (string) null;
            FakeInfoCore.location = FakeInfoCore._e("fakeLocationEnable") ? new PointF((float) Convert.ToDouble(LocalConfig.getCurrentConfig().getStringForKey("fakeLocationLat")), (float) Convert.ToDouble(LocalConfig.getCurrentConfig().getStringForKey("fakeLocationLong"))) : PointF.Empty;
            string[] strArray2 = GlobalConfig.stringSplit(FakeInfoCore.carrier);
            if (strArray2.Length >= 2)
            {
              FakeInfoCore.carrier = strArray2[1];
              foreach (string ccode in FakeInfoCore._ccodes)
              {
                if (ccode.StartsWith(strArray2[0].ToUpper()))
                {
                  FakeInfoCore.countryISO = GlobalConfig.stringSplit(ccode, ";")[1].ToLower();
                  break;
                }
              }
              foreach (string code in FakeInfoCore._codes)
              {
                if (code.StartsWith(strArray2[0].ToUpper()))
                {
                  FakeInfoCore.countryCode = GlobalConfig.stringSplit(code, " | ")[1].ToLower();
                  break;
                }
              }
            }
          }
          while (true)
          {
            if (LocalConfig.getCurrentConfig().getBooleanForKey("FakeDeviceFromFile"))
            {
              do
              {
                List<string> source = new List<string>();
                for (int index = 0; index < ((IEnumerable<string>) FakeInfoCore._models_).Count<string>(); ++index)
                {
                  if (frmMain.bool_iphone7 && FakeInfoCore._models_[index].IndexOf("iPhone 7") >= 0)
                    source.Add(FakeInfoCore._models_[index]);
                }
                FakeInfoCore.model = source.Count<string>() <= 0 ? GlobalConfig.randItem(((IEnumerable<string>) FakeInfoCore._models_).ToArray<string>()) : GlobalConfig.randItem(source.ToArray());
                string str1 = Split.tachchuoi(FakeInfoCore.model, " | ")[1];
                List<string> stringList = new List<string>();
                for (int index = 0; index < ((IEnumerable<string>) FakeInfoCore._modemlee).Count<string>(); ++index)
                {
                  string str2 = Split.tachchuoi(FakeInfoCore._modemlee[index], "|")[1];
                  if (FakeInfoCore._modemlee[index].IndexOf(str1) >= 0 && str2.IndexOf(FakeInfoCore.iosVersion) >= 0)
                    stringList.Add(FakeInfoCore._modemlee[index]);
                }
                FakeInfoCore.systemVersion = Split.tachchuoi(GlobalConfig.randItem(stringList.ToArray()), "|")[1];
              }
              while (frmMain.bool_chkonly10_3_1 && FakeInfoCore.systemVersion != "10.3.1");
              FakeInfoCore.name = GlobalConfig.randItem(FakeInfoCore._names);
            }
            else
            {
              FakeInfoCore.systemVersion = GlobalConfig.randItem(FakeInfoCore._versions_);
              FakeInfoCore.model = GlobalConfig.randItem(FakeInfoCore._models_);
              if (FakeInfoCore._e("FRandName"))
              {
                FakeInfoCore.name = GlobalConfig.randItem(FakeInfoCore._names);
                if (FakeInfoCore.model.StartsWith("iPad") || FakeInfoCore.model.StartsWith("iPhone") || !FakeInfoCore.model.StartsWith("iPod"))
                  ;
              }
              else
              {
                FakeInfoCore.name = LocalConfig.getCurrentConfig().getStringForKey("fakeName");
                if (FakeInfoCore.name == null)
                  FakeInfoCore.name = "";
              }
            }
            if (FakeInfoCore.timezone != null)
              dictionary1["tz"] = (object) FakeInfoCore.timezone;
            if (FakeInfoCore.regioncode != null)
              dictionary1["locale"] = (object) FakeInfoCore.regioncode;
            if (FakeInfoCore.langcode != null)
              dictionary1["lang"] = (object) FakeInfoCore.langcode;
            if (FakeInfoCore.carrier != null)
            {
              dictionary1["carrier"] = (object) FakeInfoCore.carrier;
              dictionary1["mcc"] = (object) FakeInfoCore.mcc;
              dictionary1["mnc"] = (object) FakeInfoCore.mnc;
              dictionary1["iso"] = (object) FakeInfoCore.iso;
            }
            if (FakeInfoCore.location != PointF.Empty)
            {
              Dictionary<string, object> dictionary2 = dictionary1;
              string index1 = "location_x";
              float num = FakeInfoCore.location.X;
              string str1 = num.ToString();
              dictionary2[index1] = (object) str1;
              Dictionary<string, object> dictionary3 = dictionary1;
              string index2 = "location_y";
              num = FakeInfoCore.location.Y;
              string str2 = num.ToString();
              dictionary3[index2] = (object) str2;
            }
            if (FakeInfoCore.name != null)
              dictionary1["name"] = (object) FakeInfoCore.name;
            if (FakeInfoCore.systemVersion != null)
            {
              string ios = ResourceList.IOS;
              string[] strArray2 = Split.tachchuoi(ios, "\r\n");
              for (int index = 0; index < ios.Count<char>(); ++index)
              {
                if (strArray2[index].ToString().Contains(FakeInfoCore.systemVersion))
                {
                  dictionary1["systemVersion"] = (object) Split.tachchuoi(strArray2[index], "-")[0];
                  dictionary1["buildversion"] = (object) Split.tachchuoi(strArray2[index], "-")[1];
                  break;
                }
              }
            }
            if (FakeInfoCore.model != null)
            {
              if (FakeInfoCore.model.StartsWith("iPhone"))
                dictionary1["model"] = (object) "iPhone";
              if (FakeInfoCore.model.StartsWith("iPad"))
                dictionary1["model"] = (object) "iPad";
              if (FakeInfoCore.model.StartsWith("iPod"))
                dictionary1["model"] = (object) "iPod";
              string[] strArray2 = GlobalConfig.stringSplit(FakeInfoCore.model);
              if (strArray2.Length == 2)
                dictionary1["machine"] = (object) strArray2[1].Trim();
            }
            if (FakeInfoCore.systemVersion != null && FakeInfoCore.model != null)
            {
              string str1 = dictionary1["model"].ToString() + ";";
              if (dictionary1["model"].ToString().StartsWith("iPod"))
                str1 = dictionary1["model"].ToString() + " touch;";
              string str2 = FakeInfoCore.systemVersion.Replace(".", "_");
              List<string> stringList = new List<string>();
              foreach (string str3 in FakeInfoCore._ua)
              {
                if (str3.Contains(str2 + " ") && str3.Contains(str1))
                  stringList.Add(str3);
              }
              if (stringList.Count > 0)
                dictionary1["ua"] = (object) GlobalConfig.randItem(stringList.ToArray());
              else if (stringList.Count == 0)
              {
                foreach (string str3 in FakeInfoCore._ua)
                {
                  if (str3.Contains(str2 + " "))
                    stringList.Add(str3);
                }
                if (stringList.Count > 0)
                  dictionary1["ua"] = (object) GlobalConfig.randItem(stringList.ToArray());
              }
            }
            Random random = new Random();
            dictionary1["serial"] = (object) GlobalConfig.RandomString((string) null, 12);
            dictionary1["imei"] = (object) FakeInfoCore.CreateImeibylee();
            List<modelVersion> source1 = new List<modelVersion>();
            foreach (string chuoi in FakeInfoCore._modelbylee)
            {
              string str = dictionary1["machine"].ToString();
              if (chuoi.Contains(str))
                source1.Add(new modelVersion()
                {
                  name = Split.tachchuoi(chuoi, "|")[0]
                });
            }
            if (source1.Count<modelVersion>() != 0)
            {
              int index = random.Next(0, source1.Count<modelVersion>());
              dictionary1["modelVersion"] = (object) (source1[index].name + "J/A");
              string str = dictionary1["machine"].ToString() + "|" + dictionary1["systemVersion"] + "|" + dictionary1["buildversion"];
              foreach (string chuoi in FakeInfoCore._modemlee)
              {
                if (chuoi.Contains(str))
                {
                  dictionary1["ModemVersion"] = (object) Split.tachchuoi(chuoi, "|")[3];
                  dictionary1["yearcpr"] = (object) Split.tachchuoi(chuoi, "|")[4];
                  break;
                }
              }
              try
              {
                if (!(dictionary1["ModemVersion"].ToString() == "") || dictionary1["ModemVersion"].ToString() != null)
                  break;
              }
              catch
              {
              }
            }
            else
              goto label_1;
          }
          for (int index = 0; index < ((IEnumerable<string>) FakeInfoCore._LocaleLanguages).Count<string>(); ++index)
          {
            if (FakeInfoCore._LocaleLanguages[index].IndexOf(FakeInfoCore.country) >= 0)
            {
              dictionary1["locale"] = dictionary1["AppleLocale"] = (object) Split.tachchuoi(FakeInfoCore._LocaleLanguages[index], "|")[1];
              dictionary1["lang"] = dictionary1["AppleLanguages"] = (object) Split.tachchuoi(FakeInfoCore._LocaleLanguages[index], "|")[2];
              break;
            }
          }
          try
          {
            if (dictionary1["AppleLocale"].ToString() == "" || dictionary1["AppleLocale"].ToString() == null || dictionary1["AppleLanguages"].ToString() == "" || dictionary1["AppleLanguages"].ToString() == null)
            {
              int num = (int) MessageBox.Show("Chua add GEO nay");
            }
          }
          catch
          {
            int num = (int) MessageBox.Show("Chua add GEO nay");
            continue;
          }
          dictionary1["BSSID"] = (object) FakeInfoCore.CreateBSSIDbylee();
          dictionary1["aliMac"] = (object) dictionary1["BSSID"].ToString();
          dictionary1["aliImei"] = (object) FakeInfoCore.TOSHA1(dictionary1["aliMac"].ToString());
          return dictionary1;
        }
        catch (Exception ex)
        {
        }
      }
    }

    private static string TOSHA1(string v)
    {
      byte[] hash = new SHA1CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(v));
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < hash.Length; ++index)
        stringBuilder.Append(hash[index].ToString("x2"));
      return stringBuilder.ToString();
    }

    private static string CreateBSSIDbylee()
    {
      return (string) null + GlobalConfig.RandomString("0123456789abcde", 2) + ":" + GlobalConfig.RandomString("0123456789abcde", 2) + ":" + GlobalConfig.RandomString("0123456789abcde", 2) + ":" + GlobalConfig.RandomString("0123456789abcde", 2) + ":" + GlobalConfig.RandomString("0123456789abcde", 2) + ":" + GlobalConfig.RandomString("0123456789abcde", 2);
    }

    private static string CreateImeibylee()
    {
      string[] strArray1 = new string[3]{ "35", "01", "99" };
      string[] strArray2 = new string[13]
      {
        "00",
        "05",
        "78",
        "79",
        "06",
        "73",
        "82",
        "28",
        "27",
        "74",
        "80",
        "83",
        "77"
      };
      string str1 = strArray1[new Random().Next(0, strArray1.Length)];
      string str2 = strArray2[new Random().Next(0, strArray2.Length)];
      string str3;
      int num1;
      if (str1 == "99")
      {
        str3 = "0002";
      }
      else
      {
        num1 = new Random().Next(3111, 8999);
        str3 = num1.ToString();
      }
      string str4 = GlobalConfig.RandomString("123456789", 6);
      string str5 = str1 + str3 + str2 + str4;
      int num2 = 0;
      char ch;
      for (int index = 0; index < 14; ++index)
      {
        ch = str5[index];
        int int32_1 = Convert.ToInt32(ch.ToString());
        if ((uint) (index % 2) > 0U)
        {
          int num3 = int32_1 * 2;
          if (num3 < 10)
          {
            num2 += num3;
          }
          else
          {
            string str6 = num3.ToString();
            int num4 = num2;
            ch = str6[0];
            int int32_2 = Convert.ToInt32(ch.ToString());
            ch = str6[1];
            int int32_3 = Convert.ToInt32(ch.ToString());
            int num5 = int32_2 + int32_3;
            num2 = num4 + num5;
          }
        }
        else
          num2 += int32_1;
      }
      string source = num2.ToString();
      ch = source[source.Count<char>() - 1];
      num1 = 10 - Convert.ToInt32(ch.ToString());
      string str7 = num1.ToString();
      return str1 + " " + str3 + str2 + " " + str4 + " " + str7;
    }

    public static int GetRandomNumber(int min, int max)
    {
      FakeInfoCore.getrandom = new Random();
      FakeInfoCore.syncLock = new object();
      int num;
      lock (FakeInfoCore.syncLock)
        num = FakeInfoCore.getrandom.Next(min, max);
      return num;
    }
  }
}
