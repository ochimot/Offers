// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.ClickOnPointTool
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AutoLeadGUI
{
  internal class ClickOnPointTool
  {
    [DllImport("user32.dll")]
    private static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);

    [DllImport("user32.dll")]
    internal static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] ClickOnPointTool.INPUT[] pInputs, int cbSize);

    public static void ClickOnPoint(IntPtr wndHandle, Point clientPoint)
    {
      Point position = Cursor.Position;
      ClickOnPointTool.ClientToScreen(wndHandle, ref clientPoint);
      Cursor.Position = new Point(clientPoint.X, clientPoint.Y);
      ClickOnPointTool.INPUT[] pInputs = new ClickOnPointTool.INPUT[2]
      {
        new ClickOnPointTool.INPUT()
        {
          Type = 0U,
          Data = {
            Mouse = {
              Flags = 2U
            }
          }
        },
        new ClickOnPointTool.INPUT()
        {
          Type = 0U,
          Data = {
            Mouse = {
              Flags = 4U
            }
          }
        }
      };
      int num = (int) ClickOnPointTool.SendInput((uint) pInputs.Length, pInputs, Marshal.SizeOf(typeof (ClickOnPointTool.INPUT)));
      Cursor.Position = position;
    }

    public static void RightClickOnPoint(IntPtr wndHandle, Point clientPoint)
    {
      Point position = Cursor.Position;
      ClickOnPointTool.ClientToScreen(wndHandle, ref clientPoint);
      Cursor.Position = new Point(clientPoint.X, clientPoint.Y);
      ClickOnPointTool.INPUT[] pInputs = new ClickOnPointTool.INPUT[2]
      {
        new ClickOnPointTool.INPUT()
        {
          Type = 0U,
          Data = {
            Mouse = {
              Flags = 8U
            }
          }
        },
        new ClickOnPointTool.INPUT()
        {
          Type = 0U,
          Data = {
            Mouse = {
              Flags = 16U
            }
          }
        }
      };
      int num = (int) ClickOnPointTool.SendInput((uint) pInputs.Length, pInputs, Marshal.SizeOf(typeof (ClickOnPointTool.INPUT)));
      Cursor.Position = position;
    }

    public static void SendDeleteKey(IntPtr wndHandle)
    {
      SendKeys.SendWait("{DELETE}");
    }

    internal struct INPUT
    {
      public uint Type;
      public ClickOnPointTool.MOUSEKEYBDHARDWAREINPUT Data;
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct MOUSEKEYBDHARDWAREINPUT
    {
      [FieldOffset(0)]
      public ClickOnPointTool.MOUSEINPUT Mouse;
    }

    internal struct MOUSEINPUT
    {
      public int X;
      public int Y;
      public uint MouseData;
      public uint Flags;
      public uint Time;
      public IntPtr ExtraInfo;
    }
  }
}
