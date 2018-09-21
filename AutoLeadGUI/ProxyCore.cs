// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.ProxyCore
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace AutoLeadGUI
{
  internal class ProxyCore
  {
    private static Process proSSH = (Process) null;
    private static IntPtr __allProxiesHandle = IntPtr.Zero;
    private static Vip72Handle __currentVip72Handle = (Vip72Handle) null;
    private static string __country = (string) null;
    private static string __region = (string) null;
    private static string __preferIp = (string) null;
    private static ProxyTool __tool = ProxyTool.Vip72;
    private static List<string> __selectedVip72IP = new List<string>();
    private static int __selectedVip72Index = -1;
    private static List<List<string>> __vip72ProxiesList = (List<List<string>>) null;
    private static Process __process = (Process) null;
    private static Vip72Handle vip72h = (Vip72Handle) null;
    private static IntPtr handle = new IntPtr(-1);
    public static Dictionary<string, object> previousSshInfo = (Dictionary<string, object>) null;

    public static void closeIfNeeds()
    {
      ProxyCore.closeCurrent();
    }

    public static void writeLog(string log)
    {
    }

    private static bool compare_vip72ip(string a, string b)
    {
      string[] strArray1 = GlobalConfig.stringSplit(a, ".");
      string[] strArray2 = GlobalConfig.stringSplit(b, ".");
      return strArray2.Length >= 3 && strArray1.Length >= 3 && (strArray1[0].Equals(strArray2[0]) && strArray1[1].Equals(strArray2[1]) && strArray1[2].Equals(strArray2[2]));
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

    public static void kill(Process process)
    {
      foreach (Process process1 in Process.GetProcessesByName(process.ProcessName))
      {
        if (process1.MainModule.FileName.Equals(process.MainModule.FileName))
        {
          process1.CloseMainWindow();
          ProxyCore.TerminateProcess(process1.Handle, 0U);
        }
      }
    }

    public static bool selectNextIP(string referIP, out string nextIP)
    {
      if (ProxyCore.__tool == ProxyTool.Vip72)
      {
        nextIP = (string) null;
        GlobalConfig.currentSSH = (Dictionary<string, object>) null;
        if (ProxyCore.__allProxiesHandle != IntPtr.Zero && ProxyCore.__currentVip72Handle != null)
        {
          ProxyCore.__currentVip72Handle.showWindow();
          ProxyCore.__allProxiesHandle = ProxyCore.__currentVip72Handle.GetWindowHandle(Vip72Handle.Vip72Window.AllProxiesListView);
          int itemCount = Vip72Handle.ListView_GetItemCount(ProxyCore.__allProxiesHandle);
          int index1;
          if (referIP != null)
          {
            ProxyCore.__currentVip72Handle.SendLeftClickToWindow(Vip72Handle.Vip72Window.MyProxies);
            IntPtr windowHandle = ProxyCore.__currentVip72Handle.GetWindowHandle(Vip72Handle.Vip72Window.MyProxiesListView);
            for (int count = Vip72Handle.GetListViewItems(windowHandle).Count; count > 0; --count)
              Vip72Handle.deleteListViewItem(windowHandle, 0);
            ProxyCore.__currentVip72Handle.SendLeftClickToWindow(Vip72Handle.Vip72Window.AllProxies);
            ProxyCore.__vip72ProxiesList = Vip72Handle.GetListViewItems(ProxyCore.__allProxiesHandle);
            int num = -1;
            string text = (string) null;
            for (int index2 = 0; index2 < ProxyCore.__vip72ProxiesList.Count; ++index2)
            {
              List<string> vip72Proxies = ProxyCore.__vip72ProxiesList[index2];
              if (ProxyCore.compare_vip72ip(vip72Proxies[0], referIP))
              {
                num = index2;
                text = vip72Proxies[0];
                break;
              }
            }
            if (num == -1)
              num = GlobalConfig.random.Next(ProxyCore.__vip72ProxiesList.Count);
            string[] strArray = GlobalConfig.stringSplit(GlobalConfig.stringSplit(text, ",")[0].Trim(), ".");
            ProxyCore.__selectedVip72IP.Remove(strArray[0] + "." + strArray[1] + "." + strArray[2] + ".***");
            ProxyCore.__selectedVip72IP.Add(strArray[0] + "." + strArray[1] + "." + strArray[2] + ".***");
            index1 = num;
          }
          else
          {
            ProxyCore.__currentVip72Handle.SendLeftClickToWindow(Vip72Handle.Vip72Window.MyProxies);
            IntPtr windowHandle = ProxyCore.__currentVip72Handle.GetWindowHandle(Vip72Handle.Vip72Window.MyProxiesListView);
            for (int count = Vip72Handle.GetListViewItems(windowHandle).Count; count > 0; --count)
              Vip72Handle.deleteListViewItem(windowHandle, 0);
            ProxyCore.__currentVip72Handle.SendLeftClickToWindow(Vip72Handle.Vip72Window.AllProxies);
            ProxyCore.__vip72ProxiesList = Vip72Handle.GetListViewItems(ProxyCore.__allProxiesHandle);
            string text1 = (string) null;
            List<int> intList = new List<int>();
            for (int index2 = 0; index2 < ProxyCore.__vip72ProxiesList.Count; ++index2)
            {
              string[] strArray = GlobalConfig.stringSplit(ProxyCore.__vip72ProxiesList[index2][0], ".");
              string str = strArray[0] + "." + strArray[1] + "." + strArray[2] + ".***";
              if (!ProxyCore.__selectedVip72IP.Contains(str))
                intList.Add(index2);
            }
            int index3;
            if (intList.Count == 0)
            {
              index3 = -1;
            }
            else
            {
              index3 = intList[GlobalConfig.random.Next(intList.Count)];
              string[] strArray = GlobalConfig.stringSplit(ProxyCore.__vip72ProxiesList[index3][0], ".");
              text1 = strArray[0] + "." + strArray[1] + "." + strArray[2] + ".***";
            }
            if (index3 == -1)
            {
              ProxyCore.__selectedVip72IP.Clear();
              index3 = GlobalConfig.random.Next(ProxyCore.__vip72ProxiesList.Count);
              string[] strArray = GlobalConfig.stringSplit(ProxyCore.__vip72ProxiesList[index3][0], ".");
              text1 = strArray[0] + "." + strArray[1] + "." + strArray[2] + ".***";
            }
            string text2 = GlobalConfig.stringSplit(text1, ",")[0].Trim();
            string[] strArray1 = GlobalConfig.stringSplit(text2, ".");
            ProxyCore.__selectedVip72IP.Remove(strArray1[0] + "." + strArray1[1] + "." + strArray1[2] + ".***");
            ProxyCore.__selectedVip72IP.Add(strArray1[0] + "." + strArray1[1] + "." + strArray1[2] + ".***");
            index1 = index3;
            ProxyCore.writeLog("\r\nWill select ip: " + text2 + " index: " + (object) index3);
          }
          if (itemCount <= index1)
            return false;
          ProxyCore.__currentVip72Handle.SendLeftClickToWindow(Vip72Handle.Vip72Window.MyProxies);
          IntPtr windowHandle1 = ProxyCore.__currentVip72Handle.GetWindowHandle(Vip72Handle.Vip72Window.MyProxiesListView);
          for (int count = Vip72Handle.GetListViewItems(windowHandle1).Count; count > 0; --count)
            Vip72Handle.deleteListViewItem(windowHandle1, 0);
          ProxyCore.__currentVip72Handle.SendLeftClickToWindow(Vip72Handle.Vip72Window.AllProxies);
          bool flag = false;
          IntPtr windowHandle2 = ProxyCore.__currentVip72Handle.GetWindowHandle(Vip72Handle.Vip72Window.CurrentSock);
          string b = Vip72Handle.GetControlText(windowHandle2);
          List<List<string>> listViewItems = Vip72Handle.GetListViewItems(ProxyCore.__allProxiesHandle);
          ProxyCore.writeLog("\r\n*** IP1: " + listViewItems[index1][0]);
          ProxyCore.writeLog("\r\n*** Begin select ***");
          Vip72Handle.selectListViewItem(ProxyCore.__allProxiesHandle, index1, true);
          ProxyCore.writeLog("\r\n*** End select ***");
          string a = listViewItems[index1][0];
          ProxyCore.writeLog("\r\n*** IP2: " + a);
          while (!flag)
          {
            if (ProxyCore.__currentVip72Handle.GetWindowHandle(Vip72Handle.Vip72Window.CurrentSock).ToInt32() > 0)
            {
              string controlText = Vip72Handle.GetControlText(ProxyCore.__currentVip72Handle.GetWindowHandle(Vip72Handle.Vip72Window.Status));
              b = Vip72Handle.GetControlText(windowHandle2);
              if (controlText != null)
              {
                if (controlText.Contains("checked") && ProxyCore.compare_vip72ip(a, b))
                {
                  flag = true;
                  b = Vip72Handle.GetControlText(windowHandle2);
                }
                else if (controlText.Contains("ERROR") || controlText.Contains("DISCONNECT"))
                {
                  flag = true;
                  b = (string) null;
                }
                else
                  Thread.Sleep(200);
              }
              else
              {
                flag = true;
                b = (string) null;
              }
            }
            else
            {
              flag = true;
              b = (string) null;
            }
          }
          ProxyCore.__currentVip72Handle.hideWindow();
          if (b != null)
          {
            nextIP = b;
            return true;
          }
        }
        return false;
      }
      nextIP = (string) null;
      if (ProxyCore.previousSshInfo != null)
      {
        Dictionary<string, object> previousSshInfo = ProxyCore.previousSshInfo;
        LocalConfig.getCurrentConfig().getLiveSSH(ProxyCore.__country, false, GlobalConfig.SSHRefresh);
        ProxyCore.previousSshInfo = (Dictionary<string, object>) null;
        if (previousSshInfo == null)
          return false;
        ProxyCore.setupProxy(ProxyCore.__tool, new IndexItem(0, previousSshInfo)
        {
          hostKeyFp = previousSshInfo["fp"].ToString()
        }, out nextIP);
        GlobalConfig.currentSSH = previousSshInfo;
        return true;
      }
      Dictionary<string, object> liveSsh = LocalConfig.getCurrentConfig().getLiveSSH(ProxyCore.__country, true, GlobalConfig.SSHRefresh);
      if (liveSsh == null)
        return false;
      ProxyCore.setupProxy(ProxyCore.__tool, new IndexItem(0, liveSsh)
      {
        hostKeyFp = liveSsh["fp"].ToString()
      }, out nextIP);
      GlobalConfig.currentSSH = liveSsh;
      return true;
    }

    public static void doChangeIP(ProxyTool tool, string country, string region, string preferIp, out string ip)
    {
      ip = (string) null;
      if (ProxyCore.__country == null || ProxyCore.__tool != tool)
      {
        ProxyCore.__tool = tool;
        ProxyCore.__country = country;
        LocalConfig.getCurrentConfig().refreshLiveSSHs(country);
        ProxyCore.__selectedVip72Index = -1;
        ProxyCore.__selectedVip72IP.Clear();
        ProxyCore.changeIP(tool, country, region, preferIp, out ip);
      }
      else if (ProxyCore.__region == null)
      {
        if (!ProxyCore.__country.Equals(country))
        {
          ProxyCore.__country = country;
          ProxyCore.__region = region;
          LocalConfig.getCurrentConfig().refreshLiveSSHs(country);
          ProxyCore.__selectedVip72Index = -1;
          ProxyCore.__selectedVip72IP.Clear();
          ProxyCore.changeIP(tool, country, region, preferIp, out ip);
        }
        else if (region != null)
        {
          ProxyCore.__country = country;
          ProxyCore.__region = region;
          LocalConfig.getCurrentConfig().refreshLiveSSHs(country);
          ProxyCore.__selectedVip72Index = -1;
          ProxyCore.__selectedVip72IP.Clear();
          ProxyCore.changeIP(tool, country, region, preferIp, out ip);
        }
        else if (!ProxyCore.selectNextIP(preferIp, out ip))
        {
          ProxyCore.__country = country;
          ProxyCore.__region = region;
          ProxyCore.__selectedVip72Index = -1;
          ProxyCore.changeIP(tool, country, region, preferIp, out ip);
        }
      }
      else if (!ProxyCore.__region.Equals(region) || !ProxyCore.__country.Equals(country))
      {
        ProxyCore.__country = country;
        ProxyCore.__region = region;
        LocalConfig.getCurrentConfig().refreshLiveSSHs(country);
        ProxyCore.__selectedVip72Index = -1;
        ProxyCore.__selectedVip72IP.Clear();
        ProxyCore.changeIP(tool, country, region, preferIp, out ip);
      }
      else
      {
        if (ProxyCore.selectNextIP(preferIp, out ip))
          return;
        ProxyCore.__country = country;
        ProxyCore.__region = region;
        ProxyCore.__selectedVip72Index = -1;
        ProxyCore.changeIP(tool, country, region, preferIp, out ip);
      }
    }

    public static void closeCurrent()
    {
      if (ProxyCore.__tool == ProxyTool.Vip72)
      {
        if (ProxyCore.__process != null)
        {
          try
          {
            ProxyCore.kill(ProxyCore.__process);
          }
          catch
          {
          }
          ProxyCore.__process = (Process) null;
        }
        ProxyCore.__currentVip72Handle = (Vip72Handle) null;
        ProxyCore.__selectedVip72IP.Clear();
        ProxyCore.__selectedVip72Index = -1;
      }
      else
      {
        if (ProxyCore.proSSH != null)
        {
          try
          {
            ProxyCore.kill(ProxyCore.proSSH);
          }
          catch
          {
          }
          ProxyCore.proSSH = (Process) null;
        }
        LocalConfig.getCurrentConfig().refreshLiveSSHs();
      }
    }

    public static void closeAll()
    {
      if (ProxyCore.__process != null)
      {
        try
        {
          ProxyCore.kill(ProxyCore.__process);
        }
        catch
        {
        }
        ProxyCore.__process = (Process) null;
      }
      ProxyCore.__currentVip72Handle = (Vip72Handle) null;
      ProxyCore.__selectedVip72IP.Clear();
      ProxyCore.__selectedVip72Index = -1;
      if (ProxyCore.proSSH != null)
      {
        try
        {
          ProxyCore.kill(ProxyCore.proSSH);
        }
        catch
        {
        }
        ProxyCore.proSSH = (Process) null;
      }
      LocalConfig.getCurrentConfig().refreshLiveSSHs();
    }

    private static List<IndexItem> matching(ProxyTool tool, string preferIp, string region, string country)
    {
      if (tool != ProxyTool.SSH)
        return new List<IndexItem>();
      Array array = (Array) LocalConfig.getCurrentConfig().allSSHs().ToArray();
      List<IndexItem> indexItemList1 = new List<IndexItem>();
      List<IndexItem> indexItemList2 = new List<IndexItem>();
      List<IndexItem> indexItemList3 = new List<IndexItem>();
      List<IndexItem> indexItemList4 = new List<IndexItem>();
      for (int index = 0; index < array.Length; ++index)
      {
        Dictionary<string, object> _info = (Dictionary<string, object>) array.GetValue(index);
        string str1 = _info["host"].ToString();
        Convert.ToInt32(_info["port"].ToString());
        _info["username"].ToString();
        _info["password"].ToString();
        string str2 = _info[nameof (country)].ToString();
        string str3 = _info[nameof (region)].ToString();
        if (str2.Equals(country))
        {
          if (preferIp != null && str1.Equals(preferIp))
            indexItemList4.Add(new IndexItem(index, _info));
          else if (region != null && str3.Equals(region))
            indexItemList3.Add(new IndexItem(index, _info));
          else
            indexItemList2.Add(new IndexItem(index, _info));
        }
      }
      indexItemList1.AddRange((IEnumerable<IndexItem>) indexItemList4);
      indexItemList1.AddRange((IEnumerable<IndexItem>) indexItemList3);
      indexItemList1.AddRange((IEnumerable<IndexItem>) indexItemList2);
      return indexItemList1;
    }

    private static void setupProxy(ProxyTool tool, IndexItem item, out string ip)
    {
      ip = (string) null;
      if (tool != ProxyTool.SSH)
        return;
      Dictionary<string, object> info = item.info;
      string str1 = info["host"].ToString();
      int int32 = Convert.ToInt32(info["port"].ToString());
      string str2 = info["username"].ToString();
      string str3 = info["password"].ToString();
      if (ProxyCore.proSSH != null)
      {
        try
        {
          ProxyCore.kill(ProxyCore.proSSH);
        }
        catch
        {
        }
        ProxyCore.proSSH = (Process) null;
      }
      LocalConfig.getCurrentConfig().updateBitviseProfileWithCurrentIP();
      ProcessStartInfo startInfo = new ProcessStartInfo();
      startInfo.WorkingDirectory = GlobalConfig.executableDirectory() + "/Tools/bitvise";
      startInfo.FileName = GlobalConfig.executableDirectory() + "/Tools/bitvise/BvSsh.exe";
      string str4 = LocalConfig.getCurrentConfig().configDirectory() + "\\ldefault.bscp";
      Console.WriteLine(item.hostKeyFp);
      startInfo.Arguments = "-profile=\"" + str4 + "\" -host=" + str1 + " -port=" + (object) int32 + " -user=" + str2 + " -password=" + str3 + " -hide=banner -hostKeyFp=\"" + item.hostKeyFp + "\" -openTerm=n -openSFTP=n -openRDP=n -loginOnStartup";
      ProxyCore.proSSH = Process.Start(startInfo);
      ip = str1 + ", " + info["country"].ToString() + ", " + info["region"].ToString();
    }

    private static void changeIP(ProxyTool tool, string country, string region, string preferIp, out string ip)
    {
      if (preferIp != null)
      {
        LocalConfig.getCurrentConfig().refreshLiveSSHs(country);
        ProxyCore.__selectedVip72Index = -1;
        ProxyCore.__selectedVip72IP.Clear();
      }
      ip = (string) null;
      ProxyCore.__preferIp = preferIp;
      ProxyCore.__country = country;
      ProxyCore.__region = region;
      ProxyCore.__tool = tool;
      switch (tool)
      {
        case ProxyTool.Vip72:
          GlobalConfig.currentSSH = (Dictionary<string, object>) null;
          Array array = (Array) LocalConfig.getCurrentConfig().allVip72s().ToArray();
          for (int index1 = 0; index1 < array.Length; ++index1)
          {
            if (index1 > ProxyCore.__selectedVip72Index)
            {
              ProxyCore.__selectedVip72Index = index1;
              bool flag = false;
              string str1 = (string) null;
              if (ProxyCore.__process == null || ProxyCore.__process.HasExited)
              {
                Dictionary<string, object> dictionary = (Dictionary<string, object>) array.GetValue(index1);
                ProxyCore.handle = new IntPtr(-1);
                if (ProxyCore.__process != null)
                {
                  try
                  {
                    ProxyCore.kill(ProxyCore.__process);
                  }
                  catch
                  {
                  }
                  ProxyCore.__process = (Process) null;
                }
                string str2 = dictionary["username"].ToString();
                string str3 = dictionary["password"].ToString();
                RegistryKey registryKey1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\AppCompatFlags\\Layers", true);
                if (registryKey1 != null)
                {
                  registryKey1.SetValue(GlobalConfig.executableDirectory() + "\\Tools\\vip72socks\\vip72socks.exe", (object) "HIGHDPIAWARE", RegistryValueKind.String);
                  registryKey1.Close();
                }
                else
                {
                  Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\AppCompatFlags\\Layers");
                  RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\AppCompatFlags\\Layers", true);
                  if (registryKey2 != null)
                  {
                    registryKey2.SetValue(GlobalConfig.executableDirectory() + "\\Tools\\vip72socks\\vip72socks.exe", (object) "HIGHDPIAWARE", RegistryValueKind.String);
                    registryKey2.Close();
                  }
                }
                ProxyCore.__process = Process.Start(new ProcessStartInfo()
                {
                  WorkingDirectory = GlobalConfig.executableDirectory() + "/Tools/vip72socks",
                  FileName = GlobalConfig.executableDirectory() + "/Tools/vip72socks/vip72socks.exe",
                  Arguments = "-login;" + str2 + ":" + str3 + "; -mp:" + (object) LocalConfig.getCurrentConfig().getSSHAndVip72Port()
                });
                while (ProxyCore.handle.ToInt32() <= 0)
                {
                  ProxyCore.handle = ProxyCore.__process.MainWindowHandle;
                  Thread.Sleep(200);
                }
                Console.WriteLine((object) ProxyCore.handle);
                ProxyCore.vip72h = new Vip72Handle(ProxyCore.handle);
                Thread.Sleep(200);
                ProxyCore.vip72h.SendLeftClickToWindow(Vip72Handle.Vip72Window.Login);
                while (!flag)
                {
                  IntPtr windowHandle = ProxyCore.vip72h.GetWindowHandle(Vip72Handle.Vip72Window.Status);
                  if (windowHandle.ToInt32() > 0)
                  {
                    string controlText = Vip72Handle.GetControlText(windowHandle);
                    if (controlText.Length > 0)
                    {
                      if (controlText.Contains("ERROR") || controlText.Contains("DISCONNECT"))
                      {
                        ProxyCore.vip72h.SendLeftClickToWindow(Vip72Handle.Vip72Window.Exit);
                        str1 = (string) null;
                        break;
                      }
                      if (controlText.Contains("ready"))
                      {
                        str1 = Vip72Handle.GetControlText(ProxyCore.vip72h.GetWindowHandle(Vip72Handle.Vip72Window.Info));
                        flag = true;
                      }
                    }
                    Thread.Sleep(200);
                  }
                  else
                  {
                    str1 = (string) null;
                    flag = true;
                  }
                }
                if (str1 != null)
                {
                  if (flag)
                  {
                    ProxyCore.vip72h.SendLeftClickToWindow(Vip72Handle.Vip72Window.SelectCountry);
                    flag = false;
                  }
                  while (!flag)
                  {
                    IntPtr windowHandle = ProxyCore.vip72h.GetWindowHandle(Vip72Handle.Vip72Window.Status);
                    if (windowHandle.ToInt32() > 0)
                    {
                      string controlText = Vip72Handle.GetControlText(windowHandle);
                      if (controlText.Length > 0)
                      {
                        if (controlText.Contains("ERROR") || controlText.Contains("DISCONNECT"))
                        {
                          ProxyCore.vip72h.SendLeftClickToWindow(Vip72Handle.Vip72Window.Exit);
                          str1 = (string) null;
                          break;
                        }
                        if (controlText.Contains("Total"))
                        {
                          str1 = Vip72Handle.GetControlText(ProxyCore.vip72h.GetWindowHandle(Vip72Handle.Vip72Window.Status));
                          flag = true;
                        }
                      }
                      Thread.Sleep(200);
                    }
                    else
                    {
                      str1 = (string) null;
                      flag = true;
                    }
                  }
                  if (str1 == null)
                  {
                    ProxyCore.__process = (Process) null;
                    continue;
                  }
                }
                else
                  continue;
              }
              else
              {
                flag = false;
                str1 = "Reuse";
                ProxyCore.__currentVip72Handle.showWindow();
                ProxyCore.vip72h.SendLeftClickToWindow(Vip72Handle.Vip72Window.SelectCountry);
                while (!flag)
                {
                  IntPtr windowHandle = ProxyCore.vip72h.GetWindowHandle(Vip72Handle.Vip72Window.Status);
                  if (windowHandle.ToInt32() > 0)
                  {
                    string controlText = Vip72Handle.GetControlText(windowHandle);
                    if (controlText.Length > 0)
                    {
                      if (controlText.Contains("ERROR") || controlText.Contains("DISCONNECT"))
                      {
                        ProxyCore.vip72h.SendLeftClickToWindow(Vip72Handle.Vip72Window.Exit);
                        str1 = (string) null;
                        break;
                      }
                      if (controlText.Contains("Total"))
                      {
                        str1 = Vip72Handle.GetControlText(ProxyCore.vip72h.GetWindowHandle(Vip72Handle.Vip72Window.Status));
                        flag = true;
                      }
                    }
                    Thread.Sleep(200);
                  }
                  else
                  {
                    str1 = (string) null;
                    flag = true;
                  }
                }
                if (str1 == null)
                {
                  ProxyCore.__process = (Process) null;
                  continue;
                }
              }
              if (flag)
              {
                IntPtr windowHandle = ProxyCore.vip72h.GetWindowHandle(Vip72Handle.Vip72Window.SelectCountryListView);
                Console.WriteLine(Vip72Handle.GetWindowClassName(windowHandle));
                int index2 = 0;
                int num = 0;
                List<List<string>> listViewItems1 = Vip72Handle.GetListViewItems(windowHandle);
                for (int index3 = 0; index3 < listViewItems1.Count; ++index3)
                {
                  List<string> stringList = listViewItems1[index3];
                  if (stringList[0].Equals(country))
                  {
                    index2 = index3;
                    num = Convert.ToInt32(stringList[1]);
                    break;
                  }
                }
                Vip72Handle.selectListViewItem(windowHandle, index2, true);
                if (region != null && num > 170)
                {
                  ProxyCore.vip72h.SendLeftClickToWindow(Vip72Handle.Vip72Window.SelectRegion);
                  IntPtr handle = windowHandle;
                  List<List<string>> listViewItems2;
                  for (listViewItems2 = Vip72Handle.GetListViewItems(handle); listViewItems2.Count == listViewItems1.Count || listViewItems2.Count == 0; listViewItems2 = Vip72Handle.GetListViewItems(handle))
                    Thread.Sleep(1000);
                  for (int index3 = 0; index3 < listViewItems2.Count; ++index3)
                  {
                    List<string> stringList = listViewItems2[index3];
                    if (stringList.Count > 0 && stringList[0].Equals(region))
                    {
                      Vip72Handle.selectListViewItem(handle, index3, true);
                      break;
                    }
                  }
                }
                ProxyCore.vip72h.SendLeftClickToWindow(Vip72Handle.Vip72Window.GetProxyByGEO);
                flag = false;
              }
              while (!flag)
              {
                IntPtr windowHandle = ProxyCore.vip72h.GetWindowHandle(Vip72Handle.Vip72Window.Status);
                if (windowHandle.ToInt32() > 0)
                {
                  string controlText = Vip72Handle.GetControlText(windowHandle);
                  if (controlText.Length > 0)
                  {
                    if (controlText.ToUpper().Contains("ERROR") || controlText.ToUpper().Contains("DISCONNECT"))
                    {
                      ProxyCore.vip72h.SendLeftClickToWindow(Vip72Handle.Vip72Window.Exit);
                      str1 = (string) null;
                      break;
                    }
                    if (controlText.Contains("Ok"))
                    {
                      str1 = Vip72Handle.GetControlText(ProxyCore.vip72h.GetWindowHandle(Vip72Handle.Vip72Window.Status));
                      flag = true;
                    }
                    else if (controlText.Contains("ONLY"))
                    {
                      str1 = Vip72Handle.GetControlText(ProxyCore.vip72h.GetWindowHandle(Vip72Handle.Vip72Window.Status));
                      flag = true;
                    }
                  }
                  Thread.Sleep(200);
                }
                else
                {
                  str1 = (string) null;
                  flag = true;
                }
              }
              if (str1 != null)
              {
                if (!flag)
                  break;
                List<IntPtr> allChildHandles = ProxyCore.vip72h.GetAllChildHandles();
                Vip72Handle.WINDOWINFO childWindowInfo1 = Vip72Handle.GetChildWindowInfo(ProxyCore.handle);
                for (int index2 = 0; index2 < allChildHandles.Count; ++index2)
                {
                  IntPtr num1 = allChildHandles[index2];
                  string controlText = Vip72Handle.GetControlText(num1);
                  Vip72Handle.WINDOWINFO childWindowInfo2 = Vip72Handle.GetChildWindowInfo(num1);
                  int num2 = childWindowInfo2.rcWindow.top - childWindowInfo1.rcWindow.top;
                  int num3 = childWindowInfo2.rcWindow.left - childWindowInfo1.rcWindow.left;
                  int num4 = childWindowInfo2.rcWindow.bottom - childWindowInfo2.rcWindow.top;
                  int num5 = childWindowInfo2.rcWindow.right - childWindowInfo2.rcWindow.left;
                  Console.WriteLine("*** [" + Vip72Handle.GetWindowClassName(num1) + "] : " + controlText + " " + (object) num2 + " : " + (object) num3 + " : " + (object) num4 + " : " + (object) num5);
                }
                IntPtr windowHandle = ProxyCore.vip72h.GetWindowHandle(Vip72Handle.Vip72Window.AllProxiesListView);
                Console.WriteLine("All Proxies visible:" + Vip72Handle.IsWindowVisible(windowHandle).ToString());
                ProxyCore.__vip72ProxiesList = Vip72Handle.GetListViewItems(windowHandle);
                Thread.Sleep(1000);
                ProxyCore.__currentVip72Handle = ProxyCore.vip72h;
                ProxyCore.__allProxiesHandle = windowHandle;
                ProxyCore.selectNextIP(preferIp, out ip);
                AutoLeadClientHelper.reloadNetwork();
                break;
              }
            }
          }
          break;
        case ProxyTool.SSH:
          if (ProxyCore.previousSshInfo != null)
          {
            Dictionary<string, object> previousSshInfo = ProxyCore.previousSshInfo;
            LocalConfig.getCurrentConfig().getLiveSSH(ProxyCore.__country, true, GlobalConfig.SSHRefresh);
            ProxyCore.previousSshInfo = (Dictionary<string, object>) null;
            if (previousSshInfo == null)
              break;
            ProxyCore.setupProxy(ProxyCore.__tool, new IndexItem(0, previousSshInfo)
            {
              hostKeyFp = previousSshInfo["fp"].ToString()
            }, out ip);
            GlobalConfig.currentSSH = previousSshInfo;
            break;
          }
          Dictionary<string, object> liveSsh = LocalConfig.getCurrentConfig().getLiveSSH(ProxyCore.__country, true, GlobalConfig.SSHRefresh);
          if (liveSsh != null)
          {
            ProxyCore.setupProxy(ProxyCore.__tool, new IndexItem(0, liveSsh)
            {
              hostKeyFp = liveSsh["fp"].ToString()
            }, out ip);
            GlobalConfig.currentSSH = liveSsh;
          }
          break;
      }
    }

    public static void doChangeIP(ProxyTool tool, string country, out string ip)
    {
      ip = (string) null;
      if (tool == ProxyTool.SSH)
      {
        LocalConfig.getCurrentConfig().changeSSHPort();
        ProxyCore.doChangeIP(tool, country, (string) null, (string) null, out ip);
        Thread.Sleep(3000);
        AutoLeadClientHelper.reloadNetwork();
      }
      else
      {
        LocalConfig.getCurrentConfig().changeVip72Port();
        ProxyCore.doChangeIP(tool, country, (string) null, (string) null, out ip);
        Thread.Sleep(3000);
        AutoLeadClientHelper.reloadNetwork();
      }
    }

    public static void restoreIP(ProxyTool tool, string _ip, string country, string region, out string result)
    {
      result = (string) null;
      if (tool == ProxyTool.SSH)
      {
        LocalConfig.getCurrentConfig().changeSSHPort();
        ProxyCore.doChangeIP(tool, country, region, _ip, out result);
        AutoLeadClientHelper.reloadNetwork();
      }
      else
      {
        LocalConfig.getCurrentConfig().changeVip72Port();
        ProxyCore.doChangeIP(tool, country, region, _ip, out result);
        AutoLeadClientHelper.reloadNetwork();
      }
    }
  }
}
