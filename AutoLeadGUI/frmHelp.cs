// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.frmHelp
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoLeadGUI
{
  public class frmHelp : Form
  {
    private IContainer components = (IContainer) null;
    private RichTextBox richTextBox1;

    public frmHelp()
    {
      this.InitializeComponent();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmHelp));
      this.richTextBox1 = new RichTextBox();
      this.SuspendLayout();
      this.richTextBox1.Location = new Point(12, 12);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.ReadOnly = true;
      this.richTextBox1.Size = new Size(822, 483);
      this.richTextBox1.TabIndex = 0;
      //this.richTextBox1.Text = componentResourceManager.GetString("richTextBox1.Text");
      this.AutoScaleDimensions = new SizeF(9f, 20f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(846, 507);
      this.Controls.Add((Control) this.richTextBox1);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Name = "frmHelp";
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Script help";
      this.ResumeLayout(false);
    }
  }
}
