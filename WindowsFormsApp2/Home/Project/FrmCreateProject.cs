using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.Project.ForAdm;
using WindowsFormsApp2.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WindowsFormsApp2.Project
{
    public partial class FrmCreateProject : Form
    {
        public FrmCreateProject()
        {
            InitializeComponent();           
        }

        private void button3_Click(object sender, EventArgs e)//建立專案
        {
            DialogResult result = MessageBox.Show("是否創立專案?", "確認", MessageBoxButtons.YesNo);//詢問視窗
            if (result == DialogResult.Yes) 
            {
                using (mdgroup1Entities dbContext = new mdgroup1Entities())
                {
                    //=========================圖像轉換
                    byte[] bytes;
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    this.pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    bytes = ms.GetBuffer();
                    //=========================
                    int.TryParse(this.textBox3.Text, out int x);
                    try
                    {
                        ProjectTable p = new ProjectTable()
                        {
                            ProjectName = textBox1.Text,
                            Budget = textBox2.Text,
                            ReleaseMemberNumber = x,
                            ProjectDescription = this.richTextBox1.Text,
                            ProductionPeriod = this.dateTimePicker1.Value,
                            CreateTime = DateTime.Now,
                            ProjectPicture = bytes,
                            ProjectStatus = 1
                        };
                        dbContext.ProjectTables.Add(p);
                        dbContext.SaveChanges();
                        MessageBox.Show("建立專案成功");
                        this.Close();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }            
        }

        private void button1_Click(object sender, EventArgs e)//上傳圖像
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox1.Image = Image.FromFile(this.openFileDialog1.FileName);
            }
        }
    }
}
