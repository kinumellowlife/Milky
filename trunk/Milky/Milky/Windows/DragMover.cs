using System.Drawing;
using System.Windows.Forms;

namespace Milky.Windows
{
	/// <summary>
	/// ウィンドウをドラッグするだけで動かせるようにするクラス
	/// </summary>
	public class DragMover
	{
		#region fields

		private bool enabled = true;
		private Control target;
		private Control listner;

		/// <summary>マウスクリック位置</summary>
		private Point mousePoint;

		private bool stop = false;

		#endregion fields

		#region property

		/// <summary>
		/// 機能の有効／無効設定
		/// </summary>
		public bool Enabled {
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		/// <summary>
		/// ドラッグ対象
		/// </summary>
		public Control Target { get { return this.target; } }

		/// <summary>
		/// リスナー
		/// </summary>
		public Control Listener { get { return this.listner; } }

		/// <summary>
		/// タグ
		/// </summary>
		public object Tag { get; set; }

		#endregion property

		#region delegate

		/// <summary>
		/// ドラッグイベント定義
		/// </summary>
		/// <param name="mover">ムーバー本体</param>
		/// <param name="c">対象コントロール</param>
		/// <param name="dx">移動量X</param>
		/// <param name="dy">移動量Y</param>
		public delegate void DragMoveDelegate(DragMover mover, Control c, int dx, int dy);

		/// <summary>
		/// ドラッグ位置計算イベント定義
		/// </summary>
		/// <param name="mover">ムーバー本体</param>
		/// <param name="c">対象コントロール</param>
		/// <param name="x">X</param>
		/// <param name="y">Y</param>
		/// <param name="basePoint">基準位置</param>
		public delegate Point CalcDragPointDelegate(DragMover mover, Control c, int x, int y, Point basePoint);

		/// <summary>
		/// ドラッグイベントハンドラ
		/// </summary>
		public DragMoveDelegate OnDragMove;

		/// <summary>
		/// ドラッグ位置計算イベントハンドラ
		/// </summary>
		public CalcDragPointDelegate OnCalcDragPoint;

		#endregion delegate

		#region construct

		/// <summary>
		/// 構築
		/// </summary>
		/// <param name="listner">マウスの動きをキャプチャーする対象</param>
		/// <param name="target">動かすウィンドウ</param>
		public DragMover(Control listner, Control target)
		{
			this.target = target;
			this.listner = listner;
			AddListner(listner);
		}

		/// <summary>
		/// 構築
		/// </summary>
		/// <param name="listner">マウスの動きをキャプチャーする対象</param>
		/// <param name="target">動かすウィンドウ</param>
		/// <param name="dragMove">移動時のイベント</param>
		/// <param name="calc">計算イベント</param>
		public DragMover(Control listner, Control target, DragMoveDelegate dragMove, CalcDragPointDelegate calc = null)
			: this(listner, target)
		{
			this.OnDragMove += dragMove;
			this.OnCalcDragPoint += calc;
		}

		/// <summary>
		/// 構築
		/// </summary>
		/// <param name="target">動かすウィンドウ</param>
		public DragMover(Control target)
			: this(target, target)
		{
		}

		/// <summary>
		/// 構築
		/// </summary>
		/// <param name="target">動かすウィンドウ</param>
		/// <param name="dragMove">移動時のイベント</param>
		/// <param name="calc">計算イベント</param>
		public DragMover(Control target, DragMoveDelegate dragMove, CalcDragPointDelegate calc = null)
			: this(target, target, dragMove)
		{
		}

		/// <summary>
		/// 破棄
		/// </summary>
		~DragMover()
		{
			DetouchListner();
		}

		#endregion construct

		#region API

		/// <summary>
		/// キャプチャーする対象を追加する。DockモードがFillとかになってるとフォームだけじゃマウスクリックとれないので。
		/// </summary>
		/// <param name="listner"></param>
		public void AddListner(Control listner)
		{
			listner.MouseUp += new MouseEventHandler(parent_MouseUp);
			listner.MouseDown += new MouseEventHandler(parent_MouseDown);
			listner.MouseMove += new MouseEventHandler(parent_MouseMove);
		}

		/// <summary>
		/// リスナーからのでタッチ
		/// </summary>
		public void DetouchListner()
		{
			this.listner.MouseDown -= parent_MouseDown;
			this.listner.MouseMove -= parent_MouseMove;
		}

		/// <summary>
		/// ドラッグ停止
		/// </summary>
		public void Stop()
		{
			this.stop = true;
		}

		/// <summary>
		/// ドラッグ再開
		/// </summary>
		public void Resume()
		{
			this.stop = false;
		}

		#endregion API

		#region private methods

		/// <summary>
		///
		/// </summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント情報</param>
		private void parent_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.target == null)
				return;
			if (this.enabled == false)
				return;
			if (this.stop)
				return;

			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				if (OnCalcDragPoint != null)
				{
					Point pos = OnCalcDragPoint(this, (Control)target, e.X, e.Y, mousePoint);
					int dx = pos.X - this.target.Left;
					int dy = pos.Y - this.target.Top;
					this.target.Left = pos.X;
					this.target.Top = pos.Y;
					this.OnDragMove?.Invoke(this, target, dx, dy);
				}
				else
				{
					int dx = e.X - mousePoint.X;
					int dy = e.Y - mousePoint.Y;
					this.target.Left += dx;
					this.target.Top += dy;
					OnDragMove?.Invoke(this, target, dx, dy);
				}
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント情報</param>
		private void parent_MouseDown(object sender, MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				//位置を記憶する
				mousePoint = new Point(e.X, e.Y);
			}
		}

		private void parent_MouseUp(object sender, MouseEventArgs e)
		{
			int dx = this.target.Left - mousePoint.X;
			int dy = this.target.Top - mousePoint.Y;
			this.OnDragMove?.Invoke(this, target, dx, dy);
		}	

		#endregion private methods
	}
}