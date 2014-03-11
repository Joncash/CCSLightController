using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace PD3_Ethernet_LightControl
{
	public class ConfigHelper
	{
		public static CCSConfigSettingModel GetConfigModel()
		{
			var appSettings = ConfigurationManager.AppSettings;
			var heartBeatEnable = appSettings["HeartbeatEnabled"] ?? "false";
			var heartBeatInterval = appSettings["HeartbeatInterval"] ?? "500";
			var enabledChannels = appSettings["EnableChannels"] ?? "00";
			var tryConnectionTimes = appSettings[""] ?? "3";
			var timeout = appSettings["ConnectionTimeout"] ?? "500";
			List<string> channels = new List<string>();
			channels.AddRange(enabledChannels.Split(','));
			CCSConfigSettingModel settings = new CCSConfigSettingModel()
			{
				HeartBeatEnable = Convert.ToBoolean(heartBeatEnable),
				HeartBeatInterval = Convert.ToInt32(heartBeatInterval),
				EnableChannels = channels,
				TryConnectionTimes = Convert.ToInt32(tryConnectionTimes),
				Timeout = Convert.ToInt32(timeout),
			};
			return settings;
		}
	}
}
