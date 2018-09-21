// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.frmUE
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoLeadGUI
{
  public class frmUE : Form
  {
    public int type = 0;
    public string result = (string) null;
    public Delegate scriptDelegate = (Delegate) null;
    private bool isRunning = false;
    private IContainer components = (IContainer) null;
    private ProgressBar progressBar1;
    public Label lbText;
    private BackgroundWorker bwGetUE;
    private Button btnStop;

    public frmUE()
    {
      this.InitializeComponent();
    }

    private void frmUE_Shown(object sender, EventArgs e)
    {
      this.bwGetUE.RunWorkerAsync();
    }

    private void bwGetUE_DoWork(object sender, DoWorkEventArgs e)
    {
      if (!AutoLeadClient.connected)
        return;
      if (this.scriptDelegate != null)
      {
        this.isRunning = true;
        while (this.isRunning)
        {
          if (AutoLeadClient.send("{\"cmd\":\"getCurrentUE\", \"type\":\"" + (object) this.type + "\"}"))
          {
            this.result = AutoLeadClient.receive();
            this.scriptDelegate.DynamicInvoke((object) this.result);
          }
        }
      }
      else if (AutoLeadClient.send("{\"cmd\":\"getCurrentUE\", \"type\":\"" + (object) this.type + "\"}"))
      {
        this.result = AutoLeadClient.receive();
        this.lbText.Invoke(new Action(delegate
        {
            base.Close();
        }));
      }
    }

    private void btnStop_Click(object sender, EventArgs e)
    {
      this.isRunning = false;
      AutoLeadClientHelper.stopRecordingEU();
      this.result = (string) null;
      this.Close();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.lbText = new Label();
      this.progressBar1 = new ProgressBar();
      this.bwGetUE = new BackgroundWorker();
      this.btnStop = new Button();
      this.SuspendLayout();
      this.lbText.Location = new Point(12, 28);
      this.lbText.Name = "lbText";
      this.lbText.Size = new Size(607, 30);
      this.lbText.TabIndex = 0;
      this.lbText.Text = "Recording your device event ...";
      this.lbText.TextAlign = ContentAlignment.MiddleCenter;
      this.progressBar1.Location = new Point(16, 99);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new Size(497, 37);
      this.progressBar1.Style = ProgressBarStyle.Marquee;
      this.progressBar1.TabIndex = 1;
      this.bwGetUE.DoWork += new DoWorkEventHandler(this.bwGetUE_DoWork);
      this.btnStop.Location = new Point(525, 99);
      this.btnStop.Name = "btnStop";
      this.btnStop.Size = new Size(94, 37);
      this.btnStop.TabIndex = 2;
      this.btnStop.Text = "Stop";
      this.btnStop.UseVisualStyleBackColor = true;
      this.btnStop.Click += new EventHandler(this.btnStop_Click);
      this.AutoScaleDimensions = new SizeF(9f, 20f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(631, 148);
      this.ControlBox = false;
      this.Controls.Add((Control) this.btnStop);
      this.Controls.Add((Control) this.progressBar1);
      this.Controls.Add((Control) this.lbText);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Name = "frmUE";
      this.StartPosition = FormStartPosition.CenterParent;
      this.Shown += new EventHandler(this.frmUE_Shown);
      this.ResumeLayout(false);
    }
  }
}
