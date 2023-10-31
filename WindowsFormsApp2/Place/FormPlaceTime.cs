using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp2
{
    public partial class FormPlaceTime : Form
    {
        public FormPlaceTime(int a)
        {
            InitializeComponent();
            this.label9.Text = a.ToString();
        }
      

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    //if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
        //    //{
        //    //    this.pictureBox1.Image = Image.FromFile(this.openFileDialog1.FileName);
        //    //}
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否修改場地?", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (mdgroup1Entities db = new mdgroup1Entities())
                {
                    try
                    {
                       var q = (from n in db.SitePeriodTables
                               where n.SiteID.ToString() == label9.Text
                               select n).FirstOrDefault();
                        q.MonMorning = this.checkBox1.Checked;
                        q.MonAfternoon = this.checkBox2.Checked;
                        q.MonNight = this.checkBox3.Checked;
                        q.MonMidnight = this.checkBox4.Checked;
                        q.TuesMorning = this.checkBox8.Checked;
                        q.TuesAfternoon = this.checkBox7.Checked;
                        q.TuesNight = this.checkBox6.Checked;
                        q.TuesMidnight = this.checkBox5.Checked;
                        q.WedMorning = this.checkBox12.Checked;
                        q.WedAfternoon = this.checkBox11.Checked;
                        q.WedNight = this.checkBox10.Checked;
                        q.WedMidnight = this.checkBox9.Checked;
                        q.ThurMorning = this.checkBox16.Checked;
                        q.ThurAfternoon = this.checkBox15.Checked;
                        q.ThurNight = this.checkBox14.Checked;
                        q.ThurMidnight = this.checkBox13.Checked;
                        q.FriMorning = this.checkBox20.Checked;
                        q.FriAfternoon = this.checkBox19.Checked;
                        q.FriNight = this.checkBox18.Checked;
                        q.FriMidnight = this.checkBox17.Checked;
                        q.SatMorning = this.checkBox24.Checked;
                        q.SatAfternoon = this.checkBox23.Checked;
                        q.SatNight = this.checkBox22.Checked;
                        q.SatMidnight = this.checkBox21.Checked;
                        q.SunMorning = this.checkBox28.Checked;
                        q.SunAfternoon = this.checkBox27.Checked;
                        q.SunNight = this.checkBox26.Checked;
                        q.SunMidnight = this.checkBox25.Checked;


                       
                        db.SaveChanges();
                        MessageBox.Show($"本次修改時段成功");
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }

            //
            //{
            //    int.TryParse(this.label9.Text, out int c);
            //    var q = (from s in db.SiteTables
            //            where s.SiteID == c
            //            select s).FirstOrDefault();

                //    SitePeriodTable p = new SitePeriodTable()
                //    {
                //        SiteID = q.SiteID,
                //        MonMorning = this.checkBox1.Checked,
                //        MonAfternoon = this.checkBox2.Checked,
                //        MonNight = this.checkBox3.Checked,
                //        MonMidnight = this.checkBox4.Checked,
                //        TuesMorning = this.checkBox8.Checked,
                //        TuesAfternoon = this.checkBox7.Checked,
                //        TuesNight = this.checkBox6.Checked,
                //        TuesMidnight = this.checkBox5.Checked,
                //        WedMorning = this.checkBox12.Checked,
                //        WedAfternoon = this.checkBox11.Checked,
                //        WedNight = this.checkBox10.Checked,
                //        WedMidnight = this.checkBox9.Checked,
                //        ThurMorning = this.checkBox16.Checked,
                //        ThurAfternoon = this.checkBox15.Checked,
                //        ThurNight = this.checkBox14.Checked,
                //        ThurMidnight = this.checkBox13.Checked,
                //        FriMorning = this.checkBox20.Checked,
                //        FriAfternoon = this.checkBox19.Checked,
                //        FriNight = this.checkBox18.Checked,
                //        FriMidnight = this.checkBox17.Checked,
                //        SatMorning = this.checkBox24.Checked,
                //        SatAfternoon = this.checkBox23.Checked,
                //        SatNight = this.checkBox22.Checked,
                //        SatMidnight = this.checkBox21.Checked,
                //        SunMorning = this.checkBox28.Checked,
                //        SunAfternoon = this.checkBox27.Checked,
                //        SunNight = this.checkBox26.Checked,
                //        SunMidnight = this.checkBox25.Checked,
                //    };

                //    //add()添增紀錄時，只是將數據提交到內存
                //    db.SitePeriodTables.Add(p);


                //    //對數據庫的操作(包括數據的增刪改查) 完成之後，一定要執行 saveChange()方法，這樣數據才會提交到數據庫
                //    db.SaveChanges();
                //    MessageBox.Show("新增場地時段成功！");
            }
        }
    }
