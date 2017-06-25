using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace NetDimension.Winform.FormShadow.Imports
{
	//你不需要知道这里面发生了什么。
	//YOU DO NOT NEED HAVE TO KNOW WHAT IS HAPPEND HERE.
	internal delegate IntPtr WndProcHandler(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
	[Flags]
	public enum SetWindowPosFlags
	{
		SWP_NOSIZE = 0x0001,
		SWP_NOMOVE = 0x0002,
		SWP_NOZORDER = 0x0004,
		SWP_NOREDRAW = 0x0008,
		SWP_NOACTIVATE = 0x0010,
		SWP_FRAMECHANGED = 0x0020,
		SWP_SHOWWINDOW = 0x0040,
		SWP_HIDEWINDOW = 0x0080,
		SWP_NOCOPYBITS = 0x0100,
		SWP_NOOWNERZORDER = 0x0200,
		SWP_NOSENDCHANGING = 0x0400,
		SWP_DRAWFRAME = 0x0020,
		SWP_NOREPOSITION = 0x0200,
		SWP_DEFERERASE = 0x2000,
		SWP_ASYNCWINDOWPOS = 0x4000
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct BLENDFUNCTION
	{
		/// <summary>
		/// BlendOp field of structure
		/// </summary>
		public byte BlendOp;

		/// <summary>
		/// BlendFlags field of structure
		/// </summary>
		public byte BlendFlags;

		/// <summary>
		/// SourceConstantAlpha field of structure
		/// </summary>
		public byte SourceConstantAlpha;

		/// <summary>
		/// AlphaFormat field of structure
		/// </summary>
		public byte AlphaFormat;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct POINT
	{
		/// <summary>
		/// x field of structure
		/// </summary>
		public int x;

		/// <summary>
		/// y field of structure
		/// </summary>
		public int y;

		#region Constructors
		public POINT(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public POINT(Point point)
		{
			x = point.X;
			y = point.Y;
		}
		#endregion
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct WINDOWPOS
	{
		public IntPtr hwnd;
		public IntPtr hwndAfter;
		public int x;
		public int y;
		public int cx;
		public int cy;
		public uint flags;

		#region Overrides
		public override string ToString()
		{
			return x + ":" + y + ":" + cx + ":" + cy + ":" + ((SetWindowPosFlags)flags);
		}
		#endregion
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct WNDCLASS
	{
		public uint style;
		public IntPtr lpfnWndProc;
		public int cbClsExtra;
		public int cbWndExtra;
		public IntPtr hInstance;
		public IntPtr hIcon;
		public IntPtr hCursor;
		public IntPtr hbrBackground;
		[MarshalAs(UnmanagedType.LPWStr)]
		public string lpszMenuName;
		[MarshalAs(UnmanagedType.LPWStr)]
		public string lpszClassName;
	}

	[Flags]
	public enum WindowExStyles
	{
		/// <summary>
		/// Specified WS_EX_DLGMODALFRAME enumeration value.
		/// </summary>
		WS_EX_DLGMODALFRAME = 0x00000001,

		/// <summary>
		/// Specified WS_EX_NOPARENTNOTIFY enumeration value.
		/// </summary>
		WS_EX_NOPARENTNOTIFY = 0x00000004,
		/// <summary>
		/// Specified WS_EX_NOACTIVATE enumeration value.
		/// </summary>
		WS_EX_NOACTIVATE = 0x08000000,
		/// <summary>
		/// Specified WS_EX_TOPMOST enumeration value.
		/// </summary>
		WS_EX_TOPMOST = 0x00000008,

		/// <summary>
		/// Specified WS_EX_ACCEPTFILES enumeration value.
		/// </summary>
		WS_EX_ACCEPTFILES = 0x00000010,

		/// <summary>
		/// Specified WS_EX_TRANSPARENT enumeration value.
		/// </summary>
		WS_EX_TRANSPARENT = 0x00000020,

		/// <summary>
		/// Specified WS_EX_MDICHILD enumeration value.
		/// </summary>
		WS_EX_MDICHILD = 0x00000040,

		/// <summary>
		/// Specified WS_EX_TOOLWINDOW enumeration value.
		/// </summary>
		WS_EX_TOOLWINDOW = 0x00000080,

		/// <summary>
		/// Specified WS_EX_WINDOWEDGE enumeration value.
		/// </summary>
		WS_EX_WINDOWEDGE = 0x00000100,

		/// <summary>
		/// Specified WS_EX_CLIENTEDGE enumeration value.
		/// </summary>
		WS_EX_CLIENTEDGE = 0x00000200,

		/// <summary>
		/// Specified WS_EX_CONTEXTHELP enumeration value.
		/// </summary>
		WS_EX_CONTEXTHELP = 0x00000400,

		/// <summary>
		/// Specified WS_EX_RIGHT enumeration value.
		/// </summary>
		WS_EX_RIGHT = 0x00001000,

		/// <summary>
		/// Specified WS_EX_LEFT enumeration value.
		/// </summary>
		WS_EX_LEFT = 0x00000000,

		/// <summary>
		/// Specified WS_EX_RTLREADING enumeration value.
		/// </summary>
		WS_EX_RTLREADING = 0x00002000,

		/// <summary>
		/// Specified WS_EX_LTRREADING enumeration value.
		/// </summary>
		WS_EX_LTRREADING = 0x00000000,

		/// <summary>
		/// Specified WS_EX_LEFTSCROLLBAR enumeration value.
		/// </summary>
		WS_EX_LEFTSCROLLBAR = 0x00004000,

		/// <summary>
		/// Specified WS_EX_RIGHTSCROLLBAR enumeration value.
		/// </summary>
		WS_EX_RIGHTSCROLLBAR = 0x00000000,

		/// <summary>
		/// Specified WS_EX_CONTROLPARENT enumeration value.
		/// </summary>
		WS_EX_CONTROLPARENT = 0x00010000,

		/// <summary>
		/// Specified WS_EX_STATICEDGE enumeration value.
		/// </summary>
		WS_EX_STATICEDGE = 0x00020000,

		/// <summary>
		/// Specified WS_EX_APPWINDOW enumeration value.
		/// </summary>
		WS_EX_APPWINDOW = 0x00040000,

		/// <summary>
		/// Specified WS_EX_OVERLAPPEDWINDOW enumeration value.
		/// </summary>
		WS_EX_OVERLAPPEDWINDOW = 0x00000300,

		/// <summary>
		/// Specified WS_EX_PALETTEWINDOW enumeration value.
		/// </summary>
		WS_EX_PALETTEWINDOW = 0x00000188,

		/// <summary>
		/// Specified WS_EX_LAYERED enumeration value.
		/// </summary>
		WS_EX_LAYERED = 0x00080000
	}

	[Flags]
	public enum WindowStyles : uint
	{
		WS_OVERLAPPED = 0x00000000,
		WS_POPUP = 0x80000000,
		WS_CHILD = 0x40000000,
		WS_MINIMIZE = 0x20000000,
		WS_VISIBLE = 0x10000000,
		WS_DISABLED = 0x08000000,
		WS_CLIPSIBLINGS = 0x04000000,
		WS_CLIPCHILDREN = 0x02000000,
		WS_MAXIMIZE = 0x01000000,
		WS_CAPTION = 0x00C00000,
		WS_BORDER = 0x00800000,
		WS_DLGFRAME = 0x00400000,
		WS_VSCROLL = 0x00200000,
		WS_HSCROLL = 0x00100000,
		WS_SYSMENU = 0x00080000,
		WS_THICKFRAME = 0x00040000,
		WS_GROUP = 0x00020000,
		WS_TABSTOP = 0x00010000,
		WS_MINIMIZEBOX = 0x00020000,

		WS_MAXIMIZEBOX = 0x00010000,
		WS_TILED = 0x00000000,
		WS_ICONIC = 0x20000000,
		WS_SIZEBOX = 0x00040000,
		WS_POPUPWINDOW = 0x80880000,
		WS_OVERLAPPEDWINDOW = 0x00CF0000,
		WS_TILEDWINDOW = 0x00CF0000,
		WS_CHILDWINDOW = 0x40000000
	}

	public enum GetWindowLongFlags
	{
		/// <summary>
		/// Specified GWL_WNDPROC enumeration value.
		/// </summary>
		GWL_WNDPROC = -4,

		/// <summary>
		/// Specified GWL_HINSTANCE enumeration value.
		/// </summary>
		GWL_HINSTANCE = -6,

		/// <summary>
		/// Specified GWL_HWNDPARENT enumeration value.
		/// </summary>
		GWL_HWNDPARENT = -8,

		/// <summary>
		/// Specified GWL_STYLE enumeration value.
		/// </summary>
		GWL_STYLE = -16,

		/// <summary>
		/// Specified GWL_EXSTYLE enumeration value.
		/// </summary>
		GWL_EXSTYLE = -20,

		/// <summary>
		/// Specified GWL_USERDATA enumeration value.
		/// </summary>
		GWL_USERDATA = -21,

		/// <summary>
		/// Specified GWL_ID enumeration value.
		/// </summary>
		GWL_ID = -12
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct RECT
	{
		/// <summary>
		/// left field of structure
		/// </summary>
		public int left;

		/// <summary>
		/// top field of structure
		/// </summary>
		public int top;

		/// <summary>
		/// right field of structure
		/// </summary>
		public int right;

		/// <summary>
		/// bottom field of structure
		/// </summary>
		public int bottom;

		public RECT(int left, int top, int width, int height)
		{
			this.left = left;
			this.top = top;
			right = this.left + width;
			bottom = this.top + height;
		}

		#region Properties

		public POINT Location
		{
			get
			{
				return new POINT(left, top);
			}
			set
			{
				right -= (left - value.x);
				bottom -= (bottom - value.y);
				left = value.x;
				top = value.y;
			}
		}

		public uint Width
		{
			get { return (uint)Math.Abs(right - left); }
			set { right = left + (int)value; }
		}

		public uint Height
		{
			get { return (uint)Math.Abs(bottom - top); }
			set { bottom = top + (int)value; }
		}
		#endregion

		#region Overrides

		public override string ToString()
		{
			return left + ":" + top + ":" + right + ":" + bottom;
		}

		#endregion

		public static explicit operator Rectangle(RECT rect)
		{
			return new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct SIZE
	{
		/// <summary>
		/// cx field of structure
		/// </summary>
		public int cx;

		/// <summary>
		/// cy field of structure
		/// </summary>
		public int cy;

		public SIZE(Int32 cx, Int32 cy)
		{
			this.cx = cx;
			this.cy = cy;
		}
	}

	public enum HitTest
	{
		HTERROR = (-2),
		HTTRANSPARENT = (-1),
		HTNOWHERE = 0,
		HTCLIENT = 1,
		HTCAPTION = 2,
		HTSYSMENU = 3,
		HTGROWBOX = 4,
		HTSIZE = HTGROWBOX,
		HTMENU = 5,
		HTHSCROLL = 6,
		HTVSCROLL = 7,
		HTMINBUTTON = 8,
		HTMAXBUTTON = 9,
		HTLEFT = 10,
		HTRIGHT = 11,
		HTTOP = 12,
		HTTOPLEFT = 13,
		HTTOPRIGHT = 14,
		HTBOTTOM = 15,
		HTBOTTOMLEFT = 16,
		HTBOTTOMRIGHT = 17,
		HTBORDER = 18,
		HTREDUCE = HTMINBUTTON,
		HTZOOM = HTMAXBUTTON,
		HTSIZEFIRST = HTLEFT,
		HTSIZELAST = HTBOTTOMRIGHT,
		HTOBJECT = 19,
		HTCLOSE = 20,
		HTHELP = 21
	}

	public enum IdcStandardCursors
	{
		IDC_ARROW = 32512,
		IDC_IBEAM = 32513,
		IDC_WAIT = 32514,
		IDC_CROSS = 32515,
		IDC_UPARROW = 32516,
		IDC_SIZE = 32640,
		IDC_ICON = 32641,
		IDC_SIZENWSE = 32642,
		IDC_SIZENESW = 32643,
		IDC_SIZEWE = 32644,
		IDC_SIZENS = 32645,
		IDC_SIZEALL = 32646,
		IDC_NO = 32648,
		IDC_HAND = 32649,
		IDC_APPSTARTING = 32650,
		IDC_HELP = 32651
	}

	public enum ShowWindowStyles : short
	{
		/// <summary>
		/// Specified SW_HIDE enumeration value.
		/// </summary>
		SW_HIDE = 0,

		/// <summary>
		/// Specified SW_SHOWNORMAL enumeration value.
		/// </summary>
		SW_SHOWNORMAL = 1,

		/// <summary>
		/// Specified SW_NORMAL enumeration value.
		/// </summary>
		SW_NORMAL = 1,

		/// <summary>
		/// Specified SW_SHOWMINIMIZED enumeration value.
		/// </summary>
		SW_SHOWMINIMIZED = 2,

		/// <summary>
		/// Specified SW_SHOWMAXIMIZED enumeration value.
		/// </summary>
		SW_SHOWMAXIMIZED = 3,

		/// <summary>
		/// Specified SW_MAXIMIZE enumeration value.
		/// </summary>
		SW_MAXIMIZE = 3,

		/// <summary>
		/// Specified SW_SHOWNOACTIVATE enumeration value.
		/// </summary>
		SW_SHOWNOACTIVATE = 4,

		/// <summary>
		/// Specified SW_SHOW enumeration value.
		/// </summary>
		SW_SHOW = 5,

		/// <summary>
		/// Specified SW_MINIMIZE enumeration value.
		/// </summary>
		SW_MINIMIZE = 6,

		/// <summary>
		/// Specified SW_SHOWMINNOACTIVE enumeration value.
		/// </summary>
		SW_SHOWMINNOACTIVE = 7,

		/// <summary>
		/// Specified SW_SHOWNA enumeration value.
		/// </summary>
		SW_SHOWNA = 8,

		/// <summary>
		/// Specified SW_RESTORE enumeration value.
		/// </summary>
		SW_RESTORE = 9,

		/// <summary>
		/// Specified SW_SHOWDEFAULT enumeration value.
		/// </summary>
		SW_SHOWDEFAULT = 10,

		/// <summary>
		/// Specified SW_FORCEMINIMIZE enumeration value.
		/// </summary>
		SW_FORCEMINIMIZE = 11,

		/// <summary>
		/// Specified SW_MAX enumeration value.
		/// </summary>
		SW_MAX = 11
	}

	public enum ResizeDirection
	{
		Left = 61441,
		Right = 61442,
		Top = 61443,
		TopLeft = 61444,
		TopRight = 61445,
		Bottom = 61446,
		BottomLeft = 61447,
		BottomRight = 61448,
	}

	[Flags]
	public enum WindowSizeEdges : uint
	{
		WMSZ_LEFT = 1,
		WMSZ_RIGHT = 2,
		WMSZ_TOP = 3,
		WMSZ_TOPLEFT = 4,
		WMSZ_TOPRIGHT = 5,
		WMSZ_BOTTOM = 6,
		WMSZ_BOTTOMLEFT = 7,
		WMSZ_BOTTOMRIGHT = 8
	}
}
