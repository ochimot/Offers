// Decompiled with JetBrains decompiler
// Type: ControliPhone.ListViewItem1
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;
using System.Runtime.InteropServices;

namespace ControliPhone
{
  public class ListViewItem1
  {
    private static readonly int GWL_ID = -12;

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr SendMessageTimeout(IntPtr hWnd, int Msg, IntPtr wParam, string lParam, int fuFlags, int uTimeout, IntPtr lpdwResult);

    [DllImport("kernel32.dll")]
    internal static extern IntPtr OpenProcess(Win32ProcessAccessType dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwProcessId);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, Win32AllocationTypes flWin32AllocationType, Win32MemoryProtection flProtect);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, ref LV_ITEM lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, ref NMITEMACTIVATE lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, ref POINT lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, ref NMHDR lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int dwSize, out int lpNumberOfBytesRead);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, Win32AllocationTypes dwFreeType);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool CloseHandle(IntPtr hObject);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr SendMessageTimeout(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam, int fuFlags, int uTimeout, IntPtr lpdwResult);

    [DllImport("user32.dll")]
    private static extern int GetDlgCtrlID(IntPtr hwndCtl);

    public static void DoubleClickListView(IntPtr hwnd, uint processId, int item, int subitem)
    {
      IntPtr zero1 = IntPtr.Zero;
      IntPtr zero2 = IntPtr.Zero;
      NMHDR lpBuffer1;
      lpBuffer1.hwndFrom = (int) hwnd;
      lpBuffer1.idFrom = 7809;
      lpBuffer1.code = 515;
      IntPtr zero3 = IntPtr.Zero;
      IntPtr hProcess = ListViewItem1.OpenProcess(Win32ProcessAccessType.AllAccess, false, processId);
      IntPtr lpBaseAddress1 = ListViewItem1.VirtualAllocEx(hProcess, IntPtr.Zero, 2048U, Win32AllocationTypes.MEM_COMMIT, Win32MemoryProtection.PAGE_READWRITE);
      IntPtr zero4 = IntPtr.Zero;
      int lpNumberOfBytesWritten = 0;
      ListViewItem1.WriteProcessMemory(hProcess, lpBaseAddress1, ref lpBuffer1, (uint) Marshal.SizeOf(typeof (NMHDR)), out lpNumberOfBytesWritten);
      POINT lpBuffer2;
      lpBuffer2.x = 31;
      lpBuffer2.y = 31;
      IntPtr lpBaseAddress2 = ListViewItem1.VirtualAllocEx(hProcess, IntPtr.Zero, 2048U, Win32AllocationTypes.MEM_COMMIT, Win32MemoryProtection.PAGE_READWRITE);
      ListViewItem1.WriteProcessMemory(hProcess, lpBaseAddress2, ref lpBuffer2, (uint) Marshal.SizeOf(typeof (POINT)), out lpNumberOfBytesWritten);
      NMITEMACTIVATE lpBuffer3 = new NMITEMACTIVATE();
      lpBuffer3.hdr = lpBaseAddress1;
      lpBuffer3.iItem = item;
      lpBuffer3.iSubItem = subitem;
      lpBuffer3.uOldState = 2U;
      lpBuffer3.uNewState = 0U;
      lpBuffer3.ptAction = lpBaseAddress2;
      IntPtr zero5 = IntPtr.Zero;
      IntPtr num = ListViewItem1.VirtualAllocEx(hProcess, IntPtr.Zero, 2048U, Win32AllocationTypes.MEM_COMMIT, Win32MemoryProtection.PAGE_READWRITE);
      ListViewItem1.WriteProcessMemory(hProcess, num, ref lpBuffer3, (uint) Marshal.SizeOf(typeof (NMITEMACTIVATE)), out lpNumberOfBytesWritten);
      ListViewItem1.SendMessageTimeout(hwnd, 78, (IntPtr) 116, num, 2, 5000, IntPtr.Zero);
    }

    public static void SelectListViewItem(IntPtr hwnd, uint processId, int item)
    {
      IntPtr zero1 = IntPtr.Zero;
      IntPtr zero2 = IntPtr.Zero;
      IntPtr num1 = IntPtr.Zero;
      LV_ITEM lpBuffer = new LV_ITEM();
      num1 = Marshal.AllocHGlobal(2048);
      IntPtr num2 = ListViewItem1.OpenProcess(Win32ProcessAccessType.AllAccess, false, processId);
      IntPtr num3 = ListViewItem1.VirtualAllocEx(num2, IntPtr.Zero, 2048U, Win32AllocationTypes.MEM_COMMIT, Win32MemoryProtection.PAGE_READWRITE);
      lpBuffer.state = 3;
      lpBuffer.stateMask = 3;
      IntPtr zero3 = IntPtr.Zero;
      int lpNumberOfBytesWritten = 0;
      ListViewItem1.WriteProcessMemory(num2, num3, ref lpBuffer, (uint) Marshal.SizeOf(typeof (LV_ITEM)), out lpNumberOfBytesWritten);
      ListViewItem1.SendMessageTimeout(hwnd, 4139, (IntPtr) item, num3, 2, 5000, IntPtr.Zero);
      ListViewItem1.VirtualFreeEx(num2, num3, 0, Win32AllocationTypes.MEM_RELEASE);
      ListViewItem1.CloseHandle(num2);
    }

    public static unsafe string GetListViewItem(IntPtr hwnd, uint processId, int item, int subItem = 0)
    {
      int num1 = 0;
      IntPtr num2 = IntPtr.Zero;
      IntPtr num3 = IntPtr.Zero;
      IntPtr num4 = IntPtr.Zero;
      try
      {
        LV_ITEM lpBuffer = new LV_ITEM();
        num4 = Marshal.AllocHGlobal(2048);
        num2 = ListViewItem1.OpenProcess(Win32ProcessAccessType.AllAccess, false, processId);
        if (num2 == IntPtr.Zero)
          throw new ApplicationException("Failed to access process!");
        num3 = ListViewItem1.VirtualAllocEx(num2, IntPtr.Zero, 2048U, Win32AllocationTypes.MEM_COMMIT, Win32MemoryProtection.PAGE_READWRITE);
        if (num3 == IntPtr.Zero)
          throw new SystemException("Failed to allocate memory in remote process");
        lpBuffer.mask = 1;
        lpBuffer.iItem = item;
        lpBuffer.iSubItem = subItem;
        lpBuffer.pszText = (char*) (num3.ToInt32() + Marshal.SizeOf(typeof (LV_ITEM)));
        lpBuffer.cchTextMax = 500;
        if (!ListViewItem1.WriteProcessMemory(num2, num3, ref lpBuffer, (uint) Marshal.SizeOf(typeof (LV_ITEM)), out num1))
          throw new SystemException("Failed to write to process memory");
        ListViewItem1.SendMessageTimeout(hwnd, 4171, IntPtr.Zero, num3, 2, 5000, IntPtr.Zero);
        if (!ListViewItem1.ReadProcessMemory(num2, num3, num4, 2048, out num1))
          throw new SystemException("Failed to read from process memory");
        return Marshal.PtrToStringUni((IntPtr) (num4.ToInt32() + Marshal.SizeOf(typeof (LV_ITEM))));
      }
      finally
      {
        if (num4 != IntPtr.Zero)
          Marshal.FreeHGlobal(num4);
        if (num3 != IntPtr.Zero)
          ListViewItem1.VirtualFreeEx(num2, num3, 0, Win32AllocationTypes.MEM_RELEASE);
        if (num2 != IntPtr.Zero)
          ListViewItem1.CloseHandle(num2);
      }
    }
  }
}
