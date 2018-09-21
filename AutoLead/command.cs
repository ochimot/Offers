// Decompiled with JetBrains decompiler
// Type: AutoLead.command
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;
using System.Text;

namespace AutoLead
{
  public class command
  {
    public command.SendControl sendControl;

    public void addProtectData(string path)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("addProtectData=" + path + "{|}"));
    }

    public void backup(string text)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("backup=" + text + "{|}"));
    }

    public void backup(string text, string filename, string comment, string timemod, string runtime)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("backup=" + text + "|" + filename + "|" + comment + timemod + "|" + runtime + "{|}"));
    }

    public void backupfull(string text)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("backupfull=" + text + "{|}"));
    }

    public void backupfull(string text, string filename, string comment, string timemod, string runtime)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("backupfull=" + text + "|" + filename + "|" + comment + "|" + timemod + "|" + runtime + "{|}"));
    }

    public void changecarrier(string text)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("changecarrier=" + text + "{|}"));
    }

    public void changecarrier(string carriername, string countrycode, string carriercode, string ioscountrycode)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("changecarrier=" + carriername + "||" + countrycode + "||" + carriercode + "||" + ioscountrycode + "{|}"));
    }

    public void changedevice(string text)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("changedevice=" + text + "{|}"));
    }

    public void changelanguage(string text)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("changelanguage=" + text + "{|}"));
    }

    public void changeLanIP(string ip)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("changelanip=" + ip + "{|}"));
    }

    public void changename(string text)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("changename=" + text + "{|}"));
    }

    public void changeregion(string text)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("changeregion=" + text + "{|}"));
    }

    public void changescreen(bool enable, double width, double heigh, double scale)
    {
      if (this.sendControl == null)
        return;
      Encoding unicode = Encoding.Unicode;
      string[] strArray1 = new string[9];
      strArray1[0] = "changescreen=";
      string[] strArray2 = strArray1;
      byte num = Convert.ToByte(enable);
      strArray2[1] = num.ToString();
      strArray2[2] = "|";
      strArray2[3] = width.ToString();
      strArray2[4] = "|";
      strArray2[5] = heigh.ToString();
      strArray2[6] = "|";
      strArray2[7] = scale.ToString();
      strArray2[8] = "{|}";
      this.sendControl(unicode.GetBytes(string.Concat(strArray2)));
    }

    public void changetimezone(string text)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("changetimezone=" + text + "{|}"));
    }

    public void changeversion(string text)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("changeversion=" + text + "{|}"));
    }

    public void checkbackup(string filename)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("checkbackup=" + filename + "{|}"));
    }

    public void checkbackup()
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("checkbackup=1{|}"));
    }

    public void checkip(string text)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("checkip=" + text + "{|}"));
    }

    public void checkrestore()
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("checkrestore=1{|}"));
    }

    public void checkwipe()
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("checkwipe=1{|}"));
    }

    public void clearipa()
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("cleanipa=1{|}"));
    }

    public void close(string AppID)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("close=" + AppID + "{|}"));
    }

    public void deletebackup(string text)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("deletebackup=" + text + "{|}"));
    }

    public void disablemouse()
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("enablemouse=NO{|}"));
    }

    public void disableProxy()
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("disableProxy={|}"));
    }

    public void enablemouse()
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("enablemouse=YES{|}"));
    }

    public void excuteScript(string text)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("script=" + text + "{|}"));
    }

    public void fakebrightness(double brightness)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("changebrightness=" + brightness.ToString() + "{|}"));
    }

    public void fakeGPS(bool enable)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("locationFaker=" + (enable ? "1" : "0") + "{|}"));
    }

    public void fakeGPS(bool enable, double latitude, double longitude)
    {
      if (this.sendControl == null)
        return;
      Encoding unicode = Encoding.Unicode;
      string[] strArray1 = new string[7];
      strArray1[0] = "locationFaker=";
      string[] strArray2 = strArray1;
      strArray2[1] = enable ? "1" : "0";
      strArray2[2] = "|";
      strArray2[3] = latitude.ToString();
      strArray2[4] = "|";
      strArray2[5] = longitude.ToString();
      strArray2[6] = "{|}";
      this.sendControl(unicode.GetBytes(string.Concat(strArray2)));
    }

    public void faketype(string text)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("faketype=" + text + "{|}"));
    }

    public void fakeversion(string text)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("fakeversion=" + text + "{|}"));
    }

    public void getactiveurl()
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("getActiveURL=1{|}"));
    }

    public void getAllProtectData()
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("getAllProtectData={|}"));
    }

    public void getAppList()
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("getapp=install{|}"));
    }

    public void getbackup()
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("getbackup=1{|}"));
    }

    public void getDeviceInfo()
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("getinfo=1{|}"));
    }

    public void getfront()
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("getapp=front{|}"));
    }

    public void getProtectData(string appID)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("getProtectData=" + appID + "{|}"));
    }

    public void getproxy()
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("getproxy=1{|}"));
    }

    public void getSignature(string param, string sign, string id)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("getSignature=" + param + "{:}" + sign + "{:}" + id + "{|}"));
    }

    public void getSubFolder(string path)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("getSubFolder=" + path + "{|}"));
    }

    public void getversion()
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("getversion=1{|}"));
    }

    public void installapp(string appId)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("install=" + appId + "{|}"));
    }

    public void mousedown(int x, int y)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("mousedown=" + x.ToString() + " " + y.ToString() + "{|}"));
    }

    public void mouseup(int x, int y)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("mouseup=" + x.ToString() + " " + y.ToString() + "{|}"));
    }

    public void movemouse(int x, int y)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("movemouse=" + x.ToString() + " " + y.ToString() + "{|}"));
    }

    public void openApp(string AppID)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("open=" + AppID + "{|}"));
    }

    public void openURL(string URL)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("openurl1=" + URL + "{|}"));
    }

    public void pauseScript(string text)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("pausescript=1{|}"));
    }

    public void randominfo()
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("randominfo=1{|}"));
    }

    public void randomtouchpause()
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("rdtouchPause={|}"));
    }

    public void randomtouchresume()
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("rdtouchResume={|}"));
    }

    public void randomtouchstop()
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("rdtouchStop={|}"));
    }

    public void removeProtectData(string path)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("removeProtectData=" + path + "{|}"));
    }

    public void resping()
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("resping={|}"));
    }

    public void restore(string text)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("restore=" + text + "{|}"));
    }

    public void savecomment(string filename, string comment)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("savecomment=" + filename + "=" + comment + "{|}"));
    }

    public void sendcommand(string command)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes(command + "{|}"));
    }

    public void sendKeyCode(string keyId)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("ALICheckCode=" + keyId + "{|}"));
    }

    public void sendKeyId(string keyId)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("ALICheckId=" + keyId + "{|}"));
    }

    public void sendtext(string text)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("send=" + text + "{|}"));
    }

    public void set3grate(int rate)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("setproperty=3grate||" + rate.ToString() + "{|}"));
    }

    public void setipv4(string ipv4, string router, string subnet)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("setIPv4=" + ipv4 + ":" + router + ":" + subnet + "{|}"));
    }

    public void setProxy(string socks, int port)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("setProxy=" + socks + ":" + port.ToString() + "{|}"));
    }

    public void setReferer(string URL)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("setreferer=" + URL + "{|}"));
    }

    public void setsocks(string text)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("setsocks=" + text + "{|}"));
    }

    public void settime(string text)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("settime=" + text + "{|}"));
    }

    public void swipe(double x1, double y1, double x2, double y2, double time)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("swipe=" + x1.ToString() + " " + y1.ToString() + " " + x2.ToString() + " " + y2.ToString() + " " + time.ToString() + "{|}"));
    }

    public void touch(double x, double y)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("touch=" + x.ToString() + " " + y.ToString() + "{|}"));
    }

    public void touchRandom(double x, double y, double x1, double y1, double time, double speed)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("randomtouch=" + x.ToString() + " " + y.ToString() + " " + x1.ToString() + " " + y1.ToString() + " " + time.ToString() + " " + speed.ToString() + "{|}"));
    }

    public void uninstallapp(string appId)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("uninstall=" + appId + "{|}"));
    }

    public void wipe(string text)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("wipe=" + text + "{|}"));
    }

    public void wipefull(string text)
    {
      if (this.sendControl == null)
        return;
      this.sendControl(Encoding.Unicode.GetBytes("wipefull=" + text + "{|}"));
    }

    public delegate void SendControl(byte[] buffer);
  }
}
