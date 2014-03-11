using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PD3_Ethernet_LightControl
{
	public partial class SettingForm : Form
	{
		private CCSConfigSettingModel _settings;
		//private ConfigurationManager
		List<string> _enableChannels = new List<string>();
		public SettingForm()
		{
			InitializeComponent();
			init();
		}

		private void init()
		{
			_settings = ConfigHelper.GetConfigModel();
			HeartBeatEnableCheckBox.Checked = _settings.HeartBeatEnable;
			HearbeatIntervalTextBox.Enabled = HeartBeatEnableCheckBox.Checked;
			HearbeatIntervalTextBox.Text = _settings.HeartBeatInterval.ToString();
			CH1CheckBox.Checked = _settings.EnableChannels.Contains("00");
			CH2CheckBox.Checked = _settings.EnableChannels.Contains("01");
			CH3CheckBox.Checked = _settings.EnableChannels.Contains("02");

			CH1CheckBox.CheckedChanged += new EventHandler(channelEnableChanged);
			CH2CheckBox.CheckedChanged += new EventHandler(channelEnableChanged);
			CH3CheckBox.CheckedChanged += new EventHandler(channelEnableChanged);
		}
		private void channelEnableChanged(object sender, EventArgs e)
		{
			var ck = sender as CheckBox;
			var val = (string)ck.Tag;
			_settings.EnableChannels.Remove(val);
			if (ck.Checked)
				_settings.EnableChannels.Add(val);
		}


		private void HeartBeatEnableCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			var ck = sender as CheckBox;
			HearbeatIntervalTextBox.Enabled = _settings.HeartBeatEnable = ck.Checked;
		}
		private void HearbeatIntervalTextBox_TextChanged(object sender, EventArgs e)
		{
			var text = sender as TextBox;
			int val;
			if (Int32.TryParse(text.Text.Trim(), out val))
			{
				_settings.HeartBeatInterval = val;
			}
			else
			{
				MessageBox.Show("請輸入整數");
			}
		}
		private void OKButton_Click(object sender, EventArgs e)
		{
			//存資料
			if (MessageBox.Show("確定變更設定？", "提醒", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
			{
				string msg = "設定變更成功";

				var appSettings = ConfigurationManager.AppSettings;
				var enableChannels = String.Join(",", _settings.EnableChannels.ToArray());
				var heartEnable = _settings.HeartBeatEnable.ToString();
				var heartInterval = _settings.HeartBeatInterval.ToString();
				appSettings.Set("EnableChannels", enableChannels);

				appSettings.Set("HeartbeatEnabled", heartEnable);
				appSettings.Set("HeartbeatInterval", heartInterval);
				try
				{
					//儲存設定檔
					var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
					config.AppSettings.Settings["EnableChannels"].Value = enableChannels;
					config.AppSettings.Settings["HeartbeatEnabled"].Value = heartEnable;
					config.AppSettings.Settings["HeartbeatInterval"].Value = heartInterval;
					config.Save(ConfigurationSaveMode.Modified);
					this.Close();
				}
				catch (Exception ex)
				{
					msg = "設定檔案儲存變更失敗, 下次開啟程式時仍維持之前設定值, 失敗訊息: " + ex.Message;
				}
				finally
				{
					MessageBox.Show(msg);
				}
			}
		}


	}
}
