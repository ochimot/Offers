// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.Capchar
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using DeathByCaptcha;
using System.Collections;

namespace AutoLeadGUI
{
  public class Capchar
  {
    public static string Getcapchar()
    {
      Client client = (Client) new SocketClient("ntdong", "ntdong");
      client.Balance.ToString();
      Captcha captcha = client.Decode("capchar.Bmp", 50, (Hashtable) null);
      if (captcha.Solved && captcha.Correct)
        return captcha.Text;
      return (string) null;
    }
  }
}
