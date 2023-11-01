using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using PnumClassFormsApp1;

namespace WindowsFormsApp2
{
    public partial class FormEditProducts : Form
    {
        public int Pnum;
        public FormEditProducts(int Pnums)
        {
            InitializeComponent();

            Pnum = Pnums;
            using (mdgroup1Entities db = new mdgroup1Entities())
            {
               
                var q = from s in db.StyleAllTables
                        select s;
                foreach (var x in q)
                {
                this.comboBoxEditProduct.Items.Add(x.StyleName);
                }
            }
            
            
        }

        mdgroup1Entities db = new mdgroup1Entities();

        
        private void FormEditProducts_Load(object sender, EventArgs e)
        {
            using (mdgroup1Entities db = new mdgroup1Entities())
            {
                var q = (from p in db.ProductTables
                         where p.ProductID == Pnum
                         select p).FirstOrDefault();

                byte[] bytes = (byte[])q.Photo;
                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                this.pictureBox1.Image = Image.FromStream(ms);

                this.comboBoxEditProduct.SelectedIndex = q.StyleID.Value - +1;


                this.txtProID.Text = q.ProductID.ToString();
                this.txtProName.Text = q.ProductName;


                this.txtDescription.Text = q.ProductDescription;
                this.label9.Text = q.ProductMediaFile.ToString();
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("是否修改作品?", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                byte[] bytes;
                System.IO.MemoryStream PicDATA = new System.IO.MemoryStream();
                this.pictureBox1.Image.Save(PicDATA, System.Drawing.Imaging.ImageFormat.Jpeg);
                bytes = PicDATA.GetBuffer();

                string filePath = this.label9.Text;

                int.TryParse(txtProID.Text, out int x);
                var update = db.ProductTables.FirstOrDefault(p => p.ProductID == x);
                update.ProductName = txtProName.Text;
                update.ProductDescription = txtDescription.Text;

                update.StyleID = comboBoxEditProduct.SelectedIndex + 1;
                update.Photo = bytes;
                update.ProductMediaFile = filePath;
                db.SaveChanges();
                MessageBox.Show("更新成功");
                this.Close();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox1.Image = Image.FromFile(this.openFileDialog1.FileName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.label9.Text = this.openFileDialog1.FileName;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int.TryParse(txtProID.Text, out int x);
            var delete = db.ProductTables.FirstOrDefault(p => p.ProductID == x);
            db.ProductTables.Remove(delete);
            db.SaveChanges();
            this.Close();
        }
    }
}
