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
    public partial class FormEditStyle : Form
    {
        public int Pnum;
        public FormEditStyle(int Pnums)
        {
            InitializeComponent();
            Pnum = Pnums;
        }
        mdgroup1Entities db = new mdgroup1Entities();

        private void buttonUpdate_Click2(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否修改作品?", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                byte[] bytes;
                System.IO.MemoryStream PicDATA = new System.IO.MemoryStream();
                this.pictureBox1.Image.Save(PicDATA, System.Drawing.Imaging.ImageFormat.Jpeg);
                bytes = PicDATA.GetBuffer();



                int.TryParse(txtProID.Text, out int x);
                var update = db.StyleAllTables.FirstOrDefault(p => p.StyleID == x);
                update.StyleName = txtProName.Text;
                update.StyleDescription = txtDescription.Text;
                update.StylePicture = bytes;
                
                db.SaveChanges();
                MessageBox.Show("更新成功");
                this.Close();
            }
        }

        private void FormEditStyle_Load(object sender, EventArgs e)
        {
            using (mdgroup1Entities db = new mdgroup1Entities())
            {
                var q = (from p in db.StyleAllTables
                         where p.StyleID == Pnum
                         select p).FirstOrDefault();

                byte[] bytes = (byte[])q.StylePicture;
                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                this.pictureBox1.Image = Image.FromStream(ms);
                this.txtProID.Text = q.StyleID.ToString();
                this.txtProName.Text = q.StyleName;
                this.txtDescription.Text = q.StyleDescription;
               
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否刪除風格?", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes) 
            { 
                int.TryParse(txtProID.Text, out int x);
            var delete = db.StyleAllTables.FirstOrDefault(p => p.StyleID == x);
            db.StyleAllTables.Remove(delete);
            db.SaveChanges();
            this.Close();
        }
        }
    }
    
}
