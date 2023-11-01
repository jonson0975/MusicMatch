using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2;
//using test4;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace prjGroup1Article.Article
{
    public partial class FrmArticlePicNew : Form
    {
        int articleId = 0;
        public FrmArticlePicNew(int x)
        {
            InitializeComponent();
            articleId = x;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox1.Image = Image.FromFile(this.openFileDialog1.FileName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否新增圖片?", "確認", MessageBoxButtons.YesNo);//詢問視窗
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
                    try
                    {
                        MusicarticlePic p = new MusicarticlePic()
                        {
                            MusicarticleID = articleId,
                            MusicarticlePic1 = bytes,
                        };
                        dbContext.MusicarticlePics.Add(p);
                        dbContext.SaveChanges();
                        MessageBox.Show("新增圖片成功");
                        this.Close();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否放棄內容?", "確認", MessageBoxButtons.YesNo);//詢問視窗
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
