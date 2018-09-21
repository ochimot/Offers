// Decompiled with JetBrains decompiler
// Type: ControliPhone.Vip72Rieng
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using AutoLeadGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace ControliPhone
{
  public class Vip72Rieng
  {
    public static Process Vip72RiengProcess = (Process) null;
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
    private static extern bool EnumThreadWindows(int threadId, Vip72Rieng.EnumWindowsProc callback, IntPtr lParam);

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
      Vip72Rieng.SendMessageTimeout(hwnd, 13, lParam.Capacity, lParam, 2, 5000, out lpdwResult);
      return lParam.ToString();
    }

    public static int ControlSetText(IntPtr hwnd, string text)
    {
      return Vip72Rieng.SendMessage(hwnd, 12, text.Length, text);
    }

    public static void ControlClick(IntPtr hwnd)
    {
      int lpdwResult = 0;
      Vip72Rieng.SendMessageTimeout(hwnd, 513, IntPtr.Zero, IntPtr.Zero, 2, 5000, out lpdwResult);
      Vip72Rieng.SendMessageTimeout(hwnd, 514, IntPtr.Zero, IntPtr.Zero, 2, 5000, out lpdwResult);
    }

    public static void ControlDoubleClick(IntPtr hwnd)
    {
      int lpdwResult = 0;
      Vip72Rieng.SendMessageTimeout(hwnd, 515, IntPtr.Zero, IntPtr.Zero, 2, 5000, out lpdwResult);
    }

    public static IntPtr ControlGetHandle(IntPtr parentHandle, string _controlClass, int ID)
    {
      IntPtr num1;
      if (parentHandle == IntPtr.Zero)
      {
        num1 = IntPtr.Zero;
      }
      else
      {
        IntPtr hwndChildAfter = IntPtr.Zero;
        IntPtr zero = IntPtr.Zero;
        int num2 = -1;
        while (num2 != ID)
        {
          IntPtr windowEx = Vip72Rieng.FindWindowEx(parentHandle, hwndChildAfter, _controlClass, (string) null);
          num2 = (int) Vip72Rieng.GetWindowLong(windowEx, -12);
          hwndChildAfter = windowEx;
          if (windowEx == IntPtr.Zero)
          {
            num1 = IntPtr.Zero;
            goto label_7;
          }
        }
        num1 = hwndChildAfter;
      }
label_7:
      return num1;
    }

    public static IntPtr ControlGetHandle(IntPtr parentHandle, string _controlClass, int ID, bool instance)
    {
      IntPtr num1;
      if (parentHandle == IntPtr.Zero)
      {
        num1 = IntPtr.Zero;
      }
      else
      {
        IntPtr hwndChildAfter = IntPtr.Zero;
        IntPtr zero = IntPtr.Zero;
        int num2 = -1;
        while (num2 != ID)
        {
          IntPtr windowEx = Vip72Rieng.FindWindowEx(parentHandle, hwndChildAfter, _controlClass, (string) null);
          num2 = (int) Vip72Rieng.GetWindowLong(windowEx, -16);
          hwndChildAfter = windowEx;
          if (windowEx == IntPtr.Zero)
          {
            num1 = IntPtr.Zero;
            goto label_7;
          }
        }
        num1 = hwndChildAfter;
      }
label_7:
      return num1;
    }

    public static IntPtr ControlGetHandle(string _text, string _class, string _controlClass, int ID)
    {
      IntPtr window = Vip72Rieng.FindWindow(_class, _text);
      IntPtr num1;
      if (window == IntPtr.Zero)
      {
        num1 = IntPtr.Zero;
      }
      else
      {
        IntPtr hwndChildAfter = IntPtr.Zero;
        IntPtr zero = IntPtr.Zero;
        int num2 = -1;
        while (num2 != ID)
        {
          IntPtr windowEx = Vip72Rieng.FindWindowEx(window, hwndChildAfter, _controlClass, (string) null);
          num2 = (int) Vip72Rieng.GetWindowLong(windowEx, -12);
          hwndChildAfter = windowEx;
          if (windowEx == IntPtr.Zero)
          {
            num1 = IntPtr.Zero;
            goto label_7;
          }
        }
        num1 = hwndChildAfter;
      }
label_7:
      return num1;
    }

    private static bool ControlGetCheck(IntPtr controlhand)
    {
      int lpdwResult;
      Vip72Rieng.SendMessageTimeout(controlhand, 240, IntPtr.Zero, IntPtr.Zero, 2, 5000, out lpdwResult);
      return lpdwResult == 1;
    }

    private static bool ControlGetState(IntPtr controlhandle, int state)
    {
      return (uint) ((int) Vip72Rieng.GetWindowLong(controlhandle, -16) & state) > 0U;
    }

    public static IntPtr FindWindowInProcess(Process process, Func<string, bool> compareTitle)
    {
      IntPtr num1 = IntPtr.Zero;
      IntPtr num2;
      if (process == null)
      {
        num2 = num1;
      }
      else
      {
        foreach (ProcessThread thread in (ReadOnlyCollectionBase) process.Threads)
        {
          num1 = Vip72Rieng.FindWindowInThread(thread.Id, compareTitle);
          if (num1 != IntPtr.Zero)
            break;
        }
        num2 = num1;
      }
      return num2;
    }

    private static IntPtr FindWindowInThread(int threadId, Func<string, bool> compareTitle)
    {
      IntPtr windowHandle = IntPtr.Zero;
      Vip72Rieng.EnumThreadWindows(threadId, (Vip72Rieng.EnumWindowsProc) ((hWnd, lParam) =>
      {
        StringBuilder text = new StringBuilder(200);
        Vip72Rieng.GetWindowText(hWnd, text, 200);
        bool flag;
        if (compareTitle(text.ToString()))
        {
          windowHandle = hWnd;
          flag = false;
        }
        else
          flag = true;
        return flag;
      }), IntPtr.Zero);
      return windowHandle;
    }

    public bool vip72login(string username, string password, int mainPort)
    {
        Thread.Sleep(1000);
        foreach (Process process in this.GetProcessByName("vip72socks"))
        {
            bool flag = Vip72Rieng.ControlGetText(Vip72Rieng.ControlGetHandle(Vip72Rieng.FindWindowInProcess(process, (string s) => s.StartsWith("VIP72 Socks Client")), "Static", 7955)).EndsWith(":" + mainPort.ToString());
            if (flag)
            {
                Vip72Rieng.Vip72RiengProcess = process;
                break;
            }
        }
        bool flag2 = Vip72Rieng.Vip72RiengProcess != null && !Vip72Rieng.Vip72RiengProcess.HasExited && Vip72Rieng.Vip72RiengProcess.Responding;
        if (flag2)
        {
            bool flag3 = !Vip72Rieng.ControlGetText(Vip72Rieng.ControlGetHandle(Vip72Rieng.FindWindowInProcess(Vip72Rieng.Vip72RiengProcess, (string s) => s.StartsWith("VIP72 Socks Client")), "Static", 7955)).EndsWith(":" + mainPort.ToString());
            if (flag3)
            {
                bool flag4 = !Vip72Rieng.Vip72RiengProcess.HasExited;
                if (flag4)
                {
                    Vip72Rieng.Vip72RiengProcess.Kill();
                }
                Vip72Rieng.Vip72RiengProcess = null;
            }
        }
        else
        {
            bool flag5 = Vip72Rieng.Vip72RiengProcess != null && !Vip72Rieng.Vip72RiengProcess.Responding;
            if (flag5)
            {
                bool flag6 = !Vip72Rieng.Vip72RiengProcess.HasExited;
                if (flag6)
                {
                    Vip72Rieng.Vip72RiengProcess.Kill();
                }
                Vip72Rieng.Vip72RiengProcess = null;
            }
        }
        bool flag7 = Vip72Rieng.Vip72RiengProcess == null || Vip72Rieng.Vip72RiengProcess.HasExited;
        if (flag7)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "vip72socks.exe",
                WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory + "vip72",
                Arguments = "-mp:" + mainPort.ToString()
            };
            Vip72Rieng.Vip72RiengProcess = Process.Start(startInfo);
            IntPtr value = Vip72Rieng.OpenProcess(2035711, false, Vip72Rieng.Vip72RiengProcess.Id);
            byte[] lpBuffer = new byte[]
            {
                235
            };
            int num = 0;
            Vip72Rieng.WriteProcessMemory((int)value, 4234074, lpBuffer, 1, ref num);
        }
        Thread.Sleep(500);
        IntPtr parentHandle = Vip72Rieng.FindWindowInProcess(Vip72Rieng.Vip72RiengProcess, (string s) => s.StartsWith("VIP72 Socks Client"));
        IntPtr intPtr = Vip72Rieng.ControlGetHandle(parentHandle, "Button", 119);
        IntPtr hwnd = Vip72Rieng.ControlGetHandle(parentHandle, "Static", 7955);
        bool flag8 = intPtr != IntPtr.Zero;
        bool result;
        if (flag8)
        {
            Vip72Rieng.ControlSetText(hwnd, "Port:" + mainPort.ToString());
            bool flag9 = !Vip72Rieng.ControlGetState(intPtr, 134217728);
            if (flag9)
            {
                Vip72Rieng.ControlSetText(Vip72Rieng.ControlGetHandle(parentHandle, "Edit", 303), username);
                Vip72Rieng.ControlSetText(Vip72Rieng.ControlGetHandle(parentHandle, "Edit", 301), password);
                Vip72Rieng.ControlClick(intPtr);
                Vip72.ControlSetText(Vip72.ControlGetHandle("", "#32770", "Edit", 111), frmMain.tokenvip);
                IntPtr hwnd2 = Vip72.ControlGetHandle("", "#32770", "Button", 88);
                Vip72.ControlClick(hwnd2);
                IntPtr hwnd3 = Vip72Rieng.ControlGetHandle(parentHandle, "Edit", 131);
                DateTime now = DateTime.Now;
                while (Vip72Rieng.ControlGetText(hwnd3) != "System ready")
                {
                    bool flag10 = Vip72Rieng.ControlGetText(hwnd3) == "ERROR!Login and Password is incorrect";
                    if (flag10)
                    {
                        return false;
                    }
                    bool flag11 = !Vip72Rieng.ControlGetState(intPtr, 134217728);
                    if (flag11)
                    {
                        return false;
                    }
                    Thread.Sleep(100);
                    bool flag12 = (DateTime.Now - now).Seconds > 20;
                    if (flag12)
                    {
                        return false;
                    }
                }
                Thread.Sleep(3000);
            }
            result = true;
        }
        else
        {
            result = this.vip72login(username, password, mainPort);
        }
        return result;
    }

        public void waitiotherVip72Rieng()
    {
    }

        public void clearIpWithPort(int port)
        {
            Process[] kq = this.GetProcessByName("vip72socks");
            bool flag = kq.Count<Process>() > 0;
            if (flag)
            {
                foreach (Process process in kq)
                {
                    bool flag2 = Vip72Rieng.ControlGetText(Vip72Rieng.ControlGetHandle(Vip72Rieng.FindWindowInProcess(Vip72Rieng.Vip72RiengProcess, (string s) => s.StartsWith("VIP72 Socks Client")), "Static", 7955)).EndsWith(":" + port.ToString());
                    if (flag2)
                    {
                        process.Kill();
                    }
                }
            }
        }

        public void clearip()
    {
      if (Vip72Rieng.Vip72RiengProcess.HasExited)
        return;
      Vip72Rieng.OpenProcess(2035711, false, Vip72Rieng.Vip72RiengProcess.Id);
      byte[] numArray = new byte[2]
      {
        (byte) 144,
        (byte) 144
      };
      IntPtr handle = Vip72Rieng.ControlGetHandle(Vip72Rieng.FindWindowInProcess(Vip72Rieng.Vip72RiengProcess, (Func<string, bool>) (s => s.StartsWith("Vip72Rieng Socks Client"))), "SysListView32", 117);
      for (int index = 0; index < 30; ++index)
      {
        int lpdwResult = 0;
        Vip72Rieng.SendMessageTimeout(handle, 256, (IntPtr) 46, IntPtr.Zero, 2, 5000, out lpdwResult);
      }
    }

    public void testssh()
    {
      Thread.Sleep(1000);
      Process[] processByName = this.GetProcessByName("BvSsh");
      int index = 0;
      if (index < processByName.Length)
        Vip72Rieng.Vip72RiengProcess = processByName[index];
      Vip72Rieng.ControlGetHandle((IntPtr) 61454, "CLogCtrl", 1002);
      DateTime now = DateTime.Now;
      Vip72Rieng.ControlGetText((IntPtr) 398388);
      IntPtr hWnd = (IntPtr) 398388;
      StringBuilder text1 = new StringBuilder();
      StringBuilder text2 = new StringBuilder(Vip72Rieng.SendMessage(hWnd, 14, (int) IntPtr.Zero, text1) + 1);
      Vip72Rieng.SendMessage(hWnd, 13, text2.Capacity, text2);
      text2.ToString();
    }

    public string clickip(int port)
    {
      string str;
      if (Vip72Rieng.Vip72RiengProcess.HasExited)
      {
        str = "not running";
      }
      else
      {
        IntPtr windowInProcess = Vip72Rieng.FindWindowInProcess(Vip72Rieng.Vip72RiengProcess, (Func<string, bool>) (s => s.StartsWith("VIP72 Socks Client")));
        int num1 = 4328465;
        int lpBaseAddress = 4324304;
        IntPtr handle1 = Vip72Rieng.ControlGetHandle(windowInProcess, "Button", 7817);
        if (Vip72Rieng.ControlGetCheck(handle1))
          Vip72Rieng.ControlClick(handle1);
        IntPtr hProcess = Vip72Rieng.OpenProcess(2035711, false, Vip72Rieng.Vip72RiengProcess.Id);
        int lpNumberOfBytesRead = 0;
        IntPtr zero = IntPtr.Zero;
        IntPtr lpBuffer1 = Marshal.AllocHGlobal(4);
        Vip72Rieng.ReadProcessMemory(hProcess, (IntPtr) num1, lpBuffer1, 4, out lpNumberOfBytesRead);
        Random random = new Random();
        uint id = (uint) Vip72Rieng.Vip72RiengProcess.Id;
        IntPtr handle2 = Vip72Rieng.ControlGetHandle(windowInProcess, "SysListView32", 117);
        int num2 = 0;
        while (ListViewItem1.GetListViewItem(handle2, id, num2, 4) != "")
        {
          string listViewItem = ListViewItem1.GetListViewItem(handle2, id, num2, 4);
          if ((listViewItem.Contains(port.ToString()) ? 1U : (listViewItem.Contains("main stream") ? 1U : 0U)) > 0U)
          {
            ListViewItem1.SelectListViewItem(handle2, id, num2);
            int lpdwResult = 0;
            Vip72Rieng.SendMessageTimeout(handle2, 256, (IntPtr) 46, IntPtr.Zero, 2, 5000, out lpdwResult);
          }
          else
            ++num2;
        }
        int num3 = 0;
        IntPtr handle3 = Vip72Rieng.ControlGetHandle(windowInProcess, "SysListView32", 116);
        while (ListViewItem1.GetListViewItem(handle3, id, num3, 0) != "")
          ++num3;
        int maxValue = num3;
        if (maxValue == 0)
        {
          str = "no IP";
        }
        else
        {
          int num4 = 0;
          int num5 = -1;
          for (; ListViewItem1.GetListViewItem(handle3, id, num4, 0) != ""; ++num4)
          {
            if (ListViewItem1.GetListViewItem(handle3, id, num4, 0).Contains(".**"))
            {
              num5 = random.Next(0, maxValue);
              while (!ListViewItem1.GetListViewItem(handle3, id, num5, 0).Contains(".**"))
                num5 = random.Next(0, maxValue);
              break;
            }
          }
          if (num5 == -1)
          {
            str = "no IP";
          }
          else
          {
            int[] lpBuffer2 = new int[1]{ num5 };
            int lpNumberOfBytesWritten = 0;
            Vip72Rieng.WriteProcessMemory((int) hProcess, lpBaseAddress, lpBuffer2, 4, ref lpNumberOfBytesWritten);
            ListViewItem1.SelectListViewItem(handle3, id, num5);
            Vip72Rieng.ControlDoubleClick(handle3);
            Thread.Sleep(500);
            IntPtr handle4 = Vip72Rieng.ControlGetHandle(windowInProcess, "Button", 7303);
            IntPtr handle5 = Vip72Rieng.ControlGetHandle(windowInProcess, "Edit", 131);
            DateTime now = DateTime.Now;
            while (!Vip72Rieng.ControlGetCheck(handle4))
            {
              if (!Vip72Rieng.ControlGetText(handle5).Contains("ffline"))
              {
                if (!Vip72Rieng.ControlGetText(handle5).Contains("limit"))
                {
                  if (!Vip72Rieng.ControlGetText(handle5).Contains("can't"))
                  {
                    if (!Vip72Rieng.ControlGetText(handle5).Contains("disconnect"))
                    {
                      if (!Vip72Rieng.ControlGetText(handle5).Contains("aximum"))
                      {
                        if ((DateTime.Now - now).TotalSeconds > 15.0)
                        {
                          str = "timeout";
                          goto label_49;
                        }
                      }
                      else
                      {
                        str = "maximum";
                        goto label_49;
                      }
                    }
                    else
                    {
                      str = "dead";
                      goto label_49;
                    }
                  }
                  else
                  {
                    str = "dead";
                    goto label_49;
                  }
                }
                else
                {
                  try
                  {
                    if (!Vip72Rieng.Vip72RiengProcess.HasExited)
                      Vip72Rieng.Vip72RiengProcess.Kill();
                  }
                  catch (Exception ex)
                  {
                  }
                  str = "limited";
                  goto label_49;
                }
              }
              else
              {
                str = "dead";
                goto label_49;
              }
            }
            Thread.Sleep(500);
            IntPtr handle6 = Vip72Rieng.ControlGetHandle(windowInProcess, "SysListView32", 117);
            for (int index = 0; ListViewItem1.GetListViewItem(handle6, id, index, 4) != ""; ++index)
            {
              if (ListViewItem1.GetListViewItem(handle6, id, index, 4).Contains("main stream"))
              {
                str = ListViewItem1.GetListViewItem(handle6, id, index, 0);
                goto label_49;
              }
            }
            str = "limited";
          }
        }
      }
label_49:
      return str;
    }

    public Process[] GetProcessByName(string processName)
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

    public bool getip(byte countryindex, string country, string region, string coderegion, string city, string codecity)
    {
      byte[] lpBuffer1 = new byte[1];
      int[] lpBuffer2 = new int[1];
      lpBuffer1[0] = countryindex;
      int lpBaseAddress = 4482683;
      bool flag;
      if ((Vip72Rieng.Vip72RiengProcess == null ? 0U : (!Vip72Rieng.Vip72RiengProcess.HasExited ? 1U : 0U)) > 0U)
      {
        IntPtr windowInProcess = Vip72Rieng.FindWindowInProcess(Vip72Rieng.Vip72RiengProcess, (Func<string, bool>) (s => s.StartsWith("VIP72 Socks Client")));
        IntPtr num = Vip72Rieng.OpenProcess(2035711, false, Vip72Rieng.Vip72RiengProcess.Id);
        int lpNumberOfBytesWritten = 0;
        Vip72Rieng.WriteProcessMemory((int) num, lpBaseAddress, lpBuffer1, 1, ref lpNumberOfBytesWritten);
        lpBuffer2[0] = 0;
        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "citycode\\" + country.ToString() + ".dat"))
        {
          int int32_1 = Convert.ToInt32(coderegion);
          if (int32_1 != (int) ushort.MaxValue)
          {
            lpBuffer2[0] = int32_1;
            Vip72Rieng.WriteProcessMemory((int) num, lpBaseAddress + 1, lpBuffer2, 4, ref lpNumberOfBytesWritten);
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "citycode\\Cities\\" + region.ToString() + ".dat"))
            {
              int int32_2 = Convert.ToInt32(codecity);
              if (int32_2 != (int) ushort.MaxValue)
              {
                lpBuffer2[0] = int32_2;
                Vip72Rieng.WriteProcessMemory((int) num, 4482686, lpBuffer2, 4, ref lpNumberOfBytesWritten);
              }
            }
          }
        }
        IntPtr handle = Vip72Rieng.ControlGetHandle(windowInProcess, "Button", (int) sbyte.MaxValue);
        Vip72Rieng.ControlClick(handle);
        Vip72Rieng.ControlGetHandle(windowInProcess, "Edit", 131);
        DateTime now = DateTime.Now;
        while (Vip72Rieng.ControlGetState(handle, 134217728))
        {
          Thread.Sleep(100);
          if (!Vip72Rieng.Vip72RiengProcess.HasExited)
          {
            if ((!Vip72Rieng.Vip72RiengProcess.Responding ? 1U : ((DateTime.Now - now).TotalSeconds > 30.0 ? 1U : 0U)) > 0U)
            {
              try
              {
                if (!Vip72Rieng.Vip72RiengProcess.HasExited)
                  Vip72Rieng.Vip72RiengProcess.Kill();
              }
              catch (Exception ex)
              {
              }
              flag = false;
              goto label_19;
            }
          }
          else
          {
            flag = false;
            goto label_19;
          }
        }
      }
      flag = true;
label_19:
      return flag;
    }

    public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);
  }
}
