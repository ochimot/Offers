// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.frmAddOffer
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using AutoLeadGUI.Properties;
using soft;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace AutoLeadGUI
{
  public class frmAddOffer : Form
  {
    public int index = -1;
    private Dictionary<string, object> editOffer = (Dictionary<string, object>) null;
    private IContainer components = (IContainer) null;
    private TextBox tbName;
    private Label label1;
    private Label label2;
    private TextBox tbURL;
    private CheckBox cbEnabled;
    private Label label3;
    private TextBox tbReferer;
    private TextBox tbURLScript;
    private CheckBox cbUseURLScript;
    private Label label4;
    private NumericUpDown numAppStoreTime;
    private NumericUpDown numAppStoreTimeMin;
    private NumericUpDown numAppStoreTimeMax;
    private Label label6;
    private NumericUpDown numAppTime;
    private Label label7;
    private Label label8;
    private ComboBox cbApps;
    private CheckBox cbUseScript;
    private BackgroundWorker appsWorker;
    private Button btRefresh;
    private Button btnSave;
    private CheckBox cbRandom;
    private Button btnHelp;
    private Label label5;
    private Button btnAddToWipeList;
    private TextBox tbWipeList;
    private CheckBox checkBox1;
    private TextBox textBox1;

    public frmAddOffer()
    {
      this.InitializeComponent();
    }

    private void frmAddOffer_Load(object sender, EventArgs e)
    {
      if (this.index >= 0)
      {
        Dictionary<string, object> dictionary = (Dictionary<string, object>) LocalConfig.getCurrentConfig().allOffers().ToArray().GetValue(this.index);
        this.Text = "Edit offer";
        this.tbName.Text = (string) dictionary["name"];
        this.tbURL.Text = (string) dictionary["url"];
        this.tbReferer.Text = (string) dictionary["referer"];
        this.cbUseURLScript.Checked = Convert.ToBoolean(dictionary["useURLScript"]);
        this.cbUseScript.Checked = Convert.ToBoolean(dictionary["useScript"]);
        this.numAppStoreTime.Value = (Decimal) Convert.ToInt32(dictionary["asTime"]);
        this.cbRandom.Checked = Convert.ToBoolean(dictionary["random"]);
        this.numAppStoreTimeMin.Value = (Decimal) Convert.ToInt32(dictionary["asMin"]);
        this.numAppStoreTimeMax.Value = (Decimal) Convert.ToInt32(dictionary["asMax"]);
        this.numAppTime.Value = (Decimal) Convert.ToInt32(dictionary["appTime"]);
        this.tbWipeList.Text = dictionary.ContainsKey("wipeList") ? dictionary["wipeList"].ToString() : "";
        this.cbApps.Items.Clear();
        this.cbApps.Items.Add(dictionary["app"]);
        this.cbApps.SelectedIndex = 0;
        this.cbEnabled.Checked = Convert.ToBoolean(dictionary["enable"]);
        if (dictionary.ContainsKey("urlScript"))
          this.tbURLScript.Text = dictionary["urlScript"].ToString();
        this.editOffer = dictionary;
      }
      this.appsWorker.RunWorkerAsync();
    }

    private void updateAppsList(string list)
    {
      try
      {
        Array array = (Array) ((ArrayList) new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(list)["apps"]).ToArray();
        this.cbApps.Items.Clear();
        int num = -1;
        for (int index = 0; index < array.Length; ++index)
        {
          Dictionary<string, object> dictionary = (Dictionary<string, object>) array.GetValue(index);
          string str = dictionary["name"].ToString() + " | " + dictionary["id"];
          this.cbApps.Items.Add((object) str);
          if (this.editOffer != null && str.Equals(this.editOffer["app"]))
            num = index;
        }
        if (array.Length <= 0)
          return;
        this.cbApps.SelectedIndex = 0;
        if (num >= 0)
          this.cbApps.SelectedIndex = num;
        else if (this.index >= 0 && num == -1 && this.editOffer != null)
        {
          this.cbApps.Items.Add(this.editOffer["app"]);
          this.cbApps.SelectedIndex = this.cbApps.Items.Count - 1;
        }
      }
      catch
      {
      }
    }

        private void appsWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            bool FTPAddress = AutoLeadClient.connected;
            if (FTPAddress)
            {
                bool savefile = AutoLeadClient.send("{\"cmd\":\"apps\"}");
                if (savefile)
                {
                    string result = AutoLeadClient.receive();
                    bool username = result != null;
                    if (username)
                    {
                        this.cbApps.Invoke(new Action(delegate
                        {
                            this.updateAppsList(result);
                        }));
                    }
                }
            }
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            bool FTPAddress = AutoLeadClient.connected;
            if (FTPAddress)
            {
                bool savefile = AutoLeadClient.send("{\"cmd\":\"apps\"}");
                if (savefile)
                {
                    string result = AutoLeadClient.receive();
                    bool username = result != null;
                    if (username)
                    {
                        this.cbApps.Invoke(new Action(delegate
                        {
                            this.updateAppsList(result);
                        }));
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
    {
      if (this.index >= 0)
      {
        Dictionary<string, object> editOffer = this.editOffer;
        editOffer["name"] = (object) this.tbName.Text.Replace(" ", "");
        editOffer["url"] = (object) this.tbURL.Text;
        editOffer["referer"] = (object) this.tbReferer.Text;
        editOffer["useURLScript"] = (object) this.cbUseURLScript.Checked;
        editOffer["useScript"] = (object) this.cbUseScript.Checked;
        editOffer["asTime"] = (object) this.numAppStoreTime.Value;
        editOffer["random"] = (object) this.cbRandom.Checked;
        editOffer["asMin"] = (object) this.numAppStoreTimeMin.Value;
        editOffer["asMax"] = (object) this.numAppStoreTimeMax.Value;
        editOffer["appTime"] = (object) this.numAppTime.Value;
        if (this.checkBox1.Checked)
        {
          string str = this.textBox1.Text + " | " + Split.tachchuoi(this.cbApps.SelectedItem.ToString(), " | ")[1];
          editOffer["app"] = (object) str;
        }
        else
          editOffer["app"] = this.cbApps.SelectedItem;
        editOffer["enable"] = (object) this.cbEnabled.Checked;
        editOffer["urlScript"] = (object) this.tbURLScript.Text;
        editOffer["wipeList"] = (object) this.tbWipeList.Text;
        LocalConfig.getCurrentConfig().saveOffersList();
        this.Close();
      }
      else
      {
        ArrayList arrayList = LocalConfig.getCurrentConfig().allOffers();
        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        dictionary["name"] = (object) this.tbName.Text.Replace(" ", "");
        dictionary["url"] = (object) this.tbURL.Text;
        dictionary["referer"] = (object) this.tbReferer.Text;
        dictionary["useURLScript"] = (object) this.cbUseURLScript.Checked;
        dictionary["useScript"] = (object) this.cbUseScript.Checked;
        dictionary["asTime"] = (object) this.numAppStoreTime.Value;
        dictionary["random"] = (object) this.cbRandom.Checked;
        dictionary["asMin"] = (object) this.numAppStoreTimeMin.Value;
        dictionary["asMax"] = (object) this.numAppStoreTimeMax.Value;
        dictionary["appTime"] = (object) this.numAppTime.Value;
        if (this.checkBox1.Checked)
        {
          string str = this.textBox1.Text + " | " + Split.tachchuoi(this.cbApps.SelectedItem.ToString(), " | ")[1];
          dictionary["app"] = (object) str;
        }
        else
          dictionary["app"] = this.cbApps.SelectedItem;
        dictionary["enable"] = (object) this.cbEnabled.Checked;
        dictionary["urlScript"] = (object) this.tbURLScript.Text;
        dictionary["wipeList"] = (object) this.tbWipeList.Text;
        arrayList.Add((object) dictionary);
        LocalConfig.getCurrentConfig().saveOffersList();
        this.Close();
      }
    }

    private void btnHelp_Click(object sender, EventArgs e)
    {
      int num = (int) new frmHelp().ShowDialog();
    }

    private void btnAddToWipeList_Click(object sender, EventArgs e)
    {
      if (this.cbApps.SelectedItem == null)
        return;
      string str = !this.checkBox1.Checked ? this.cbApps.SelectedItem.ToString() : this.textBox1.Text + " | " + Split.tachchuoi(this.cbApps.SelectedItem.ToString(), " | ")[1];
      if (this.tbWipeList.Text.Length == 0)
      {
        this.tbWipeList.Text += str;
      }
      else
      {
        TextBox tbWipeList = this.tbWipeList;
        tbWipeList.Text = tbWipeList.Text + "\r\n" + str;
      }
    }

    private void tbWipeList_TextChanged(object sender, EventArgs e)
    {
    }

    private void tbURL_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyData != (Keys.A | Keys.Control))
        return;
      ((TextBoxBase) sender).SelectAll();
      e.Handled = e.SuppressKeyPress = true;
    }

    private void tbReferer_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyData != (Keys.A | Keys.Control))
        return;
      ((TextBoxBase) sender).SelectAll();
      e.Handled = e.SuppressKeyPress = true;
    }

    private void tbURLScript_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyData != (Keys.A | Keys.Control))
        return;
      ((TextBoxBase) sender).SelectAll();
      e.Handled = e.SuppressKeyPress = true;
    }

    private void tbWipeList_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyData != (Keys.A | Keys.Control))
        return;
      ((TextBoxBase) sender).SelectAll();
      e.Handled = e.SuppressKeyPress = true;
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
      if (this.checkBox1.Checked)
        this.textBox1.Enabled = true;
      else
        this.textBox1.Enabled = false;
    }

    private void numAppStoreTimeMax_ValueChanged(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.tbName = new TextBox();
      this.label1 = new Label();
      this.label2 = new Label();
      this.tbURL = new TextBox();
      this.cbEnabled = new CheckBox();
      this.label3 = new Label();
      this.tbReferer = new TextBox();
      this.tbURLScript = new TextBox();
      this.cbUseURLScript = new CheckBox();
      this.label4 = new Label();
      this.numAppStoreTime = new NumericUpDown();
      this.numAppStoreTimeMin = new NumericUpDown();
      this.numAppStoreTimeMax = new NumericUpDown();
      this.label6 = new Label();
      this.numAppTime = new NumericUpDown();
      this.label7 = new Label();
      this.label8 = new Label();
      this.cbApps = new ComboBox();
      this.cbUseScript = new CheckBox();
      this.appsWorker = new BackgroundWorker();
      this.btRefresh = new Button();
      this.btnSave = new Button();
      this.cbRandom = new CheckBox();
      this.btnHelp = new Button();
      this.label5 = new Label();
      this.btnAddToWipeList = new Button();
      this.tbWipeList = new TextBox();
      this.checkBox1 = new CheckBox();
      this.textBox1 = new TextBox();
      this.numAppStoreTime.BeginInit();
      this.numAppStoreTimeMin.BeginInit();
      this.numAppStoreTimeMax.BeginInit();
      this.numAppTime.BeginInit();
      this.SuspendLayout();
      this.tbName.Location = new Point(61, 11);
      this.tbName.Margin = new Padding(1);
      this.tbName.Name = "tbName";
      this.tbName.Size = new Size(461, 20);
      this.tbName.TabIndex = 0;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(7, 11);
      this.label1.Margin = new Padding(1, 0, 1, 0);
      this.label1.Name = "label1";
      this.label1.Size = new Size(38, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Name:";
      this.label2.AutoSize = true;
      this.label2.Location = new Point(7, 42);
      this.label2.Margin = new Padding(1, 0, 1, 0);
      this.label2.Name = "label2";
      this.label2.Size = new Size(32, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "URL:";
      this.tbURL.Location = new Point(61, 42);
      this.tbURL.Margin = new Padding(1);
      this.tbURL.Multiline = true;
      this.tbURL.Name = "tbURL";
      this.tbURL.Size = new Size(547, 38);
      this.tbURL.TabIndex = 2;
      this.tbURL.KeyDown += new KeyEventHandler(this.tbURL_KeyDown);
      this.cbEnabled.AutoSize = true;
      this.cbEnabled.Checked = true;
      this.cbEnabled.CheckState = CheckState.Checked;
      this.cbEnabled.Location = new Point(544, 12);
      this.cbEnabled.Margin = new Padding(1);
      this.cbEnabled.Name = "cbEnabled";
      this.cbEnabled.Size = new Size(65, 17);
      this.cbEnabled.TabIndex = 4;
      this.cbEnabled.Text = "Enabled";
      this.cbEnabled.UseVisualStyleBackColor = true;
      this.label3.AutoSize = true;
      this.label3.Location = new Point(7, 90);
      this.label3.Margin = new Padding(1, 0, 1, 0);
      this.label3.Name = "label3";
      this.label3.Size = new Size(45, 13);
      this.label3.TabIndex = 6;
      this.label3.Text = "Referer:";
      this.tbReferer.Location = new Point(61, 90);
      this.tbReferer.Margin = new Padding(1);
      this.tbReferer.Multiline = true;
      this.tbReferer.Name = "tbReferer";
      this.tbReferer.Size = new Size(547, 61);
      this.tbReferer.TabIndex = 5;
      this.tbReferer.Text = "https://google.com\r\nhttps://yahoo.com\r\nhttps://bing.com\r\nhttps://facebook.com";
      this.tbReferer.KeyDown += new KeyEventHandler(this.tbReferer_KeyDown);
      this.tbURLScript.Font = new Font("Segoe UI", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.tbURLScript.Location = new Point(61, 174);
      this.tbURLScript.Margin = new Padding(1);
      this.tbURLScript.Multiline = true;
      this.tbURLScript.Name = "tbURLScript";
      this.tbURLScript.Size = new Size(547, 48);
      this.tbURLScript.TabIndex = 7;
      this.tbURLScript.KeyDown += new KeyEventHandler(this.tbURLScript_KeyDown);
      this.cbUseURLScript.AutoSize = true;
      this.cbUseURLScript.Checked = true;
      this.cbUseURLScript.CheckState = CheckState.Checked;
      this.cbUseURLScript.Location = new Point(61, 155);
      this.cbUseURLScript.Margin = new Padding(1);
      this.cbUseURLScript.Name = "cbUseURLScript";
      this.cbUseURLScript.Size = new Size(252, 17);
      this.cbUseURLScript.TabIndex = 8;
      this.cbUseURLScript.Text = "[Script 1] Use this script after opening App Store";
      this.cbUseURLScript.UseVisualStyleBackColor = true;
      this.label4.AutoSize = true;
      this.label4.Location = new Point(63, 230);
      this.label4.Margin = new Padding(1, 0, 1, 0);
      this.label4.Name = "label4";
      this.label4.Size = new Size(103, 13);
      this.label4.TabIndex = 9;
      this.label4.Text = "AppStore open time:";
      this.numAppStoreTime.Location = new Point(171, 228);
      this.numAppStoreTime.Margin = new Padding(1);
      this.numAppStoreTime.Name = "numAppStoreTime";
      this.numAppStoreTime.Size = new Size(60, 20);
      this.numAppStoreTime.TabIndex = 10;
      this.numAppStoreTime.Value = new Decimal(new int[4]
      {
        5,
        0,
        0,
        0
      });
      this.numAppStoreTimeMin.Location = new Point(417, 228);
      this.numAppStoreTimeMin.Margin = new Padding(1);
      this.numAppStoreTimeMin.Name = "numAppStoreTimeMin";
      this.numAppStoreTimeMin.Size = new Size(62, 20);
      this.numAppStoreTimeMin.TabIndex = 12;
      this.numAppStoreTimeMin.Value = new Decimal(new int[4]
      {
        5,
        0,
        0,
        0
      });
      this.numAppStoreTimeMax.Location = new Point(544, 228);
      this.numAppStoreTimeMax.Margin = new Padding(1);
      this.numAppStoreTimeMax.Name = "numAppStoreTimeMax";
      this.numAppStoreTimeMax.Size = new Size(63, 20);
      this.numAppStoreTimeMax.TabIndex = 14;
      this.numAppStoreTimeMax.Value = new Decimal(new int[4]
      {
        25,
        0,
        0,
        0
      });
      this.numAppStoreTimeMax.ValueChanged += new EventHandler(this.numAppStoreTimeMax_ValueChanged);
      this.label6.AutoSize = true;
      this.label6.Location = new Point(515, 230);
      this.label6.Margin = new Padding(1, 0, 1, 0);
      this.label6.Name = "label6";
      this.label6.Size = new Size(19, 13);
      this.label6.TabIndex = 13;
      this.label6.Text = "to:";
      this.numAppTime.Location = new Point(171, 257);
      this.numAppTime.Margin = new Padding(1);
      this.numAppTime.Maximum = new Decimal(new int[4]
      {
        3600,
        0,
        0,
        0
      });
      this.numAppTime.Name = "numAppTime";
      this.numAppTime.Size = new Size(60, 20);
      this.numAppTime.TabIndex = 16;
      this.numAppTime.Value = new Decimal(new int[4]
      {
        30,
        0,
        0,
        0
      });
      this.label7.AutoSize = true;
      this.label7.Location = new Point(63, 260);
      this.label7.Margin = new Padding(1, 0, 1, 0);
      this.label7.Name = "label7";
      this.label7.Size = new Size(78, 13);
      this.label7.TabIndex = 15;
      this.label7.Text = "App open time:";
      this.label8.AutoSize = true;
      this.label8.Location = new Point(318, 260);
      this.label8.Margin = new Padding(1, 0, 1, 0);
      this.label8.Name = "label8";
      this.label8.Size = new Size(58, 13);
      this.label8.TabIndex = 17;
      this.label8.Text = "App name:";
      this.cbApps.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cbApps.FormattingEnabled = true;
      this.cbApps.Location = new Point(417, 258);
      this.cbApps.Margin = new Padding(1);
      this.cbApps.Name = "cbApps";
      this.cbApps.Size = new Size(191, 21);
      this.cbApps.TabIndex = 18;
      this.cbUseScript.AutoSize = true;
      this.cbUseScript.Checked = true;
      this.cbUseScript.CheckState = CheckState.Checked;
      this.cbUseScript.Location = new Point(61, 440);
      this.cbUseScript.Margin = new Padding(1);
      this.cbUseScript.Name = "cbUseScript";
      this.cbUseScript.Size = new Size(250, 17);
      this.cbUseScript.TabIndex = 19;
      this.cbUseScript.Text = "[Script 2] Use script correspond to app (if exists)";
      this.cbUseScript.UseVisualStyleBackColor = true;
      this.appsWorker.DoWork += new DoWorkEventHandler(this.appsWorker_DoWork);
      this.btRefresh.Image = (Image) Resources.refresh;
      this.btRefresh.Location = new Point(484, 322);
      this.btRefresh.Margin = new Padding(2);
      this.btRefresh.Name = "btRefresh";
      this.btRefresh.Size = new Size(28, 19);
      this.btRefresh.TabIndex = 20;
      this.btRefresh.UseVisualStyleBackColor = true;
      this.btRefresh.Click += new EventHandler(this.btRefresh_Click);
      this.btnSave.Location = new Point(535, 434);
      this.btnSave.Margin = new Padding(2);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new Size(73, 27);
      this.btnSave.TabIndex = 21;
      this.btnSave.Text = "Done";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new EventHandler(this.btnSave_Click);
      this.cbRandom.AutoSize = true;
      this.cbRandom.Location = new Point(315, 229);
      this.cbRandom.Margin = new Padding(2);
      this.cbRandom.Name = "cbRandom";
      this.cbRandom.Size = new Size(92, 17);
      this.cbRandom.TabIndex = 22;
      this.cbRandom.Text = "Random from:";
      this.cbRandom.UseVisualStyleBackColor = true;
      this.btnHelp.Location = new Point(8, 173);
      this.btnHelp.Margin = new Padding(2);
      this.btnHelp.Name = "btnHelp";
      this.btnHelp.Size = new Size(44, 20);
      this.btnHelp.TabIndex = 23;
      this.btnHelp.Text = "Help";
      this.btnHelp.UseVisualStyleBackColor = true;
      this.btnHelp.Click += new EventHandler(this.btnHelp_Click);
      this.label5.AutoSize = true;
      this.label5.Location = new Point(63, 312);
      this.label5.Margin = new Padding(1, 0, 1, 0);
      this.label5.Name = "label5";
      this.label5.Size = new Size(50, 13);
      this.label5.TabIndex = 24;
      this.label5.Text = "Wipe list:";
      this.btnAddToWipeList.Location = new Point(516, 322);
      this.btnAddToWipeList.Margin = new Padding(2);
      this.btnAddToWipeList.Name = "btnAddToWipeList";
      this.btnAddToWipeList.Size = new Size(91, 19);
      this.btnAddToWipeList.TabIndex = 26;
      this.btnAddToWipeList.Text = "Add to Wipe list";
      this.btnAddToWipeList.UseVisualStyleBackColor = true;
      this.btnAddToWipeList.Click += new EventHandler(this.btnAddToWipeList_Click);
      this.tbWipeList.Location = new Point(63, 345);
      this.tbWipeList.Margin = new Padding(2);
      this.tbWipeList.Multiline = true;
      this.tbWipeList.Name = "tbWipeList";
      this.tbWipeList.Size = new Size(545, 87);
      this.tbWipeList.TabIndex = 27;
      this.tbWipeList.TextChanged += new EventHandler(this.tbWipeList_TextChanged);
      this.tbWipeList.KeyDown += new KeyEventHandler(this.tbWipeList_KeyDown);
      this.checkBox1.AutoSize = true;
      this.checkBox1.Location = new Point(315, 288);
      this.checkBox1.Margin = new Padding(2);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new Size(106, 17);
      this.checkBox1.TabIndex = 28;
      this.checkBox1.Text = "Name app no EL";
      this.checkBox1.UseVisualStyleBackColor = true;
      this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
      this.textBox1.Enabled = false;
      this.textBox1.Location = new Point(417, 287);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(190, 20);
      this.textBox1.TabIndex = 29;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(617, 470);
      this.Controls.Add((Control) this.textBox1);
      this.Controls.Add((Control) this.checkBox1);
      this.Controls.Add((Control) this.tbWipeList);
      this.Controls.Add((Control) this.btnAddToWipeList);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.btnHelp);
      this.Controls.Add((Control) this.cbRandom);
      this.Controls.Add((Control) this.btnSave);
      this.Controls.Add((Control) this.btRefresh);
      this.Controls.Add((Control) this.cbUseScript);
      this.Controls.Add((Control) this.cbApps);
      this.Controls.Add((Control) this.label8);
      this.Controls.Add((Control) this.numAppTime);
      this.Controls.Add((Control) this.label7);
      this.Controls.Add((Control) this.numAppStoreTimeMax);
      this.Controls.Add((Control) this.label6);
      this.Controls.Add((Control) this.numAppStoreTimeMin);
      this.Controls.Add((Control) this.numAppStoreTime);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.cbUseURLScript);
      this.Controls.Add((Control) this.tbURLScript);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.tbReferer);
      this.Controls.Add((Control) this.cbEnabled);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.tbURL);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.tbName);
      this.Margin = new Padding(1);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmAddOffer";
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "New offer";
      this.Load += new EventHandler(this.frmAddOffer_Load);
      this.numAppStoreTime.EndInit();
      this.numAppStoreTimeMin.EndInit();
      this.numAppStoreTimeMax.EndInit();
      this.numAppTime.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
