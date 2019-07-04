namespace MilkyTestApp.UI
{
	partial class FMilkyControls
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.milkyButton1 = new Milky.Windows.Forms.MilkyButton();
			this.milkyTextBox1 = new Milky.Windows.Forms.MilkyTextBox();
			this.milkyLabel1 = new Milky.Windows.Forms.MilkyLabel();
			this.milkyPanel1 = new Milky.Windows.Forms.MilkyPanel();
			this.milkyLabel2 = new Milky.Windows.Forms.MilkyLabel();
			this.MilkyLog = new Milky.Windows.Forms.Jornal.MilkyLogListView();
			this.milkyButton2 = new Milky.Windows.Forms.MilkyButton();
			this.milkyButton3 = new Milky.Windows.Forms.MilkyButton();
			this.SuspendLayout();
			// 
			// milkyButton1
			// 
			this.milkyButton1.Location = new System.Drawing.Point(16, 179);
			this.milkyButton1.Name = "milkyButton1";
			this.milkyButton1.Size = new System.Drawing.Size(75, 23);
			this.milkyButton1.TabIndex = 4;
			this.milkyButton1.Text = "milkyButton1";
			this.milkyButton1.UseVisualStyleBackColor = true;
			this.milkyButton1.Click += new System.EventHandler(this.MilkyButton1_Click);
			// 
			// milkyTextBox1
			// 
			this.milkyTextBox1.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.milkyTextBox1.Location = new System.Drawing.Point(12, 145);
			this.milkyTextBox1.Name = "milkyTextBox1";
			this.milkyTextBox1.Size = new System.Drawing.Size(280, 27);
			this.milkyTextBox1.TabIndex = 2;
			this.milkyTextBox1.WatermarkText = "Watermark Text here.";
			// 
			// milkyLabel1
			// 
			this.milkyLabel1.BackColor = System.Drawing.SystemColors.Control;
			this.milkyLabel1.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.milkyLabel1.Location = new System.Drawing.Point(12, 9);
			this.milkyLabel1.Name = "milkyLabel1";
			this.milkyLabel1.ParentContainer = this;
			this.milkyLabel1.ShadowColor = System.Drawing.Color.Gray;
			this.milkyLabel1.ShadowDir = Milky.Windows.Forms.MilkyLabel.ShadowDirection.TopRight;
			this.milkyLabel1.ShadowSize = 2;
			this.milkyLabel1.Size = new System.Drawing.Size(137, 50);
			this.milkyLabel1.TabIndex = 0;
			this.milkyLabel1.Text = "Shadow\r\nON";
			this.milkyLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.milkyLabel1.UseTransparent = false;
			// 
			// milkyPanel1
			// 
			this.milkyPanel1.AlphaInnerColor1 = 1F;
			this.milkyPanel1.AlphaInnerColor2 = 1F;
			this.milkyPanel1.GradientAngle = 0F;
			this.milkyPanel1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
			this.milkyPanel1.HatchStyle = System.Drawing.Drawing2D.HatchStyle.Horizontal;
			this.milkyPanel1.InnerColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.milkyPanel1.InnerColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.milkyPanel1.LineColor = System.Drawing.Color.Red;
			this.milkyPanel1.LineWidth = 1F;
			this.milkyPanel1.Location = new System.Drawing.Point(12, 62);
			this.milkyPanel1.Name = "milkyPanel1";
			this.milkyPanel1.Size = new System.Drawing.Size(280, 77);
			this.milkyPanel1.TabIndex = 1;
			this.milkyPanel1.UseFrame = true;
			this.milkyPanel1.UseGammaCorrection = false;
			this.milkyPanel1.UseHatchStyle = false;
			this.milkyPanel1.UseTexture = false;
			// 
			// milkyLabel2
			// 
			this.milkyLabel2.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.milkyLabel2.Location = new System.Drawing.Point(155, 9);
			this.milkyLabel2.Name = "milkyLabel2";
			this.milkyLabel2.ParentContainer = this;
			this.milkyLabel2.ShadowColor = System.Drawing.Color.Gray;
			this.milkyLabel2.ShadowDir = Milky.Windows.Forms.MilkyLabel.ShadowDirection.TopRight;
			this.milkyLabel2.ShadowSize = 0;
			this.milkyLabel2.Size = new System.Drawing.Size(137, 50);
			this.milkyLabel2.TabIndex = 0;
			this.milkyLabel2.Text = "Shadow\r\nOFF\r\n";
			this.milkyLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.milkyLabel2.UseTransparent = false;
			// 
			// MilkyLog
			// 
			this.MilkyLog.FullRowSelect = true;
			this.MilkyLog.GridLines = true;
			this.MilkyLog.HideSelection = false;
			this.MilkyLog.Location = new System.Drawing.Point(11, 208);
			this.MilkyLog.MultiSelect = false;
			this.MilkyLog.Name = "MilkyLog";
			this.MilkyLog.Size = new System.Drawing.Size(301, 106);
			this.MilkyLog.TabIndex = 5;
			this.MilkyLog.UseCompatibleStateImageBehavior = false;
			this.MilkyLog.View = System.Windows.Forms.View.Details;
			this.MilkyLog.VirtualMode = true;
			// 
			// milkyButton2
			// 
			this.milkyButton2.Location = new System.Drawing.Point(97, 178);
			this.milkyButton2.Name = "milkyButton2";
			this.milkyButton2.Size = new System.Drawing.Size(75, 23);
			this.milkyButton2.TabIndex = 4;
			this.milkyButton2.Text = "milkyButton1";
			this.milkyButton2.UseVisualStyleBackColor = true;
			this.milkyButton2.Click += new System.EventHandler(this.MilkyButton2_Click);
			// 
			// milkyButton3
			// 
			this.milkyButton3.Location = new System.Drawing.Point(178, 178);
			this.milkyButton3.Name = "milkyButton3";
			this.milkyButton3.Size = new System.Drawing.Size(75, 23);
			this.milkyButton3.TabIndex = 4;
			this.milkyButton3.Text = "milkyButton1";
			this.milkyButton3.UseVisualStyleBackColor = true;
			this.milkyButton3.Click += new System.EventHandler(this.MilkyButton3_Click);
			// 
			// FMilkyControls
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(324, 326);
			this.Controls.Add(this.MilkyLog);
			this.Controls.Add(this.milkyButton3);
			this.Controls.Add(this.milkyButton2);
			this.Controls.Add(this.milkyButton1);
			this.Controls.Add(this.milkyTextBox1);
			this.Controls.Add(this.milkyLabel1);
			this.Controls.Add(this.milkyPanel1);
			this.Controls.Add(this.milkyLabel2);
			this.Name = "FMilkyControls";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Milky Controls";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Milky.Windows.Forms.MilkyLabel milkyLabel1;
		private Milky.Windows.Forms.MilkyLabel milkyLabel2;
		private Milky.Windows.Forms.MilkyPanel milkyPanel1;
		private Milky.Windows.Forms.MilkyTextBox milkyTextBox1;
		private Milky.Windows.Forms.MilkyButton milkyButton1;
		private Milky.Windows.Forms.Jornal.MilkyLogListView MilkyLog;
		private Milky.Windows.Forms.MilkyButton milkyButton3;
		private Milky.Windows.Forms.MilkyButton milkyButton2;
	}
}