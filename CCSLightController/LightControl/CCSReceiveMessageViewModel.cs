using System;
using System.Collections.Generic;
using System.Text;

namespace LightControl
{
	public class CCSReceiveMessageViewModel
	{
		public string Channel { get; set; }
		public string Intensity { get; set; }
		public string OnOff { get; set; }
		public string Status { get; set; }

		public string LightMode { get; set; }

		public string Instruction { get; set; }
	}
}
