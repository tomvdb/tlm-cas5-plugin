using TLMSuite.decoder;
using System.Collections;

namespace tlm_cas5_plugin
{
	public class cas5_decoder_plugin : TLMSuite.decoder.IPlugin
	{
		public int Version
		{
			get
			{
				return 1;
			}
		}

		public string Author
		{
			get
			{
				return "Tom - ZR6TG";
			}
		}

		public string Name
		{
			get
			{
				return "Cas-5a Telemetry Decoder";
			}
		}

		public int NoradID
		{
			get
			{
				return 54684;
			}
		}

		public string Description
		{
			get
			{
				return "A decoder pluging for Cas-5A. Use HS Soundmodem from http://uz7.ho.ua/packetradio.htm. \nUse FSK G3RUH 4800bd setting for Sound Modem to decode received packets.";
			}
		}

		string hexData(byte[] arr)
        {
            string hexframe = "";


            for (int x = 0; x < arr.Length; x++)
			{
				hexframe += arr[x].ToString("X2");
			}

			return hexframe;
		}

		string hexData(byte[] arr, int start, int end)
		{
			string hexframe = "";

			for (int x = start; x <= end; x++)
			{
				hexframe += arr[x].ToString("X2");
			}

			return hexframe;
		}

		string binaryData(byte data)
        {
			return Convert.ToString(data, 2).PadLeft(8,'0');
        }

		int temperatureConversion(byte data)
        {
			int temperature = 0;

			temperature = data & 0x7F;
			int sign = data & 0x80;

			if (sign == 128)
				temperature *= -1;

			return temperature;
        }

		double voltageConversion(byte p1, byte p2)
        {
			double voltage = 0;
			voltage = Convert.ToDouble(p1.ToString() + "." + p2.ToString());
			return voltage;
		}

		int currentConversion(byte[] data, int index)
        {
			int current = 0;

			current = BitConverter.ToUInt16(new byte[] { data[index + 1], data[index] },0);

			return current;
        }

		string cameraQuality(byte data)
        {
			switch (data)
            {
				case 0: return "Highest Quality";
				case 1: return "Medium Quality";
				case 2: return "Low Quality";
			}

			return "Unknown";
        }

		string cameraResolution(byte data)
		{
			switch (data)
			{
				case 0: return "800x480";
				case 1: return "1280x720";
				case 2: return "320x240";
				case 3: return "1440x896";
				case 4: return "640x480";
				case 5: return "1920x1080";
				case 6: return "800x600";
				case 7: return "1024x768";
			}

			return "Unknown";
		}

		string datetimeFormat(byte[] data, int index)
        {
			string dateString = "";

			dateString = data[index].ToString().PadLeft(2, '0') + "/";
			dateString += data[index+1].ToString().PadLeft(2, '0') + "/";
			dateString += data[index+2].ToString().PadLeft(2, '0') + " ";
			dateString += data[index+3].ToString().PadLeft(2, '0') + ":";
			dateString += data[index+4].ToString().PadLeft(2, '0') + ":";
			dateString += data[index + 5].ToString().PadLeft(2, '0');

			return dateString;
        }

		string timeFormat(byte[] data, int index)
		{
			string dateString = "";

			dateString += data[index].ToString().PadLeft(2, '0') + ":";
			dateString += data[index + 1].ToString().PadLeft(2, '0') + ":";
			dateString += data[index + 2].ToString().PadLeft(2, '0');

			return dateString;
		}

		public Hashtable parseFrame(byte[] frame)
		{
			Hashtable parsedData = new Hashtable();

			try
            {
				Ax25frames ax25frame = new Ax25frames(new Kaitai.KaitaiStream(frame));

				if (ax25frame != null)
				{
					if (ax25frame.ax25frame.payload != null)
					{
						Ax25frames.UiFrame payload = (Ax25frames.UiFrame)ax25frame.ax25frame.payload;

						if (payload.Ax25Info.Length == 167)
						{
							// function code
							string functionCode = hexData(payload.Ax25Info, 0, 6);
							parsedData.Add("Satellite Time", datetimeFormat(payload.Ax25Info, 7));
							parsedData.Add("Function Code", functionCode);
							parsedData.Add("IHU total reset counter", payload.Ax25Info[13]);
							parsedData.Add("Battery Status", binaryData(payload.Ax25Info[14]));
							parsedData.Add("Remote Control Frame Reception Counter", payload.Ax25Info[15]);
							parsedData.Add("Remote Control Command Execution Counter", payload.Ax25Info[16]);
							parsedData.Add("Telemetry Frame Transmission Counter", payload.Ax25Info[17]);
							parsedData.Add("IHU Status", binaryData(payload.Ax25Info[18]));
							parsedData.Add("I2C Status", binaryData(payload.Ax25Info[20]));
							parsedData.Add("IHU Status 2", binaryData(payload.Ax25Info[24]));
							parsedData.Add("IHU Status 3", binaryData(payload.Ax25Info[25]));
							parsedData.Add("+X Cabin Plate Inner Temperature", temperatureConversion(payload.Ax25Info[26]));
							parsedData.Add("-X Cabin Plate Inner Temperature", temperatureConversion(payload.Ax25Info[27]));
							parsedData.Add("PCDU Temperature", temperatureConversion(payload.Ax25Info[28]));
							parsedData.Add("DC/DC Temperature", temperatureConversion(payload.Ax25Info[29]));
							parsedData.Add("+Z Cabin Plate Inner Temperature", temperatureConversion(payload.Ax25Info[30]));
							parsedData.Add("-Z Cabin Plate Inner Temperature", temperatureConversion(payload.Ax25Info[31]));
							parsedData.Add("+X Solar Array Temperature", temperatureConversion(payload.Ax25Info[32]));
							parsedData.Add("-X Solar Array Temperature", temperatureConversion(payload.Ax25Info[33]));
							parsedData.Add("+Y Solar Array Temperature", temperatureConversion(payload.Ax25Info[34]));
							parsedData.Add("-Y Solar Array Temperature", temperatureConversion(payload.Ax25Info[35]));
							parsedData.Add("+Z Solar Array Temperature", temperatureConversion(payload.Ax25Info[36]));
							parsedData.Add("-Z Solar Array Temperature", temperatureConversion(payload.Ax25Info[37]));

							parsedData.Add("Battery Pack 1 Temperature 1", temperatureConversion(payload.Ax25Info[38]));
							parsedData.Add("Battery Pack 1 Temperature 2", temperatureConversion(payload.Ax25Info[39]));
							parsedData.Add("Battery Pack 2 Temperature 3", temperatureConversion(payload.Ax25Info[40]));
							parsedData.Add("Battery Pack 2 Temperature 4", temperatureConversion(payload.Ax25Info[41]));

							parsedData.Add("IHU Temperature", temperatureConversion(payload.Ax25Info[42]));
							parsedData.Add("UHF1 PA Temperature", temperatureConversion(payload.Ax25Info[43]));
							parsedData.Add("Camera 3 Temperature", temperatureConversion(payload.Ax25Info[44]));
							parsedData.Add("Camera 1 Temperature", temperatureConversion(payload.Ax25Info[45]));
							parsedData.Add("Camera 2 Temperature", temperatureConversion(payload.Ax25Info[46]));
							parsedData.Add("UHF2 PA Temperature", temperatureConversion(payload.Ax25Info[47]));

							parsedData.Add("Battery Voltage", voltageConversion(payload.Ax25Info[48], payload.Ax25Info[49]));
							parsedData.Add("Primary Power Supply Voltage (12V)", voltageConversion(payload.Ax25Info[50], payload.Ax25Info[51]));
							parsedData.Add("3.8V Bus Voltage", voltageConversion(payload.Ax25Info[52], payload.Ax25Info[53]));
							parsedData.Add("5.5V Bus Voltage", voltageConversion(payload.Ax25Info[54], payload.Ax25Info[55]));
							parsedData.Add("IHU 3.3V Voltage", voltageConversion(payload.Ax25Info[56], payload.Ax25Info[57]));

							parsedData.Add("Total Solar Array Current", currentConversion(payload.Ax25Info, 58));
							parsedData.Add("Primary Bus Current", currentConversion(payload.Ax25Info, 60));
							parsedData.Add("Total load Current", currentConversion(payload.Ax25Info, 62));
							parsedData.Add("IHU Current", currentConversion(payload.Ax25Info, 64));
							parsedData.Add("HF Receiver Current", currentConversion(payload.Ax25Info, 68));
							parsedData.Add("UHF Transmitter 2 Current", currentConversion(payload.Ax25Info, 72));
							parsedData.Add("UHF Transmitter 1 Current", currentConversion(payload.Ax25Info, 76));
							parsedData.Add("UHF1 RF Power", currentConversion(payload.Ax25Info, 78));
							parsedData.Add("UHF2 RF Power", currentConversion(payload.Ax25Info, 80));
							parsedData.Add("VHF Receiver Current", currentConversion(payload.Ax25Info, 82));
							parsedData.Add("VHF AGC Voltage", voltageConversion(payload.Ax25Info[84], payload.Ax25Info[85]));

							parsedData.Add("Delayed Telemetry Start Time", datetimeFormat(payload.Ax25Info, 86));
							parsedData.Add("Delayed Telemetry Interval Setting", timeFormat(payload.Ax25Info, 92));
							parsedData.Add("Camera Controller Operating Current", currentConversion(payload.Ax25Info, 98));
							parsedData.Add("Camera Operating Voltage", voltageConversion(payload.Ax25Info[100], payload.Ax25Info[101]));
							parsedData.Add("Total Camera Current", currentConversion(payload.Ax25Info, 102));
							parsedData.Add("Camera Working Status", binaryData(payload.Ax25Info[104]));

							parsedData.Add("Camera 1 Photo Counter", binaryData(payload.Ax25Info[105]));
							parsedData.Add("Camera 2 Photo Counter", binaryData(payload.Ax25Info[107]));
							parsedData.Add("Camera 3 Photo Counter", binaryData(payload.Ax25Info[109]));

							parsedData.Add("Camera 1 Delayed Photography Start Time", datetimeFormat(payload.Ax25Info, 111));
							parsedData.Add("Camera 1 Delayed Photography Interval Setting", timeFormat(payload.Ax25Info, 117));
							parsedData.Add("Camera 2 Delayed Photography Start Time", datetimeFormat(payload.Ax25Info, 121));
							parsedData.Add("Camera 2 Delayed Photography Interval Setting", timeFormat(payload.Ax25Info, 127));
							parsedData.Add("Camera 3 Delayed Photography Start Time", datetimeFormat(payload.Ax25Info, 131));
							parsedData.Add("Camera 3 Delayed Photography Interval Setting", timeFormat(payload.Ax25Info, 137));

							parsedData.Add("48 Hours Reset Time", datetimeFormat(payload.Ax25Info, 144));


							parsedData.Add("Current Operating Mode", payload.Ax25Info[141].ToString());
							parsedData.Add("Device Switch Status", binaryData(payload.Ax25Info[143]) + binaryData(payload.Ax25Info[142]));

							parsedData.Add("Camera 1 Resolution", cameraResolution(payload.Ax25Info[158]));
							parsedData.Add("Camera 2 Resolution", cameraResolution(payload.Ax25Info[160]));
							parsedData.Add("Camera 3 Resolution", cameraResolution(payload.Ax25Info[162]));

							parsedData.Add("Camera 1 Image Quality", cameraQuality(payload.Ax25Info[159]));
							parsedData.Add("Camera 2 Image Quality", cameraQuality(payload.Ax25Info[161]));
							parsedData.Add("Camera 3 Image Quality", cameraQuality(payload.Ax25Info[163]));
						}
					}
				}
			}
			catch ( Exception Ex )
			{ 


			}
			

			return parsedData;
		}
	}
}