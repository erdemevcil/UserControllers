namespace User_Controllers
{
    partial class DateComboBoxForm
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
            this.dateComboBoxLabel = new System.Windows.Forms.Label();
            this.dateComboBox1 = new User_Controllers.DateComboBox();
            this.SuspendLayout();
            // 
            // dateComboBoxLabel
            // 
            this.dateComboBoxLabel.AutoSize = true;
            this.dateComboBoxLabel.Location = new System.Drawing.Point(13, 58);
            this.dateComboBoxLabel.Name = "dateComboBoxLabel";
            this.dateComboBoxLabel.Size = new System.Drawing.Size(45, 14);
            this.dateComboBoxLabel.TabIndex = 1;
            this.dateComboBoxLabel.Text = "label1";
            // 
            // dateComboBox1
            // 
            this.dateComboBox1.BackColor = System.Drawing.Color.Transparent;
            this.dateComboBox1.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.dateComboBox1.Location = new System.Drawing.Point(16, 12);
            this.dateComboBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dateComboBox1.MaximumDate = new System.DateTime(2026, 6, 10, 23, 10, 43, 408);
            this.dateComboBox1.MaximumSize = new System.Drawing.Size(256, 23);
            this.dateComboBox1.MinimumDate = new System.DateTime(2020, 6, 10, 23, 10, 43, 408);
            this.dateComboBox1.MinimumSize = new System.Drawing.Size(205, 23);
            this.dateComboBox1.Name = "dateComboBox1";
            this.dateComboBox1.Size = new System.Drawing.Size(205, 23);
            this.dateComboBox1.TabIndex = 2;
            this.dateComboBox1.Text = "dateComboBox1";
            this.dateComboBox1.Value = new System.DateTime(2023, 6, 10, 0, 0, 0, 0);
            // 
            // DateComboBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(236, 97);
            this.Controls.Add(this.dateComboBox1);
            this.Controls.Add(this.dateComboBoxLabel);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "DateComboBoxForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DateComboBoxForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label dateComboBoxLabel;
        private DateComboBox dateComboBox1;
    }
}