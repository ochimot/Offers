// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.sshcommand
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace AutoLeadGUI
{
  internal class sshcommand
  {
    private Process[] myProcess = Process.GetProcessesByName("program name here");
    public const int WM_LBUTTONDOWN = 513;
    public const int WM_LBUTTONUP = 514;

    [DllImport("User32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int SendMessageA(IntPtr hwnd, int wMsg, int wParam, uint lParam);

    [DllImport("user32.dll")]
    private static extern IntPtr FindWindow(string sClass, string sWindow);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, int msg, int Param, StringBuilder text);

    [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
    public static extern int SendMessage1(IntPtr hWnd, int msg, IntPtr Param, IntPtr text);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetDlgItem(int hwnd, int childID);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int GetWindowText(IntPtr hwnd, IntPtr windowString, int maxcount);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetWindowLong(IntPtr hwnd, int nIndex);

    [DllImport("user32", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool EnumThreadWindows(int threadId, sshcommand.EnumWindowsProc callback, IntPtr lParam);

    [DllImport("user32", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool EnumChildWindows(IntPtr hwndParent, sshcommand.EnumWindowsProc lpEnumFunc, IntPtr lParam);

    [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int maxCount);

    public static string ControlGetText(IntPtr hwnd)
    {
      StringBuilder text = new StringBuilder((int) byte.MaxValue);
      sshcommand.SendMessage(hwnd, 13, text.Capacity, text);
      return text.ToString();
    }

    public static void ControlClick(IntPtr hwnd)
    {
      sshcommand.SendMessage1(hwnd, 513, IntPtr.Zero, IntPtr.Zero);
      sshcommand.SendMessage1(hwnd, 514, IntPtr.Zero, IntPtr.Zero);
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
        IntPtr windowEx = sshcommand.FindWindowEx(parentHandle, hwndChildAfter, _controlClass, (string) null);
        int nIndex = -12;
        num = (int) sshcommand.GetWindowLong(windowEx, nIndex);
        hwndChildAfter = windowEx;
        IntPtr zero2 = IntPtr.Zero;
        if (windowEx == zero2)
          return IntPtr.Zero;
      }
      return hwndChildAfter;
    }

    public static IntPtr ControlGetHandle(string _text, string _class, string _controlClass, int ID)
    {
      IntPtr window = sshcommand.FindWindow(_class, _text);
      if (window == IntPtr.Zero)
        return IntPtr.Zero;
      IntPtr hwndChildAfter = IntPtr.Zero;
      IntPtr zero1 = IntPtr.Zero;
      int num = -1;
      while (num != ID)
      {
        IntPtr windowEx = sshcommand.FindWindowEx(window, hwndChildAfter, _controlClass, (string) null);
        int nIndex = -12;
        num = (int) sshcommand.GetWindowLong(windowEx, nIndex);
        hwndChildAfter = windowEx;
        IntPtr zero2 = IntPtr.Zero;
        if (windowEx == zero2)
          return IntPtr.Zero;
      }
      return hwndChildAfter;
    }

    public static IntPtr FindWindowInProcess(Process process, Func<string, bool> compareTitle)
    {
      IntPtr num = IntPtr.Zero;
      foreach (ProcessThread thread in (ReadOnlyCollectionBase) process.Threads)
      {
        num = sshcommand.FindWindowInThread(thread.Id, compareTitle);
        if (num != IntPtr.Zero)
          break;
      }
      return num;
    }

    private static IntPtr FindWindowInThread(int threadId, Func<string, bool> compareTitle)
    {
      IntPtr windowHandle = IntPtr.Zero;
      sshcommand.EnumThreadWindows(threadId, (sshcommand.EnumWindowsProc) ((hWnd, lParam) =>
      {
        StringBuilder text = new StringBuilder(200);
        sshcommand.GetWindowText(hWnd, text, 200);
        if (!compareTitle(text.ToString()))
          return true;
        windowHandle = hWnd;
        return false;
      }), IntPtr.Zero);
      return windowHandle;
    }

    public static void closebitvise(int port)
    {
      foreach (Process process in Process.GetProcessesByName("BvSsh"))
      {
        IntPtr windowInProcess = sshcommand.FindWindowInProcess(process, (Func<string, bool>) (s => s.StartsWith("Bitvise SSH Client")));
        StringBuilder stringBuilder = new StringBuilder(200);
        StringBuilder text = stringBuilder;
        int maxCount = 200;
        sshcommand.GetWindowText(windowInProcess, text, maxCount);
        Match match = new Regex("_(.*?).bscp").Match(stringBuilder.ToString());
        if (match.Success && match.Groups[1].Value == port.ToString())
        {
          process.Kill();
          process.Close();
          process.Dispose();
        }
      }
    }

    private static void createProfile(string IPforward, string Portforward, string fileLocation)
    {
      sshcommand.profile profile = new sshcommand.profile();
      FileStream fileStream = File.Open(AppDomain.CurrentDomain.BaseDirectory + "1.bscp", FileMode.Open);
      BinaryReader binaryReader = new BinaryReader((Stream) fileStream);
      long length = fileStream.Length;
      profile.header = binaryReader.ReadBytes(21);
      profile.length = binaryReader.ReadByte();
      profile.body = binaryReader.ReadBytes((int) length - 189);
      profile.iplen = binaryReader.ReadByte();
      profile.ip = binaryReader.ReadBytes((int) profile.iplen);
      profile.s1 = binaryReader.ReadBytes(3);
      profile.portlen = binaryReader.ReadByte();
      profile.port = binaryReader.ReadBytes((int) profile.portlen);
      profile.end = binaryReader.ReadBytes(149);
      profile.ip = Encoding.UTF8.GetBytes(IPforward);
      profile.length += (byte) ((uint) (IPforward.Length + Portforward.Length) - (uint) profile.iplen - (uint) profile.portlen);
      profile.iplen = (byte) IPforward.Length;
      profile.port = Encoding.UTF8.GetBytes(Portforward);
      profile.portlen = (byte) Portforward.Length;
      fileStream.Close();
      using (Stream output = (Stream) new FileStream(fileLocation, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
      {
        using (BinaryWriter binaryWriter = new BinaryWriter(output, Encoding.Default))
        {
          binaryWriter.Write(profile.header);
          binaryWriter.Write(profile.length);
          binaryWriter.Write(profile.body);
          binaryWriter.Write(profile.iplen);
          binaryWriter.Write(profile.ip);
          binaryWriter.Write(profile.s1);
          binaryWriter.Write(profile.portlen);
          binaryWriter.Write(profile.port);
          binaryWriter.Write(profile.end);
        }
        output.Close();
      }
    }

    public static bool SetSSH(string host, string username, string password, string ipforward, string portfoward, ref Process refproc)
    {
      string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
      string str = "\\Bitviseprofile";
      if (!File.Exists(directoryName + str))
        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Bitviseprofile");
      string fileLocation = Path.Combine(new Uri(directoryName).LocalPath + "\\Bitviseprofile", "_" + portfoward + ".bscp");
      sshcommand.createProfile(ipforward, portfoward, fileLocation);
      Process process = Process.Start(AppDomain.CurrentDomain.BaseDirectory + "Bitvise SSH Client\\BvSsh.exe", "-profile=\"" + fileLocation + "\" -host=" + host + " -port=22 -user=" + username + " -password=" + password + " -openTerm=n -openSFTP=n -openRDP=n -loginOnStartup -menu=small");
      refproc = process;
      while ((DateTime.Now - process.StartTime).Seconds < 1)
        Thread.Sleep(100);
      IntPtr windowInProcess = sshcommand.FindWindowInProcess(process, (Func<string, bool>) (s => s.StartsWith("Bitvise SSH Client")));
      while (windowInProcess == IntPtr.Zero)
      {
        windowInProcess = sshcommand.FindWindowInProcess(process, (Func<string, bool>) (s => s.StartsWith("Bitvise SSH Client")));
        Thread.Sleep(100);
      }
      if (!sshcommand.threadAccept(windowInProcess))
      {
        process.Kill();
        return false;
      }
      refproc = process;
      return true;
    }

    public static bool threadAccept(IntPtr hwnd)
    {
      IntPtr handle1 = sshcommand.ControlGetHandle(hwnd, "Button", 1);
      DateTime now = DateTime.Now;
      do
      {
        IntPtr handle2 = sshcommand.ControlGetHandle("Host Key Verification", "#32770", "Button", 102);
        if (handle2 != IntPtr.Zero)
          sshcommand.ControlClick(handle2);
        Thread.Sleep(100);
        sshcommand.ControlGetText(handle1);
        if (sshcommand.ControlGetText(handle1) == "Logout")
          return true;
      }
      while ((DateTime.Now - now).Seconds <= 15);
      return false;
    }

    public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);

    private struct profile
    {
      public byte[] header;
      public byte length;
      public byte[] body;
      public byte iplen;
      public byte[] ip;
      public byte[] s1;
      public byte portlen;
      public byte[] port;
      public byte[] end;
    }
  }
}
