namespace MilkyTestApp
{
	partial class FMain
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

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.CheckBoxTestButton = new System.Windows.Forms.Button();
			this.BindTest = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// CheckBoxTestButton
			// 
			this.CheckBoxTestButton.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.CheckBoxTestButton.Location = new System.Drawing.Point(533, 197);
			this.CheckBoxTestButton.Name = "CheckBoxTestButton";
			this.CheckBoxTestButton.Size = new System.Drawing.Size(129, 35);
			this.CheckBoxTestButton.TabIndex = 0;
			this.CheckBoxTestButton.Text = "CheckBox";
			this.CheckBoxTestButton.UseVisualStyleBackColor = true;
			this.CheckBoxTestButton.Click += new System.EventHandler(this.CheckBoxTestButton_Click);
			// 
			// BindTest
			// 
			this.BindTest.Location = new System.Drawing.Point(382, 197);
			this.BindTest.Name = "BindTest";
			this.BindTest.Size = new System.Drawing.Size(119, 35);
			this.BindTest.TabIndex = 1;
			this.BindTest.Text = "Bind";
			this.BindTest.UseVisualStyleBackColor = true;
			this.BindTest.Click += new System.EventHandler(this.BindTest_Click);
			// 
			// FMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.BindTest);
			this.Controls.Add(this.CheckBoxTestButton);
			this.Name = "FMain";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button CheckBoxTestButton;
		private System.Windows.Forms.Button BindTest;
	}
}

