using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using WindowsFormsApp2.Class;
using WindowsFormsApp2.g1DataSetTableAdapters;
using WindowsFormsApp2.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace WindowsFormsApp2
{
    public partial class FormClassManage : Form
    {
        public FormClassManage()
        {
            InitializeComponent();

            classAllTableBindingSource.DataSource = g1DataSet1.ClassAllTable;
            pictureBox1.DataBindings.Add("Image", classAllTableBindingSource, "ClassPicture", true);
            pictureBox2.DataBindings.Add("Image", classAllTableBindingSource, "ClassPicture", true);
            classAllTableTableAdapter.Fill(g1DataSet1.ClassAllTable);

            //處理拖放操作
            this.pictureBox2.AllowDrop = true;//可以拖放操作
            this.pictureBox2.DragEnter += PictureBox2_DragEnter;//拖曳至物件時觸發
            this.pictureBox2.DragDrop += PictureBox2_DragDrop;//鬆手後觸發
        }

        mdgroup1Entities dbContext = new mdgroup1Entities();//實體資料模型

        //清除預設文字
        private void textBox5_Enter(object sender, EventArgs e)
        {
            this.textBox5.Text = string.Empty;
        }
        private void textBox3_Enter(object sender, EventArgs e)
        {
            this.textBox3.Text = string.Empty;
        }

        //處理圖片拖放操作
        private void PictureBox2_DragDrop(object sender, DragEventArgs e)
        {
            string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop);
            //e.Data.GetData(DataFormats.FileDrop)是該圖片的位址

            this.pictureBox2.Image = Image.FromFile(filenames[0]);
            //依照位址換圖片
        }
        private void PictureBox2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }


        //===(新增頁面)=======================================================================


        //新增資料
        private void button1_Click(object sender, EventArgs e)
        {
            //實體db insert
            int memberID = int.Parse(this.memberIDTextBox1.Text);
            //DateTime ClassTime = this.classTimeDateTimePicker1.Value;
            DateTime StartDate = startDateDateTimePicker.Value;
            DateTime EndDate = endDateDateTimePicker.Value;
            decimal Price = Convert.ToDecimal(this.priceTextBox1.Text);
            string ClassName = this.classNameTextBox1.Text;
            string ClassDescription = this.classDescriptionTextBox1.Text;
            int SiteID = int.Parse(siteIDTextBox.Text);

            //將圖片存成二進制物件
            //==========================================
            byte[] bytes;

            //"Stream"資料流，建立支援的存放區為記憶體的資料流
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            this.pictureBox2.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            bytes = ms.GetBuffer();

            ClassAllTable Class = new ClassAllTable()//類別ClassAllTable，系統自己建的，在mdModel下
            {
                MemberID = memberID,
                StartDate = StartDate,
                EndDate = EndDate,
                Price = Price,
                ClassName = ClassName,
                ClassDescription = ClassDescription,
                ClassPicture = bytes,
                SiteID = SiteID,
            };

            try
            {
                dbContext.ClassAllTables.Add(Class);
                dbContext.SaveChanges();

                MessageBox.Show("新增資料成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        //選取圖片
        private void button2_Click(object sender, EventArgs e)
        {
            //如果使用者點了OK(確認)
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image image = Image.FromFile(this.openFileDialog1.FileName);
                if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
                {
                    //把圖片塞到pictureBox1
                    this.pictureBox2.Image = Image.FromFile(this.openFileDialog1.FileName);
                }
                else
                {
                    MessageBox.Show("只可以使用Jpeg");
                }
            }
        }


        //===(查詢、修改頁面)==================================================================


        //BindingNavigator
        private void classAllTableBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.classAllTableBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.g1DataSet1);
        }

        //選取圖片
        private void button4_Click(object sender, EventArgs e)
        {
            //如果使用者點了OK(確認)
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //把圖片塞到pictureBox1
                this.pictureBox1.Image = Image.FromFile(this.openFileDialog1.FileName);
            }
        }

        //修改資料
        private void button5_Click(object sender, EventArgs e)
        {
            //實體db 修改update
            int ID = int.Parse(classIDTextBox.Text);//找到classID

            //將圖片存成二進制物件
            //==========================================
            byte[] bytes;

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            if(this.pictureBox1.Image != null)
            {
                this.pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            bytes = ms.GetBuffer();

            var Class = (from c in dbContext.ClassAllTables
                        where c.ClassID == ID
                        select c).FirstOrDefault();//找我要改的項目

            Class.MemberID = int.Parse(this.memberIDTextBox.Text);
            Class.StartDate = this.dateTimePicker2.Value;
            Class.EndDate = this.dateTimePicker1.Value;
            Class.Price = Convert.ToDecimal(this.priceTextBox.Text);
            Class.ClassName = this.classNameTextBox.Text;
            Class.ClassDescription = this.classDescriptionTextBox.Text;
            Class.ClassPicture = bytes;
            Class.SiteID = int.Parse(siteIDTextBox1.Text);

            try
            {
                dbContext.SaveChanges();

                MessageBox.Show("修改資料成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        //===(搜尋按鈕)=======================================================================


        //找課程ID
        private void button3_Click(object sender, EventArgs e)
        {
            classAllTableBindingSource.DataSource = g1DataSet1.ClassAllTable;
            int a = Convert.ToInt32(this.textBox1.Text);
            int index = classAllTableBindingSource.Find("ClassID", a);//是否有符合這個條件的資料
            if (this.textBox1.Text != null && index != -1)//index != -1就代表沒有符合的資料
            {
                classAllTableBindingSource.Position = index;
            }
            else
            {
                MessageBox.Show("未輸入ID，或此ID不存在");
            }

            var q = from c in dbContext.ClassAllTables
                    where c.ClassID == a
                    select c;
            this.dataGridView1.DataSource = q.ToList();
        }

        //找會員ID
        private void button6_Click(object sender, EventArgs e)
        {
            classAllTableBindingSource.DataSource = g1DataSet1.ClassAllTable;
            int a = Convert.ToInt32(this.textBox2.Text);
            int index = classAllTableBindingSource.Find("MemberID", a);

            if (this.textBox2.Text != null && index != -1)
            {
                classAllTableBindingSource.Position = index;
            }
            else
            {
                MessageBox.Show("未輸入ID，或此ID沒有開課程");
            }

            var q = from c in dbContext.ClassAllTables
                    where c.MemberID == a
                    select c;
            this.dataGridView1.DataSource = q.ToList();
        }

        //找日期區間
        private void button7_Click(object sender, EventArgs e)
        {
            classAllTableBindingSource.DataSource = g1DataSet1.ClassAllTable;
            DateTime d1 = dateTimePicker3.Value;
            DateTime d2 = dateTimePicker4.Value;
            var q = from c in dbContext.ClassAllTables
                    where c.StartDate >= d1 && c.EndDate <= d2
                    select c;
            if (dateTimePicker3.Value != null && dateTimePicker4.Value != null)
            {
                classAllTableBindingSource.DataSource = q.ToList();//讓BindingSource變成選取好的資料
                classAllTableBindingSource.Position = 0;//顯示第一筆資料
            }
            else
            {
                MessageBox.Show("未輸入時間，或沒有課程符合條件");
            }
            this.dataGridView1.DataSource = q.ToList();
        }

        //找價錢區間
        private void button8_Click(object sender, EventArgs e)
        {
            classAllTableBindingSource.DataSource = g1DataSet1.ClassAllTable;//重新連線
            int p1 = Convert.ToInt32(this.textBox5.Text);
            int p2 = Convert.ToInt32(this.textBox3.Text);
            var q = from c in dbContext.ClassAllTables
                    where c.Price >= p1 && c.Price <= p2
                    select c;
            if (textBox5.Text != null && textBox3.Text != null)
            {
                classAllTableBindingSource.DataSource = q.ToList();//讓BindingSource變成選取好的資料
                classAllTableBindingSource.Position = 0;//顯示第一筆資料
            }
            else
            {
                MessageBox.Show("未輸入價錢，或沒有課程符合條件");
            }
            this.dataGridView1.DataSource = q.ToList();
        }

        //找課程名稱
        private void button9_Click(object sender, EventArgs e)
        {
            classAllTableBindingSource.DataSource = g1DataSet1.ClassAllTable;
            string s = this.textBox4.Text;
            var q = from c in dbContext.ClassAllTables
                    where c.ClassName.Contains(s)
                    select c;
            if (textBox4.Text != null)
            {
                classAllTableBindingSource.DataSource = q.ToList();//讓BindingSource變成選取好的資料
                classAllTableBindingSource.Position = 0;//顯示第一筆資料
            }
            else
            {
                MessageBox.Show("未輸入名稱，或沒有課程符合條件");
            }
            this.dataGridView1.DataSource = q.ToList();
        }
    }
}
