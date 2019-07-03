namespace MilkyTestApp.UI
{
	partial class ImageTestPanel
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region コンポーネント デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.ValueLabel = new Milky.Windows.Forms.MilkyLabel();
			this.ValueTracBar = new System.Windows.Forms.TrackBar();
			this.Pict = new Milky.Windows.Forms.MilkyPictureBox();
			((System.ComponentModel.ISupportInitialize)(this.ValueTracBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Pict)).BeginInit();
			this.SuspendLayout();
			//
			// ValueLabel
			//
			this.ValueLabel.AutoSize = true;
			this.ValueLabel.Location = new System.Drawing.Point(217, 209);
			this.ValueLabel.Name = "ValueLabel";
			this.ValueLabel.ParentContainer = this;
			this.ValueLabel.ShadowColor = System.Drawing.Color.Gray;
			this.ValueLabel.ShadowDir = Milky.Windows.Forms.MilkyLabel.ShadowDirection.TopRight;
			this.ValueLabel.ShadowSize = 0;
			this.ValueLabel.Size = new System.Drawing.Size(49, 12);
			this.ValueLabel.TabIndex = 4;
			this.ValueLabel.Text = "Contrast";
			this.ValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.ValueLabel.UseTransparent = false;
			//
			// ValueTracBar
			//
			this.ValueTracBar.AutoSize = false;
			this.ValueTracBar.Location = new System.Drawing.Point(6, 209);
			this.ValueTracBar.Maximum = 255;
			this.ValueTracBar.Name = "ValueTracBar";
			this.ValueTracBar.Size = new System.Drawing.Size(211, 25);
			this.ValueTracBar.TabIndex = 6;
			this.ValueTracBar.TickStyle = System.Windows.Forms.TickStyle.None;
			//
			// Pict
			//
			this.Pict.Image = null;
			this.Pict.Location = new System.Drawing.Point(3, 3);
			this.Pict.Name = "Pict";
			this.Pict.Size = new System.Drawing.Size(263, 200);
			this.Pict.TabIndex = 5;
			this.Pict.TabStop = false;
			//
			// ImageTestPanel
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ValueLabel);
			this.Controls.Add(this.Pict);
			this.Controls.Add(this.ValueTracBar);
			this.Name = "ImageTestPanel";
			this.Size = new System.Drawing.Size(295, 246);
			((System.ComponentModel.ISupportInitialize)(this.ValueTracBar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Pict)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion コンポーネント デザイナーで生成されたコード

		private Milky.Windows.Forms.MilkyLabel ValueLabel;
		private Milky.Windows.Forms.MilkyPictureBox Pict;
		private System.Windows.Forms.TrackBar ValueTracBar;
	}
}