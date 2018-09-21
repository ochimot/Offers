// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.dv.AliChilkat
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using AutoLead;
using Chilkat;
using soft;
using System;

namespace AutoLeadGUI.dv
{
  public class AliChilkat
  {
    public static void SentCheat()
    {
    }

    public static void test()
    {
      Http http = new Http();
      http.UnlockComponent("VTMCMH.CB10917_4Vc9gp5C55lc");
      http.SocksHostname = "192.168.1.8";
      http.SocksPort = Convert.ToInt32(1235);
      http.SocksVersion = 5;
      HttpRequest req = new HttpRequest();
      req.HttpVerb = "GET";
      req.SendCharset = false;
      req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
      req.AddHeader("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 10_1_1 like Mac OS X) AppleWebKit/602.1.50 (KHTML, like Gecko) Version/10.0 Mobile/14B150 Safari/602.1");
      req.Path = "/aff_c?offer_id=81700&aff_id=1250";
      http.FollowRedirects = true;
      HttpResponse httpResponse = http.SynchronousRequest("tracking.affixate.com", 80, false, req);
      if (httpResponse == null)
      {
        Console.WriteLine(http.LastErrorText);
      }
      else
      {
        string[] strArray = Split.tachchuoi(httpResponse.FinalRedirectUrl, ".com/");
        AliChilkat.test2(strArray[0], strArray[1]);
      }
    }

    public static void test2(string s1, string s2)
    {
      Http http = new Http();
      http.UnlockComponent("VTMCMH.CB10917_4Vc9gp5C55lc");
      http.SocksHostname = "192.168.1.8";
      http.SocksPort = Convert.ToInt32(1235);
      http.SocksVersion = 5;
      HttpRequest req = new HttpRequest();
      req.HttpVerb = "GET";
      req.SendCharset = false;
      req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
      req.AddHeader("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 10_1_1 like Mac OS X) AppleWebKit/602.1.50 (KHTML, like Gecko) Version/10.0 Mobile/14B150 Safari/602.1");
      req.Path = "/" + s2;
      http.FollowRedirects = true;
      s1 += ".com";
      s1 = s1.Replace("http://", "");
      HttpResponse httpResponse = http.SynchronousRequest(s1, 80, false, req);
      if (httpResponse == null)
      {
        Console.WriteLine(http.LastErrorText);
      }
      else
      {
        string[] strArray = Split.tachchuoi(httpResponse.BodyStr, ".com/");
        AliChilkat.test2(strArray[0], strArray[1]);
      }
    }

    internal static void SentCheat(AliParameter aliParameter3, AliParameter aliParameter2, AliParameter aliParameter, iDevice iDevice, commandResult cmdResult, string ip, string port)
    {
      Http http = new Http();
      http.UnlockComponent("VTMCMH.CB10917_4Vc9gp5C55lc");
      http.SocksHostname = ip;
      http.SocksPort = Convert.ToInt32(port);
      http.SocksVersion = 5;
      HttpRequest req1 = new HttpRequest();
      req1.HttpVerb = "POST";
      req1.SendCharset = false;
      req1.AddHeader("content-type", "application/x-www-form-urlencoded");
      req1.AddHeader("accept-language", iDevice.language + "-" + iDevice.countryCode + ";q=1");
      req1.AddHeader("user-agent", iDevice.GetAliUserAgent());
      req1.LoadBodyFromString("_aop_nonce=BE6EBD8F-FD9D-4A3F-9E04-7EAB9E19AC08&_aop_signature=2DAE2B7AA159A2195303144DB3657156A0958562&_currency=USD&_lang=en_US&activateTime=2147483647&adId=C1AE31EA-8152-4F76-8F4E-EC4941F19E2C&carrier=SLO%20Cellular%20Inc%20%2F%20Cellular%20One%20of%20San%20Luis&clientName=AliExpress&clientVersion=5.1.6&country=US&deviceId=WH8xrU03%2B2oDAIflU2kgOQWR&deviceMode=iPhone&language=en&lat=1&lifecycle=setting&localtime=2017-01-18%2001%3A13%3A22&osName=iPhone%20OS&osVersion=8.0.1&timezone=America%2FLos_Angeles", "utf-8");
      req1.Path = "/openapi/param2/101/aliexpress.mobile/deviceInfo/8495";
      http.FollowRedirects = true;
      HttpResponse httpResponse1 = http.SynchronousRequest("api.aliexpress.com", 443, true, req1);
      if (httpResponse1 == null)
      {
        Console.WriteLine(http.LastErrorText);
      }
      else
      {
        string bodyStr1 = httpResponse1.BodyStr;
      }
      HttpRequest req2 = new HttpRequest();
      req2.HttpVerb = "POST";
      req2.SendCharset = false;
      req2.AddHeader("content-type", "application/x-www-form-urlencoded");
      req2.AddHeader("accept-language", iDevice.language + "-" + iDevice.countryCode + ";q=1");
      req2.AddHeader("User-Agent", iDevice.GetAliUserAgent());
      req2.LoadBodyFromString("_aop_nonce=82B91EAC-E831-4772-9201-123E3C42F2F1&_aop_signature=7D89B30AB19F226A8910864B82FE323FBEA650CD&_currency=USD&_lang=en_US&activateTime=1500884105356&adid=F813A1B4-FC96-4775-A71F-333EAB0EB930&afCounter=1&afUid=B0733E4D-5D89-40-05635&carrier=Cambridge%20Telephone%20Company%20Inc.&clientName=AliExpress&clientVersion=5.3.1&country=US&deviceId=WXWsg40QVRIDAAMDdWRD69Yt&deviceMode=iPhone&language=en&lat=1&lifecycle=active&localtime=2017-07-24%2002%3A15%3A05&osName=iPhone%20OS&osVersion=9.3.2&referrer=utmContent%253DC1FBD82B-1967-42ED-9D5A-C43CD40B51DE&timezone=America/Denver&umidToken=WXWsg40QVRIDAAMDdWRD69Yt", "utf-8");
      req2.Path = "/openapi/param2/101/aliexpress.mobile/deviceInfo/8495";
      http.FollowRedirects = true;
      HttpResponse httpResponse2 = http.SynchronousRequest("api.aliexpress.com", 443, true, req2);
      if (httpResponse2 == null)
      {
        Console.WriteLine(http.LastErrorText);
      }
      else
      {
        string bodyStr2 = httpResponse2.BodyStr;
      }
    }
  }
}
