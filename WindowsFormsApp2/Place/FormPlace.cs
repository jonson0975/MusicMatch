using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp2
{
    public partial class FormPlace : Form
    {
        public FormPlace()
        {
            InitializeComponent();
            using (mdgroup1Entities db = new mdgroup1Entities())
            {
                comboBox1.DataSource = db.CityTables.ToList();
                comboBox1.ValueMember = "CityID";
                comboBox1.DisplayMember = "City";
            }
            using (mdgroup1Entities db = new mdgroup1Entities())
            {
                comboBox2.DataSource = db.CityTables.ToList();
                comboBox2.ValueMember = "CityID";
                comboBox2.DisplayMember = "City";
            }
        }
        mdgroup1Entities dbContext = new mdgroup1Entities();
        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            using (mdgroup1Entities db = new mdgroup1Entities())
            {
                int s = int.Parse(this.textBox3.Text);
                // var result = db.SiteTables.FirstOrDefault(x => x.MemberID == s);
                var result =
                    from r in dbContext.SiteTables
                    where r.SiteID == s
                    select new
                    {
                        場地ID = r.SiteID,
                        會員ID = r.MemberID,
                        場地名稱 = r.SiteName,
                        場地電話 = r.SitePhone,
                        城市 = r.CityTable.City,
                        地址 = r.Address,
                        場地型態 = r.SiteType,
                    };
                dataGridView1.DataSource = result.ToList();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            using (mdgroup1Entities db = new mdgroup1Entities())
            {
                int s = int.Parse(this.textBox1.Text);
                // var result = db.SiteTables.FirstOrDefault(x => x.MemberID == s);
                var result =
                    from r in dbContext.SiteTables
                    where r.MemberID == s
                    select new
                    {
                        場地ID = r.SiteID,
                        會員ID = r.MemberID,
                        場地名稱 = r.SiteName,
                        場地電話 = r.SitePhone,
                        城市 = r.CityTable.City,
                        地址 = r.Address,
                        場地型態 = r.SiteType,
                    };
                dataGridView1.DataSource = result.ToList();
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataToGrid();
            //using (mdgroup1Entities db = new mdgroup1Entities())
            //{
            //    int.TryParse(comboBox1.SelectedIndex.ToString(), out int x);
            //    var result =
            //        from r in dbContext.SiteTables
            //        where r.CityID == x + 1
            //        select new
            //        {
            //            場地ID = r.SiteID,
            //            會員ID = r.MemberID,
            //            場地名稱 = r.SiteName,
            //            場地電話 = r.SitePhone,
            //            城市 = r.CityTable.City,
            //            地址 = r.Address,
            //            場地型態 = r.SiteType,
            //        };
            //    dataGridView1.DataSource = result.ToList();
            //}
        }

        private void LoadDataToGrid()
        {
            dataGridView1.DataSource = null;
            using (mdgroup1Entities db = new mdgroup1Entities())
            {
                int.TryParse(comboBox1.SelectedIndex.ToString(), out int x);
                var result =
                    from r in dbContext.SiteTables
                    where r.CityID == x + 1
                    select new
                    {
                        場地ID = r.SiteID,
                        會員ID = r.MemberID,
                        場地名稱 = r.SiteName,
                        場地電話 = r.SitePhone,
                        城市 = r.CityTable.City,
                        地址 = r.Address,
                        場地型態 = r.SiteType,
                    };
                dataGridView1.DataSource = result.ToList();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //=================
            byte[] bytes;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            this.pictureBox2.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            bytes = ms.GetBuffer();
            //=================
            using (mdgroup1Entities db = new mdgroup1Entities())
            {
                int.TryParse(comboBox2.SelectedIndex.ToString(), out int x);
                SiteTable p = new SiteTable()
                {
                    CityID = x + 1,
                    MemberID = int.Parse(this.textBox4.Text),
                    SitePhone = this.textBox5.Text,
                    Address = this.textBox6.Text,
                    SiteType=this.textBox12.Text,
                    SiteName=this.textBox13.Text
                    //logintime = DateTime.Now
                };
                db.SiteTables.Add(p);
                db.SaveChanges();

               

                using (mdgroup1Entities db2 = new mdgroup1Entities())
                {
                    SitePicTable sp = new SitePicTable()
                    {
                        SiteID = p.SiteID,
                        SitePicture = bytes
                    };
                    db2.SitePicTables.Add(sp);
                    SitePeriodTable p2 = new SitePeriodTable()
                    {
                        SiteID =p.SiteID,
                        MonMorning = false,
                        MonAfternoon = false,
                        MonNight = false,
                        MonMidnight = false,
                        TuesMorning = false,
                        TuesAfternoon = false,
                        TuesNight = false,
                        TuesMidnight = false,
                        WedMorning = false,
                        WedAfternoon = false,
                        WedNight = false,
                        WedMidnight = false,
                        ThurMorning = false,
                        ThurAfternoon = false,
                        ThurNight = false,
                        ThurMidnight = false,
                        FriMorning = false,
                        FriAfternoon = false,
                        FriNight = false,
                        FriMidnight = false,
                        SatMorning = false,
                        SatAfternoon = false,
                        SatNight = false,
                        SatMidnight = false,
                        SunMorning = false,
                        SunAfternoon = false,
                        SunNight = false,
                        SunMidnight = false,
                    };
                    db2.SitePeriodTables.Add(p2);            
                    db2.SaveChanges();               
                }
                MessageBox.Show("新增場地成功！");  
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox2.Image = Image.FromFile(this.openFileDialog1.FileName);
            }
        }
        //private void dateTimePickerDateOfBirth_ValueChanged(object sender, EventArgs e)
        //{
        //    dataGridView1.DataSource = null;
        //    using (mdgroup1Entities db = new mdgroup1Entities())
        //    {
        //        var result = db.SitePeriodTables.All(p => p.BusinessTimeID == 1);
        //        dataGridView1.DataSource = result;
        //    }
        //}
        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否修改場地?", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (mdgroup1Entities db = new mdgroup1Entities())
                {
                    try
                    {
                        var q = (from p in db.SiteTables
                                 where p.SiteID.ToString() == label9.Text
                                 select p).FirstOrDefault();
                        string s = "";

                        //if (q.CityID != int.Parse(textBox9.Text))
                        //{
                        //    q.CityID = int.Parse(textBox9.Text);
                        //    s += ",城市";
                        //}
                        if (q.SitePhone != textBox10.Text)
                        {
                            q.SitePhone = textBox10.Text;
                            s += ",場地電話";
                        }
                        if(q.Address != textBox7.Text)
                        {
                            q.Address = textBox7.Text;
                            s += ",場地地址";
                        }
                        db.SaveChanges();
                        MessageBox.Show($"本次修改 {s}");
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            using (mdgroup1Entities dbContext = new mdgroup1Entities())
            {
                int.TryParse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString(), out int x);
           
                try
                {
                    var q = (
                        from p in dbContext.SiteTables
                        join sp in dbContext.SitePicTables on p.SiteID equals sp.SiteID
                        where p.SiteID == x
                        select new
                        {
                            p.SiteID,
                            Ct = p.CityTable.City,
                            p.SitePhone,
                            sp.SitePicture,
                            p.SiteName,
                            p.SiteType,
                            p.MemberID,
                            p.Address
                        }).FirstOrDefault();
                    
                    if (q != null)
                    {
                        //===============================================================
                        //圖片叫出來
                        byte[] bytes = (byte[])q.SitePicture;
                        System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                        this.pictureBox1.Image = Image.FromStream(ms);
                        //===============================================================
                        this.textBox10.Text = q.SitePhone;
                        //this.textBox9.Text = q.Ct;
                        this.label9.Text = q.SiteID.ToString();
                        this.textBox2.Text = q.SiteType;
                        this.textBox7.Text = q.Address;
                        this.textBox8.Text = q.MemberID.ToString();
                        this.textBox11.Text = q.SiteName;
                    }


                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否刪除場地?", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (mdgroup1Entities db = new mdgroup1Entities())
                {
                    try
                    {
                        var q2 =(from p in db.SitePeriodTables
                                 where p.SiteID.ToString() == label9.Text
                                 select p);                       
                        if (q2 != null) 
                        {
                            foreach (var p in q2)
                            {
                                db.SitePeriodTables.Remove(p);
                            }
                        }
                        //==================
                        var q = (from p in db.SiteTables
                                 where p.SiteID.ToString() == label9.Text
                                 select p).FirstOrDefault();
                        db.SiteTables.Remove(q);
                        //==================
                        var q3 =(from p in db.SitePicTables
                                 where p.SiteID.ToString() == label9.Text
                                 select p);
                        if (q3 != null)
                        {
                            foreach (var p in q3)
                            {
                                db.SitePicTables.Remove(p);
                            }
                        }
                        //==================
                        db.SaveChanges();
                        MessageBox.Show("已刪除場地ID：" + q.SiteID);
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
            LoadDataToGrid();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            using (mdgroup1Entities db = new mdgroup1Entities())
            {


                var result =
                    from r in dbContext.SiteTables

                    select new
                    {
                        場地ID = r.SiteID,
                        會員ID = r.MemberID,
                        場地名稱 = r.SiteName,
                        場地電話 = r.SitePhone,
                        城市 = r.CityTable.City,
                        地址 = r.Address,
                        場地型態 = r.SiteType,
                        
                    };
                dataGridView1.DataSource = result.ToList();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int.TryParse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString(), out int x);
            FormPlaceTime formPlaceTime = new FormPlaceTime(x);
            formPlaceTime.ShowDialog();
        }

        private void FormPlace_Load(object sender, EventArgs e)
        {

        }
    }
}

