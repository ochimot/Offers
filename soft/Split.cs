// Decompiled with JetBrains decompiler
// Type: soft.Split
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;

namespace soft
{
  public class Split
  {
    public static string[] tachchuoi(string chuoi, string s)
    {
      try
      {
        return chuoi.Split(new string[1]{ s }, StringSplitOptions.None);
      }
      catch
      {
        return (string[]) null;
      }
    }

    public static string[] tachchuoi(string chuoi, char s)
    {
      try
      {
        return chuoi.Split(s);
      }
      catch
      {
        return (string[]) null;
      }
    }

    public static string tachso(string input)
    {
      string str1 = "";
      string str2 = input;
      int length = input.Length;
      for (int index = 0; index < length; ++index)
      {
        if (str2[index] != '.' && (str2[index] >= '0' && str2[index] <= '9'))
          str1 += str2[index].ToString();
      }
      return str1;
    }

    public static string tachchu(string input)
    {
      string str1 = "";
      string str2 = input;
      int length = input.Length;
      for (int index = 0; index < length; ++index)
      {
        if (str2[index] != '[' && str2[index] != ']')
          str1 += str2[index].ToString();
      }
      return str1;
    }

    public static string Getdata(string input, char ktu)
    {
      string str1 = "";
      string str2 = input;
      int length = input.Length;
      for (int index = 0; index < length; ++index)
      {
        if ((int) str2[index] != (int) ktu)
          str1 += str2[index].ToString();
      }
      return str1;
    }
  }
}
