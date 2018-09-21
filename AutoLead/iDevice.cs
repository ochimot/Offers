// Decompiled with JetBrains decompiler
// Type: AutoLead.iDevice
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using AutoLeadGUI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;

namespace AutoLead
{
  public class iDevice
  {
    private List<deviceScreens> listscreens = new List<deviceScreens>();
    private List<countrycodeiOS> listcountrycodeiOS = new List<countrycodeiOS>();
    private List<Carrier> carrierList = new List<Carrier>();
    private static readonly Random getrandom = new Random();
    private static readonly object syncLock = new object();
    public const string aliexpressver = "5.3.1";

    public string advertisingID { get; set; }

    public string afUid { get; set; }

    public string aliDeviceID { get; }

    public string buildversion { get; set; }

    public Carrier carrier { get; set; }

    public string country { get; set; }

    public string countryCode { get; set; }

    public string currency { get; set; }

    public int height { get; set; }

    public string language { get; set; }

    public double latude { get; set; }

    public string loginId { get; set; }

    public double longtude { get; set; }

    public string model { get; set; }

    public string modelType { get; set; }

    public string name { get; set; }

    public double scale { get; set; }

    public string timezone { get; set; }

    public string version { get; set; }

    public int width { get; set; }

    public iDevice()
    {
      this.loadcountrycodeiOS();
      this.loadcarrier();
      this.loadscreen();
    }

    public iDevice(string name, string version, string buildversion, string model, string ipfake)
    {
      this.loadcountrycodeiOS();
      this.randomdevice(name, version, buildversion, model);
      this.setCountryByIP(ipfake);
      this.advertisingID = Guid.NewGuid().ToString().ToUpper();
      this.aliDeviceID = this.generateAliDeviceID();
      this.loginId = "";
      this.afUid = this.GetafUid();
    }

    private string GetafUid()
    {
      return Guid.NewGuid().ToString().Substring(0, 16).ToUpper() + "-0" + (object) new Random().Next(1000, 9999);
    }

    public AliParameter AliActiveParameter(string countrycode)
    {
      AliParameter aliParameter = this.getAliParameter("active", countrycode);
      aliParameter.addParameter("referrer", "utmContent%3D" + this.advertisingID);
      aliParameter.addParameter("umidToken", this.aliDeviceID);
      return aliParameter;
    }

    public AliParameter AliOpenParameter(string countrycode)
    {
      return this.getAliParameter("open", countrycode);
    }

    public AliParameter AliSettingParameter(string countrycode)
    {
      return this.getAliParameter("setting", countrycode);
    }

    private string generateAliDeviceID()
    {
      string text = Guid.NewGuid().ToString();
      double totalSeconds = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
      MemoryStream memoryStream = new MemoryStream();
      memoryStream.Write(((IEnumerable<byte>) BitConverter.GetBytes((uint) totalSeconds)).Reverse<byte>().ToArray<byte>(), 0, 4);
      memoryStream.Write(generateDeviceID.randombyte(), 0, 1);
      memoryStream.Write(generateDeviceID.randombyte(), 0, 1);
      memoryStream.Write(generateDeviceID.randombyte(), 0, 1);
      memoryStream.Write(generateDeviceID.randombyte(), 0, 1);
      memoryStream.Write(new byte[2]{ (byte) 3, (byte) 0 }, 0, 2);
      memoryStream.Write(((IEnumerable<byte>) BitConverter.GetBytes(generateDeviceID.hashCode(text))).Reverse<byte>().ToArray<byte>(), 0, 4);
      memoryStream.Write(((IEnumerable<byte>) BitConverter.GetBytes(generateDeviceID.hashCode(generateDeviceID.hmacBase64Value(memoryStream.ToArray(), "d6fc3a4a06adbde89223bvefedc24fecde188aa")))).Reverse<byte>().ToArray<byte>(), 0, 4);
      return Convert.ToBase64String(memoryStream.ToArray());
    }

    public string GetAliHeaderRequest()
    {
      return "Accept-Language: " + this.language + "-" + this.countryCode + "\r\nq: 1" + "\r\nContent-Type: application/x-www-form-UrlPathEncoded" + "\r\nUser-Agent: " + this.GetAliUserAgent();
    }

    public AliParameter getAliParameter(string lifecycle, string countrycode)
    {
      string upper = Guid.NewGuid().ToString().ToUpper();
      AliParameter aliParameter = new AliParameter();
      aliParameter.addParameter("_aop_nonce", upper);
      aliParameter.addParameter("_currency", this.currency);
      aliParameter.addParameter("_lang", this.language + "_" + this.countryCode);
      aliParameter.addParameter("adId", this.advertisingID);
      aliParameter.addParameter("carrier", Operation.dict["carrier"].ToString());
      aliParameter.addParameter("clientName", "AliExpress");
      aliParameter.addParameter("clientVersion", "5.3.1");
      aliParameter.addParameter("country", countrycode);
      aliParameter.addParameter("deviceId", this.aliDeviceID);
      aliParameter.addParameter("deviceModel", "iPhone");
      aliParameter.addParameter("language", Operation.dict["lang"].ToString());
      aliParameter.addParameter(nameof (lifecycle), lifecycle);
      aliParameter.addParameter("loginId", this.loginId);
      aliParameter.addParameter("osName", "iPhone OS");
      aliParameter.addParameter("osVersion", this.version);
      return aliParameter;
    }

    public string GetAliUserAgent()
    {
      return string.Format("AIFAPIRequest/{0} ({1}; iOS {2}; Scale/{3}.00)", (object) "1.0.0", (object) "iPhone", (object) this.version, (object) "2");
    }

    public countrycodeiOS getCountryInfoByCountryCode(string countryCode)
    {
      return this.listcountrycodeiOS.First<countrycodeiOS>((Func<countrycodeiOS, bool>) (x => x.countrycode == countryCode));
    }

    private ipData getIPData(string IpAddress)
    {
      ipData ipData;
      try
      {
        ipData = (ipData) new DataContractJsonSerializer(typeof (ipData)).ReadObject((Stream) new MemoryStream(Encoding.UTF8.GetBytes(new WebClient().DownloadString("http://pro.ip-api.com/json/" + IpAddress + "?key=kISXZimHUJwHgV7"))));
      }
      catch (Exception ex)
      {
        ipData = (ipData) null;
      }
      return ipData;
    }

    public countrycodeiOS getRandomCountryInfo()
    {
      return this.listcountrycodeiOS[new Random().Next(0, this.listcountrycodeiOS.Count)];
    }

    public static int GetRandomNumber(int min, int max)
    {
      int num;
      lock (iDevice.syncLock)
        num = iDevice.getrandom.Next(min, max);
      return num;
    }

    public string GetUserAgent()
    {
      string str1 = this.version.First<char>().ToString() + ".0";
      string str2 = str1 == "9.0" ? "601.1" : "600.1.4";
      string str3 = str1 == "9.0" ? "601.1.46" : "600.1.4";
      object[] objArray1 = new object[7];
      objArray1[0] = (object) this.modelType;
      object[] objArray2 = objArray1;
      objArray2[1] = this.modelType == "iPad" ? (object) "" : (object) " iPhone";
      objArray2[2] = (object) this.version.Replace(".", "_");
      objArray2[3] = (object) str3;
      objArray2[4] = (object) str1;
      objArray2[5] = (object) this.buildversion;
      objArray2[6] = (object) str2;
      return string.Format("Mozilla/5.0 ({0}; CPU{1} OS {2} like Mac OS X) AppleWebKit/{3} (KHTML, like Gecko) Version/{4} Mobile/{5} Safari/{6}", objArray2);
    }

    public static string executableDirectory()
    {
      return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }

    private void loadcarrier()
    {
      System.IO.File.ReadAllLines(iDevice.executableDirectory() + "\\carrierlist.txt");
      foreach (string readAllLine in System.IO.File.ReadAllLines(iDevice.executableDirectory() + "\\carrierlist.txt"))
      {
        string[] separator = new string[1]{ "||" };
        int num = 0;
        string[] strArray = readAllLine.Split(separator, (StringSplitOptions) num);
        this.carrierList.Add(new Carrier()
        {
          CarrierName = strArray[0],
          country = strArray[1],
          ISOCountryCode = strArray[2],
          mobileCarrierCode = strArray[3],
          mobileCountryCode = strArray[4]
        });
      }
    }

    private void loadcountrycodeiOS()
    {
      string str1 = "AD\r\nAE|ar\r\nAF\r\nAG|en\r\nAI\r\nAL|ar\r\nAM\r\nAO\r\nAQ\r\nAR|es\r\nAS\r\nAT|de\r\nAU|en-AU\r\nAW\r\nAX\r\nAZ\r\nBA\r\nBB\r\nBD\r\nBE\r\nBF\r\nBG\r\nBH|ar\r\nBI\r\nBJ\r\nBL\r\nBM\r\nBN\r\nBO|es\r\nBQ\r\nBR|pt-BR\r\nBS\r\nBT\r\nBV\r\nBW\r\nBY\r\nBZ\r\nCA|en\r\nCC\r\nCD|ar\r\nCF\r\nCG\r\nCH|de\r\nCI\r\nCK\r\nCL|es\r\nCM\r\nCN|zh-Hant\r\nCO|es\r\nCR|es\r\nCU\r\nCV\r\nCW\r\nCX\r\nCY\r\nCZ|cs\r\nDE|de\r\nDJ\r\nDK|da\r\nDM\r\nDO|es\r\nDZ|ar\r\nEC|es\r\nEE\r\nEG|ar\r\nEH\r\nER\r\nES|es\r\nET\r\nFI|fi\r\nFJ\r\nFK\r\nFM\r\nFO\r\nFR|fr\r\nGA\r\nGB|en-GB\r\nGD\r\nGE\r\nGF\r\nGG\r\nGH\r\nGI\r\nGL\r\nGM\r\nGN\r\nGP\r\nGQ\r\nGR|el\r\nGS\r\nGT|es\r\nGU\r\nGW\r\nGY\r\nHK|zh-HK\r\nHM\r\nHN|es\r\nHR|hr\r\nHT\r\nHU|hu\r\nID|id\r\nIE\r\nIL|he\r\nIM\r\nIN|en-IN\r\nIO\r\nIQ|ar\r\nIR\r\nIS\r\nIT|it\r\nJE\r\nJM\r\nJO|ar\r\nJP|ja\r\nKE|en\r\nKG\r\nKH\r\nKI\r\nKM\r\nKN\r\nKP\r\nKR|ko\r\nKW|ar\r\nKY\r\nKZ\r\nLA\r\nLB|ar\r\nLC\r\nLI\r\nLK\r\nLR\r\nLS\r\nLT\r\nLU|fr\r\nLV\r\nLY\r\nMA|ar\r\nMC|fr\r\nMD\r\nME\r\nMF\r\nMG\r\nMH\r\nMK\r\nML\r\nMM\r\nMN\r\nMO|zh-Hant\r\nMP\r\nMQ\r\nMR\r\nMS\r\nMT\r\nMU\r\nMV\r\nMW\r\nMX|es-MX\r\nMY|ms\r\nMZ\r\nNA\r\nNC\r\nNE\r\nNF\r\nNG\r\nNI|es\r\nNL|nl\r\nNO|nb\r\nNP\r\nNR\r\nNU\r\nNZ\r\nOM|ar\r\nPA|es\r\nPE|es\r\nPF\r\nPG\r\nPH\r\nPK\r\nPL|pl\r\nPM\r\nPN\r\nPR|es\r\nPS\r\nPT|pt\r\nPW\r\nPY|es\r\nQA|ar\r\nRE\r\nRO|ro\r\nRS\r\nRU|ru\r\nRW\r\nSA|ar\r\nSB\r\nSC\r\nSD\r\nSE|sv\r\nSG|zh-Hant\r\nSH\r\nSI\r\nSJ\r\nSK|sk\r\nSL\r\nSM\r\nSN\r\nSO\r\nSR\r\nSS\r\nST\r\nSV|es\r\nSX\r\nSY|ar\r\nSZ\r\nTC\r\nTD\r\nTF\r\nTG\r\nTH|th\r\nTJ\r\nTK\r\nTL\r\nTM\r\nTN|ar\r\nTO\r\nTR|tr\r\nTT\r\nTV\r\nTW|zh-Hant\r\nTZ\r\nUA|uk\r\nUG\r\nUM\r\nUS|en\r\nUY|es\r\nUZ\r\nVA\r\nVC\r\nVE|es\r\nVG\r\nVI\r\nVN|vi\r\nVU\r\nWF\r\nWS\r\nYE|ar\r\nYT\r\nZA|ar\r\nZM\r\nZW";
      string[] separator = new string[1]{ "\r\n" };
      int num = 0;
      foreach (string str2 in str1.Split(separator, (StringSplitOptions) num))
      {
        try
        {
          string[] strArray = str2.Split(new string[1]
          {
            "|"
          }, StringSplitOptions.None);
          RegionInfo regionInfo = new RegionInfo(strArray[0]);
          countrycodeiOS countrycodeiOs = new countrycodeiOS()
          {
            countrycode = strArray[0],
            countryname = regionInfo.EnglishName
          };
          countrycodeiOs.languageCode = ((IEnumerable<string>) strArray).Count<string>() == 1 ? "en" : strArray[1];
          countrycodeiOs.currency = regionInfo.ISOCurrencySymbol;
          this.listcountrycodeiOS.Add(countrycodeiOs);
        }
        catch (Exception ex)
        {
        }
      }
    }

    private void loadscreen()
    {
      foreach (string readAllLine in System.IO.File.ReadAllLines(iDevice.executableDirectory() + "\\deviceScreen.txt"))
      {
        string[] separator = new string[1]{ " " };
        int num = 0;
        string[] strArray = readAllLine.Split(separator, (StringSplitOptions) num);
        this.listscreens.Add(new deviceScreens()
        {
          model = strArray[0],
          width = Convert.ToDouble(strArray[1]),
          heigh = Convert.ToDouble(strArray[2]),
          scale = Convert.ToDouble(strArray[3])
        });
      }
    }

    private void randomdevice(string name, string version, string buildversion, string model)
    {
      this.name = name;
      this.version = version;
      this.buildversion = buildversion;
      this.model = model;
      this.width = 320;
      this.height = 568;
      this.scale = 87.0;
    }

    public void setCountryByIP(string IP)
    {
      ipData ipData = this.getIPData(IP);
      if (ipData == null)
        return;
      this.timezone = ipData.timezone;
      countrycodeiOS infoByCountryCode = this.getCountryInfoByCountryCode(ipData.countryCode);
      this.country = infoByCountryCode.countryname;
      this.countryCode = infoByCountryCode.countrycode;
      this.currency = infoByCountryCode.currency;
      this.language = infoByCountryCode.languageCode;
      double num1 = (double) iDevice.GetRandomNumber(-10000, 10000) / 100000.0;
      double num2 = (double) iDevice.GetRandomNumber(-10000, 10000) / 100000.0;
      this.latude = ipData.lat + num1;
      this.longtude = ipData.lon + num2;
    }
  }
}
