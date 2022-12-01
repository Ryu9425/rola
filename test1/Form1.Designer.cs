namespace test1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button = new System.Windows.Forms.Button();
            this.dateLabel = new System.Windows.Forms.Label();
            this.dataGridView_1 = new System.Windows.Forms.DataGridView();
            this.dataGridView_2 = new System.Windows.Forms.DataGridView();
            this.dataGridView_3 = new System.Windows.Forms.DataGridView();
            this.dataGridView_4 = new System.Windows.Forms.DataGridView();
            this.dataGridView_5 = new System.Windows.Forms.DataGridView();
            this.timeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_5)).BeginInit();
            this.SuspendLayout();
            // 
            // button
            // 
            this.button.Location = new System.Drawing.Point(866, 27);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(70, 62);
            this.button.TabIndex = 8;
            this.button.Text = "Submit";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Font = new System.Drawing.Font("UD Digi Kyokasho NP-B", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.dateLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.dateLabel.Location = new System.Drawing.Point(43, 61);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(247, 31);
            this.dateLabel.TabIndex = 10;
            this.dateLabel.Text = "2022年12月10日";
            // 
            // dataGridView_1
            // 
            this.dataGridView_1.AllowUserToAddRows = false;
            this.dataGridView_1.AllowUserToDeleteRows = false;
            this.dataGridView_1.AllowUserToResizeColumns = false;
            this.dataGridView_1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_1.ColumnHeadersVisible = false;
            this.dataGridView_1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView_1.Location = new System.Drawing.Point(91, 304);
            this.dataGridView_1.MultiSelect = false;
            this.dataGridView_1.Name = "dataGridView_1";
            this.dataGridView_1.RowHeadersVisible = false;
            this.dataGridView_1.RowTemplate.Height = 25;
            this.dataGridView_1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView_1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_1.Size = new System.Drawing.Size(209, 205);
            this.dataGridView_1.TabIndex = 0;
            this.dataGridView_1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DisplayDetailDatas);
            // 
            // dataGridView_2
            // 
            this.dataGridView_2.AllowUserToAddRows = false;
            this.dataGridView_2.AllowUserToDeleteRows = false;
            this.dataGridView_2.AllowUserToResizeColumns = false;
            this.dataGridView_2.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_2.ColumnHeadersVisible = false;
            this.dataGridView_2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView_2.Location = new System.Drawing.Point(355, 304);
            this.dataGridView_2.MultiSelect = false;
            this.dataGridView_2.Name = "dataGridView_2";
            this.dataGridView_2.RowHeadersVisible = false;
            this.dataGridView_2.RowTemplate.Height = 25;
            this.dataGridView_2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView_2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_2.Size = new System.Drawing.Size(205, 205);
            this.dataGridView_2.TabIndex = 1;
            // 
            // dataGridView_3
            // 
            this.dataGridView_3.AllowUserToAddRows = false;
            this.dataGridView_3.AllowUserToDeleteRows = false;
            this.dataGridView_3.AllowUserToResizeColumns = false;
            this.dataGridView_3.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView_3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_3.ColumnHeadersVisible = false;
            this.dataGridView_3.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView_3.Location = new System.Drawing.Point(355, 61);
            this.dataGridView_3.MultiSelect = false;
            this.dataGridView_3.Name = "dataGridView_3";
            this.dataGridView_3.RowHeadersVisible = false;
            this.dataGridView_3.RowTemplate.Height = 25;
            this.dataGridView_3.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView_3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_3.Size = new System.Drawing.Size(205, 205);
            this.dataGridView_3.TabIndex = 11;
            // 
            // dataGridView_4
            // 
            this.dataGridView_4.AllowUserToAddRows = false;
            this.dataGridView_4.AllowUserToDeleteRows = false;
            this.dataGridView_4.AllowUserToResizeColumns = false;
            this.dataGridView_4.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_4.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView_4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_4.ColumnHeadersVisible = false;
            this.dataGridView_4.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView_4.Location = new System.Drawing.Point(625, 61);
            this.dataGridView_4.MultiSelect = false;
            this.dataGridView_4.Name = "dataGridView_4";
            this.dataGridView_4.RowHeadersVisible = false;
            this.dataGridView_4.RowTemplate.Height = 25;
            this.dataGridView_4.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView_4.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_4.Size = new System.Drawing.Size(203, 205);
            this.dataGridView_4.TabIndex = 12;
            // 
            // dataGridView_5
            // 
            this.dataGridView_5.AllowUserToAddRows = false;
            this.dataGridView_5.AllowUserToDeleteRows = false;
            this.dataGridView_5.AllowUserToResizeColumns = false;
            this.dataGridView_5.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_5.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView_5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_5.ColumnHeadersVisible = false;
            this.dataGridView_5.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView_5.Location = new System.Drawing.Point(625, 304);
            this.dataGridView_5.MultiSelect = false;
            this.dataGridView_5.Name = "dataGridView_5";
            this.dataGridView_5.RowHeadersVisible = false;
            this.dataGridView_5.RowTemplate.Height = 25;
            this.dataGridView_5.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView_5.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_5.Size = new System.Drawing.Size(203, 205);
            this.dataGridView_5.TabIndex = 13;
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Font = new System.Drawing.Font("UD Digi Kyokasho NK-B", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.timeLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.timeLabel.Location = new System.Drawing.Point(80, 104);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(163, 34);
            this.timeLabel.TabIndex = 14;
            this.timeLabel.Text = "13:40:00";
            this.timeLabel.UseMnemonic = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 591);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.dataGridView_5);
            this.Controls.Add(this.dataGridView_4);
            this.Controls.Add(this.dataGridView_3);
            this.Controls.Add(this.dataGridView_2);
            this.Controls.Add(this.dataGridView_1);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.button);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button button;
        private Label dateLabel;
        private DataGridView dataGridView_1;
        private DataGridView dataGridView_2;
        private DataGridView dataGridView_3;
        private DataGridView dataGridView_4;
        private DataGridView dataGridView_5;
        private Label timeLabel;
    }
}