using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Milky.Windows.Forms
{
	/// <summary>
	/// マウスでドラッグ可能なプリントプレビュー
	/// </summary>
	public class MilkyPrintPreview : PrintPreviewControl
	{

		#region fields

		private Point oldPoint;
		private Point currentPosition;
		private Type type;
		private MethodInfo minfo;
		private FieldInfo finfo;

		#endregion fields

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public MilkyPrintPreview()
		{
			this.AutoZoom = false;

			this.type = typeof(PrintPreviewControl);
			this.finfo = type.GetField("position", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.ExactBinding);
			this.minfo = type.GetMethod("SetPositionNoInvalidate", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.ExactBinding);

			this.MouseMove += DragablePrintPreview_MouseMove;
			this.MouseDown += DragablePrintPreview_MouseDown;
			this.MouseUp += DragablePrintPreview_MouseUp;
			this.MouseWheel += DragablePrintPreview_MouseWheel;
		}

		private void DragablePrintPreview_MouseUp(object sender, MouseEventArgs e)
		{
			this.Cursor = Cursors.Default;
		}

		private void DragablePrintPreview_MouseWheel(object sender, MouseEventArgs e)
		{
			if (ModifierKeys == Keys.Control)
			{
				if (e.Delta < 0)
				{
					this.Zoom *= 0.9;
				}
				else
				{
					this.Zoom *= 1.1;
				}
			}
		}

		public void DoWheel(MouseEventArgs e)
		{
			if (ModifierKeys == Keys.Control)
			{
				if (e.Delta < 0)
				{
					this.Zoom *= 0.9;
				}
				else
				{
					this.Zoom *= 1.1;
				}
			}
		}

		private void DragablePrintPreview_MouseDown(object sender, MouseEventArgs e)
		{
			this.Select();
			this.Cursor = Cursors.NoMove2D;
		}

		private void DragablePrintPreview_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				Point movePoint = new Point(
					this.currentPosition.X + (this.oldPoint.X - e.X),
					this.currentPosition.Y + (this.oldPoint.Y - e.Y)
				);
				this.minfo.Invoke(this, new object[] { movePoint });
			}
			else
			{
				this.oldPoint = new Point(e.X, e.Y);
				this.currentPosition = (Point)this.finfo.GetValue(this);
			}
		}
	}
}
