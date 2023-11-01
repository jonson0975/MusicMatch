using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2.Project.ForAdm
{
    public partial class FrmAdmProjectMenager : Form
    {
        public FrmAdmProjectMenager()
        {
            InitializeComponent();
        }

        private void FrmAdmProjectMenager_Load(object sender, EventArgs e)
        {
            LoadStatusToComboBox();
            LoadProjectToGridView();
        }

        private void LoadStatusToComboBox()
        {
            using (mdgroup1Entities db = new mdgroup1Entities())
            {
                try
                {
                    var q = from p in db.ProjectStatusAllTables
                            select p;
                    foreach (var t in q)
                    {
                        this.comboBox1.Items.Add(t.StatusName);
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

         int selectedRowIndex = 0;//TODO選擇項目紀錄

        private void LoadProjectToGridView()
        {
            this.dataGridView1.DataSource = null;
            using (mdgroup1Entities db = new mdgroup1Entities())
            {
                try
                {
                    if (this.textBox3.Text == "" && this.textBox4.Text == "")
                    {
                        var q = from p in db.ProjectTables
                                select new { p.ProjectID, p.ProjectName, p.ReleaseMemberNumber };
                        this.dataGridView1.DataSource = q.ToList();
                    }
                    else if (int.TryParse(this.textBox3.Text, out int x))
                    {
                        var q = from p in db.ProjectTables
                                where p.ReleaseMemberNumber == x
                                select new { p.ProjectID, p.ProjectName, p.ReleaseMemberNumber };
                        this.dataGridView1.DataSource = q.ToList();
                    }
                    else if (int.TryParse(this.textBox4.Text, out int y))
                    {
                        var q = from p in db.ProjectTables
                                where p.ProjectID == y
                                select new { p.ProjectID, p.ProjectName, p.ReleaseMemberNumber };
                        this.dataGridView1.DataSource = q.ToList();
                    }
                    else if (int.TryParse(this.textBox3.Text, out int w) && int.TryParse(this.textBox4.Text, out int e))
                    {
                        var q = from p in db.ProjectTables
                                where p.ReleaseMemberNumber == w && p.ProjectID == e
                                select new { p.ProjectID, p.ProjectName, p.ReleaseMemberNumber };
                        this.dataGridView1.DataSource = q.ToList();
                    }
                    else 
                    { 
                        MessageBox.Show("搜尋條件有誤");
                        this.textBox3.Text = "";
                        this.textBox4.Text = "";
                        var q = from p in db.ProjectTables
                                select new { p.ProjectID, p.ProjectName, p.ReleaseMemberNumber };
                        this.dataGridView1.DataSource = q.ToList();
                    }
                    //if (selectedRowIndex > 0)
                    //{
                    //    this.dataGridView1.Rows[selectedRowIndex].Selected = true;
                    //}
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            using (mdgroup1Entities db = new mdgroup1Entities())
            {
                int.TryParse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString(), out int x);
                try 
                {
                    var q = (from p in db.ProjectTables
                             where p.ProjectID == x
                             select p).FirstOrDefault();
                    if (q != null)
                    {
                        this.textBox1.Text = q.ProjectName;
                        this.textBox2.Text = q.Budget;
                        this.label14.Text = q.ReleaseMemberNumber.ToString();
                        this.label13.Text = q.ProjectID.ToString();
                        this.dateTimePicker1.Value = q.ProductionPeriod.Value;
                        this.dateTimePicker2.Value = q.CreateTime.Value;
                        this.richTextBox1.Text = q.ProjectDescription;
                        int statusSelect = q.ProjectStatus.Value;
                        this.comboBox1.SelectedIndex = statusSelect-1;
                        //================================
                        if (q.ProjectPicture != null)
                        {
                            byte[] bytes = (byte[])q.ProjectPicture;
                            System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                            this.pictureBox1.Image = Image.FromStream(ms);
                        }
                        //================================
                    }
                } catch (Exception ex) { MessageBox.Show(ex.Message); }
            }

            //for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //{
            //    if (dataGridView1.Rows[i].Selected)
            //    {
            //        selectedRowIndex = i;
            //        break;
            //    }
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmCreateProject f = new FrmCreateProject();
            f.ShowDialog();
            selectedRowIndex = this.dataGridView1.Rows.Count - 1;
            LoadProjectToGridView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否修改專案?", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                int statusSelect = this.comboBox1.SelectedIndex + 1;
                using (mdgroup1Entities db = new mdgroup1Entities())
                {
                    try 
                    {
                        var q = (from p in db.ProjectTables
                                where p.ProjectID.ToString() == label13.Text
                                select p).FirstOrDefault();
                        string s = "";

                        if (q.ProjectName != textBox1.Text)
                        {
                            q.ProjectName = textBox1.Text;
                            s += ",專案名稱";
                        }
                        if (q.Budget != textBox2.Text)
                        {
                            q.Budget = textBox2.Text;
                            s += ",專案預算";
                        }
                        if (q.ProductionPeriod != this.dateTimePicker1.Value)
                        {
                            q.ProductionPeriod = this.dateTimePicker1.Value;
                            s += ",專案工期";
                        }
                        if (q.CreateTime != this.dateTimePicker2.Value)
                        {
                            q.CreateTime = this.dateTimePicker2.Value;
                            s += ",創建時間";
                        }
                        if (q.ProjectDescription != this.richTextBox1.Text)
                        {
                            q.ProjectDescription = this.richTextBox1.Text;
                            s += ",專案描述";
                        }
                        if (q.ProjectStatus != statusSelect)
                        {
                            q.ProjectStatus = statusSelect;
                            s += ",專案狀態";
                        }
                        db.SaveChanges();
                        MessageBox.Show($"本次修改 {s}");
                    }
                    catch (Exception ex){ MessageBox.Show(ex.Message); }
                    LoadProjectToGridView();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadProjectToGridView();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否刪除本專案?", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (mdgroup1Entities db = new mdgroup1Entities())
                {
                    try 
                    {
                        var q = (from p in db.ProjectTables
                                where p.ProjectID.ToString() == label13.Text
                                select p).FirstOrDefault();
                        db.ProjectTables.Remove(q);
                        db.SaveChanges();
                        MessageBox.Show("專案刪除成功");
                    } catch (Exception ex){ MessageBox.Show(ex.Message); }
                }
                if (selectedRowIndex > 0) 
                {
                    selectedRowIndex--;
                    LoadProjectToGridView();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.textBox4.Text = "";
            LoadProjectToGridView();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.textBox3.Text = "";
            LoadProjectToGridView();
        }
    }
}
