// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.frmUEScript
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace AutoLeadGUI
{
  public class frmUEScript : Form
  {
    public int type = 0;
    public string result = (string) null;
    private bool isRunning = false;
    private bool pause = false;
    private IContainer components = (IContainer) null;
    private JavaScriptSerializer jss;
    private DateTime time;
    private ListView lvScript;
    private ColumnHeader columnHeader1;
    public Label lbText;
    private Button btnDone;
    private BackgroundWorker bwMain;
    private ProgressBar progressBar1;
    private Button r_email;
    private Button r_firstname;
    private Button r_middlename;
    private Button r_lastname;
    private Button r_address;
    private Button r_touch;
    private Button r_number;
    private Button r_password;
    private Button r_name;
    private Button button2;
    private Button button1;

    public frmUEScript()
    {
      this.InitializeComponent();
    }

    private void frmUEScript_Load(object sender, EventArgs e)
    {
      this.time = DateTime.Now;
      this.jss = new JavaScriptSerializer();
      this.lvScript.Items.Clear();
      this.bwMain.RunWorkerAsync();
    }

    private void bwMain_DoWork(object sender, DoWorkEventArgs e)
    {
      if (!AutoLeadClient.connected)
        return;
      this.isRunning = true;
      while (this.isRunning)
      {
        if (AutoLeadClient.send("{\"cmd\":\"getCurrentUE\", \"type\":\"" + (object) this.type + "\"}"))
        {
          this.result = AutoLeadClient.receive();
          double secs = DateTime.Now.Subtract(this.time).TotalSeconds;
          this.time = DateTime.Now;
          this.lbText.Invoke(new Action(delegate
          {
            Dictionary<string, object> dictionary = this.jss.Deserialize<Dictionary<string, object>>(this.result);
            switch (Convert.ToInt32(dictionary["type"]))
            {
              case 1:
                ListViewItem listViewItem1 = new ListViewItem();
                listViewItem1.Text = "wait(" + (object) secs + ")";
                if (!this.pause)
                  this.lvScript.Items.Add(listViewItem1);
                ListViewItem listViewItem2 = new ListViewItem();
                double num1 = Convert.ToDouble(dictionary["x1"]);
                double num2 = Convert.ToDouble(dictionary["y1"]);
                double num3 = Convert.ToDouble(dictionary["duration"]);
                listViewItem2.Text = "touch(" + (object) num1 + "," + (object) num2 + "," + (object) num3 + ")";
                if (this.pause)
                  break;
                this.lvScript.Items.Add(listViewItem2);
                break;
              case 2:
                ListViewItem listViewItem3 = new ListViewItem();
                listViewItem3.Text = "wait(" + (object) secs + ")";
                if (!this.pause)
                  this.lvScript.Items.Add(listViewItem3);
                ListViewItem listViewItem4 = new ListViewItem();
                double num4 = Convert.ToDouble(dictionary["x1"]);
                double num5 = Convert.ToDouble(dictionary["y1"]);
                double num6 = Convert.ToDouble(dictionary["x2"]);
                double num7 = Convert.ToDouble(dictionary["y2"]);
                double num8 = Convert.ToDouble(dictionary["duration"]);
                listViewItem4.Text = "swipe(" + (object) num4 + "," + (object) num5 + "," + (object) num6 + "," + (object) num7 + "," + (object) num8 + ")";
                if (!this.pause)
                  this.lvScript.Items.Add(listViewItem4);
                break;
            }
          }));
        }
      }
    }

    private void btnDone_Click(object sender, EventArgs e)
    {
      this.isRunning = false;
      AutoLeadClientHelper.stopRecordingEU();
      this.result = (string) null;
      if (this.lvScript.Items.Count > 0)
      {
        this.result = "";
        for (int index = 0; index < this.lvScript.Items.Count; ++index)
          this.result = this.result + this.lvScript.Items[index].Text + "\r\n";
      }
      this.Close();
    }

    private void append_text(string type)
    {
      if (this.pause)
        return;
      this.lvScript.Items.Add(new ListViewItem()
      {
        Text = "text(" + type + ")"
      });
    }

    private void r_name_Click(object sender, EventArgs e)
    {
      this.append_text("rand_name");
    }

    private void r_password_Click(object sender, EventArgs e)
    {
      this.append_text("rand_password");
    }

    private void r_number_Click(object sender, EventArgs e)
    {
      this.append_text("rand_number,6");
    }

    private void r_touch_Click(object sender, EventArgs e)
    {
      if (this.pause)
        return;
      this.lvScript.Items.Add(new ListViewItem()
      {
        Text = "rand_touch(x1, y1, x2, y2, duration, count)"
      });
    }

    private void r_email_Click(object sender, EventArgs e)
    {
      this.append_text("rand_email");
    }

    private void r_firstname_Click(object sender, EventArgs e)
    {
      this.append_text("rand_firstname");
    }

    private void r_middlename_Click(object sender, EventArgs e)
    {
      this.append_text("rand_middlename");
    }

    private void r_lastname_Click(object sender, EventArgs e)
    {
      this.append_text("rand_lastname");
    }

    private void r_address_Click(object sender, EventArgs e)
    {
      this.append_text("rand_address");
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.button2.Enabled = true;
      this.button1.Enabled = false;
      this.pause = true;
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.button1.Enabled = true;
      this.button2.Enabled = false;
      this.pause = false;
    }

    private void lbText_Click(object sender, EventArgs e)
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
      ListViewItem listViewItem1 = new ListViewItem("hello");
      ListViewItem listViewItem2 = new ListViewItem("hello2");
      ListViewItem listViewItem3 = new ListViewItem("hello3");
      ListViewItem listViewItem4 = new ListViewItem("");
      ListViewItem listViewItem5 = new ListViewItem("");
      ListViewItem listViewItem6 = new ListViewItem("");
      this.lvScript = new ListView();
      this.columnHeader1 = new ColumnHeader();
      this.lbText = new Label();
      this.btnDone = new Button();
      this.bwMain = new BackgroundWorker();
      this.progressBar1 = new ProgressBar();
      this.r_email = new Button();
      this.r_firstname = new Button();
      this.r_middlename = new Button();
      this.r_lastname = new Button();
      this.r_address = new Button();
      this.r_touch = new Button();
      this.r_number = new Button();
      this.r_password = new Button();
      this.r_name = new Button();
      this.button2 = new Button();
      this.button1 = new Button();
      this.SuspendLayout();
      this.lvScript.Columns.AddRange(new ColumnHeader[1]
      {
        this.columnHeader1
      });
      this.lvScript.Font = new Font("Segoe UI", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lvScript.FullRowSelect = true;
      this.lvScript.HeaderStyle = ColumnHeaderStyle.None;
      this.lvScript.HideSelection = false;
      this.lvScript.Items.AddRange(new ListViewItem[6]
      {
        listViewItem1,
        listViewItem2,
        listViewItem3,
        listViewItem4,
        listViewItem5,
        listViewItem6
      });
      this.lvScript.Location = new Point(11, 27);
      this.lvScript.Margin = new Padding(2, 2, 2, 2);
      this.lvScript.Name = "lvScript";
      this.lvScript.Size = new Size(443, 230);
      this.lvScript.TabIndex = 0;
      this.lvScript.UseCompatibleStateImageBehavior = false;
      this.lvScript.View = View.Details;
      this.columnHeader1.Text = "";
      this.columnHeader1.Width = 600;
      this.lbText.Location = new Point(8, 6);
      this.lbText.Margin = new Padding(2, 0, 2, 0);
      this.lbText.Name = "lbText";
      this.lbText.Size = new Size(410, 19);
      this.lbText.TabIndex = 1;
      this.lbText.Text = "Recording your device event ...";
      this.lbText.TextAlign = ContentAlignment.MiddleCenter;
      this.lbText.Click += new EventHandler(this.lbText_Click);
      this.btnDone.Location = new Point(368, 319);
      this.btnDone.Margin = new Padding(2, 2, 2, 2);
      this.btnDone.Name = "btnDone";
      this.btnDone.Size = new Size(85, 29);
      this.btnDone.TabIndex = 2;
      this.btnDone.Text = "Done";
      this.btnDone.UseVisualStyleBackColor = true;
      this.btnDone.Click += new EventHandler(this.btnDone_Click);
      this.bwMain.DoWork += new DoWorkEventHandler(this.bwMain_DoWork);
      this.progressBar1.Location = new Point(11, 319);
      this.progressBar1.Margin = new Padding(2, 2, 2, 2);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new Size(347, 29);
      this.progressBar1.Style = ProgressBarStyle.Marquee;
      this.progressBar1.TabIndex = 3;
      this.r_email.Location = new Point(11, 289);
      this.r_email.Margin = new Padding(2, 2, 2, 2);
      this.r_email.Name = "r_email";
      this.r_email.Size = new Size(91, 21);
      this.r_email.TabIndex = 4;
      this.r_email.Text = "r_email";
      this.r_email.UseVisualStyleBackColor = true;
      this.r_email.Click += new EventHandler(this.r_email_Click);
      this.r_firstname.Location = new Point(106, 289);
      this.r_firstname.Margin = new Padding(2, 2, 2, 2);
      this.r_firstname.Name = "r_firstname";
      this.r_firstname.Size = new Size(83, 21);
      this.r_firstname.TabIndex = 5;
      this.r_firstname.Text = "r_firstname";
      this.r_firstname.UseVisualStyleBackColor = true;
      this.r_firstname.Click += new EventHandler(this.r_firstname_Click);
      this.r_middlename.Location = new Point(193, 289);
      this.r_middlename.Margin = new Padding(2, 2, 2, 2);
      this.r_middlename.Name = "r_middlename";
      this.r_middlename.Size = new Size(83, 21);
      this.r_middlename.TabIndex = 6;
      this.r_middlename.Text = "r_middlename";
      this.r_middlename.UseVisualStyleBackColor = true;
      this.r_middlename.Click += new EventHandler(this.r_middlename_Click);
      this.r_lastname.Location = new Point(280, 289);
      this.r_lastname.Margin = new Padding(2, 2, 2, 2);
      this.r_lastname.Name = "r_lastname";
      this.r_lastname.Size = new Size(84, 21);
      this.r_lastname.TabIndex = 7;
      this.r_lastname.Text = "r_lastname";
      this.r_lastname.UseVisualStyleBackColor = true;
      this.r_lastname.Click += new EventHandler(this.r_lastname_Click);
      this.r_address.Location = new Point(368, 289);
      this.r_address.Margin = new Padding(2, 2, 2, 2);
      this.r_address.Name = "r_address";
      this.r_address.Size = new Size(85, 21);
      this.r_address.TabIndex = 8;
      this.r_address.Text = "r_address";
      this.r_address.UseVisualStyleBackColor = true;
      this.r_address.Click += new EventHandler(this.r_address_Click);
      this.r_touch.Location = new Point(320, 264);
      this.r_touch.Margin = new Padding(2, 2, 2, 2);
      this.r_touch.Name = "r_touch";
      this.r_touch.Size = new Size(84, 21);
      this.r_touch.TabIndex = 12;
      this.r_touch.Text = "r_touch";
      this.r_touch.UseVisualStyleBackColor = true;
      this.r_touch.Click += new EventHandler(this.r_touch_Click);
      this.r_number.Location = new Point(233, 264);
      this.r_number.Margin = new Padding(2, 2, 2, 2);
      this.r_number.Name = "r_number";
      this.r_number.Size = new Size(83, 21);
      this.r_number.TabIndex = 11;
      this.r_number.Text = "r_number";
      this.r_number.UseVisualStyleBackColor = true;
      this.r_number.Click += new EventHandler(this.r_number_Click);
      this.r_password.Location = new Point(146, 264);
      this.r_password.Margin = new Padding(2, 2, 2, 2);
      this.r_password.Name = "r_password";
      this.r_password.Size = new Size(83, 21);
      this.r_password.TabIndex = 10;
      this.r_password.Text = "r_password";
      this.r_password.UseVisualStyleBackColor = true;
      this.r_password.Click += new EventHandler(this.r_password_Click);
      this.r_name.Location = new Point(51, 264);
      this.r_name.Margin = new Padding(2, 2, 2, 2);
      this.r_name.Name = "r_name";
      this.r_name.Size = new Size(91, 21);
      this.r_name.TabIndex = 9;
      this.r_name.Text = "r_name";
      this.r_name.UseVisualStyleBackColor = true;
      this.r_name.Click += new EventHandler(this.r_name_Click);
      this.button2.BackColor = Color.FromArgb(0, 192, 0);
      this.button2.Enabled = false;
      this.button2.ForeColor = Color.White;
      this.button2.Location = new Point(386, 357);
      this.button2.Margin = new Padding(1);
      this.button2.Name = "button2";
      this.button2.Size = new Size(68, 27);
      this.button2.TabIndex = 29;
      this.button2.Text = "Resume >||";
      this.button2.UseVisualStyleBackColor = false;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.button1.BackColor = Color.Red;
      this.button1.ForeColor = Color.Yellow;
      this.button1.Location = new Point(11, 357);
      this.button1.Margin = new Padding(1);
      this.button1.Name = "button1";
      this.button1.Size = new Size(68, 27);
      this.button1.TabIndex = 28;
      this.button1.Text = "Pause ||";
      this.button1.UseVisualStyleBackColor = false;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(461, 394);
      this.ControlBox = false;
      this.Controls.Add((Control) this.button2);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.r_touch);
      this.Controls.Add((Control) this.r_number);
      this.Controls.Add((Control) this.r_password);
      this.Controls.Add((Control) this.r_name);
      this.Controls.Add((Control) this.r_address);
      this.Controls.Add((Control) this.r_lastname);
      this.Controls.Add((Control) this.r_middlename);
      this.Controls.Add((Control) this.r_firstname);
      this.Controls.Add((Control) this.r_email);
      this.Controls.Add((Control) this.progressBar1);
      this.Controls.Add((Control) this.btnDone);
      this.Controls.Add((Control) this.lbText);
      this.Controls.Add((Control) this.lvScript);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Margin = new Padding(2, 2, 2, 2);
      this.Name = "frmUEScript";
      this.StartPosition = FormStartPosition.CenterParent;
      this.Load += new EventHandler(this.frmUEScript_Load);
      this.ResumeLayout(false);
    }
  }
}
