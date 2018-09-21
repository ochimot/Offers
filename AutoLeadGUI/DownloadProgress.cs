// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.DownloadProgress
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoLeadGUI
{
  public class DownloadProgress : Form
  {
    private IContainer components = (IContainer) null;
    public System.Windows.Forms.ProgressBar progressBar1;
    private void InitializeComponent()
    {
        this.progressBar1 = new ProgressBar();
        this.SuspendLayout();
        this.progressBar1.Dock = DockStyle.Fill;
        this.progressBar1.Location = new Point(0, 0);
        this.progressBar1.Name = "progressBar1";
        this.progressBar1.Size = new Size(125, 33);
        this.progressBar1.TabIndex = 0;
        this.AutoScaleDimensions = new SizeF(6f, 13f);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(125, 33);
        this.Controls.Add((Control)this.progressBar1);
        this.FormBorderStyle = FormBorderStyle.None;
        this.MinimizeBox = false;
        this.Name = "DownloadProgress";
        this.ShowIcon = false;
        this.ShowInTaskbar = false;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "DownloadProgress";
        this.Load += new EventHandler(this.DownloadProgress_Load);
        this.ResumeLayout(false);
    }
    public DownloadProgress()
    {
      InitializeComponent();
    }

    private void DownloadProgress_Load(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    
  }
}
