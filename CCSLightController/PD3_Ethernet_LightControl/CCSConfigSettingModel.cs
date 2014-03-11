using System;
using System.Collections.Generic;
using System.Text;

namespace PD3_Ethernet_LightControl
{

	public class CCSConfigSettingModel
	{
		public CCSConfigSettingModel()
		{
			this.EnableChannels = new List<string>();
		}
		/// <summary>
		/// 檢查連線是否啟用
		/// </summary>
		public bool HeartBeatEnable { get; set; }

		/// <summary>
		/// 檢查間隔
		/// </summary>
		public int HeartBeatInterval { get; set; }

		/// <summary>
		/// 頻道啟用
		/// </summary>
		public List<string> EnableChannels { get; set; }

		/// <summary>
		/// 最大嘗試連線次數
		/// </summary>
		public int TryConnectionTimes { get; set; }

		public int Timeout { get; set; }
	}
}
