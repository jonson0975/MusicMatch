using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp2
{
    public partial class FormCreartProducts : Form
    {
        public FormCreartProducts()
        {
            InitializeComponent();
            var x = this.openFileDialog1.FileName;
            using(mdgroup1Entities db= new mdgroup1Entities())
            {
                StyleComboBox.DataSource = db.StyleAllTables.ToList();
                StyleComboBox.ValueMember = "StyleID";
                StyleComboBox.DisplayMember = "StyleName";
            }
        }

        private void FormCreartProducts_Load(object sender, EventArgs e)
        {

        }


        private void button2_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("是否新增作品?", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {


                try
                {
                    string name = txtProductName.Text;
                    if (string.IsNullOrEmpty(name))
                    {
                        MessageBox.Show("請輸入名稱!");
                        return;
                    }

                    if (!int.TryParse(txtMenberID.Text, out int memberId))
                    {
                        MessageBox.Show("請重新輸入會員編號!");
                        return;
                    }

                    string description = txtDescription.Text;
                    DateTime updateTime = dateTimePicker1.Value;

                    byte[] bytes;
                    System.IO.MemoryStream PicDATA = new System.IO.MemoryStream();
                    Image.FromFile(this.label8.Text).Save(PicDATA, System.Drawing.Imaging.ImageFormat.Jpeg);
                    bytes = PicDATA.GetBuffer();

                    string filePath = this.label9.Text;




                    using (mdgroup1Entities db = new mdgroup1Entities())
                    {
                        int.TryParse(StyleComboBox.SelectedIndex.ToString(), out int x);

                        ProductTable p = new ProductTable()
                        {

                            ProductName = name,
                            MemberID = memberId,
                            StyleID = x + 1,
                            UpdateTime = updateTime,
                            ProductDescription = description,
                            Photo = bytes,
                            ProductMediaFile = filePath,

                        };

                        db.ProductTables.Add(p);
                        db.SaveChanges();
                        MessageBox.Show("新增成功!");
                        this.Close();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("請正確填寫資料! " + ex.Message);
                    return;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.label8.Text = this.openFileDialog1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.label9.Text = this.openFileDialog1.FileName;
            }
        }
    }
}
