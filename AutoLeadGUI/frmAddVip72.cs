// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.frmAddVip72
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoLeadGUI
{
  public class frmAddVip72 : Form
  {
    public string username = (string) null;
    public string password = (string) null;
    public string tokenvip = (string) null;
    private IContainer components = (IContainer) null;
    private TextBox tbVip72Password;
    private Label label17;
    private TextBox tbVip72ID;
    private Label label16;
    private Button btnCancel;
    private Button btnOk;
    private TextBox token;
    private Label label1;

    public frmAddVip72()
    {
      this.InitializeComponent();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      this.username = this.tbVip72ID.Text;
      this.password = this.tbVip72Password.Text;
      this.tokenvip = this.token.Text;
      this.Close();
    }

    private void tbVip72ID_TextChanged(object sender, EventArgs e)
    {
      if (this.tbVip72ID.Text.Length > 0 && this.tbVip72Password.Text.Length > 0)
        this.btnOk.Enabled = true;
      else
        this.btnOk.Enabled = false;
    }

    private void tbVip72Password_TextChanged(object sender, EventArgs e)
    {
      if (this.tbVip72ID.Text.Length > 0 && this.tbVip72Password.Text.Length > 0)
        this.btnOk.Enabled = true;
      else
        this.btnOk.Enabled = false;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.tbVip72Password = new TextBox();
      this.label17 = new Label();
      this.tbVip72ID = new TextBox();
      this.label16 = new Label();
      this.btnCancel = new Button();
      this.btnOk = new Button();
      this.token = new TextBox();
      this.label1 = new Label();
      this.SuspendLayout();
      this.tbVip72Password.Location = new Point(107, 50);
      this.tbVip72Password.Margin = new Padding(1);
      this.tbVip72Password.Name = "tbVip72Password";
      this.tbVip72Password.Size = new Size(237, 20);
      this.tbVip72Password.TabIndex = 9;
      this.tbVip72Password.TextChanged += new EventHandler(this.tbVip72Password_TextChanged);
      this.label17.AutoSize = true;
      this.label17.Location = new Point(42, 52);
      this.label17.Margin = new Padding(1, 0, 1, 0);
      this.label17.Name = "label17";
      this.label17.Size = new Size(56, 13);
      this.label17.TabIndex = 8;
      this.label17.Text = "Password:";
      this.tbVip72ID.Location = new Point(107, 16);
      this.tbVip72ID.Margin = new Padding(1);
      this.tbVip72ID.Name = "tbVip72ID";
      this.tbVip72ID.Size = new Size(237, 20);
      this.tbVip72ID.TabIndex = 7;
      this.tbVip72ID.TextChanged += new EventHandler(this.tbVip72ID_TextChanged);
      this.label16.AutoSize = true;
      this.label16.Location = new Point(42, 18);
      this.label16.Margin = new Padding(1, 0, 1, 0);
      this.label16.Name = "label16";
      this.label16.Size = new Size(21, 13);
      this.label16.TabIndex = 6;
      this.label16.Text = "ID:";
      this.btnCancel.Location = new Point(47, 135);
      this.btnCancel.Margin = new Padding(2);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(50, 28);
      this.btnCancel.TabIndex = 10;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.btnOk.Enabled = false;
      this.btnOk.Location = new Point(206, 135);
      this.btnOk.Margin = new Padding(2);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new Size(50, 28);
      this.btnOk.TabIndex = 11;
      this.btnOk.Text = "OK";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new EventHandler(this.btnOk_Click);
      this.token.Location = new Point(107, 88);
      this.token.Margin = new Padding(1);
      this.token.Name = "token";
      this.token.Size = new Size(237, 20);
      this.token.TabIndex = 13;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(42, 90);
      this.label1.Margin = new Padding(1, 0, 1, 0);
      this.label1.Name = "label1";
      this.label1.Size = new Size(41, 13);
      this.label1.TabIndex = 12;
      this.label1.Text = "Token:";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(379, 196);
      this.ControlBox = false;
      this.Controls.Add((Control) this.token);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.btnOk);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.tbVip72Password);
      this.Controls.Add((Control) this.label17);
      this.Controls.Add((Control) this.tbVip72ID);
      this.Controls.Add((Control) this.label16);
      this.FormBorderStyle = FormBorderStyle.Fixed3D;
      this.Margin = new Padding(2);
      this.Name = "frmAddVip72";
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Enter Vip72 account";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
