using System.Runtime.InteropServices;

namespace GamepadListener
{
	public class GuideButton
	{
		[DllImport("xinput1_3.dll", EntryPoint = "#100")]
		static extern int secret_get_gamepad(int playerIndex, out XINPUT_GAMEPAD_SECRET struc);

		struct XINPUT_GAMEPAD_SECRET
		{
			public uint eventCount;
			public ushort wButtons;
			public byte bLeftTrigger;
			public byte bRightTrigger;
			public short sThumbLX;
			public short sThumbLY;
			public short sThumbRX;
			public short sThumbRY;
		}

		static XINPUT_GAMEPAD_SECRET xgs;

		public static bool WasGuideButtonPressed()
		{
			int stat;
			bool val;

			for (int i = 0; i < 4; i++)
			{
				stat = secret_get_gamepad(0, out xgs);
				if (stat != 0) continue;

				val = ((xgs.wButtons & 0x0400) != 0);

				if (val) return true;
			}

			return false;
		}
	}
}
