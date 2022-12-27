namespace test1
{
    partial class Form3
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.startLabel = new System.Windows.Forms.Label();
            this.finishLabel = new System.Windows.Forms.Label();
            this.rolaLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(168, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "子機の応答待ち";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(81, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 31);
            this.label2.TabIndex = 1;
            this.label2.Text = "Start";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(81, 219);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 31);
            this.label3.TabIndex = 2;
            this.label3.Text = "Finish";
            // 
            // startLabel
            // 
            this.startLabel.AutoSize = true;
            this.startLabel.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.startLabel.Location = new System.Drawing.Point(200, 166);
            this.startLabel.Name = "startLabel";
            this.startLabel.Size = new System.Drawing.Size(277, 31);
            this.startLabel.TabIndex = 3;
            this.startLabel.Text = "yyyy/mm/dd hh:mm:ss";
            // 
            // finishLabel
            // 
            this.finishLabel.AutoSize = true;
            this.finishLabel.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.finishLabel.Location = new System.Drawing.Point(200, 219);
            this.finishLabel.Name = "finishLabel";
            this.finishLabel.Size = new System.Drawing.Size(277, 31);
            this.finishLabel.TabIndex = 4;
            this.finishLabel.Text = "yyyy/mm/dd hh:mm:ss";
            // 
            // rolaLabel
            // 
            this.rolaLabel.AutoSize = true;
            this.rolaLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rolaLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.rolaLabel.Location = new System.Drawing.Point(13, 5);
            this.rolaLabel.Name = "rolaLabel";
            this.rolaLabel.Size = new System.Drawing.Size(53, 30);
            this.rolaLabel.TabIndex = 5;
            this.rolaLabel.Text = "Rola";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.panel1.Controls.Add(this.rolaLabel);
            this.panel1.Location = new System.Drawing.Point(-7, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(595, 40);
            this.panel1.TabIndex = 6;
            // 
            // Form3
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(564, 331);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.finishLabel);
            this.Controls.Add(this.startLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximumSize = new System.Drawing.Size(580, 370);
            this.MinimumSize = new System.Drawing.Size(580, 0);
            this.Name = "Form3";
            this.Text = "Form3";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form3_MouseDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label startLabel;
        private Label finishLabel;
        private Label rolaLabel;
        private Panel panel1;
    }
}