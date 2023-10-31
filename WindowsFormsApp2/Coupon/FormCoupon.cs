using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class FormCoupon : Form
    {
        public FormCoupon()
        {
            InitializeComponent();
            LoadCouponToGridView();
            this.dateTimePicker3.MinDate = DateTime.Now.AddDays(1);
            this.dateTimePicker4.MinDate = DateTime.Now.AddDays(1);

        }

        private void FrmCoupon_Load(object sender, EventArgs e)
        {
            LoadCouponToGridView();
        }
        private void LoadCouponToGridView()
        {
            this.dataGridView1.DataSource = null;
            using (mdgroup1Entities dbContext = new mdgroup1Entities())
            {
                try
                {
                    var q = from p in dbContext.CouponTables
                            select new { p.CouponID, p.CouponContent, p.MemberID, p.CouponCode, p.StartDate, p.EndDate, p.CouponDescription, p.CouponPicture };
                    this.dataGridView1.DataSource = q.ToList();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            using (mdgroup1Entities dbContext = new mdgroup1Entities())
            {
                int.TryParse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString(), out int x);
                try
                {
                    var q = (from p in dbContext.CouponTables
                             where p.CouponID == x
                             select p).FirstOrDefault();

                    this.textBox1.Text = q.CouponID.ToString();
                    this.textBox2.Text = q.CouponCode;
                    this.textBox3.Text = q.MemberID.ToString();
                    this.dateTimePicker1.Value = q.StartDate.Value;
                    this.dateTimePicker2.Value = q.EndDate.Value;
                    this.richTextBox1.Text = q.CouponDescription;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否修改專案?", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (mdgroup1Entities db = new mdgroup1Entities())
                {
                    try
                    {
                        
                        var q = (from p in db.CouponTables
                                 where p.CouponID.ToString() == textBox1.Text
                                 select p).FirstOrDefault();
                        

                        string s = "";
                        string mid = q.MemberID.ToString();
                        string cid = q.CouponID.ToString();




                        if (cid != textBox1.Text)
                        {
                            q.CouponID = int.Parse(textBox1.Text);
                            s += ",優惠券編號";
                        }

                        if (q.CouponCode != textBox2.Text)
                        {
                            q.CouponCode = textBox2.Text;
                            s += ",優惠券代碼";
                        }
                        if (mid != textBox3.Text)
                        {
                            q.MemberID = int.Parse(textBox3.Text);
                            s += ",會員編號";
                        }
                        if (q.CouponContent != textBox7.Text)
                        {
                            q.CouponContent = textBox7.Text;
                            s += ",優惠券內容";
                        }

                        if (q.StartDate != this.dateTimePicker1.Value)
                        {
                            q.StartDate = this.dateTimePicker1.Value;
                            s += ",開始時間";
                        }
                        if (q.EndDate != this.dateTimePicker2.Value)
                        {
                            q.EndDate = this.dateTimePicker2.Value;
                            s += ",結束時間";
                        }
                        if (q.CouponDescription != richTextBox1.Text)
                        {
                            q.CouponDescription = richTextBox1.Text;
                            s += ",優惠券描述";
                        }


                        //儲存_上傳圖片檔案資料
                        byte[] bytes;
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        this.pictureBox2.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        bytes = ms.GetBuffer();
                        q.CouponPicture = bytes;
                        
                        db.SaveChanges();
                        MessageBox.Show($"本次修改{s}");

                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    LoadCouponToGridView();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadCouponToGridView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否刪除?", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (mdgroup1Entities dd = new mdgroup1Entities())
                {
                    try
                    {
                        var q = (from p in dd.CouponTables
                                 where p.CouponID.ToString() == textBox1.Text
                                 select p).FirstOrDefault();
                        dd.CouponTables.Remove(q);
                        dd.SaveChanges();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否新增折價券", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (mdgroup1Entities cc = new mdgroup1Entities())
                {
                    int.TryParse(textBox4.Text, out int x);
                    try
                    {
                        CouponTable p = new CouponTable()
                        {
                            CouponCode = this.textBox5.Text,
                            MemberID = int.Parse(this.textBox4.Text),
                            CouponContent = this.textBox8.Text,
                            StartDate = this.dateTimePicker3.Value,
                            EndDate = this.dateTimePicker4.Value,
                            CouponDescription = this.richTextBox2.Text,
                        };
                        
                        byte[] bytes;
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        this.pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        bytes = ms.GetBuffer();

                        p.CouponPicture = bytes;


                        cc.CouponTables.Add(p);

                        cc.SaveChanges();
                        MessageBox.Show("建立成功");
                        LoadCouponToGridView();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
        }

        private void 編輯_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (mdgroup1Entities fcid = new mdgroup1Entities())
            {
                var q = (from p in fcid.CouponTables
                         where p.CouponID.ToString() == textBox9.Text
                         select p).ToList();
                this.dataGridView2.DataSource = q.ToList();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("OK" + this.openFileDialog1.FileName);
                this.pictureBox1.Image = Image.FromFile(this.openFileDialog1.FileName);
            }
            else { this.pictureBox1 = null; }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (mdgroup1Entities fmid = new mdgroup1Entities())
            {
                var q = (from p in fmid.CouponTables
                         where p.MemberID.ToString() == textBox6.Text
                         select p).ToList();
                this.dataGridView2.DataSource = q.ToList();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("OK" + this.openFileDialog1.FileName);
                this.pictureBox2.Image = Image.FromFile(this.openFileDialog1.FileName);
            }
            else { this.pictureBox2 = null; }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }


}
