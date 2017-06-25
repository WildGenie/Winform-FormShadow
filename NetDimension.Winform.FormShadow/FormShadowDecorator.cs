using NetDimension.Winform.FormShadow.Extensions;
using NetDimension.Winform.FormShadow.Imports;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace NetDimension.Winform.FormShadow
{
	/// <summary>
	/// 窗口投影装饰器
	/// </summary>
	public class FormShadowDecorator : NativeWindow, IDisposable
	{
		private IntPtr _parentWindowHwnd;
		private Form _window;

		private FormShadowElement _topFormShadow;
		private FormShadowElement _leftFormShadow;
		private FormShadowElement _bottomFormShadow;
		private FormShadowElement _rightFormShadow;

		private WINDOWPOS _lastLocation;

		private readonly List<FormShadowElement> _shadows = new List<FormShadowElement>();
		private Color _activeColor = Color.DimGray;
		private Color _inactiveColor = Color.LightGray;
		private Color _borderColor;
		private bool _isEnabled;
		private bool _setTopMost;
		private bool _isFocused = false;

		/// <summary>
		/// 设置或获取投影窗口是否至于顶层。
		/// </summary>
		public bool TopMost
		{
			get
			{
				return _setTopMost;
			}
			set
			{
				_setTopMost = value;
				AlignSideShadowToTopMost();
			}
		}

		/// <summary>
		/// 设置或获取主窗体激活时投影的颜色。
		/// </summary>
		public Color ActiveColor
		{
			get
			{
				return _activeColor;
			}

			set
			{
				_activeColor = value;
				foreach (FormShadowElement sideShadow in _shadows)
				{
					sideShadow.ActiveColor = _activeColor;
				}
			}
		}

		/// <summary>
		/// 设置或获取主窗体失去焦点时投影的颜色。
		/// </summary>
		public Color InactiveColor
		{
			get
			{
				return _inactiveColor;
			}

			set
			{
				_inactiveColor = value;
				foreach (FormShadowElement sideShadow in _shadows)
				{
					sideShadow.InactiveColor = _inactiveColor;
				}
			}
		}

		/// <summary>
		/// 设置或获取主窗体边框颜色颜色。
		/// 
		///		摘要：暂未实现
		/// 
		/// </summary>
		public Color BorderColor
		{
			get
			{
				return _borderColor;
			}

			set
			{
				_borderColor = value;
				foreach (FormShadowElement sideShadow in _shadows)
				{
					sideShadow.BorderColor = _inactiveColor;
				}
			}
		}

		/// <summary>
		/// 设置或获取投影是否可用
		/// </summary>
		public bool IsEnabled
		{
			get { return _isEnabled; }
		}


		public FormShadowDecorator(Form window, bool enable = true)
		{
			_window = window;
			_parentWindowHwnd = window.Handle;

			_topFormShadow = new FormShadowElement(FormShadowDockPositon.Top, _parentWindowHwnd, this);
			_leftFormShadow = new FormShadowElement(FormShadowDockPositon.Left, _parentWindowHwnd, this);
			_bottomFormShadow = new FormShadowElement(FormShadowDockPositon.Bottom, _parentWindowHwnd, this);
			_rightFormShadow = new FormShadowElement(FormShadowDockPositon.Right, _parentWindowHwnd, this);

			_shadows.Add(_topFormShadow);
			_shadows.Add(_leftFormShadow);
			_shadows.Add(_bottomFormShadow);
			_shadows.Add(_rightFormShadow);

			AssignHandle(_parentWindowHwnd);


			User32.ShowWindow(_topFormShadow.Handle, ShowWindowStyles.SW_SHOWNOACTIVATE);
			User32.ShowWindow(_leftFormShadow.Handle, ShowWindowStyles.SW_SHOWNOACTIVATE);
			User32.ShowWindow(_bottomFormShadow.Handle, ShowWindowStyles.SW_SHOWNOACTIVATE);
			User32.ShowWindow(_rightFormShadow.Handle, ShowWindowStyles.SW_SHOWNOACTIVATE);

			_isEnabled = false;
			AlignSideShadowToTopMost();
			Enable(true);



		}

		/// <summary>
		/// 启用或禁用窗体投影效果。
		/// </summary>
		/// <param name="enable">True/False</param>
		public void Enable(bool enable)
		{
			if (_isEnabled && !enable)
			{
				Show(false);
				UnregisterEvents();
			}
			else if (!_isEnabled && enable)
			{
				RegisterEvents();
				if (_window != null)
				{
					UpdateLocations(new WINDOWPOS
					{
						x = (int)_window.Left,
						y = (int)_window.Top,
						cx = (int)_window.Width,
						cy = (int)_window.Height,
						flags = (int)SetWindowPosFlags.SWP_SHOWWINDOW
					});

					UpdateSizes((int)_window.Width, (int)_window.Height);
				}
			}

			_isEnabled = enable;
		}

		/// <summary>
		/// 启用或禁用窗口大小调整。
		/// </summary>
		/// <param name="enable">True/False</param>
		public void EnableResize(bool enable)
		{
			foreach (FormShadowElement sideShadow in _shadows)
			{
				sideShadow.ExternalResizeEnable = enable;
			}
		}

		protected override void WndProc(ref Message m)
		{
			if (!_isEnabled || IsDisposed)
			{
				base.WndProc(ref m);
				return;
			}
			var msg = m.Msg;
			var lParam = m.LParam;
			var wParam = m.WParam;

			switch (msg)
			{
				case (int)WindowsMessages.WM_WINDOWPOSCHANGED:
					_lastLocation = (WINDOWPOS)Marshal.PtrToStructure(lParam, typeof(WINDOWPOS));
					WindowPosChanged(_lastLocation);
					break;
				case (int)WindowsMessages.WM_ACTIVATE:
					{
						var className = new StringBuilder(256);

						if (m.LParam != IntPtr.Zero && User32.GetClassName(m.LParam, className, className.Capacity) != 0)
						{
							var hWnd = m.LParam;

							var name = className.ToString();
							//prevent redraw from border active or lose focus.
							if (name.StartsWith(CONSTS.CLASS_NAME) && _isFocused && _shadows.Exists(p => p.Handle == hWnd))
							{
								return;
							}
						}


						if (wParam.ToInt32() == 0)
						{
							_isFocused = false;
							KillFocus();
						}
						else
						{
							_isFocused = true;
							SetFocus();
						}

					}
					break;
				case (int)WindowsMessages.WM_SIZE:
					Size(wParam, lParam);
					break;
			}

			base.WndProc(ref m);
		}


		private void DestroyShadows()
		{
			_parentWindowHwnd = IntPtr.Zero;

			CloseShadows();

			_window = null;
		}


		private void RegisterEvents()
		{
			foreach (FormShadowElement sideShadow in _shadows)
			{
				sideShadow.MouseDown += HandleSideMouseDown;
			}

			if (_window != null)
			{

				_window.VisibleChanged += HandleWindowVisibleChanged;
			}
		}

		private void HandleWindowVisibleChanged(object sender, EventArgs e)
		{
			Show(_window.Visible);
		}

		private void UnregisterEvents()
		{
			foreach (FormShadowElement sideShadow in _shadows)
			{
				sideShadow.MouseDown -= HandleSideMouseDown;
			}

			if (_window != null)
			{
				_window.VisibleChanged -= HandleWindowVisibleChanged;
			}
		}

		private void HandleSideMouseDown(object sender, FormShadowResizeArgs e)
		{
			if (e.Mode == HitTest.HTNOWHERE || e.Mode == HitTest.HTCAPTION)
			{
				return;
			}

			User32.SendMessage(_parentWindowHwnd, (uint)WindowsMessages.WM_SYSCOMMAND, (IntPtr)e.Mode.ToInt(), IntPtr.Zero);
		}

		private void CloseShadows()
		{
			foreach (var sideShadow in _shadows)
			{
				sideShadow.Close();
			}

			_shadows.Clear();

			_topFormShadow = null;
			_bottomFormShadow = null;
			_leftFormShadow = null;
			_rightFormShadow = null;
		}

		private void Show(bool show)
		{
			foreach (FormShadowElement sideShadow in _shadows)
			{
				sideShadow.Show(show);
			}
		}

		private void UpdateZOrder()
		{
			foreach (FormShadowElement sideShadow in _shadows)
			{
				sideShadow.UpdateZOrder();
			}
		}

		private void UpdateFocus(bool isFocused)
		{


			foreach (FormShadowElement sideShadow in _shadows)
			{
				sideShadow.ParentWindowIsFocused = isFocused;
			}
		}

		private void UpdateSizes(int width, int height)
		{
			foreach (FormShadowElement sideShadow in _shadows)
			{
				sideShadow.SetSize(width, height);
			}
		}

		private void UpdateLocations(WINDOWPOS location)
		{
			foreach (FormShadowElement sideShadow in _shadows)
			{
				sideShadow.SetLocation(location);
			}

			if ((location.flags & (int)SetWindowPosFlags.SWP_HIDEWINDOW) != 0)
			{
				Show(false);
			}
			else if ((location.flags & (int)SetWindowPosFlags.SWP_SHOWWINDOW) != 0)
			{
				Show(true);
				UpdateZOrder();
			}
		}

		private void AlignSideShadowToTopMost()
		{
			if (_shadows == null)
			{
				return;
			}

			foreach (FormShadowElement sideShadow in _shadows)
			{
				sideShadow.IsTopMost = _setTopMost;
				sideShadow.UpdateZOrder();
			}
		}


		public void SetFocus()
		{
			if (!_isEnabled) return;
			UpdateFocus(true);
			UpdateZOrder();
		}

		public void KillFocus()
		{
			if (!_isEnabled) return;

			UpdateFocus(false);
			UpdateZOrder();
		}

		private void WindowPosChanged(WINDOWPOS location)
		{
			if (!_isEnabled) return;
			UpdateLocations(location);
		}

		public void Activate(bool isActive)
		{
			if (!_isEnabled) return;
			UpdateZOrder();
		}

		private void Size(IntPtr wParam, IntPtr lParam)
		{
			if (!_isEnabled) return;
			if ((int)wParam == 2 || (int)wParam == 1) // maximized/minimized
			{
				Show(false);
			}
			else
			{
				Show(true);
				int width = (int)User32.LoWord(lParam);
				int height = (int)User32.HiWord(lParam);
				UpdateSizes(width, height);
			}
		}



		#region Dispose

		private bool _isDisposed;

		/// <summary>
		/// IsDisposed status
		/// </summary>
		public bool IsDisposed
		{
			get { return _isDisposed; }
		}

		/// <summary>
		/// Standard Dispose
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="disposing">True if disposing, false otherwise</param>
		protected virtual void Dispose(bool disposing)
		{
			if (!_isDisposed)
			{
				if (disposing)
				{
					// release unmanaged resources
				}

				_isDisposed = true;

				DestroyShadows();

				UnregisterEvents();

				_window = null;
			}
		}

		#endregion

	}

}
