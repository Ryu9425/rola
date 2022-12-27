namespace test1
{
    partial class Form2
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.apiBox = new System.Windows.Forms.TextBox();
            this.minuteBox = new System.Windows.Forms.TextBox();
            this.secondBox = new System.Windows.Forms.TextBox();
            this.countBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.urlBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.closeBtn = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.moduleTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.portBox = new System.Windows.Forms.TextBox();
            this.portSearchBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(112, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "LoRa親機接続時間";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(112, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "データ保存先";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(112, 225);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "管理モジュール数";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(112, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "WEB API";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(112, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(173, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "LoRa親機接続間隔";
            // 
            // apiBox
            // 
            this.apiBox.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.apiBox.Location = new System.Drawing.Point(305, 70);
            this.apiBox.Name = "apiBox";
            this.apiBox.Size = new System.Drawing.Size(444, 32);
            this.apiBox.TabIndex = 5;
            // 
            // minuteBox
            // 
            this.minuteBox.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.minuteBox.Location = new System.Drawing.Point(305, 108);
            this.minuteBox.Name = "minuteBox";
            this.minuteBox.Size = new System.Drawing.Size(117, 32);
            this.minuteBox.TabIndex = 6;
            this.minuteBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.minuteBox.TextChanged += new System.EventHandler(this.minuteBox_TextChanged);
            // 
            // secondBox
            // 
            this.secondBox.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.secondBox.Location = new System.Drawing.Point(305, 146);
            this.secondBox.Name = "secondBox";
            this.secondBox.Size = new System.Drawing.Size(117, 32);
            this.secondBox.TabIndex = 7;
            this.secondBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.secondBox.TextChanged += new System.EventHandler(this.secondBox_TextChanged);
            // 
            // countBox
            // 
            this.countBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.countBox.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.countBox.Location = new System.Drawing.Point(305, 226);
            this.countBox.Name = "countBox";
            this.countBox.Size = new System.Drawing.Size(82, 32);
            this.countBox.TabIndex = 9;
            this.countBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.countBox.TextChanged += new System.EventHandler(this.countBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(461, 229);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 25);
            this.label6.TabIndex = 10;
            this.label6.Text = "台";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(461, 149);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 25);
            this.label7.TabIndex = 11;
            this.label7.Text = "秒";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(461, 111);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 25);
            this.label8.TabIndex = 12;
            this.label8.Text = "分";
            // 
            // urlBox
            // 
            this.urlBox.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.urlBox.Location = new System.Drawing.Point(305, 185);
            this.urlBox.Name = "urlBox";
            this.urlBox.Size = new System.Drawing.Size(444, 32);
            this.urlBox.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.button1.Location = new System.Drawing.Point(753, 185);
            this.button1.Margin = new System.Windows.Forms.Padding(1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 32);
            this.button1.TabIndex = 15;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.MenuBar;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(112, 313);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 25;
            this.dataGridView.Size = new System.Drawing.Size(709, 231);
            this.dataGridView.TabIndex = 16;
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            // 
            // closeBtn
            // 
            this.closeBtn.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.closeBtn.Location = new System.Drawing.Point(836, 27);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(63, 49);
            this.closeBtn.TabIndex = 17;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(112, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(121, 25);
            this.label9.TabIndex = 18;
            this.label9.Text = "WEB Module";
            // 
            // moduleTextBox
            // 
            this.moduleTextBox.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.moduleTextBox.Location = new System.Drawing.Point(305, 21);
            this.moduleTextBox.Name = "moduleTextBox";
            this.moduleTextBox.Size = new System.Drawing.Size(444, 32);
            this.moduleTextBox.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label10.Location = new System.Drawing.Point(112, 267);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(148, 25);
            this.label10.TabIndex = 20;
            this.label10.Text = "COM Port Name";
            // 
            // portBox
            // 
            this.portBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.portBox.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.portBox.Location = new System.Drawing.Point(305, 267);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(82, 32);
            this.portBox.TabIndex = 21;
            this.portBox.Text = "COM6";
            this.portBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // portSearchBtn
            // 
            this.portSearchBtn.Location = new System.Drawing.Point(418, 267);
            this.portSearchBtn.Name = "portSearchBtn";
            this.portSearchBtn.Size = new System.Drawing.Size(75, 32);
            this.portSearchBtn.TabIndex = 22;
            this.portSearchBtn.Text = "Search";
            this.portSearchBtn.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(922, 561);
            this.Controls.Add(this.portSearchBtn);
            this.Controls.Add(this.portBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.moduleTextBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.urlBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.countBox);
            this.Controls.Add(this.secondBox);
            this.Controls.Add(this.minuteBox);
            this.Controls.Add(this.apiBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(950, 600);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox apiBox;
        private TextBox minuteBox;
        private TextBox secondBox;
        private TextBox countBox;
        private Label label6;
        private Label label7;
        private Label label8;
        private FolderBrowserDialog folderBrowserDialog;
        private TextBox urlBox;
        private Button button1;
        private DataGridView dataGridView;
        private Button closeBtn;
        private Label label9;
        private TextBox moduleTextBox;
        private Label label10;
        private TextBox portBox;
        private Button portSearchBtn;
    }
}