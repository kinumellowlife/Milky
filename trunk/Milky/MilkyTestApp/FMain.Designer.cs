﻿namespace MilkyTestApp
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
			this.MilkyControl = new System.Windows.Forms.Button();
			this.ImageTest = new System.Windows.Forms.Button();
			this.ActionCenterButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// CheckBoxTestButton
			// 
			this.CheckBoxTestButton.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.CheckBoxTestButton.Location = new System.Drawing.Point(137, 12);
			this.CheckBoxTestButton.Name = "CheckBoxTestButton";
			this.CheckBoxTestButton.Size = new System.Drawing.Size(129, 35);
			this.CheckBoxTestButton.TabIndex = 0;
			this.CheckBoxTestButton.Text = "CheckBox";
			this.CheckBoxTestButton.UseVisualStyleBackColor = true;
			this.CheckBoxTestButton.Click += new System.EventHandler(this.CheckBoxTestButton_Click);
			// 
			// BindTest
			// 
			this.BindTest.Location = new System.Drawing.Point(12, 12);
			this.BindTest.Name = "BindTest";
			this.BindTest.Size = new System.Drawing.Size(119, 35);
			this.BindTest.TabIndex = 1;
			this.BindTest.Text = "Bind";
			this.BindTest.UseVisualStyleBackColor = true;
			this.BindTest.Click += new System.EventHandler(this.BindTest_Click);
			// 
			// MilkyControl
			// 
			this.MilkyControl.Location = new System.Drawing.Point(12, 53);
			this.MilkyControl.Name = "MilkyControl";
			this.MilkyControl.Size = new System.Drawing.Size(119, 35);
			this.MilkyControl.TabIndex = 1;
			this.MilkyControl.Text = "MilkyControl";
			this.MilkyControl.UseVisualStyleBackColor = true;
			this.MilkyControl.Click += new System.EventHandler(this.MilkyControl_Click);
			// 
			// ImageTest
			// 
			this.ImageTest.Location = new System.Drawing.Point(137, 53);
			this.ImageTest.Name = "ImageTest";
			this.ImageTest.Size = new System.Drawing.Size(129, 35);
			this.ImageTest.TabIndex = 1;
			this.ImageTest.Text = "Image";
			this.ImageTest.UseVisualStyleBackColor = true;
			this.ImageTest.Click += new System.EventHandler(this.ImageTest_Click);
			// 
			// ActionCenterButton
			// 
			this.ActionCenterButton.Location = new System.Drawing.Point(12, 94);
			this.ActionCenterButton.Name = "ActionCenterButton";
			this.ActionCenterButton.Size = new System.Drawing.Size(129, 35);
			this.ActionCenterButton.TabIndex = 1;
			this.ActionCenterButton.Text = "ActionCenter(Win10)";
			this.ActionCenterButton.UseVisualStyleBackColor = true;
			// 
			// FMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.ActionCenterButton);
			this.Controls.Add(this.ImageTest);
			this.Controls.Add(this.MilkyControl);
			this.Controls.Add(this.BindTest);
			this.Controls.Add(this.CheckBoxTestButton);
			this.Name = "FMain";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button CheckBoxTestButton;
		private System.Windows.Forms.Button BindTest;
		private System.Windows.Forms.Button MilkyControl;
		private System.Windows.Forms.Button ImageTest;
		private System.Windows.Forms.Button ActionCenterButton;
	}
}

