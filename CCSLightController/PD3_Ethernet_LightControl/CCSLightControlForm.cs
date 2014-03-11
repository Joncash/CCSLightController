using LightControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PD3_Ethernet_LightControl
{
	public partial class CCSLightControlForm : Form
	{
		CCSLightControlManager _lightControlManager;
		CCSLightControlAssistant _lightControlAssistant;

		private CCSConfigSettingModel _configModel;
		private bool _lock = false;
		private bool _isConnected = false;
		private int _maxTry = 3;	//最大嘗試次數
		private int _curTry = 0;	//目前嘗試次數
		private bool _latestHardwareConnectedStatus;
		public CCSLightControlForm()
		{
			InitializeComponent();
			_configModel = ConfigHelper.GetConfigModel();
			initLightControlSettings();
			LightConnectTimer.Tick += LightConnectTimer_Tick;
		}
		#region Form Event
		private void CCSLightControlForm_Load(object sender, EventArgs e)
		{
			CCSLightControlForm_LocationChanged(sender, e);
			_lightControlManager = new CCSLightControlManager();
			_lightControlManager.On_SafetyClosed += _lightControlManager_On_SafetyClosed;
			if (_configModel.HeartBeatEnable)
				_lightControlManager.StartProbeConnetion(_configModel.HeartBeatInterval);

			_lightControlAssistant = new CCSLightControlAssistant(_lightControlManager);
			initializeLightControl();
		}

		private void CCSLightControlForm_Shown(object sender, EventArgs e)
		{
			LightConnectTimer.Interval = 1500;
			LightConnectTimer.Enabled = true;
			LightConnectTimer.Start();
		}
		private void CCSLightControlForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_lightControlManager != null)
			{
				_lightControlManager.DoSafetyClose();
				if (!_lightControlManager.IsDoSafetyCloseDone)
				{
					e.Cancel = true;
				}
			}
		}
		void _lightControlManager_On_SafetyClosed(object sender, object args)
		{
			this.Close();
		}
		#endregion
		void LightConnectTimer_Tick(object sender, EventArgs e)
		{
			var timer = sender as System.Windows.Forms.Timer;
			_curTry++;
			bool isOverRetryCont = _curTry > _maxTry;
			if (isOverRetryCont || _isConnected)
			{
				timer.Stop();
				timer.Enabled = false;
			}
			else if (!_isConnected)
			{
				initializeLightControl();
			}

			if (isOverRetryCont && !_isConnected)
			{
				MessageBox.Show("光源控制器尚未連線，請確認電源及網路介面是否連接正確。");
			}
		}

		private void initializeLightControl()
		{
			ResetButton.Enabled = false;
			this.ResetButton.Image = global::PD3_Ethernet_LightControl.Properties.Resources.power_Off;
			ControlPanel.Enabled = false;
			if (!_lightControlManager.Connected)
			{
				var msg = _lightControlManager.Connect(_configModel.Timeout);
				if (msg == "Timeout")
				{
					StatusLabel.Text = "狀態: 未連線";
				}
			}
			IPLabel.Text = "IP :" + _lightControlManager.ControllerIPAddress.ToString();
			PortLabel.Text = "Port :" + _lightControlManager.ControllerPort.ToString();
			_isConnected = _lightControlManager.Connected;

			if (_isConnected)
			{
				this.ResetButton.Image = global::PD3_Ethernet_LightControl.Properties.Resources.power_On_48;
				ControlPanel.Enabled = true;
				StatusLabel.Text = "狀態: 已連線";

				//binding Event
				_lightControlManager.On_ReceivedData += _lightControlManager_On_ReceivedData;
				L1CheckBox.CheckedChanged += new EventHandler(switchOnOff);
				L2CheckBox.CheckedChanged += new EventHandler(switchOnOff);
				L3CheckBox.CheckedChanged += new EventHandler(switchOnOff);

				numericUpDown1.ValueChanged += new EventHandler(intensityChanged);
				numericUpDown2.ValueChanged += new EventHandler(intensityChanged);
				numericUpDown3.ValueChanged += new EventHandler(intensityChanged);

				trackBar1.Scroll += new EventHandler(intensityChanged);
				trackBar2.Scroll += new EventHandler(intensityChanged);
				trackBar3.Scroll += new EventHandler(intensityChanged);

				_lightControlAssistant.SendMessage(_lightControlManager.GetChannelStatusModel(new string[] { "00", "01", "02" }));
				//init panel value
				//_lightControlManager.GetChannelStatusAsync(new string[] { "00", "01", "02" });
				//_lightControlManager.On_ProbeConnectionWorked += _lightControlManager_On_ProbeConnectionWorked;
				//_lightControlManager.StartProbeConnetion(null);
			}
			ResetButton.Enabled = true;
		}

		void _lightControlManager_On_ProbeConnectionWorked(object sender, object args)
		{
			var hardwareConnectedStatus = (bool)args;
			if (_latestHardwareConnectedStatus ^ hardwareConnectedStatus)
			{
				_latestHardwareConnectedStatus = ControlPanel.Enabled = hardwareConnectedStatus;
				this.ResetButton.Image = (!hardwareConnectedStatus) ?
					global::PD3_Ethernet_LightControl.Properties.Resources.power_Off :
					global::PD3_Ethernet_LightControl.Properties.Resources.power_On_48;

				var statusText = (hardwareConnectedStatus) ? "狀態: 已連線" : "狀態: 未連線";
				StatusLabel.Text = statusText;
			}
		}

		private int _curIntensity;
		private void intensityChanged(object sender, EventArgs e)
		{
			//var upDown = sender as NumericUpDown;
			IntensityModel dataModel = getIntensityModel(sender);
			if (_lightControlManager.Connected && dataModel != null)
			{
				if (!_lock)
				{
					var ccsCommandModel = _lightControlManager.GetIntesnityModel(dataModel.Intensity, dataModel.Channel);
					_lightControlAssistant.SendMessage(ccsCommandModel);
					_curIntensity = dataModel.Intensity;
				}
			}
		}

		private IntensityModel getIntensityModel(object sender)
		{
			IntensityModel model = null;
			if (sender is NumericUpDown)
			{
				var upDown = sender as NumericUpDown;
				model = new IntensityModel()
				{
					Channel = (string)upDown.Tag,
					Intensity = (int)upDown.Value,
				};
			}
			else if (sender is TrackBar)
			{
				var tkBar = sender as TrackBar;
				model = new IntensityModel()
				{
					Channel = (string)tkBar.Tag,
					Intensity = (int)tkBar.Value,
				};
			}
			return model;
		}

		private bool _curOnOff;
		private void switchOnOff(object sender, EventArgs e)
		{
			var ck = sender as CheckBox;
			if (_lightControlManager.Connected)
			{
				if (!_lock)
				{
					var channel = (string)ck.Tag;
					var onOff = ck.Checked ? LightSwitch.On : LightSwitch.OFF;
					var model = _lightControlManager.GetLightOnOffModel(onOff, channel);
					_lightControlAssistant.SendMessage(model);
					_curOnOff = ck.Checked;
				}
			}
		}

		void _lightControlManager_On_ReceivedData(object sender, object receiveMessage)
		{
			//解析 receiveMessage
			var model = CCSReceiveDataResolver.Resolve((string)receiveMessage);
			int intensity;
			if (!Int32.TryParse(model.Intensity, out intensity))
			{
				intensity = _curIntensity;
			}
			bool onOff;
			if (!Boolean.TryParse(model.OnOff, out onOff))
			{
				onOff = _curOnOff;
			}
			_lock = true;
			switch (model.Channel)
			{
				case "00":
					if (model.Instruction == "L" || model.Instruction == "M")
					{
						L1CheckBox.Checked = onOff;
					}
					if (model.Instruction == "F" || model.Instruction == "M")
					{
						numericUpDown1.Value = intensity;
						trackBar1.Value = intensity;
					}
					break;
				case "01":
					if (model.Instruction == "L" || model.Instruction == "M")
					{
						L2CheckBox.Checked = onOff;
					}
					if (model.Instruction == "F" || model.Instruction == "M")
					{
						numericUpDown2.Value = intensity;
						trackBar2.Value = intensity;
					}
					break;
				case "02":
					if (model.Instruction == "L" || model.Instruction == "M")
					{
						L3CheckBox.Checked = onOff;
					}
					if (model.Instruction == "F" || model.Instruction == "M")
					{
						numericUpDown3.Value = intensity;
						trackBar3.Value = intensity;
					}
					break;
			}
			_lock = false;

		}

		private void ResetButton_Click(object sender, EventArgs e)
		{
			initializeLightControl();
		}
		private void TraceConnectTimer_Tick(object sender, EventArgs e)
		{
			//偵測連線是否中斷

			//this.ResetButton.Image = global::PD3_Ethernet_LightControl.Properties.Resources.power_Off;
			//ControlPanel.Enabled = false;
		}

		private void CCSLightControlForm_LocationChanged(object sender, EventArgs e)
		{
			var win = sender as Form;
			LocationLabel.Text = String.Format("x: {0} y: {1}", win.Location.X, win.Location.Y);
		}

		private void SettingButton_Click(object sender, EventArgs e)
		{
			//Original
			var heartBeatEnable = _configModel.HeartBeatEnable;
			//var hearBeatInterval = _configModel.HeartBeatInterval;

			var form = new SettingForm();
			form.ShowDialog();
			_configModel = ConfigHelper.GetConfigModel();
			initLightControlSettings();
		}

		private void initLightControlSettings()
		{
			flowLayoutPanel1.Enabled = _configModel.EnableChannels.Contains("00");
			flowLayoutPanel2.Enabled = _configModel.EnableChannels.Contains("01");
			flowLayoutPanel3.Enabled = _configModel.EnableChannels.Contains("02");
			if (_lightControlManager != null)
			{
				if (!_configModel.HeartBeatEnable)
					_lightControlManager.StopProbeConnection();
				else if (_configModel.HeartBeatEnable && (_configModel.HeartBeatInterval >= 200))
				{
					_lightControlManager.StartProbeConnetion(_configModel.HeartBeatInterval);

				}
			}
		}

		#region Public APIs

		public void SetLightData(string channel, string intensity, string onOff)
		{
			var switchOn = (onOff == "On");
			int intensityValue;
			Int32.TryParse(intensity, out intensityValue);
			switch (channel)
			{
				case "00":
					numericUpDown1.Value = intensityValue;
					L1CheckBox.Checked = switchOn;
					break;
				case "01":
					numericUpDown2.Value = intensityValue;
					L2CheckBox.Checked = switchOn;
					break;
			}
		}

		public void ResetLightControlMode()
		{
			//var replyMsg = _lightManager.SetLightOnOff(LightSwitch.OFF, "FF");
			//foreach (var channel in _channelList)
			//{
			//	//set continuous mode
			//	var lightMode = "00";
			//	replyMsg = _lightManager.SetLightMode(lightMode, channel);
			//}
		}
		public void GetChannelsInfo()
		{

		}
		#endregion
	}
}
