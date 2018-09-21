// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.RandomMail
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using soft;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AutoLeadGUI
{
  public class RandomMail
  {
    public static string Getmail()
    {
      if (frmMain.bool_chkmailfile)
      {
        string chuoi = File.ReadAllText(frmMain.patchfileMail);
        string[] strArray = Split.tachchuoi(chuoi, "\r\n");
        int index = new Random().Next(0, ((IEnumerable<string>) strArray).Count<string>());
        string str = strArray[index];
        string contents = chuoi.Replace(str + "\r\n", "");
        File.WriteAllText(frmMain.patchfileMail, contents);
        return str;
      }
      string[] strArray1 = Split.tachchuoi(File.ReadAllText(Application.StartupPath.ToString() + "\\DataRandom\\Mail.txt"), "\r\n");
      Random random = new Random();
      int index1 = random.Next(0, ((IEnumerable<string>) strArray1).Count<string>());
      string str1 = strArray1[index1];
      string str2;
      string str3;
      string str4;
      do
      {
        int index2 = random.Next(0, ((IEnumerable<string>) strArray1).Count<string>());
        str2 = strArray1[index2];
        char ch = str1[0];
        str3 = ch.ToString();
        ch = str2[0];
        str4 = ch.ToString();
      }
      while (str3 == str4);
      int num = random.Next(0, 9999);
      string[] strArray2;
      if (frmMain.bool_onlymail)
        strArray2 = new string[5]
        {
          "@gmail.com",
          "@hotmail.com",
          "@comcast.com",
          "@verizon.net",
          "@yahoo.com"
        };
      else
        strArray2 = new string[32]
        {
          "@gmail.com",
          "@hotmail.com",
          "@comcast.com",
          "@verizon.net",
          "@meekness.com",
          "@dps.centrin.net.id",
          "@telkomsel.co.id",
          "@astonrasuna.com",
          "@cbn.net.id",
          "@tuguhotels.com",
          "@yahoo.com",
          "@thebale.com",
          "@denpasar.wasantara.net.id",
          "@ramayanahotel.com",
          "@balimandira.com",
          "@ifc.org",
          "@interconti.com",
          "@novotelbali.com",
          "@hotelpadma.com",
          "@balibless.com",
          "@jayakartahotelsresorts.com",
          "@indosat.net.id",
          "@idn.xerox.com",
          "@mailcity.com",
          "@houseofbali.com",
          "@toureast.net",
          "@nusaduahotel.com",
          "@mataram.wasantara.net.id",
          "@indo.net.id",
          "@kbatur.com",
          "@bonansatours.com",
          "@stpbali.ac.id"
        };
      int index3 = random.Next(0, ((IEnumerable<string>) strArray2).Count<string>());
      string str5 = strArray2[index3];
      return str1 + str2 + (object) num + str5;
    }

    public static string Getpass()
    {
      string str1 = (string) null;
      string str2 = "qQwWeErRtTyYuUiIoOpPaAsSdDfFgGhHjJkKlLzZxXcCvVbBnNmM";
      string str3 = "1234567890";
      Random random = new Random();
      int num = random.Next(14, 16);
      for (int index1 = 0; index1 < num / 2; ++index1)
      {
        int index2 = random.Next(0, str2.Length);
        int index3 = random.Next(0, str3.Length);
        string str4 = str1;
        char ch = str2[index2];
        string str5 = ch.ToString();
        ch = str3[index3];
        string str6 = ch.ToString();
        str1 = str4 + str5 + str6;
      }
      return str1;
    }

    public static string GetName()
    {
      string[] strArray = Split.tachchuoi(File.ReadAllText(Application.StartupPath.ToString() + "\\DataRandom\\Mail.txt"), "\r\n");
      int index = new Random().Next(0, ((IEnumerable<string>) strArray).Count<string>());
      return strArray[index];
    }

    internal static string GetText()
    {
      string[] strArray = Split.tachchuoi(File.ReadAllText(Application.StartupPath.ToString() + "\\DataRandom\\Textinput.txt").Replace("\r\n", "\n"), "\n");
      int index = new Random().Next(0, ((IEnumerable<string>) strArray).Count<string>());
      return strArray[index];
    }

    internal static string Amail()
    {
      string[] strArray = Split.tachchuoi(File.ReadAllText(Application.StartupPath.ToString() + "\\DataRandom\\@mail.txt").Replace("\r\n", "\n"), "\n");
      int index = new Random().Next(0, ((IEnumerable<string>) strArray).Count<string>());
      return strArray[index];
    }

    internal static string Fromfile(string file)
    {
      string[] strArray = Split.tachchuoi(File.ReadAllText(Application.StartupPath.ToString() + "\\DataRandom\\" + file + ".txt").Replace("\r\n", "\n"), "\n");
      int index = new Random().Next(0, ((IEnumerable<string>) strArray).Count<string>());
      return strArray[index];
    }

    internal static string DelFromfile(string file)
    {
      string path = Application.StartupPath.ToString() + "\\DataRandom\\" + file + ".txt";
      string str1 = File.ReadAllText(path).Replace("\r\n", "\n");
      string[] strArray = Split.tachchuoi(str1, "\n");
      int index = new Random().Next(0, ((IEnumerable<string>) strArray).Count<string>());
      string str2 = strArray[index];
      str1.Replace(str2 + "\n", "");
      File.WriteAllText(path, str1);
      return str2;
    }
  }
}
