// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.frmTestScript
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoLeadGUI
{
  public class frmTestScript : Form
  {
    public string scripts = (string) null;
    private string[] lines = (string[]) null;
    private IContainer components = (IContainer) null;
    private ListView lvScript;
    private ColumnHeader columnHeader1;
    private ColumnHeader columnHeader2;
    private Button btnDone;
    private BackgroundWorker bwScriptTest;

    public frmTestScript()
    {
      this.InitializeComponent();
    }

    private void frmTestScript_Load(object sender, EventArgs e)
    {
      this.lvScript.Items.Clear();
      if (this.scripts == null)
        return;
      this.btnDone.Enabled = false;
      this.lines = this.scripts.Split(new string[2]
      {
        "\r\n",
        "\n"
      }, StringSplitOptions.None);
      for (int index = 0; index < this.lines.Length; ++index)
        this.lvScript.Items.Add(new ListViewItem()
        {
          Text = this.lines[index],
          SubItems = {
            ""
          }
        });
      this.bwScriptTest.RunWorkerAsync();
    }

    private void bwScriptTest_DoWork(object sender, DoWorkEventArgs e)
    {
      GlobalConfig.parseStart();
      if ((uint) this.lines.Length > 0U)
      {
        for (int i = 0; i < this.lines.Length; i++)
        {
          string script1Line = this.lines[i].Trim();
          if (!script1Line.StartsWith(";") && script1Line.Length > 0)
          {
            this.lvScript.Invoke(new Action(delegate
            {
              this.lvScript.SelectedIndices.Clear();
              this.lvScript.SelectedIndices.Add(i);
              this.lvScript.Items[i].SubItems[1].Text = "Running ...";
            }));
            bool result = false;
            if (script1Line == "capcharAli")
            {
              frmMain.FindAli();
              script1Line = "text(\"" + Capchar.Getcapchar() + "\")";
            }
            if (script1Line == "downChMavrochain")
            {
              frmMain.FindMavrochain();
              script1Line = "";
            }
            if (script1Line == "capcharMavrochain")
              script1Line = "rd(\"" + Capchar.Getcapchar() + "\")";
            if (ScriptsCore.executeScript(script1Line, out result))
            {
              this.lvScript.Invoke(new Action(delegate
              {
                this.lvScript.SelectedIndices.Clear();
                this.lvScript.SelectedIndices.Add(i);
                ListViewItem listViewItem = this.lvScript.Items[i];
                listViewItem.SubItems[1].Text = "Success [result=" + result.ToString() + "]";
                listViewItem.EnsureVisible();
              }));
            }
            else
            {
              if (result)
              {
                this.lvScript.Invoke(new Action(delegate
                {
                  this.lvScript.SelectedIndices.Clear();
                  this.lvScript.SelectedIndices.Add(i);
                  ListViewItem listViewItem = this.lvScript.Items[i];
                  listViewItem.SubItems[1].Text = "Success [End]";
                  listViewItem.EnsureVisible();
                }));
                break;
              }
              this.lvScript.Invoke(new Action(delegate
              {
                this.lvScript.SelectedIndices.Clear();
                this.lvScript.SelectedIndices.Add(i);
                ListViewItem listViewItem = this.lvScript.Items[i];
                listViewItem.SubItems[1].Text = "#Failed";
                listViewItem.EnsureVisible();
              }));
              break;
            }
          }
        }
      }
        this.lvScript.Invoke(new Action(delegate
        {
            this.btnDone.Enabled = true;
        }));
        }

    private void btnDone_Click(object sender, EventArgs e)
    {
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
      this.lvScript = new ListView();
      this.columnHeader1 = new ColumnHeader();
      this.columnHeader2 = new ColumnHeader();
      this.btnDone = new Button();
      this.bwScriptTest = new BackgroundWorker();
      this.SuspendLayout();
      this.lvScript.Columns.AddRange(new ColumnHeader[2]
      {
        this.columnHeader1,
        this.columnHeader2
      });
      this.lvScript.Dock = DockStyle.Top;
      this.lvScript.FullRowSelect = true;
      this.lvScript.GridLines = true;
      this.lvScript.Location = new Point(0, 0);
      this.lvScript.Margin = new Padding(2, 2, 2, 2);
      this.lvScript.Name = "lvScript";
      this.lvScript.Size = new Size(527, 265);
      this.lvScript.TabIndex = 0;
      this.lvScript.UseCompatibleStateImageBehavior = false;
      this.lvScript.View = View.Details;
      this.columnHeader1.Text = "Script";
      this.columnHeader1.Width = 529;
      this.columnHeader2.Text = "Status";
      this.columnHeader2.Width = 209;
      this.btnDone.Location = new Point(439, 275);
      this.btnDone.Margin = new Padding(2, 2, 2, 2);
      this.btnDone.Name = "btnDone";
      this.btnDone.Size = new Size(78, 26);
      this.btnDone.TabIndex = 1;
      this.btnDone.Text = "Done";
      this.btnDone.UseVisualStyleBackColor = true;
      this.btnDone.Click += new EventHandler(this.btnDone_Click);
      this.bwScriptTest.DoWork += new DoWorkEventHandler(this.bwScriptTest_DoWork);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(527, 309);
      this.ControlBox = false;
      this.Controls.Add((Control) this.btnDone);
      this.Controls.Add((Control) this.lvScript);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Margin = new Padding(2, 2, 2, 2);
      this.Name = "frmTestScript";
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Test script";
      this.Load += new EventHandler(this.frmTestScript_Load);
      this.ResumeLayout(false);
    }
  }
}
