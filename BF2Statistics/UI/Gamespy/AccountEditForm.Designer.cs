﻿namespace BF2Statistics
{
    partial class AccountEditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountEditForm));
            this.label4 = new System.Windows.Forms.Label();
            this.UpdateBtn = new System.Windows.Forms.Button();
            this.AccountEmail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.AccountPass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.AccountNick = new System.Windows.Forms.TextBox();
            this.DeleteBtn = new System.Windows.Forms.Button();
            this.PlayerID = new System.Windows.Forms.NumericUpDown();
            this.DisconnectBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.SatusLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerID)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.SystemColors.Window;
            this.label4.Name = "label4";
            // 
            // UpdateBtn
            // 
            resources.ApplyResources(this.UpdateBtn, "UpdateBtn");
            this.UpdateBtn.Name = "UpdateBtn";
            this.UpdateBtn.UseVisualStyleBackColor = true;
            this.UpdateBtn.Click += new System.EventHandler(this.UpdateBtn_Click);
            // 
            // AccountEmail
            // 
            resources.ApplyResources(this.AccountEmail, "AccountEmail");
            this.AccountEmail.Name = "AccountEmail";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.SystemColors.Window;
            this.label3.Name = "label3";
            // 
            // AccountPass
            // 
            resources.ApplyResources(this.AccountPass, "AccountPass");
            this.AccountPass.Name = "AccountPass";
            this.AccountPass.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.SystemColors.Window;
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.SystemColors.Window;
            this.label1.Name = "label1";
            // 
            // AccountNick
            // 
            resources.ApplyResources(this.AccountNick, "AccountNick");
            this.AccountNick.Name = "AccountNick";
            // 
            // DeleteBtn
            // 
            resources.ApplyResources(this.DeleteBtn, "DeleteBtn");
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.UseVisualStyleBackColor = true;
            this.DeleteBtn.Click += new System.EventHandler(this.DeleteBtn_Click);
            // 
            // PlayerID
            // 
            resources.ApplyResources(this.PlayerID, "PlayerID");
            this.PlayerID.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.PlayerID.Name = "PlayerID";
            // 
            // DisconnectBtn
            // 
            resources.ApplyResources(this.DisconnectBtn, "DisconnectBtn");
            this.DisconnectBtn.Name = "DisconnectBtn";
            this.DisconnectBtn.UseVisualStyleBackColor = true;
            this.DisconnectBtn.Click += new System.EventHandler(this.DisconnectBtn_Click);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.SystemColors.Window;
            this.label5.Name = "label5";
            // 
            // SatusLabel
            // 
            resources.ApplyResources(this.SatusLabel, "SatusLabel");
            this.SatusLabel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.SatusLabel.Name = "SatusLabel";
            // 
            // AccountEditForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.SatusLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.DisconnectBtn);
            this.Controls.Add(this.PlayerID);
            this.Controls.Add(this.DeleteBtn);
            this.Controls.Add(this.AccountNick);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.UpdateBtn);
            this.Controls.Add(this.AccountEmail);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.AccountPass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccountEditForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AccountEditForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.PlayerID)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button UpdateBtn;
        private System.Windows.Forms.TextBox AccountEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox AccountPass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox AccountNick;
        private System.Windows.Forms.Button DeleteBtn;
        private System.Windows.Forms.NumericUpDown PlayerID;
        private System.Windows.Forms.Button DisconnectBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label SatusLabel;


    }
}