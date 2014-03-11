namespace PD3_Ethernet_LightControl
{
	partial class SettingForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.HeartBeatEnableCheckBox = new System.Windows.Forms.CheckBox();
			this.CH1CheckBox = new System.Windows.Forms.CheckBox();
			this.CH2CheckBox = new System.Windows.Forms.CheckBox();
			this.CH3CheckBox = new System.Windows.Forms.CheckBox();
			this.HearbeatIntervalTextBox = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.OKButton = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(35, 39);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(114, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "WatchDog 間隔 ( ms )";
			// 
			// HeartBeatEnableCheckBox
			// 
			this.HeartBeatEnableCheckBox.AutoSize = true;
			this.HeartBeatEnableCheckBox.Location = new System.Drawing.Point(6, 19);
			this.HeartBeatEnableCheckBox.Name = "HeartBeatEnableCheckBox";
			this.HeartBeatEnableCheckBox.Size = new System.Drawing.Size(105, 17);
			this.HeartBeatEnableCheckBox.TabIndex = 1;
			this.HeartBeatEnableCheckBox.Text = "啟用 WatchDog";
			this.HeartBeatEnableCheckBox.UseVisualStyleBackColor = true;
			this.HeartBeatEnableCheckBox.CheckedChanged += new System.EventHandler(this.HeartBeatEnableCheckBox_CheckedChanged);
			// 
			// CH1CheckBox
			// 
			this.CH1CheckBox.AutoSize = true;
			this.CH1CheckBox.Location = new System.Drawing.Point(6, 19);
			this.CH1CheckBox.Name = "CH1CheckBox";
			this.CH1CheckBox.Size = new System.Drawing.Size(74, 17);
			this.CH1CheckBox.TabIndex = 2;
			this.CH1CheckBox.Tag = "00";
			this.CH1CheckBox.Text = "啟用 CH1";
			this.CH1CheckBox.UseVisualStyleBackColor = true;
			// 
			// CH2CheckBox
			// 
			this.CH2CheckBox.AutoSize = true;
			this.CH2CheckBox.Location = new System.Drawing.Point(105, 19);
			this.CH2CheckBox.Name = "CH2CheckBox";
			this.CH2CheckBox.Size = new System.Drawing.Size(74, 17);
			this.CH2CheckBox.TabIndex = 3;
			this.CH2CheckBox.Tag = "01";
			this.CH2CheckBox.Text = "啟用 CH2";
			this.CH2CheckBox.UseVisualStyleBackColor = true;
			// 
			// CH3CheckBox
			// 
			this.CH3CheckBox.AutoSize = true;
			this.CH3CheckBox.Location = new System.Drawing.Point(6, 42);
			this.CH3CheckBox.Name = "CH3CheckBox";
			this.CH3CheckBox.Size = new System.Drawing.Size(74, 17);
			this.CH3CheckBox.TabIndex = 4;
			this.CH3CheckBox.Tag = "02";
			this.CH3CheckBox.Text = "啟用 CH3";
			this.CH3CheckBox.UseVisualStyleBackColor = true;
			// 
			// HearbeatIntervalTextBox
			// 
			this.HearbeatIntervalTextBox.Location = new System.Drawing.Point(155, 36);
			this.HearbeatIntervalTextBox.Name = "HearbeatIntervalTextBox";
			this.HearbeatIntervalTextBox.Size = new System.Drawing.Size(100, 20);
			this.HearbeatIntervalTextBox.TabIndex = 5;
			this.HearbeatIntervalTextBox.TextChanged += new System.EventHandler(this.HearbeatIntervalTextBox_TextChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.HeartBeatEnableCheckBox);
			this.groupBox1.Controls.Add(this.HearbeatIntervalTextBox);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(298, 69);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "WatchDog";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.CH1CheckBox);
			this.groupBox2.Controls.Add(this.CH2CheckBox);
			this.groupBox2.Controls.Add(this.CH3CheckBox);
			this.groupBox2.Location = new System.Drawing.Point(12, 87);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(298, 73);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "頻道設定";
			// 
			// OKButton
			// 
			this.OKButton.Location = new System.Drawing.Point(198, 166);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 8;
			this.OKButton.Text = "確定";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// SettingForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(323, 209);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "SettingForm";
			this.Text = "SettingForm";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox HeartBeatEnableCheckBox;
		private System.Windows.Forms.CheckBox CH1CheckBox;
		private System.Windows.Forms.CheckBox CH2CheckBox;
		private System.Windows.Forms.CheckBox CH3CheckBox;
		private System.Windows.Forms.TextBox HearbeatIntervalTextBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button OKButton;
	}
}