using NetDimension.Winform.FormShadow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormShadowDemo
{
	public partial class Form1 : Form
	{
		const int WM_LBUTTONDOWN = 0x0201;
		const int WM_NCLBUTTONDOWN = 0x0201;
		const int HTCAPTION = 0x02;
		protected readonly FormShadowDecorator ShadowDecorator;
		public Form1()
		{
			InitializeComponent();
			ShadowDecorator = new FormShadowDecorator(this);
			//启用窗口大小调整
			ShadowDecorator.EnableResize(true);
		}

		protected override void WndProc(ref Message m)
		{
			//鼠标左键按下的消息 
			if (m.Msg == WM_LBUTTONDOWN)
			{
				m.Msg = 0x00A1; //更改消息为非客户区按下鼠标
				m.LParam = IntPtr.Zero;
				m.WParam = new IntPtr(HTCAPTION); //鼠标放在标题栏内
			}

			base.WndProc(ref m);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var f = new Form1();

			f.Show();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("https://github.com/NetDimension/");

		}
	}
}
