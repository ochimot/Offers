// Decompiled with JetBrains decompiler
// Type: ControliPhone.ComboboxItem
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

namespace ControliPhone
{
  public class ComboboxItem
  {
    public string Text { get; set; }

    public object Value { get; set; }

    public override string ToString()
    {
      return this.Text;
    }
  }
}
