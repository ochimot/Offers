// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.frmSSHImport
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AutoLeadGUI
{
  public class frmSSHImport : Form
  {
    private IContainer components = (IContainer) null;
    public string filePath;
    public string clipboardText;
    private BackgroundWorker bwImport;
    private ProgressBar progressBar1;

    public frmSSHImport()
    {
      this.InitializeComponent();
    }

    private void bwImport_DoWork(object sender, DoWorkEventArgs e)
    {
      string[] strArray1 = Regex.Split(this.clipboardText, "\r\n|\r|\n");
      if (this.filePath != null)
      {
        Console.WriteLine(this.filePath);
        strArray1 = File.ReadAllLines(this.filePath);
      }
      Regex regex = new Regex("^\\s*([^(]*)");
      ArrayList arrayList = LocalConfig.getCurrentConfig().allSSHs() ?? new ArrayList();
      for (int index = 0; index < strArray1.Length; ++index)
      {
        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        string[] strArray2 = GlobalConfig.stringSplit(strArray1[index].Trim(), "|");
        if (strArray2.Length >= 5)
        {
          dictionary["host"] = (object) strArray2[0];
          dictionary["username"] = (object) strArray2[1];
          dictionary["password"] = (object) strArray2[2];
          string input = strArray2[3];
          string s = strArray2[4];
          Match match = regex.Match(input);
          string str = match.Captures.Count < 1 ? input.Trim().ToUpper() : match.Captures[0].Value.Trim().ToUpper();
          dictionary["country"] = (object) str;
          dictionary["region"] = (object) str;
          int result = 0;
          int.TryParse(s, out result);
          if (result == 0)
            result = 22;
          dictionary["port"] = (object) result;
          arrayList.Add((object) dictionary);
        }
      }
      LocalConfig.getCurrentConfig().saveSSHsList();
        this.progressBar1.Invoke(new Action(delegate
        {
            base.Close();
        }));
        }

    private void frmSSHImport_Load(object sender, EventArgs e)
    {
      this.clipboardText = Clipboard.GetText();
      this.bwImport.RunWorkerAsync();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.bwImport = new BackgroundWorker();
      this.progressBar1 = new ProgressBar();
      this.SuspendLayout();
      this.bwImport.DoWork += new DoWorkEventHandler(this.bwImport_DoWork);
      this.progressBar1.Location = new Point(12, 12);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new Size(560, 36);
      this.progressBar1.Style = ProgressBarStyle.Marquee;
      this.progressBar1.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(9f, 20f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(584, 65);
      this.ControlBox = false;
      this.Controls.Add((Control) this.progressBar1);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Name = "frmSSHImport";
      this.StartPosition = FormStartPosition.CenterParent;
      this.Load += new EventHandler(this.frmSSHImport_Load);
      this.ResumeLayout(false);
    }
  }
}
