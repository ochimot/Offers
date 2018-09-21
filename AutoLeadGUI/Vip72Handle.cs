// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.Vip72Handle
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace AutoLeadGUI
{
  internal class Vip72Handle
  {
    private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    public static Point InvalidPoint = new Point(-100000, -100000);
    private static int hHook = 0;
    private Vip72Handle.WinEventDelegate procDelegate = (Vip72Handle.WinEventDelegate) null;
    private IntPtr _MainHandle;
    private const int WM_GETTEXT = 13;
    private const int WM_GETTEXTLENGTH = 14;
    private const uint EVENT_OBJECT_NAMECHANGE = 32780;
    private const uint WINEVENT_OUTOFCONTEXT = 0;
    private const uint SWP_NOSIZE = 1;
    private const uint SWP_NOMOVE = 2;
    private const uint SWP_SHOWWINDOW = 64;
    public const uint WS_OVERLAPPED = 0;
    public const uint WS_POPUP = 2147483648;
    public const uint WS_CHILD = 1073741824;
    public const uint WS_MINIMIZE = 536870912;
    public const uint WS_VISIBLE = 268435456;
    public const uint WS_DISABLED = 134217728;
    public const uint WS_CLIPSIBLINGS = 67108864;
    public const uint WS_CLIPCHILDREN = 33554432;
    public const uint WS_MAXIMIZE = 16777216;
    public const uint WS_CAPTION = 12582912;
    public const uint WS_BORDER = 8388608;
    public const uint WS_DLGFRAME = 4194304;
    public const uint WS_VSCROLL = 2097152;
    public const uint WS_HSCROLL = 1048576;
    public const uint WS_SYSMENU = 524288;
    public const uint WS_THICKFRAME = 262144;
    public const uint WS_GROUP = 131072;
    public const uint WS_TABSTOP = 65536;
    public const uint WS_MINIMIZEBOX = 131072;
    public const uint WS_MAXIMIZEBOX = 65536;
    public const uint WS_TILED = 0;
    public const uint WS_ICONIC = 536870912;
    public const uint WS_SIZEBOX = 262144;
    private const int LVM_FIRST = 4096;
    private const int LVM_GETITEMCOUNT = 4100;
    private const int LVM_GETITEM = 4171;
    private const int LVIF_TEXT = 1;
    private const uint WM_LBUTTONDBLCLK = 515;
    private const uint BM_CLICK = 245;
    private const uint HDM_FIRST = 4608;
    private const uint LVM_GETITEMW = 4171;
    private const uint LVM_GETHEADER = 4127;
    private const uint HDM_GETITEMCOUNT = 4608;
    private const uint LVM_SETITEMSTATE = 4139;
    private const uint LVIS_SELECTED = 2;
    private const uint PROCESS_VM_OPERATION = 8;
    private const uint PROCESS_VM_READ = 16;
    private const uint PROCESS_VM_WRITE = 32;
    private const uint MEM_COMMIT = 4096;
    private const uint MEM_RELEASE = 32768;
    private const uint MEM_RESERVE = 8192;
    private const uint PAGE_READWRITE = 4;
    private const uint LVIS_FOCUSED = 1;
    private const uint LVM_GETITEMPOSITION = 4112;
    private const uint LVM_SETITEMPOSITION = 4111;
    private const int WM_VSCROLL = 277;
    private const int SB_HORZ = 0;
    private const int SB_VERT = 1;
    private const int MOUSEEVENTF_MOVE = 1;
    private const int MOUSEEVENTF_LEFTDOWN = 2;
    private const int MOUSEEVENTF_LEFTUP = 4;
    private const int MOUSEEVENTF_RIGHTDOWN = 8;
    private const int MOUSEEVENTF_RIGHTUP = 16;
    private const int MOUSEEVENTF_MIDDLEDOWN = 32;
    private const int MOUSEEVENTF_MIDDLEUP = 64;
    private const int MOUSEEVENTF_ABSOLUTE = 32768;
    public const int WH_MOUSE = 7;
    private Vip72Handle.HookProc MouseHookProcedure;
    private Vip72Handle.HookProc SetTextHookProcedure;
    private IntPtr statusHandle;
    private const int WH_GETMESSAGE = 3;
    private const int WH_CALLWNDPROC = 4;

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetWindowInfo(IntPtr hwnd, ref Vip72Handle.WINDOWINFO pwi);

    [DllImport("user32.dll")]
    private static extern bool SetWindowText(IntPtr hWnd, string text);

    [DllImport("user32")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool EnumChildWindows(IntPtr window, Vip72Handle.EnumWindowProc callback, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int RegisterWindowMessage(string lpString);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool SendMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr SendMessage(int hWnd, int Msg, int wparam, int lparam);

    [DllImport("user32.dll")]
    private static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, Vip72Handle.WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

    [DllImport("user32.dll")]
    private static extern bool UnhookWinEvent(IntPtr hWinEventHook);

    public void RegisterControlforMessages()
    {
      Vip72Handle.RegisterWindowMessage("WM_GETTEXT");
    }

    public static string GetControlText(IntPtr hWnd)
    {
      StringBuilder lParam = new StringBuilder();
      int int32 = Vip72Handle.SendMessage((int) hWnd, 14, 0, 0).ToInt32();
      if (int32 > 0)
      {
        lParam = new StringBuilder(int32 + 1);
        Vip72Handle.SendMessage(hWnd, 13U, lParam.Capacity, lParam);
      }
      return lParam.ToString();
    }

    public Vip72Handle(IntPtr handle)
    {
      this._MainHandle = handle;
    }

    public List<IntPtr> GetAllChildHandles()
    {
      List<IntPtr> numList = new List<IntPtr>();
      GCHandle gcHandle = GCHandle.Alloc((object) numList);
      IntPtr intPtr = GCHandle.ToIntPtr(gcHandle);
      try
      {
        Vip72Handle.EnumChildWindows(this._MainHandle, new Vip72Handle.EnumWindowProc(this.EnumWindow), intPtr);
      }
      finally
      {
        gcHandle.Free();
      }
      return numList;
    }

    private bool EnumWindow(IntPtr hWnd, IntPtr lParam)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(lParam);
      if (gcHandle.Target == null)
        return false;
      (gcHandle.Target as List<IntPtr>).Add(hWnd);
      return true;
    }

    public static Vip72Handle.WINDOWINFO GetChildWindowInfo(IntPtr handle)
    {
      Vip72Handle.WINDOWINFO pwi = new Vip72Handle.WINDOWINFO(new bool?(false));
      Vip72Handle.GetWindowInfo(handle, ref pwi);
      return pwi;
    }

    public List<Vip72Handle.ChildWindowItem> GetAllChildWindowItems()
    {
      List<Vip72Handle.ChildWindowItem> childWindowItemList = new List<Vip72Handle.ChildWindowItem>();
      List<IntPtr> allChildHandles = this.GetAllChildHandles();
      Vip72Handle.WINDOWINFO childWindowInfo1 = Vip72Handle.GetChildWindowInfo(this._MainHandle);
      for (int index = 0; index < allChildHandles.Count; ++index)
      {
        IntPtr num = allChildHandles[index];
        string controlText = Vip72Handle.GetControlText(num);
        Vip72Handle.WINDOWINFO childWindowInfo2 = Vip72Handle.GetChildWindowInfo(num);
        Vip72Handle.ChildWindowItem childWindowItem;
        childWindowItem.rect.y = childWindowInfo2.rcWindow.top - childWindowInfo1.rcWindow.top;
        childWindowItem.rect.x = childWindowInfo2.rcWindow.left - childWindowInfo1.rcWindow.left;
        childWindowItem.rect.h = childWindowInfo2.rcWindow.bottom - childWindowInfo2.rcWindow.top;
        childWindowItem.rect.w = childWindowInfo2.rcWindow.right - childWindowInfo2.rcWindow.left;
        childWindowItem.className = Vip72Handle.GetWindowClassName(num);
        childWindowItem.text = controlText;
        childWindowItem.handle = num;
        childWindowItem.info = childWindowInfo2;
        childWindowItemList.Add(childWindowItem);
      }
      childWindowItemList.Sort((Comparison<Vip72Handle.ChildWindowItem>) ((p1, p2) =>
      {
        if (p1.rect.x == p2.rect.x || p1.rect.y != p2.rect.y)
          return p1.rect.y.CompareTo(p2.rect.y);
        return p1.rect.x.CompareTo(p2.rect.x);
      }));
      return childWindowItemList;
    }

    public IntPtr GetWindowHandle(Vip72Handle.Vip72Window window)
    {
      List<Vip72Handle.ChildWindowItem> childWindowItems = this.GetAllChildWindowItems();
      if (childWindowItems.Count == 50)
      {
        switch (window)
        {
          case Vip72Handle.Vip72Window.Exit:
            return childWindowItems[25].handle;
          case Vip72Handle.Vip72Window.Login:
            return childWindowItems[17].handle;
          case Vip72Handle.Vip72Window.Hide:
            return childWindowItems[24].handle;
          case Vip72Handle.Vip72Window.GetRandomList:
            return childWindowItems[23].handle;
          case Vip72Handle.Vip72Window.GetProxyByGEO:
            return childWindowItems[40].handle;
          case Vip72Handle.Vip72Window.Username:
            return childWindowItems[12].handle;
          case Vip72Handle.Vip72Window.Password:
            return childWindowItems[13].handle;
          case Vip72Handle.Vip72Window.SelectCountry:
            return childWindowItems[25].handle;
          case Vip72Handle.Vip72Window.SelectCountryListView:
            if (Vip72Handle.IsWindowVisible(childWindowItems[15].handle))
              return childWindowItems[15].handle;
            break;
          case Vip72Handle.Vip72Window.SelectRegion:
            return childWindowItems[27].handle;
          case Vip72Handle.Vip72Window.Info:
            return childWindowItems[2].handle;
          case Vip72Handle.Vip72Window.Status:
            return childWindowItems[49].handle;
          case Vip72Handle.Vip72Window.Socks5:
            return childWindowItems[1].handle;
          case Vip72Handle.Vip72Window.HTTPProxy:
            return childWindowItems[1].handle;
          case Vip72Handle.Vip72Window.MyProxies:
            return childWindowItems[5].handle;
          case Vip72Handle.Vip72Window.AllProxies:
            return childWindowItems[4].handle;
          case Vip72Handle.Vip72Window.MyProxiesListView:
            if (Vip72Handle.IsButtonChecked(this.GetWindowHandle(Vip72Handle.Vip72Window.MyProxies)))
            {
              if (Vip72Handle.IsWindowVisible(childWindowItems[7].handle))
                return childWindowItems[7].handle;
              if (Vip72Handle.IsWindowVisible(childWindowItems[8].handle))
                return childWindowItems[8].handle;
            }
            else
            {
              if (!Vip72Handle.IsWindowVisible(childWindowItems[7].handle))
                return childWindowItems[7].handle;
              if (!Vip72Handle.IsWindowVisible(childWindowItems[8].handle))
                return childWindowItems[8].handle;
            }
            break;
          case Vip72Handle.Vip72Window.AllProxiesListView:
            if (Vip72Handle.IsButtonChecked(this.GetWindowHandle(Vip72Handle.Vip72Window.AllProxies)))
            {
              if (Vip72Handle.IsWindowVisible(childWindowItems[7].handle))
                return childWindowItems[7].handle;
              if (Vip72Handle.IsWindowVisible(childWindowItems[8].handle))
                return childWindowItems[8].handle;
              break;
            }
            if (!Vip72Handle.IsWindowVisible(childWindowItems[7].handle))
              return childWindowItems[7].handle;
            if (!Vip72Handle.IsWindowVisible(childWindowItems[8].handle))
              return childWindowItems[8].handle;
            break;
          case Vip72Handle.Vip72Window.CurrentSock:
            return childWindowItems[41].handle;
        }
      }
      return new IntPtr(-1);
    }

    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    public void setVip72TopMost()
    {
      Vip72Handle.SetWindowPos(this._MainHandle, Vip72Handle.HWND_TOPMOST, 0, 0, 0, 0, 67U);
    }

    public static bool IsButtonChecked(IntPtr handle)
    {
      return (Vip72Handle.GetChildWindowInfo(handle).dwStyle & 65536U) > 0U;
    }

    public static bool IsWindowVisible(IntPtr handle)
    {
      return (Vip72Handle.GetChildWindowInfo(handle).dwStyle & 268435456U) > 0U;
    }

    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint dwProcessId);

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

    [DllImport("kernel32.dll")]
    private static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

    [DllImport("kernel32.dll")]
    private static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint dwFreeType);

    [DllImport("kernel32.dll")]
    private static extern bool CloseHandle(IntPtr handle);

    [DllImport("kernel32.dll")]
    private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, ref uint vNumberOfBytesRead);

    [DllImport("kernel32.dll")]
    private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, ref uint vNumberOfBytesRead);

    [DllImport("user32.DLL")]
    private static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

    public static int ListView_GetItemCount(IntPtr hwnd)
    {
      return Vip72Handle.SendMessage(hwnd, 4100U, 0, 0);
    }

    public static IntPtr ListView_GetHeader(IntPtr hwnd)
    {
      return (IntPtr) Vip72Handle.SendMessage(hwnd, 4127U, 0, 0);
    }

    public static int Header_GetItemCount(IntPtr header)
    {
      return Vip72Handle.SendMessage(header, 4608U, 0, 0);
    }

    public static int ListViewColumnCount(IntPtr listViewHandle)
    {
      return Vip72Handle.Header_GetItemCount(Vip72Handle.ListView_GetHeader(listViewHandle));
    }

    [DllImport("user32.dll")]
    private static extern bool RedrawWindow(IntPtr hWnd, [In] ref Vip72Handle.RECT lprcUpdate, IntPtr hrgnUpdate, Vip72Handle.RedrawWindowFlags flags);

    [DllImport("user32.dll")]
    private static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, Vip72Handle.RedrawWindowFlags flags);

    public static void deleteListViewItem(IntPtr handle, int index)
    {
      Point index1 = Vip72Handle.scrollListViewToIndex(handle, index);
      if (index1 == Vip72Handle.InvalidPoint)
        return;
      index1.X += 5;
      index1.Y += 5;
      Thread.Sleep(200);
      ClickOnPointTool.ClickOnPoint(handle, index1);
      ClickOnPointTool.SendDeleteKey(handle);
    }

    public static void selectListViewItem(IntPtr handle, int index, bool doubleclick)
    {
      Point index1 = Vip72Handle.scrollListViewToIndex(handle, index);
      if (index1 == Vip72Handle.InvalidPoint)
        return;
      index1.X += 5;
      index1.Y += 5;
      Thread.Sleep(200);
      if (doubleclick)
      {
        ClickOnPointTool.ClickOnPoint(handle, index1);
        ClickOnPointTool.ClickOnPoint(handle, index1);
      }
      else
        ClickOnPointTool.ClickOnPoint(handle, index1);
    }

    public static Point GetListViewItemPoint(IntPtr handle, int index)
    {
      uint dwProcessId;
      int windowThreadProcessId = (int) Vip72Handle.GetWindowThreadProcessId(handle, out dwProcessId);
      IntPtr num1 = Vip72Handle.OpenProcess(56U, false, dwProcessId);
      IntPtr num2 = Vip72Handle.VirtualAllocEx(num1, IntPtr.Zero, 4096U, 12288U, 4U);
      try
      {
        Point[] arr = new Point[1];
        uint vNumberOfBytesRead = 0;
        Vip72Handle.WriteProcessMemory(num1, num2, Marshal.UnsafeAddrOfPinnedArrayElement<Point>(arr, 0), Marshal.SizeOf(typeof (Vip72Handle.LVITEM)), ref vNumberOfBytesRead);
        Vip72Handle.SendMessage(handle, 4112U, index, num2.ToInt32());
        Vip72Handle.ReadProcessMemory(num1, num2, Marshal.UnsafeAddrOfPinnedArrayElement<Point>(arr, 0), Marshal.SizeOf(typeof (Point)), ref vNumberOfBytesRead);
        return arr[0];
      }
      catch (Exception ex)
      {
        Console.WriteLine((object) ex);
      }
      finally
      {
        Vip72Handle.VirtualFreeEx(num1, num2, 0U, 32768U);
        Vip72Handle.CloseHandle(num1);
      }
      return Vip72Handle.InvalidPoint;
    }

    [DllImport("user32.dll")]
    private static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int GetScrollPos(IntPtr hWnd, int nBar);

    public static Point scrollListViewToIndex(IntPtr handle, int index)
    {
      if (Vip72Handle.ListView_GetItemCount(handle) <= index)
        return Vip72Handle.InvalidPoint;
      Size windowSize = Vip72Handle.GetWindowSize(handle);
      Point listViewItemPoint = Vip72Handle.GetListViewItemPoint(handle, index);
      if (listViewItemPoint != Vip72Handle.InvalidPoint)
      {
        int num = 0;
        if (listViewItemPoint.Y > windowSize.Height - 25)
          num = 1;
        if (listViewItemPoint.Y < 25)
          num = -1;
        for (int index1 = 30; num != 0 && index1 > 0; --index1)
        {
          switch (num)
          {
            case -1:
              int wParam1 = 2;
              Vip72Handle.SendMessage(handle, 277U, wParam1, 0);
              break;
            case 1:
              int wParam2 = 3;
              Vip72Handle.SendMessage(handle, 277U, wParam2, 0);
              break;
          }
          listViewItemPoint = Vip72Handle.GetListViewItemPoint(handle, index);
          num = 0;
          if (listViewItemPoint.Y > windowSize.Height - 25)
            num = 1;
          if (listViewItemPoint.Y < 25)
            num = -1;
        }
      }
      return listViewItemPoint;
    }

    public static Size GetWindowSize(IntPtr handle)
    {
      Vip72Handle.WINDOWINFO pwi = new Vip72Handle.WINDOWINFO(new bool?(false));
      Vip72Handle.GetWindowInfo(handle, ref pwi);
      return new Size(pwi.rcWindow.right - pwi.rcWindow.left, pwi.rcWindow.bottom - pwi.rcWindow.top);
    }

    public static List<List<string>> GetListViewItems(IntPtr handle)
    {
      int itemCount = Vip72Handle.ListView_GetItemCount(handle);
      int num1 = Vip72Handle.ListViewColumnCount(handle);
      List<List<string>> stringListList = new List<List<string>>();
      uint dwProcessId;
      int windowThreadProcessId = (int) Vip72Handle.GetWindowThreadProcessId(handle, out dwProcessId);
      IntPtr num2 = Vip72Handle.OpenProcess(56U, false, dwProcessId);
      IntPtr num3 = Vip72Handle.VirtualAllocEx(num2, IntPtr.Zero, 16384U, 12288U, 4U);
      try
      {
        for (int index1 = 0; index1 < itemCount; ++index1)
        {
          List<string> stringList = new List<string>();
          for (int index2 = 0; index2 < num1; ++index2)
          {
            try
            {
              byte[] arr1 = new byte[256];
              Vip72Handle.LVITEM[] arr2 = new Vip72Handle.LVITEM[1];
              arr2[0].mask = 1;
              arr2[0].iItem = index1;
              arr2[0].iSubItem = index2;
              arr2[0].cchTextMax = arr1.Length;
              arr2[0].pszText = (IntPtr) ((int) num3 + Marshal.SizeOf(typeof (Vip72Handle.LVITEM)));
              uint vNumberOfBytesRead = 0;
              Vip72Handle.WriteProcessMemory(num2, num3, Marshal.UnsafeAddrOfPinnedArrayElement<Vip72Handle.LVITEM>(arr2, 0), Marshal.SizeOf(typeof (Vip72Handle.LVITEM)), ref vNumberOfBytesRead);
              Vip72Handle.SendMessage(handle, 4171U, 0, num3.ToInt32());
              Vip72Handle.ReadProcessMemory(num2, (IntPtr) ((int) num3 + Marshal.SizeOf(typeof (Vip72Handle.LVITEM))), Marshal.UnsafeAddrOfPinnedArrayElement<byte>(arr1, 0), arr1.Length, ref vNumberOfBytesRead);
              string stringUni = Marshal.PtrToStringUni(Marshal.UnsafeAddrOfPinnedArrayElement<byte>(arr1, 0));
              stringList.Add(stringUni);
            }
            catch (Exception ex)
            {
              stringList.Add("");
              Console.WriteLine((object) ex);
            }
          }
          stringListList.Add(stringList);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine((object) ex);
      }
      finally
      {
        Vip72Handle.VirtualFreeEx(num2, num3, 0U, 32768U);
        Vip72Handle.CloseHandle(num2);
      }
      return stringListList;
    }

    [DllImport("user32.dll")]
    private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool ShowWindow(IntPtr hWnd, Vip72Handle.ShowWindowCommands nCmdShow);

    public bool showWindow()
    {
      bool flag = Vip72Handle.ShowWindow(this._MainHandle, Vip72Handle.ShowWindowCommands.Show);
      this.setVip72TopMost();
      return flag;
    }

    public bool hideWindow()
    {
      return Vip72Handle.ShowWindow(this._MainHandle, Vip72Handle.ShowWindowCommands.Hide);
    }

    public void SetWindowText(Vip72Handle.Vip72Window window, string text)
    {
      Vip72Handle.SetWindowText(this.GetWindowHandle(window), text);
    }

    public void SendLeftClick(int x, int y)
    {
      Console.WriteLine("x :" + (object) x + " y: " + (object) y);
      Vip72Handle.MoveTo(x, y);
      Vip72Handle.mouse_event(2, x, y, 0, 0);
      Vip72Handle.mouse_event(4, x, y, 0, 0);
    }

    public static void Move(int xDelta, int yDelta)
    {
      Vip72Handle.mouse_event(1, xDelta, yDelta, 0, 0);
    }

    public static void MoveTo(int x, int y)
    {
      Vip72Handle.mouse_event(32769, x, y, 0, 0);
    }

    public static string GetWindowClassName(IntPtr handle)
    {
      StringBuilder lpClassName = new StringBuilder(256);
      if ((uint) Vip72Handle.GetClassName(handle, lpClassName, lpClassName.Capacity) > 0U)
        return lpClassName.ToString();
      return "";
    }

    public void SendLeftClickToWindow(Vip72Handle.Vip72Window window)
    {
      this.setVip72TopMost();
      Vip72Handle.SetForegroundWindow(this._MainHandle);
      Vip72Handle.GetChildWindowInfo(this.GetWindowHandle(window));
      Point clientPoint = new Point(5, 5);
      ClickOnPointTool.ClickOnPoint(this.GetWindowHandle(window), clientPoint);
    }

    public void SendDeleteKeyToWindow(Vip72Handle.Vip72Window window)
    {
    }

    public void SendRightClickToWindow(Vip72Handle.Vip72Window window)
    {
      this.setVip72TopMost();
      Vip72Handle.SetForegroundWindow(this._MainHandle);
      Vip72Handle.GetChildWindowInfo(this.GetWindowHandle(window));
      Point clientPoint = new Point(5, 5);
      ClickOnPointTool.RightClickOnPoint(this.GetWindowHandle(window), clientPoint);
    }

    public void SendLeftClickToWindow(Vip72Handle.Vip72Window window, Point point)
    {
      this.setVip72TopMost();
      Vip72Handle.SetForegroundWindow(this._MainHandle);
      Vip72Handle.GetChildWindowInfo(this.GetWindowHandle(window));
      ClickOnPointTool.ClickOnPoint(this.GetWindowHandle(window), point);
    }

    public void SendRightClickToWindow(Vip72Handle.Vip72Window window, Point point)
    {
      this.setVip72TopMost();
      Vip72Handle.SetForegroundWindow(this._MainHandle);
      Vip72Handle.GetChildWindowInfo(this.GetWindowHandle(window));
      ClickOnPointTool.RightClickOnPoint(this.GetWindowHandle(window), point);
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern int SetWindowsHookEx(int idHook, Vip72Handle.HookProc lpfn, IntPtr hInstance, int threadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern bool UnhookWindowsHookEx(int idHook);

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll")]
    private static extern int GetCurrentThreadId();

    public bool startEventListener()
    {
      this.statusHandle = this.GetWindowHandle(Vip72Handle.Vip72Window.Status);
      this.SetTextHookProcedure = new Vip72Handle.HookProc(this.SetTextHookProc);
      Vip72Handle.hHook = Vip72Handle.SetWindowsHookEx(3, this.SetTextHookProcedure, IntPtr.Zero, Vip72Handle.GetCurrentThreadId());
      return (uint) Vip72Handle.hHook > 0U;
    }

    public void stopEventListener()
    {
      if ((uint) Vip72Handle.hHook <= 0U)
        return;
      Vip72Handle.UnhookWindowsHookEx(Vip72Handle.hHook);
    }

    public int SetTextHookProc(int nCode, IntPtr wParam, IntPtr lParam)
    {
      Console.WriteLine("*** SetText");
      return Vip72Handle.CallNextHookEx(Vip72Handle.hHook, nCode, wParam, lParam);
    }

    public enum Vip72Window
    {
      Exit,
      Login,
      Hide,
      GetRandomList,
      GetProxyByIP,
      GetProxyByGEO,
      Username,
      Password,
      SelectCountry,
      SelectCountryListView,
      SelectRegion,
      Info,
      Status,
      Socks5,
      HTTPProxy,
      MyProxies,
      AllProxies,
      MyProxiesListView,
      AllProxiesListView,
      CurrentSock,
    }

    public struct WINDOWINFO
    {
      public uint cbSize;
      public Vip72Handle.RECT rcWindow;
      public Vip72Handle.RECT rcClient;
      public uint dwStyle;
      public uint dwExStyle;
      public uint dwWindowStatus;
      public uint cxWindowBorders;
      public uint cyWindowBorders;
      public ushort atomWindowType;
      public ushort wCreatorVersion;

      public WINDOWINFO(bool? filler)
      {
        this = new Vip72Handle.WINDOWINFO();
        this.cbSize = (uint) Marshal.SizeOf(typeof (Vip72Handle.WINDOWINFO));
      }
    }

    public struct RECT
    {
      public int left;
      public int top;
      public int right;
      public int bottom;
    }

    private delegate bool EnumWindowProc(IntPtr hwnd, IntPtr lParam);

    private delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

    public struct Rect
    {
      public int x;
      public int y;
      public int w;
      public int h;
    }

    public struct ChildWindowItem
    {
      public IntPtr handle;
      public Vip72Handle.Rect rect;
      public Vip72Handle.WINDOWINFO info;
      public string className;
      public string text;
    }

    public struct LVITEM
    {
      public int mask;
      public int iItem;
      public int iSubItem;
      public int state;
      public int stateMask;
      public IntPtr pszText;
      public int cchTextMax;
      public int iImage;
      public IntPtr lParam;
      public int iIndent;
      public int iGroupId;
      public int cColumns;
      public IntPtr puColumns;
    }

    private enum RedrawWindowFlags : uint
    {
      Invalidate = 1,
      InternalPaint = 2,
      Erase = 4,
      Validate = 8,
      NoInternalPaint = 16, // 0x00000010
      NoErase = 32, // 0x00000020
      NoChildren = 64, // 0x00000040
      AllChildren = 128, // 0x00000080
      UpdateNow = 256, // 0x00000100
      EraseNow = 512, // 0x00000200
      Frame = 1024, // 0x00000400
      NoFrame = 2048, // 0x00000800
    }

    public enum ScrollBarCommands
    {
      SB_LINELEFT = 0,
      SB_LINEUP = 0,
      SB_LINEDOWN = 1,
      SB_LINERIGHT = 1,
      SB_PAGELEFT = 2,
      SB_PAGEUP = 2,
      SB_PAGEDOWN = 3,
      SB_PAGERIGHT = 3,
      SB_THUMBPOSITION = 4,
      SB_THUMBTRACK = 5,
      SB_LEFT = 6,
      SB_TOP = 6,
      SB_BOTTOM = 7,
      SB_RIGHT = 7,
      SB_ENDSCROLL = 8,
    }

    private enum ShowWindowCommands
    {
      Hide = 0,
      Normal = 1,
      ShowMinimized = 2,
      Maximize = 3,
      ShowMaximized = 3,
      ShowNoActivate = 4,
      Show = 5,
      Minimize = 6,
      ShowMinNoActive = 7,
      ShowNA = 8,
      Restore = 9,
      ShowDefault = 10, // 0x0000000A
      ForceMinimize = 11, // 0x0000000B
    }

    public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

    [StructLayout(LayoutKind.Sequential)]
    public class POINT
    {
      public int x;
      public int y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class MouseHookStruct
    {
      public Vip72Handle.POINT pt;
      public int hwnd;
      public int wHitTestCode;
      public int dwExtraInfo;
    }
  }
}
