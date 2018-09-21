// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.AppURLToAppID
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace AutoLeadGUI
{
  internal class AppURLToAppID
  {
    private static Dictionary<string, object> urlCache = (Dictionary<string, object>) null;
    private static JavaScriptSerializer jss = new JavaScriptSerializer();

    private static string storeIDFromURL(string url)
    {
      return ((IEnumerable<string>) GlobalConfig.stringSplit(((IEnumerable<string>) GlobalConfig.stringSplit(url, "/")).Last<string>(), "?")).First<string>().Replace("id", "");
    }

    public static string AppIDFromURL(string url)
    {
      string str1 = "";
      if (AppURLToAppID.urlCache == null)
      {
        try
        {
          string input = System.IO.File.ReadAllText(LocalConfig.getCurrentConfig().configDirectory() + "\\url.list");
          AppURLToAppID.urlCache = AppURLToAppID.jss.Deserialize<Dictionary<string, object>>(input);
        }
        catch
        {
        }
        if (AppURLToAppID.urlCache == null)
          AppURLToAppID.urlCache = new Dictionary<string, object>();
      }
      if (AppURLToAppID.urlCache.ContainsKey(url))
        return AppURLToAppID.urlCache[url].ToString();
      try
      {
        string input = (string) null;
        using (HttpWebResponse response = (HttpWebResponse) WebRequest.Create("http://itunes.apple.com/lookup?id=" + AppURLToAppID.storeIDFromURL(url)).GetResponse())
        {
          using (Stream responseStream = response.GetResponseStream())
          {
            using (StreamReader streamReader = new StreamReader(responseStream))
              input = streamReader.ReadToEnd();
          }
        }
        if (input != null)
        {
          string str2 = ((Dictionary<string, object>) ((ArrayList) AppURLToAppID.jss.Deserialize<Dictionary<string, object>>(input)["results"])[0])["bundleId"].ToString();
          AppURLToAppID.urlCache[url] = (object) str2;
          System.IO.File.WriteAllText(LocalConfig.getCurrentConfig().configDirectory() + "\\url.list", AppURLToAppID.jss.Serialize((object) AppURLToAppID.urlCache));
          str1 = str2;
        }
      }
      catch
      {
        str1 = "";
      }
      return str1;
    }

    internal static string AppIDFromSiteLee(string url)
    {
      string str1 = "";
      try
      {
        string input = (string) null;
        using (HttpWebResponse response = (HttpWebResponse) WebRequest.Create("http://45.77.21.123/detectoff.html").GetResponse())
        {
          using (Stream responseStream = response.GetResponseStream())
          {
            using (StreamReader streamReader = new StreamReader(responseStream))
            {
              input = streamReader.ReadToEnd();
              input = input.Replace("\r\n", "").Replace("\n", "").Replace("\\", "");
            }
          }
        }
        if (input != null)
        {
          ArrayList arrayList = AppURLToAppID.jss.Deserialize<ArrayList>(input);
          for (int index = 0; index < arrayList.Count; ++index)
          {
            Dictionary<string, object> dictionary = (Dictionary<string, object>) arrayList[index];
            string str2 = dictionary[nameof (url)].ToString();
            if (url == str2)
            {
              str1 = dictionary["id"].ToString();
              break;
            }
          }
        }
      }
      catch
      {
        int num = (int) MessageBox.Show("Không thể kết nối tới site detect app");
        str1 = "";
      }
      return str1;
    }
  }
}
