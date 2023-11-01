namespace prjGroup1Plus.summit
{
    partial class FrmAppliCreate
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
            this.txtboxMemID = new System.Windows.Forms.TextBox();
            this.txtboxPrjID = new System.Windows.Forms.TextBox();
            this.txtboxDescri = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnNO = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtboxMemID
            // 
            this.txtboxMemID.Location = new System.Drawing.Point(111, 53);
            this.txtboxMemID.Name = "txtboxMemID";
            this.txtboxMemID.Size = new System.Drawing.Size(100, 22);
            this.txtboxMemID.TabIndex = 0;
            // 
            // txtboxPrjID
            // 
            this.txtboxPrjID.Location = new System.Drawing.Point(111, 101);
            this.txtboxPrjID.Name = "txtboxPrjID";
            this.txtboxPrjID.Size = new System.Drawing.Size(100, 22);
            this.txtboxPrjID.TabIndex = 1;
            // 
            // txtboxDescri
            // 
            this.txtboxDescri.Location = new System.Drawing.Point(111, 156);
            this.txtboxDescri.Name = "txtboxDescri";
            this.txtboxDescri.Size = new System.Drawing.Size(257, 238);
            this.txtboxDescri.TabIndex = 2;
            this.txtboxDescri.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "會員ID :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "專案ID :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "應徵描述 :";
            // 
            // btnNO
            // 
            this.btnNO.Location = new System.Drawing.Point(199, 437);
            this.btnNO.Name = "btnNO";
            this.btnNO.Size = new System.Drawing.Size(75, 23);
            this.btnNO.TabIndex = 6;
            this.btnNO.Text = "取消";
            this.btnNO.UseVisualStyleBackColor = true;
            this.btnNO.Click += new System.EventHandler(this.btnNO_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(299, 437);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "確定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FrmAppliCreate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 506);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnNO);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtboxDescri);
            this.Controls.Add(this.txtboxPrjID);
            this.Controls.Add(this.txtboxMemID);
            this.Name = "FrmAppliCreate";
            this.Text = "FrmAppliCreate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtboxMemID;
        private System.Windows.Forms.TextBox txtboxPrjID;
        private System.Windows.Forms.RichTextBox txtboxDescri;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnNO;
        private System.Windows.Forms.Button btnOK;
    }
}