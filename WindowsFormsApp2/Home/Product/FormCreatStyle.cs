using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2.ProductAndEva
{
    public partial class FormCreatStyle : Form
    {
        public FormCreatStyle()
        {
            InitializeComponent();
           
        }
      

        private void button2_Click(object sender, EventArgs e)
        {

            byte[] bytes;
            System.IO.MemoryStream PicDATA = new System.IO.MemoryStream();
            this.pictureBox1.Image.Save(PicDATA, System.Drawing.Imaging.ImageFormat.Jpeg);
            bytes = PicDATA.GetBuffer();
            DialogResult result = MessageBox.Show("是否建立風格?", "確認", MessageBoxButtons.YesNo);//詢問視窗
            if (result == DialogResult.Yes)
            {
                try
                {
                    int.TryParse(this.textBox1.Text, out int x);
                    using (mdgroup1Entities db = new mdgroup1Entities())
                    {
                        StyleAllTable sty = new StyleAllTable()
                        {
                            StyleID = x,
                            StyleName=textBox2.Text,
                            StyleDescription=richTextBox1.Text,
                            StylePicture= bytes,
                        };
                        db.StyleAllTables.Add(sty);
                        db.SaveChanges();
                        MessageBox.Show("新增風格成功");
                        this.Close();
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox1.Image = Image.FromFile(this.openFileDialog1.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    }
    

