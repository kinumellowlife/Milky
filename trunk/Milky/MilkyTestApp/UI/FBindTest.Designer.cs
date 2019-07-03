namespace MilkyTestApp.UI
{
	partial class FBindTest
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
			this.StartThreadButton = new System.Windows.Forms.Button();
			this.milkyCheckBox2 = new Milky.Windows.Forms.MilkyCheckBox();
			this.milkyCheckBox1 = new Milky.Windows.Forms.MilkyCheckBox();
			this.milkyButton1 = new Milky.Windows.Forms.MilkyButton();
			this.SuspendLayout();
			// 
			// StartThreadButton
			// 
			this.StartThreadButton.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.StartThreadButton.Location = new System.Drawing.Point(12, 120);
			this.StartThreadButton.Name = "StartThreadButton";
			this.StartThreadButton.Size = new System.Drawing.Size(154, 37);
			this.StartThreadButton.TabIndex = 3;
			this.StartThreadButton.Text = "StartThreead";
			this.StartThreadButton.UseVisualStyleBackColor = true;
			this.StartThreadButton.Click += new System.EventHandler(this.StartThreadButton_Click);
			// 
			// milkyCheckBox2
			// 
			this.milkyCheckBox2.AutoSize = true;
			this.milkyCheckBox2.DefaultChecked = false;
			this.milkyCheckBox2.Location = new System.Drawing.Point(12, 63);
			this.milkyCheckBox2.Name = "milkyCheckBox2";
			this.milkyCheckBox2.Size = new System.Drawing.Size(109, 16);
			this.milkyCheckBox2.TabIndex = 5;
			this.milkyCheckBox2.Text = "milkyCheckBox1";
			this.milkyCheckBox2.UseVisualStyleBackColor = true;
			// 
			// milkyCheckBox1
			// 
			this.milkyCheckBox1.AutoSize = true;
			this.milkyCheckBox1.DefaultChecked = false;
			this.milkyCheckBox1.Location = new System.Drawing.Point(12, 41);
			this.milkyCheckBox1.Name = "milkyCheckBox1";
			this.milkyCheckBox1.Size = new System.Drawing.Size(109, 16);
			this.milkyCheckBox1.TabIndex = 5;
			this.milkyCheckBox1.Text = "milkyCheckBox1";
			this.milkyCheckBox1.UseVisualStyleBackColor = true;
			// 
			// milkyButton1
			// 
			this.milkyButton1.Location = new System.Drawing.Point(12, 12);
			this.milkyButton1.Name = "milkyButton1";
			this.milkyButton1.Size = new System.Drawing.Size(217, 23);
			this.milkyButton1.TabIndex = 4;
			this.milkyButton1.Text = "milkyButton1";
			this.milkyButton1.UseVisualStyleBackColor = true;
			// 
			// FBindTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(585, 505);
			this.Controls.Add(this.milkyCheckBox2);
			this.Controls.Add(this.milkyCheckBox1);
			this.Controls.Add(this.milkyButton1);
			this.Controls.Add(this.StartThreadButton);
			this.Name = "FBindTest";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Bind";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button StartThreadButton;
		private Milky.Windows.Forms.MilkyButton milkyButton1;
		private Milky.Windows.Forms.MilkyCheckBox milkyCheckBox1;
		private Milky.Windows.Forms.MilkyCheckBox milkyCheckBox2;
	}
}