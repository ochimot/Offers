// Decompiled with JetBrains decompiler
// Type: ControliPhone.Vip72
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using AutoLeadGUI;
using ManagedWinapi.Windows;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace ControliPhone
{
  internal class Vip72
  {
    public const int WM_LBUTTONDOWN = 513;
    public const int WM_LBUTTONUP = 514;

    [DllImport("user32.dll")]
    private static extern IntPtr FindWindow(string sClass, string sWindow);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, int msg, int Param, StringBuilder text);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int SendMessageTimeout(IntPtr hWnd, int Msg, int wParam, StringBuilder lParam, int fuFlags, int uTimeout, out int lpdwResult);

    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, int msg, int Param, string text);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int SendMessageTimeout(IntPtr hWnd, int Msg, int wParam, string lParam, int fuFlags, int uTimeout, out int lpdwResult);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int SendMessage(IntPtr hWnd, int msg, IntPtr Param, IntPtr text);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int SendMessageTimeout(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam, int fuFlags, int uTimeout, out int lpdwResult);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetDlgItem(int hwnd, int childID);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int GetWindowText(IntPtr hwnd, IntPtr windowString, int maxcount);

    [DllImport("user32", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool EnumThreadWindows(int threadId, Vip72.EnumWindowsProc callback, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetWindowLong(IntPtr hwnd, int nIndex);

    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress, int[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);

    [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int maxCount);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int dwSize, out int lpNumberOfBytesRead);

    public static string ControlGetText(IntPtr hwnd)
    {
      StringBuilder lParam = new StringBuilder((int) byte.MaxValue);
      int lpdwResult = 0;
      Vip72.SendMessageTimeout(hwnd, 13, lParam.Capacity, lParam, 2, 5000, out lpdwResult);
      return lParam.ToString();
    }

    public static int ControlSetText(IntPtr hwnd, string text)
    {
      return Vip72.SendMessage(hwnd, 12, text.Length, text);
    }

    public static void ControlClick(IntPtr hwnd)
    {
      int lpdwResult = 0;
      Vip72.SendMessageTimeout(hwnd, 513, IntPtr.Zero, IntPtr.Zero, 2, 5000, out lpdwResult);
      Vip72.SendMessageTimeout(hwnd, 514, IntPtr.Zero, IntPtr.Zero, 2, 5000, out lpdwResult);
    }

    public static void ControlDoubleClick(IntPtr hwnd)
    {
      int lpdwResult = 0;
      Vip72.SendMessageTimeout(hwnd, 515, IntPtr.Zero, IntPtr.Zero, 2, 5000, out lpdwResult);
    }

    public static IntPtr ControlGetHandle(IntPtr parentHandle, string _controlClass, int ID)
    {
      if (parentHandle == IntPtr.Zero)
        return IntPtr.Zero;
      IntPtr hwndChildAfter = IntPtr.Zero;
      IntPtr zero1 = IntPtr.Zero;
      int num = -1;
      while (num != ID)
      {
        IntPtr windowEx = Vip72.FindWindowEx(parentHandle, hwndChildAfter, _controlClass, (string) null);
        int nIndex = -12;
        num = (int) Vip72.GetWindowLong(windowEx, nIndex);
        hwndChildAfter = windowEx;
        IntPtr zero2 = IntPtr.Zero;
        if (windowEx == zero2)
          return IntPtr.Zero;
      }
      return hwndChildAfter;
    }

    public static IntPtr ControlGetHandle(IntPtr parentHandle, string _controlClass, int ID, bool instance)
    {
      if (parentHandle == IntPtr.Zero)
        return IntPtr.Zero;
      IntPtr hwndChildAfter = IntPtr.Zero;
      IntPtr zero1 = IntPtr.Zero;
      int num = -1;
      while (num != ID)
      {
        IntPtr windowEx = Vip72.FindWindowEx(parentHandle, hwndChildAfter, _controlClass, (string) null);
        int nIndex = -16;
        num = (int) Vip72.GetWindowLong(windowEx, nIndex);
        hwndChildAfter = windowEx;
        IntPtr zero2 = IntPtr.Zero;
        if (windowEx == zero2)
          return IntPtr.Zero;
      }
      return hwndChildAfter;
    }

    public static IntPtr ControlGetHandle(string _text, string _class, string _controlClass, int ID)
    {
      IntPtr window = Vip72.FindWindow(_class, _text);
      if (window == IntPtr.Zero)
        return IntPtr.Zero;
      IntPtr hwndChildAfter = IntPtr.Zero;
      IntPtr zero1 = IntPtr.Zero;
      int num = -1;
      while (num != ID)
      {
        IntPtr windowEx = Vip72.FindWindowEx(window, hwndChildAfter, _controlClass, (string) null);
        int nIndex = -12;
        num = (int) Vip72.GetWindowLong(windowEx, nIndex);
        hwndChildAfter = windowEx;
        IntPtr zero2 = IntPtr.Zero;
        if (windowEx == zero2)
          return IntPtr.Zero;
      }
      return hwndChildAfter;
    }

    private static bool ControlGetCheck(IntPtr controlhand)
    {
      int lpdwResult;
      Vip72.SendMessageTimeout(controlhand, 240, IntPtr.Zero, IntPtr.Zero, 2, 5000, out lpdwResult);
      return lpdwResult == 1;
    }

    private static bool ControlGetState(IntPtr controlhandle, int state)
    {
      return (uint) ((int) Vip72.GetWindowLong(controlhandle, -16) & state) > 0U;
    }

    public static IntPtr FindWindowInProcess(Process process, Func<string, bool> compareTitle)
    {
      IntPtr num = IntPtr.Zero;
      foreach (ProcessThread thread in (ReadOnlyCollectionBase) process.Threads)
      {
        num = Vip72.FindWindowInThread(thread.Id, compareTitle);
        if (num != IntPtr.Zero)
          break;
      }
      return num;
    }

    private static IntPtr FindWindowInThread(int threadId, Func<string, bool> compareTitle)
    {
      IntPtr windowHandle = IntPtr.Zero;
      Vip72.EnumThreadWindows(threadId, (Vip72.EnumWindowsProc) ((hWnd, lParam) =>
      {
        StringBuilder text = new StringBuilder(200);
        Vip72.GetWindowText(hWnd, text, 200);
        if (!compareTitle(text.ToString()))
          return true;
        windowHandle = hWnd;
        return false;
      }), IntPtr.Zero);
      return windowHandle;
    }

    public static void waitiotherVIP72()
    {
      Process[] processesByName = Process.GetProcessesByName("ControliPhone");
      while (true)
      {
        bool flag = true;
        foreach (Process process1 in processesByName)
        {
          if (Process.GetCurrentProcess().Id != process1.Id)
          {
            Process process2 = process1;
            Func<string, bool> func = (Func<string, bool>) (x => x.StartsWith(""));
            Func<string, bool> compareTitle = (Func<string, bool>) null;
            if (Vip72.ControlGetText(Vip72.ControlGetHandle(Vip72.FindWindowInProcess(process2, compareTitle), "WindowsForms10.STATIC.app.0.141b42a_r12_ad1", 1442840576, true)) == "Getting Vip72 IP...")
              flag = false;
          }
        }
        if (!flag)
          Thread.Sleep(500);
        else
          break;
      }
    }

    public static bool vip72login(string username, string password)
    {
      Thread.Sleep(1000);
      Process[] processByName1 = Vip72.GetProcessByName("vip72socks");
      if (((IEnumerable<Process>) processByName1).Count<Process>() > 0)
      {
        if (!processByName1[0].Responding)
        {
          try
          {
            if (!processByName1[0].HasExited)
              processByName1[0].Kill();
          }
          catch (Exception ex)
          {
          }
          return Vip72.vip72login(username, password);
        }
      }
      IntPtr handle1 = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "Button", 119);
      if (handle1 != IntPtr.Zero)
      {
        if (!Vip72.ControlGetState(handle1, 134217728))
        {
          Vip72.ControlSetText(Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "Edit", 303), username);
          Vip72.ControlSetText(Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "Edit", 301), password);
          Vip72.ControlClick(handle1);
          Vip72.ControlSetText(Vip72.ControlGetHandle("", "#32770", "Edit", 111), frmMain.tokenvip);
          Vip72.ControlClick(Vip72.ControlGetHandle("", "#32770", "Button", 88));
          IntPtr handle2 = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "Edit", 131);
          DateTime now = DateTime.Now;
          while (Vip72.ControlGetText(handle2) != "System ready")
          {
            if (((IEnumerable<Process>) processByName1).Count<Process>() > 0)
            {
              processByName1 = Vip72.GetProcessByName("vip72socks");
              if (!processByName1[0].Responding)
              {
                try
                {
                  if (!processByName1[0].HasExited)
                    processByName1[0].Kill();
                }
                catch (Exception ex)
                {
                }
                return Vip72.vip72login(username, password);
              }
            }
            if (((IEnumerable<Process>) processByName1).Count<Process>() == 0 || Vip72.ControlGetText(handle2) == "ERROR!Login and Password is incorrect" || !Vip72.ControlGetState(handle1, 134217728))
              return false;
            Thread.Sleep(100);
            if ((DateTime.Now - now).Seconds > 20)
              return false;
          }
        }
        return true;
      }
      Process[] processByName2 = Vip72.GetProcessByName("vip72socks");
      if ((uint) ((IEnumerable<Process>) processByName2).Count<Process>() > 0U)
      {
        try
        {
          if (!processByName2[0].HasExited)
            processByName2[0].Kill();
        }
        catch (Exception ex)
        {
        }
      }
      ProcessStartInfo startInfo = new ProcessStartInfo();
      startInfo.FileName = "vip72socks.exe";
      string str = AppDomain.CurrentDomain.BaseDirectory + "vip72";
      startInfo.WorkingDirectory = str;
      Process.Start(startInfo);
      Thread.Sleep(500);
      return Vip72.vip72login(username, password);
    }

    public static void clearip()
    {
      Process[] processByName = Vip72.GetProcessByName("vip72socks");
      if (((IEnumerable<Process>) processByName).Count<Process>() <= 0)
        return;
      Vip72.OpenProcess(2035711, false, processByName[0].Id);
      byte[] numArray = new byte[2]
      {
        (byte) 144,
        (byte) 144
      };
      IntPtr handle = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "SysListView32", 117);
      for (int index = 0; index < 30; ++index)
      {
        int lpdwResult = 0;
        Vip72.SendMessageTimeout(handle, 256, (IntPtr) 46, IntPtr.Zero, 2, 5000, out lpdwResult);
      }
    }

    public static void clearIpWithPort(int port)
    {
      Process[] processByName = Vip72.GetProcessByName("vip72socks");
      if (((IEnumerable<Process>) processByName).Count<Process>() == 0)
        return;
      uint id = (uint) processByName[0].Id;
      IntPtr handle = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "SysListView32", 117);
      for (int index = 0; ListViewItem1.GetListViewItem(handle, id, index, 4) != ""; ++index)
      {
        string listViewItem = ListViewItem1.GetListViewItem(handle, id, index, 4);
        if (listViewItem.Contains(port.ToString()) || listViewItem.Contains("main stream"))
        {
          ListViewItem1.SelectListViewItem(handle, id, index);
          int lpdwResult = 0;
          Vip72.SendMessageTimeout(handle, 256, (IntPtr) 46, IntPtr.Zero, 2, 5000, out lpdwResult);
          --index;
        }
      }
    }

    public static List<int> notlistport(int portstart, int portcount)
    {
      List<int> intList = new List<int>();
      for (int index = portstart; index < portstart + portcount; ++index)
        intList.Add(index);
      uint id = (uint) Vip72.GetProcessByName("vip72socks")[0].Id;
      IntPtr handle = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "SysListView32", 117);
      for (int index = 0; ListViewItem1.GetListViewItem(handle, id, index, 4) != ""; ++index)
      {
        string listViewItem = ListViewItem1.GetListViewItem(handle, id, index, 4);
        if (!listViewItem.Contains("main stream"))
        {
          if (intList.Contains(Convert.ToInt32(listViewItem.Split(new string[1]
          {
            ":"
          }, StringSplitOptions.None)[1])))
            intList.Remove(Convert.ToInt32(listViewItem.Split(new string[1]
            {
              ":"
            }, StringSplitOptions.None)[1]));
        }
      }
      return intList;
    }

    public static bool checkExistPort(int port)
    {
      uint id = (uint) Vip72.GetProcessByName("vip72socks")[0].Id;
      IntPtr handle = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "SysListView32", 117);
      for (int index = 0; ListViewItem1.GetListViewItem(handle, id, index, 4) != ""; ++index)
      {
        string listViewItem = ListViewItem1.GetListViewItem(handle, id, index, 4);
        if (!listViewItem.Contains("main stream"))
        {
          if (Convert.ToInt32(listViewItem.Split(new string[1]
          {
            ":"
          }, StringSplitOptions.None)[1]) == port)
            return true;
        }
      }
      return false;
    }

    public static void clearIPcountrycode(string countrycode)
    {
      Process[] processByName = Vip72.GetProcessByName("vip72socks");
      if (((IEnumerable<Process>) processByName).Count<Process>() == 0)
        return;
      uint id = (uint) processByName[0].Id;
      IntPtr handle = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "SysListView32", 117);
      for (int index = 0; ListViewItem1.GetListViewItem(handle, id, index, 1) != ""; ++index)
      {
        if (!ListViewItem1.GetListViewItem(handle, id, index, 1).Contains(countrycode))
        {
          ListViewItem1.SelectListViewItem(handle, id, index);
          int lpdwResult = 0;
          Vip72.SendMessageTimeout(handle, 256, (IntPtr) 46, IntPtr.Zero, 2, 5000, out lpdwResult);
          --index;
        }
      }
    }

    public static void clearIPMultiPort(int portstart, int portcount)
    {
      Process[] processByName = Vip72.GetProcessByName("vip72socks");
      if (((IEnumerable<Process>) processByName).Count<Process>() == 0)
        return;
      uint id = (uint) processByName[0].Id;
      IntPtr handle = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "SysListView32", 117);
      for (int index = 0; ListViewItem1.GetListViewItem(handle, id, index, 4) != ""; ++index)
      {
        string listViewItem = ListViewItem1.GetListViewItem(handle, id, index, 4);
        if (listViewItem.Contains("main stream"))
        {
          ListViewItem1.SelectListViewItem(handle, id, index);
          int lpdwResult = 0;
          Vip72.SendMessageTimeout(handle, 256, (IntPtr) 46, IntPtr.Zero, 2, 5000, out lpdwResult);
          --index;
        }
        else
        {
          int int32 = Convert.ToInt32(listViewItem.Split(new string[1]
          {
            ":"
          }, StringSplitOptions.None)[1]);
          if (int32 < portstart || int32 >= portstart + portcount)
          {
            ListViewItem1.SelectListViewItem(handle, id, index);
            int lpdwResult = 0;
            Vip72.SendMessageTimeout(handle, 256, (IntPtr) 46, IntPtr.Zero, 2, 5000, out lpdwResult);
            --index;
          }
        }
      }
    }

    [DllImport("user32.dll")]
    public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    public static string clickipbk2(int port)
    {
      int num1 = 4327367;
      int lpBaseAddress1 = 4323513;
      int lpBaseAddress2 = 4323206;
      int lpBaseAddress3 = 4249536;
      Process[] processByName = Vip72.GetProcessByName("vip72socks");
      string str;
      if (((IEnumerable<Process>) processByName).Count<Process>() == 0)
      {
        str = "not running";
      }
      else
      {
        IntPtr handle1 = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "Button", 7817);
        if (Vip72.ControlGetCheck(handle1))
          Vip72.ControlClick(handle1);
        IntPtr hProcess = Vip72.OpenProcess(2035711, false, processByName[0].Id);
        int lpNumberOfBytesRead = 0;
        IntPtr lpBuffer1 = Marshal.AllocHGlobal(4);
        Vip72.ReadProcessMemory(hProcess, (IntPtr) num1, lpBuffer1, 4, out lpNumberOfBytesRead);
        Random random = new Random();
        uint id = (uint) processByName[0].Id;
        IntPtr handle2 = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "SysListView32", 117);
        int num2 = 0;
        while (ListViewItem1.GetListViewItem(handle2, id, num2, 4) != "")
        {
          string listViewItem = ListViewItem1.GetListViewItem(handle2, id, num2, 4);
          if (listViewItem.Contains(port.ToString()) || listViewItem.Contains("main stream"))
          {
            ListViewItem1.SelectListViewItem(handle2, id, num2);
            int lpdwResult = 0;
            Vip72.SendMessageTimeout(handle2, 256, (IntPtr) 46, IntPtr.Zero, 2, 5000, out lpdwResult);
          }
          else
            ++num2;
        }
        IntPtr handle3 = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "SysListView32", 116);
        while (ListViewItem1.GetListViewItem(handle3, id, num2, 0) != "")
          ++num2;
        int maxValue = num2;
        if ((uint) maxValue > 0U)
        {
          int num3 = 0;
          for (; ListViewItem1.GetListViewItem(handle3, id, num3, 0) != ""; ++num3)
          {
            if (ListViewItem1.GetListViewItem(handle3, id, num3, 0).Contains(".**"))
            {
              int num4 = random.Next(0, maxValue);
              while (!ListViewItem1.GetListViewItem(handle3, id, num4, 0).Contains(".**"))
                num4 = random.Next(0, maxValue);
              if (num4 == -1)
                return "no IP";
              int[] lpBuffer2 = new int[1]{ 462569 };
              int[] lpBuffer3 = new int[1]{ port };
              int[] lpBuffer4 = new int[1]{ num4 };
              int lpNumberOfBytesWritten = 0;
              Vip72.WriteProcessMemory((int) hProcess, lpBaseAddress1, lpBuffer3, 4, ref lpNumberOfBytesWritten);
              Vip72.WriteProcessMemory((int) hProcess, lpBaseAddress3, lpBuffer2, 4, ref lpNumberOfBytesWritten);
              Vip72.WriteProcessMemory((int) hProcess, lpBaseAddress2, lpBuffer4, 4, ref lpNumberOfBytesWritten);
              ListViewItem1.SelectListViewItem(handle3, id, num4);
              Vip72.ControlDoubleClick(handle3);
              Thread.Sleep(500);
              lpBuffer2[0] = 21529871;
              Vip72.WriteProcessMemory((int) hProcess, lpBaseAddress3, lpBuffer2, 4, ref lpNumberOfBytesWritten);
              IntPtr handle4 = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "Button", 7303);
              IntPtr handle5 = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "Edit", 131);
              DateTime now = DateTime.Now;
              while (!Vip72.ControlGetCheck(handle4))
              {
                if (Vip72.ControlGetText(handle5).Contains("ffline"))
                  return "dead";
                if (Vip72.ControlGetText(handle5).Contains("limit"))
                {
                  try
                  {
                    if (!processByName[0].HasExited)
                      processByName[0].Kill();
                  }
                  catch (Exception ex)
                  {
                  }
                  return "limited";
                }
                if (Vip72.ControlGetText(handle5).Contains("can't") || Vip72.ControlGetText(handle5).Contains("disconnect"))
                  return "dead";
                if (Vip72.ControlGetText(handle5).Contains("aximum"))
                  return "maximum";
                if ((DateTime.Now - now).TotalSeconds > 15.0)
                  return "timeout";
              }
              IntPtr handle6 = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "SysListView32", 117);
              Thread.Sleep(2000);
              for (int index = 0; ListViewItem1.GetListViewItem(handle6, id, index, 4) != ""; ++index)
              {
                if (ListViewItem1.GetListViewItem(handle6, id, index, 4).Contains("main stream"))
                  return ListViewItem1.GetListViewItem(handle6, id, index, 0);
              }
              return "no IP";
            }
          }
        }
        str = "no IP";
      }
      return str;
    }

    public static string clickip(int port)
    {
      int num1 = 4328465;
      int lpBaseAddress1 = 4324611;
      int lpBaseAddress2 = 4324304;
      int lpBaseAddress3 = 4253085;
      Process[] processByName = Vip72.GetProcessByName("vip72socks");
      string str1;
      if (((IEnumerable<Process>) processByName).Count<Process>() == 0)
      {
        str1 = "not running";
      }
      else
      {
        IntPtr handle1 = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "Button", 7817);
        if (Vip72.ControlGetCheck(handle1))
          Vip72.ControlClick(handle1);
        IntPtr hProcess = Vip72.OpenProcess(2035711, false, processByName[0].Id);
        int lpNumberOfBytesRead = 0;
        IntPtr zero = IntPtr.Zero;
        IntPtr lpBuffer1 = Marshal.AllocHGlobal(4);
        Vip72.ReadProcessMemory(hProcess, (IntPtr) num1, lpBuffer1, 4, out lpNumberOfBytesRead);
        Random random = new Random();
        uint id = (uint) processByName[0].Id;
        IntPtr handle2 = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "SysListView32", 117);
        int num2 = 0;
        while (ListViewItem1.GetListViewItem(handle2, id, num2, 4) != "")
        {
          string listViewItem = ListViewItem1.GetListViewItem(handle2, id, num2, 4);
          if (listViewItem.Contains(port.ToString()) || listViewItem.Contains("main stream"))
          {
            ListViewItem1.SelectListViewItem(handle2, id, num2);
            int lpdwResult = 0;
            Vip72.SendMessageTimeout(handle2, 256, (IntPtr) 46, IntPtr.Zero, 2, 5000, out lpdwResult);
          }
          else
            ++num2;
        }
        IntPtr handle3 = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "SysListView32", 116);
        while (ListViewItem1.GetListViewItem(handle3, id, num2, 0) != "")
          ++num2;
        int maxValue = num2;
        if (maxValue == 0)
        {
          str1 = "no IP";
        }
        else
        {
          int num3 = 0;
          int num4 = -1;
          for (; ListViewItem1.GetListViewItem(handle3, id, num3, 0) != ""; ++num3)
          {
            if (ListViewItem1.GetListViewItem(handle3, id, num3, 0).Contains(".**"))
            {
              num4 = random.Next(0, maxValue);
              while (!ListViewItem1.GetListViewItem(handle3, id, num4, 0).Contains(".**"))
                num4 = random.Next(0, maxValue);
              break;
            }
          }
          if (num4 == -1)
          {
            str1 = "no IP";
          }
          else
          {
            int[] lpBuffer2 = new int[1]{ 472809 };
            int[] lpBuffer3 = new int[1]{ port };
            int[] lpBuffer4 = new int[1]{ num4 };
            int lpNumberOfBytesWritten = 0;
            ListViewItem1.SelectListViewItem(handle3, id, num4);
            Vip72.WriteProcessMemory((int) hProcess, lpBaseAddress1, lpBuffer3, 4, ref lpNumberOfBytesWritten);
            Vip72.WriteProcessMemory((int) hProcess, lpBaseAddress3, lpBuffer2, 4, ref lpNumberOfBytesWritten);
            Vip72.WriteProcessMemory((int) hProcess, lpBaseAddress2, lpBuffer4, 4, ref lpNumberOfBytesWritten);
            Vip72.ControlDoubleClick(handle3);
            Thread.Sleep(500);
            lpBuffer2[0] = 21529871;
            Vip72.WriteProcessMemory((int) hProcess, lpBaseAddress3, lpBuffer2, 4, ref lpNumberOfBytesWritten);
            IntPtr handle4 = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "Button", 7303);
            IntPtr handle5 = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "Edit", 131);
            DateTime now = DateTime.Now;
            while (!Vip72.ControlGetCheck(handle4))
            {
              if (Vip72.ControlGetText(handle5).Contains("ffline"))
                return "dead";
              if (Vip72.ControlGetText(handle5).Contains("limit"))
              {
                try
                {
                  if (!processByName[0].HasExited)
                    processByName[0].Kill();
                }
                catch (Exception ex)
                {
                }
                return "limited";
              }
              if (Vip72.ControlGetText(handle5).Contains("can't") || Vip72.ControlGetText(handle5).Contains("disconnect"))
                return "dead";
              if (Vip72.ControlGetText(handle5).Contains("aximum"))
                return "maximum";
              if ((DateTime.Now - now).TotalSeconds > 15.0)
                return "timeout";
            }
            Thread.Sleep(500);
            IntPtr handle6 = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "SysListView32", 117);
            int num5 = 0;
            string str2 = "";
            for (; ListViewItem1.GetListViewItem(handle6, id, num5, 4) != ""; ++num5)
            {
              if (ListViewItem1.GetListViewItem(handle6, id, num5, 4).Contains(port.ToString()))
              {
                str2 = ListViewItem1.GetListViewItem(handle6, id, num5, 0);
                return ListViewItem1.GetListViewItem(handle6, id, num5, 0);
              }
            }
            str1 = "no IP";
          }
        }
      }
      return str1;
    }

    public static string clickipok(int port)
    {
      int num1 = 4327364;
      int lpBaseAddress = 4249536;
      Process[] processByName = Vip72.GetProcessByName("vip72socks");
      if (((IEnumerable<Process>) processByName).Count<Process>() == 0)
        return "not running";
      IntPtr handle1 = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "Button", 7817);
      if (Vip72.ControlGetCheck(handle1))
        Vip72.ControlClick(handle1);
      IntPtr hProcess = Vip72.OpenProcess(2035711, false, processByName[0].Id);
      int lpNumberOfBytesRead = 0;
      IntPtr zero = IntPtr.Zero;
      IntPtr lpBuffer1 = Marshal.AllocHGlobal(4);
      Vip72.ReadProcessMemory(hProcess, (IntPtr) num1, lpBuffer1, 4, out lpNumberOfBytesRead);
      Random random = new Random();
      uint id = (uint) processByName[0].Id;
      IntPtr handle2 = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "SysListView32", 117);
      int num2 = 0;
      while (ListViewItem1.GetListViewItem(handle2, id, num2, 4) != "")
      {
        string listViewItem = ListViewItem1.GetListViewItem(handle2, id, num2, 4);
        if (listViewItem.Contains(port.ToString()) || listViewItem.Contains("main stream"))
        {
          ListViewItem1.SelectListViewItem(handle2, id, num2);
          int lpdwResult = 0;
          Vip72.SendMessageTimeout(handle2, 256, (IntPtr) 46, IntPtr.Zero, 2, 5000, out lpdwResult);
        }
        else
          ++num2;
      }
      IntPtr handle3 = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "SysListView32", 116);
      while (ListViewItem1.GetListViewItem(handle3, id, num2, 0) != "")
        ++num2;
      int maxValue = num2;
      if (maxValue == 0)
        return "no IP";
      int num3 = 0;
      int num4 = -1;
      for (; ListViewItem1.GetListViewItem(handle3, id, num3, 0) != ""; ++num3)
      {
        if (ListViewItem1.GetListViewItem(handle3, id, num3, 0).Contains(".**"))
        {
          num4 = random.Next(0, maxValue);
          while (!ListViewItem1.GetListViewItem(handle3, id, num4, 0).Contains(".**"))
            num4 = random.Next(0, maxValue);
          break;
        }
      }
      if (num4 == -1)
        return "no IP";
      int[] lpBuffer2 = new int[1]{ 462569 };
      int[] numArray1 = new int[1]{ port };
      int[] numArray2 = new int[1]{ num4 };
      int lpNumberOfBytesWritten = 0;
      ListViewItem1.SelectListViewItem(handle3, id, num4);
      Vip72.ControlDoubleClick(handle3);
      Thread.Sleep(2000);
      Thread.Sleep(1000);
      SystemListView systemListView = SystemListView.FromSystemWindow(new SystemWindow(handle2));
      SystemListViewItem systemListViewItem = (SystemListViewItem) null;
      if (systemListView == null)
      {
        if (Vip72.ControlGetText(Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "Edit", 131)).Contains("limit"))
        {
          try
          {
            if (!processByName[0].HasExited)
              processByName[0].Kill();
          }
          catch (Exception ex)
          {
          }
          return "limited";
        }
      }
      do
      {
        for (int index = 0; index < systemListView.Count; ++index)
        {
          if (ListViewItem1.GetListViewItem(handle2, id, index, 4).Contains("main stream"))
          {
            ListViewItem1.SelectListViewItem(handle2, id, index);
            systemListViewItem = systemListView[index];
            break;
          }
          Thread.Sleep(500);
        }
      }
      while (systemListViewItem == null);
      uint Msg1 = 516;
      uint Msg2 = 516;
      uint num5 = 2;
      uint Msg3 = 513;
      uint Msg4 = 514;
      uint num6 = 1;
      int num7 = systemListViewItem.Position.X + 5;
      Point point = systemListViewItem.Position;
      int num8 = point.Y + 5 << 16;
      int num9 = num7 | num8;
      point = systemListViewItem.Position;
      int x1 = point.X - 5;
      point = systemListViewItem.Position;
      int y1 = point.Y - 5;
      Vip72.MouseSimulator.SetCursorPos(x1, y1);
      Vip72.PostMessage(handle2, Msg1, (IntPtr) ((long) num5), (IntPtr) num9);
      Vip72.PostMessage(handle2, Msg2, (IntPtr) 0, (IntPtr) num9);
      Thread.Sleep(200);
      IntPtr window1 = Vip72.FindWindow("#32768", "");
      SystemWindow systemWindow1 = new SystemWindow(window1);
      point = systemWindow1.Location;
      int x2 = point.X - 5;
      point = systemWindow1.Location;
      int y2 = point.Y - 5;
      Vip72.MouseSimulator.SetCursorPos(x2, y2);
      point = systemWindow1.Location;
      int num10 = point.X + 60;
      point = systemWindow1.Location;
      int num11 = point.Y + 185 << 16;
      int num12 = num10 | num11;
      Vip72.PostMessage(window1, Msg3, (IntPtr) ((long) num6), new IntPtr(num12));
      Vip72.PostMessage(window1, Msg4, (IntPtr) 0, new IntPtr(num12));
      Thread.Sleep(200);
      IntPtr window2 = Vip72.FindWindow("#32768", "");
      SystemWindow systemWindow2 = new SystemWindow(window2);
      point = systemWindow2.Location;
      int num13 = point.X + 60;
      point = systemWindow2.Location;
      int num14 = point.Y + 42 << 16;
      int num15 = num13 | num14;
      Vip72.PostMessage(window2, Msg3, (IntPtr) ((long) num6), new IntPtr(num15));
      Vip72.PostMessage(window2, Msg4, (IntPtr) 0, new IntPtr(num15));
      Thread.Sleep(200);
      Vip72.ControlSetText(Vip72.ControlGetHandle("", "#32770", "Edit", 5671), port.ToString());
      Thread.Sleep(200);
      Vip72.ControlClick(Vip72.ControlGetHandle("", "#32770", "Button", 5675));
      Thread.Sleep(200);
      lpBuffer2[0] = 21529871;
      Vip72.WriteProcessMemory((int) hProcess, lpBaseAddress, lpBuffer2, 4, ref lpNumberOfBytesWritten);
      IntPtr handle4 = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "Button", 7303);
      IntPtr handle5 = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "Edit", 131);
      DateTime now = DateTime.Now;
      while (!Vip72.ControlGetCheck(handle4))
      {
        if (Vip72.ControlGetText(handle5).Contains("ffline"))
          return "dead";
        if (Vip72.ControlGetText(handle5).Contains("rror") || Vip72.ControlGetText(handle5).Contains("RROR"))
          return "rror";
        if (Vip72.ControlGetText(handle5).Contains("limit"))
        {
          try
          {
            if (!processByName[0].HasExited)
              processByName[0].Kill();
          }
          catch (Exception ex)
          {
          }
          return "limited";
        }
        if (Vip72.ControlGetText(handle5).Contains("can't") || Vip72.ControlGetText(handle5).Contains("disconnect"))
          return "dead";
        if (Vip72.ControlGetText(handle5).Contains("aximum"))
          return "maximum";
        if ((DateTime.Now - now).TotalSeconds > 15.0)
          return "timeout";
      }
      Thread.Sleep(500);
      IntPtr handle6 = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "SysListView32", 117);
      string str = "";
      int num16 = 0;
      if (!(ListViewItem1.GetListViewItem(handle6, id, num16, 4) != ""))
        return "no IP";
      if (ListViewItem1.GetListViewItem(handle6, id, num16, 4).Contains(port.ToString()))
        str = ListViewItem1.GetListViewItem(handle6, id, num16, 0);
      return ListViewItem1.GetListViewItem(handle6, id, num16, 0);
    }

    public static Process[] GetProcessByName(string processName)
    {
      Process[] processes = Process.GetProcesses();
      List<Process> processList = new List<Process>();
      foreach (Process process in processes)
      {
        if (process.ProcessName.StartsWith(processName))
          processList.Add(process);
      }
      return processList.ToArray();
    }

    public static bool getipbk(byte countryindex, string country)
    {
      byte[] lpBuffer = new byte[1];
      int[] numArray = new int[1];
      lpBuffer[0] = countryindex;
      int lpBaseAddress = 4334232;
      Process[] processByName1 = Vip72.GetProcessByName("vip72socks");
      Process process = new Process();
      if (((IEnumerable<Process>) processByName1).Count<Process>() > 0)
      {
        int lpNumberOfBytesWritten = 0;
        Vip72.WriteProcessMemory((int) Vip72.OpenProcess(2035711, false, processByName1[0].Id), lpBaseAddress, lpBuffer, 1, ref lpNumberOfBytesWritten);
        numArray[0] = 0;
        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "citycode\\" + countryindex.ToString() + ".dat"))
        {
          string[] strArray = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "citycode\\" + countryindex.ToString() + ".dat");
          Random random = new Random();
          int int32 = Convert.ToInt32(strArray[random.Next(0, ((IEnumerable<string>) strArray).Count<string>())].Split(new string[1]
          {
            "|"
          }, StringSplitOptions.None)[1]);
          numArray[0] = int32;
        }
        Vip72.ControlClick(Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "Static", 7811));
        IntPtr handle1 = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "SysListView32", 7809);
        int num1 = 0;
        uint id = (uint) processByName1[0].Id;
        int num2 = 0;
        while (ListViewItem1.GetListViewItem(handle1, id, num1, 0) != "")
        {
          if (ListViewItem1.GetListViewItem(handle1, id, num2, 0) == country)
          {
            ListViewItem1.SelectListViewItem(handle1, id, num2);
            Vip72.ControlDoubleClick(handle1);
            break;
          }
          ++num2;
        }
        IntPtr handle2 = Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "Button", (int) sbyte.MaxValue);
        Vip72.ControlClick(handle2);
        Vip72.ControlGetHandle("VIP72 Socks Client", "#32770", "Edit", 131);
        DateTime now = DateTime.Now;
        while (Vip72.ControlGetState(handle2, 134217728))
        {
          Thread.Sleep(100);
          Process[] processByName2 = Vip72.GetProcessByName("vip72socks");
          if (((IEnumerable<Process>) processByName2).Count<Process>() == 0)
            return false;
          if (!processByName2[0].Responding || (DateTime.Now - now).TotalSeconds > 30.0)
          {
            try
            {
              if (!processByName2[0].HasExited)
                processByName2[0].Kill();
            }
            catch (Exception ex)
            {
            }
            return false;
          }
        }
      }
      return true;
    }

    public static bool getip(byte countryindex, string country, string region, string coderegion, string city, string codecity)
    {
      byte[] lpBuffer1 = new byte[1];
      int[] lpBuffer2 = new int[1];
      lpBuffer1[0] = countryindex;
      int lpBaseAddress = 4482683;
      Process[] processByName1 = Vip72.GetProcessByName("vip72socks");
      Process process1 = new Process();
      if (((IEnumerable<Process>) processByName1).Count<Process>() > 0)
      {
        Process process2 = processByName1[0];
        IntPtr windowInProcess = Vip72.FindWindowInProcess(process2, (Func<string, bool>) (s => s.StartsWith("VIP72 Socks Client")));
        IntPtr num = Vip72.OpenProcess(2035711, false, process2.Id);
        int lpNumberOfBytesWritten = 0;
        Vip72.WriteProcessMemory((int) num, lpBaseAddress, lpBuffer1, 1, ref lpNumberOfBytesWritten);
        lpBuffer2[0] = 0;
        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "citycode\\" + country.ToString() + ".dat"))
        {
          int int32_1 = Convert.ToInt32(coderegion);
          if (int32_1 != (int) ushort.MaxValue)
          {
            lpBuffer2[0] = int32_1;
            Vip72.WriteProcessMemory((int) num, lpBaseAddress + 1, lpBuffer2, 4, ref lpNumberOfBytesWritten);
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "citycode\\Cities\\" + region.ToString() + ".dat"))
            {
              int int32_2 = Convert.ToInt32(codecity);
              if (int32_2 != (int) ushort.MaxValue)
              {
                lpBuffer2[0] = int32_2;
                Vip72.WriteProcessMemory((int) num, 4482686, lpBuffer2, 4, ref lpNumberOfBytesWritten);
              }
            }
          }
        }
        IntPtr handle = Vip72.ControlGetHandle(windowInProcess, "Button", (int) sbyte.MaxValue);
        Vip72.ControlClick(handle);
        Vip72.ControlGetHandle(windowInProcess, "Edit", 131);
        DateTime now = DateTime.Now;
        while (Vip72.ControlGetState(handle, 134217728))
        {
          Thread.Sleep(100);
          Process[] processByName2 = Vip72.GetProcessByName("vip72socks");
          if (((IEnumerable<Process>) processByName2).Count<Process>() == 0)
            return false;
          if (!processByName2[0].Responding || (DateTime.Now - now).TotalSeconds > 30.0)
          {
            try
            {
              if (!processByName2[0].HasExited)
                processByName2[0].Kill();
            }
            catch (Exception ex)
            {
            }
            return false;
          }
        }
      }
      return true;
    }

    public class MouseSimulator
    {
      [DllImport("User32.dll")]
      public static extern bool SetCursorPos(int x, int y);

      [DllImport("user32.dll", SetLastError = true)]
      private static extern uint SendInput(uint nInputs, ref Vip72.MouseSimulator.INPUT pInputs, int cbSize);

        public static void ClickLeftMouseButton()
        {
            Vip72.MouseSimulator.INPUT input = default(Vip72.MouseSimulator.INPUT);
            input.type = Vip72.MouseSimulator.SendInputEventType.InputMouse;
            input.mkhi.mi.dwFlags = Vip72.MouseSimulator.MouseEventFlags.MOUSEEVENTF_LEFTDOWN;
            Vip72.MouseSimulator.SendInput(1u, ref input, Marshal.SizeOf<Vip72.MouseSimulator.INPUT>(default(Vip72.MouseSimulator.INPUT)));
            Vip72.MouseSimulator.INPUT input2 = default(Vip72.MouseSimulator.INPUT);
            input2.type = Vip72.MouseSimulator.SendInputEventType.InputMouse;
            input2.mkhi.mi.dwFlags = Vip72.MouseSimulator.MouseEventFlags.MOUSEEVENTF_LEFTUP;
            Vip72.MouseSimulator.SendInput(1u, ref input2, Marshal.SizeOf<Vip72.MouseSimulator.INPUT>(default(Vip72.MouseSimulator.INPUT)));
        }

            public static void ClickRightMouseButton()
        {
            Vip72.MouseSimulator.INPUT input = default(Vip72.MouseSimulator.INPUT);
            input.type = Vip72.MouseSimulator.SendInputEventType.InputMouse;
            input.mkhi.mi.dwFlags = Vip72.MouseSimulator.MouseEventFlags.MOUSEEVENTF_RIGHTDOWN;
            Vip72.MouseSimulator.SendInput(1u, ref input, Marshal.SizeOf<Vip72.MouseSimulator.INPUT>(default(Vip72.MouseSimulator.INPUT)));
            Vip72.MouseSimulator.INPUT input2 = default(Vip72.MouseSimulator.INPUT);
            input2.type = Vip72.MouseSimulator.SendInputEventType.InputMouse;
            input2.mkhi.mi.dwFlags = Vip72.MouseSimulator.MouseEventFlags.MOUSEEVENTF_RIGHTUP;
            Vip72.MouseSimulator.SendInput(1u, ref input2, Marshal.SizeOf<Vip72.MouseSimulator.INPUT>(default(Vip72.MouseSimulator.INPUT)));
        }

            private struct INPUT
      {
        public Vip72.MouseSimulator.SendInputEventType type;
        public Vip72.MouseSimulator.MouseKeybdhardwareInputUnion mkhi;
      }

      [StructLayout(LayoutKind.Explicit)]
      private struct MouseKeybdhardwareInputUnion
      {
        [FieldOffset(0)]
        public Vip72.MouseSimulator.MouseInputData mi;
        [FieldOffset(0)]
        public Vip72.MouseSimulator.KEYBDINPUT ki;
        [FieldOffset(0)]
        public Vip72.MouseSimulator.HARDWAREINPUT hi;
      }

      private struct KEYBDINPUT
      {
        public ushort wVk;
        public ushort wScan;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
      }

      private struct HARDWAREINPUT
      {
        public int uMsg;
        public short wParamL;
        public short wParamH;
      }

      private struct MouseInputData
      {
        public int dx;
        public int dy;
        public uint mouseData;
        public Vip72.MouseSimulator.MouseEventFlags dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
      }

      [Flags]
      private enum MouseEventFlags : uint
      {
        MOUSEEVENTF_MOVE = 1,
        MOUSEEVENTF_LEFTDOWN = 2,
        MOUSEEVENTF_LEFTUP = 4,
        MOUSEEVENTF_RIGHTDOWN = 8,
        MOUSEEVENTF_RIGHTUP = 16, // 0x00000010
        MOUSEEVENTF_MIDDLEDOWN = 32, // 0x00000020
        MOUSEEVENTF_MIDDLEUP = 64, // 0x00000040
        MOUSEEVENTF_XDOWN = 128, // 0x00000080
        MOUSEEVENTF_XUP = 256, // 0x00000100
        MOUSEEVENTF_WHEEL = 2048, // 0x00000800
        MOUSEEVENTF_VIRTUALDESK = 16384, // 0x00004000
        MOUSEEVENTF_ABSOLUTE = 32768, // 0x00008000
      }

      private enum SendInputEventType
      {
        InputMouse,
        InputKeyboard,
        InputHardware,
      }
    }

    public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);
  }
}
