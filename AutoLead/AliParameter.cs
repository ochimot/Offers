// Decompiled with JetBrains decompiler
// Type: AutoLead.AliParameter
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoLead
{
  public class AliParameter
  {
    private string url = "";
    private string signurl = "";
    public List<param> Listparam = new List<param>();

    public AliParameter()
    {
      List<param> objList = new List<param>();
    }

    public void addParameter(string param, string value)
    {
      this.Listparam.Add(new param()
      {
        key = param,
        value = value
      });
      this.Listparam = this.Listparam.OrderBy<param, string>((Func<param, string>) (x => x.key)).ToList<param>();
    }

    public string getencodeparameter()
    {
      string str = "";
      foreach (param obj in this.Listparam)
      {
        if (str != "")
          str += "&";
        str = str + obj.key + "=" + Uri.EscapeDataString(obj.value);
      }
      return str;
    }

    public string getparameter()
    {
      string str = "";
      foreach (param obj in this.Listparam)
      {
        if (str != "")
          str += "&";
        str = str + obj.key + "=" + obj.value;
      }
      return str;
    }

    public string getsignparameter()
    {
      string str = this.signurl;
      foreach (param obj in this.Listparam)
        str = str + obj.key + obj.value;
      return str;
    }

    public void setSignature(string signature)
    {
      this.addParameter("_aop_signature", signature);
    }

    public void setURL(string URL)
    {
      this.url = URL;
      this.signurl = URL.Split(new string[1]{ "openapi/" }, StringSplitOptions.None)[1];
    }
  }
}
