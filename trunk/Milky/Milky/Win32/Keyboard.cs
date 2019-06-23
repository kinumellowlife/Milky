using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Milky.Win32
{
	public class Keyboard
	{
		///<summary>Usefull virtual keys to use</summary>
		public enum VirtualKeys
		{

			/// <summary>Left Shift formatKey</summary>
			LSHIFT = 0xA0,

			/// <summary>Right Shift Key</summary>
			RSHIFT = 0xA1,

			/// <summary>Left Control Key</summary>
			LCONTROL = 0xA2,

			/// <summary>Right Control Key</summary>
			RCONTROL = 0xA3,

			/// <summary>Left Menu Key</summary>
			LMENU = 0xA4,

			/// <summary>Right Menu Key</summary>
			RMENU = 0xA5,

			/// <summary>F1</summary>
			F1 = 0x70,

			/// <summary>F2</summary>
			F2 = 0x71,

			/// <summary>F3</summary>
			F3 = 0x72,

			/// <summary>F4</summary>
			F4 = 0x73,

			/// <summary>F5</summary>
			F5 = 0x74,

			/// <summary>F6</summary>
			F6 = 0x75,

			/// <summary>F7</summary>
			F7 = 0x76,

			/// <summary>F8</summary>
			F8 = 0x77,

			/// <summary>F9</summary>
			F9 = 0x78,

			/// <summary>F10</summary>
			F10 = 0x79,

			/// <summary>F11</summary>
			F11 = 0x7A,

			/// <summary>F12</summary>
			F12 = 0x7B,
		}

		///<summary>
		///Get the state of a particular formatKey (pressed etc..)
		///</summary>
		///<param text="keyCode" type="int">Keycode to check</param>
		///<returns>short</returns>
		///<remarks>
		/// When the Hi-order bit of the return is set then the formatKey is pressed
		/// When the Lo-order bit of the return is set,
		/// it means the formatKey is toggled (CAPSLOCK in on)
		/// </remarks>
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true,
					CallingConvention = CallingConvention.Winapi, SetLastError = false)]
		public static extern Int16 GetKeyState(int keyCode);

		///<summary>
		/// Return true if either shift formatKey is pressed
		///</summary>
		public static bool ShiftPressed {
			get
			{
				ushort sStateL = (ushort)GetKeyState((int)VirtualKeys.LSHIFT);
				ushort sStateR = (ushort)GetKeyState((int)VirtualKeys.RSHIFT);
				return ((0 != (sStateL & 0x8000)) || (0 != (sStateR & 0x8000)));
			}
		}

		///<summary>
		/// See if a Controls formatKey is pressed
		///</summary>
		public static bool CtrlPressed {
			get
			{
				ushort sStateL = (ushort)GetKeyState((int)VirtualKeys.LCONTROL);
				ushort sStateR = (ushort)GetKeyState((int)VirtualKeys.RCONTROL);
				return ((0 != (sStateL & 0x8000)) || (0 != (sStateR & 0x8000)));
			}
		}

		private Keyboard()
		{
		}
	}
}
