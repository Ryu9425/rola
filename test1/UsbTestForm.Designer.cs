namespace test1
{
    partial class UsbTestForm
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
            this.OpenBtn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SensorBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OpenBtn
            // 
            this.OpenBtn.Location = new System.Drawing.Point(373, 50);
            this.OpenBtn.Name = "OpenBtn";
            this.OpenBtn.Size = new System.Drawing.Size(113, 23);
            this.OpenBtn.TabIndex = 0;
            this.OpenBtn.Text = "OpenBtn";
            this.OpenBtn.UseVisualStyleBackColor = true;
            this.OpenBtn.Click += new System.EventHandler(this.OpenBtn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(119, 51);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(185, 23);
            this.textBox1.TabIndex = 1;
            // 
            // SensorBtn
            // 
            this.SensorBtn.Location = new System.Drawing.Point(373, 114);
            this.SensorBtn.Name = "SensorBtn";
            this.SensorBtn.Size = new System.Drawing.Size(103, 38);
            this.SensorBtn.TabIndex = 2;
            this.SensorBtn.Text = "SensorBtn";
            this.SensorBtn.UseVisualStyleBackColor = true;
            this.SensorBtn.Click += new System.EventHandler(this.SensorBtn_Click);
            // 
            // UsbTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SensorBtn);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.OpenBtn);
            this.Name = "UsbTestForm";
            this.Text = "UsbTestForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button OpenBtn;
        private TextBox textBox1;
        private Button SensorBtn;
    }
}