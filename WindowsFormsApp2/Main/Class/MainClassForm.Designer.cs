namespace WindowsFormsApp2.Main.Class
{
    partial class MainClassForm
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.g1DataSet1 = new WindowsFormsApp2.g1DataSet();
            this.classAllTableTableAdapter1 = new WindowsFormsApp2.g1DataSetTableAdapters.ClassAllTableTableAdapter();
            this.memberTableTableAdapter1 = new WindowsFormsApp2.g1DataSetTableAdapters.MemberTableTableAdapter();
            this.siteTableTableAdapter1 = new WindowsFormsApp2.g1DataSetTableAdapters.SiteTableTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.g1DataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.textBox1.Location = new System.Drawing.Point(114, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(629, 30);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "請輸入關鍵字";
            // 
            // g1DataSet1
            // 
            this.g1DataSet1.DataSetName = "g1DataSet";
            this.g1DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // classAllTableTableAdapter1
            // 
            this.classAllTableTableAdapter1.ClearBeforeFill = true;
            // 
            // memberTableTableAdapter1
            // 
            this.memberTableTableAdapter1.ClearBeforeFill = true;
            // 
            // siteTableTableAdapter1
            // 
            this.siteTableTableAdapter1.ClearBeforeFill = true;
            // 
            // MainClassForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(897, 720);
            this.Controls.Add(this.textBox1);
            this.Name = "MainClassForm";
            this.Text = "MainClassForm";
            ((System.ComponentModel.ISupportInitialize)(this.g1DataSet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private g1DataSet g1DataSet1;
        private g1DataSetTableAdapters.ClassAllTableTableAdapter classAllTableTableAdapter1;
        private g1DataSetTableAdapters.MemberTableTableAdapter memberTableTableAdapter1;
        private g1DataSetTableAdapters.SiteTableTableAdapter siteTableTableAdapter1;
    }
}