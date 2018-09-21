// Decompiled with JetBrains decompiler
// Type: AutoLead.generateDeviceID
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AutoLead
{
  internal class generateDeviceID
  {
    public static string deviceID()
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

    public static uint hashCode(string text)
    {
      int num1 = 31;
      int num2 = 0;
      for (int index = 0; index < text.Length; ++index)
        num2 += (int) text[text.Length - index - 1] * (int) (ushort) (int) Math.Pow((double) num1, (double) index);
      return (uint) num2;
    }

    public static string hmacBase64Value(byte[] data, string key)
    {
      return Convert.ToBase64String(new HMACSHA1(Encoding.ASCII.GetBytes(key)).ComputeHash((Stream) new MemoryStream(data)));
    }

    public static byte[] randombyte()
    {
      return BitConverter.GetBytes(new Random().Next(0, 256));
    }
  }
}
