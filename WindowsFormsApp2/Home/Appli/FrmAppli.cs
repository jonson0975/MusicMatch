using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2;

namespace prjGroup1Plus.summit
{
    public partial class FrmAppli : Form
    {
        public FrmAppli()
        {
            InitializeComponent();
            LoadStatusToComboBox();
            LoadDataToGridView();           
        }
        int selectedRowIndex = 0;

        private void LoadStatusToComboBox()
        {
            using (mdgroup1Entities db = new mdgroup1Entities())
            {
                try
                {
                    var q = from p in db.ApplicationStatusTables
                            select p;
                    foreach (var t in q)
                    {
                        this.comboBox1.Items.Add(t.RecruitmentStatus);
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void LoadDataToGridView()
        {
            try 
            {
                using (mdgroup1Entities db = new mdgroup1Entities())
                {
                    if (this.textBox1.Text == "" && this.textBox2.Text == "")
                    {
                        var q = from a in db.ApplicationRecordTables
                                join s in db.ApplicationStatusTables
                                on a.ApplicationStatusID equals s.ApplicationStatusID
                                select new { a.ApplicationRecordID, a.ProjectID, a.MemberID, s.RecruitmentStatus };
                        if (q != null)
                        {
                            this.dataGridView1.DataSource = q.ToList();
                            selectedRowIndex = this.dataGridView1.Rows.Count - 1;
                        }
                        
                    }
                    else if (int.TryParse(this.textBox1.Text, out int x) && this.textBox2.Text == "")
                    {
                        var q = from a in db.ApplicationRecordTables
                                join s in db.ApplicationStatusTables
                                on a.ApplicationStatusID equals s.ApplicationStatusID
                                where a.MemberID == x
                                select new { a.ApplicationRecordID, a.ProjectID, a.MemberID, s.RecruitmentStatus };
                        if (q != null)
                        {
                            this.dataGridView1.DataSource = q.ToList();
                            selectedRowIndex = this.dataGridView1.Rows.Count - 1;
                        }
                    }
                    else if (this.textBox1.Text == "" && int.TryParse(this.textBox2.Text, out int y))
                    {
                        var q = from a in db.ApplicationRecordTables
                                join s in db.ApplicationStatusTables
                                on a.ApplicationStatusID equals s.ApplicationStatusID
                                where a.ProjectID == y
                                select new { a.ApplicationRecordID, a.ProjectID, a.MemberID, s.RecruitmentStatus };
                        if (q != null)
                        {
                            this.dataGridView1.DataSource = q.ToList();
                            selectedRowIndex = this.dataGridView1.Rows.Count - 1;
                        }
                    }
                    else if (int.TryParse(this.textBox1.Text, out int x2) && int.TryParse(this.textBox2.Text, out int y2))
                    {
                        var q = from a in db.ApplicationRecordTables
                                join s in db.ApplicationStatusTables
                                on a.ApplicationStatusID equals s.ApplicationStatusID
                                where a.MemberID == x2 && a.ProjectID == y2
                                select new { a.ApplicationRecordID, a.ProjectID, a.MemberID, s.RecruitmentStatus };
                        if (q != null)
                        {
                            this.dataGridView1.DataSource = q.ToList();
                            selectedRowIndex = this.dataGridView1.Rows.Count - 1;
                        }
                    }                        
                }
            } 
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmAppliCreate f = new FrmAppliCreate();
            f.ShowDialog();
            LoadDataToGridView();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            using (mdgroup1Entities db = new mdgroup1Entities())
            {
                int.TryParse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString(), out int x);
                try
                {
                    var q = (from p in db.ApplicationRecordTables
                             where p.ApplicationRecordID == x
                             select p).FirstOrDefault();
                    if (q != null)
                    {
                        this.label7.Text = q.ApplicationRecordID.ToString();
                        this.textBox3.Text = q.MemberID.ToString();
                        this.textBox4.Text = q.ProjectID.ToString();
                        int statusSelected = q.ApplicationStatusID;
                        this.comboBox1.SelectedIndex = statusSelected-1;
                        this.richTextBox1.Text = q.ApplicationRecord;
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否修改專案?", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                int.TryParse(this.textBox3.Text, out int memid);
                int.TryParse(this.textBox4.Text, out int prjid);
                List<int> memIDs = null;
                List<int> prjIDs = null;
                using (mdgroup1Entities dbContext = new mdgroup1Entities())//獲取會員ID
                {
                    var w = from m in dbContext.MemberTables
                            select m.MemberID;
                    memIDs = w.ToList();
                }
                using (mdgroup1Entities dbContext = new mdgroup1Entities())//獲取專案ID
                {
                    var w = from m in dbContext.ProjectTables
                            select m.ProjectID;
                    prjIDs = w.ToList();
                }

                int statusSelect = this.comboBox1.SelectedIndex + 1;
                using (mdgroup1Entities db = new mdgroup1Entities())
                {
                    try
                    {
                        int TryCount = 0;
                        foreach (int id in memIDs)//確定會員ID存在與否
                        {
                            if (memid == id)
                            {
                                foreach (int id2 in prjIDs)//確定專案ID存在與否
                                {
                                    if (prjid == id2)
                                    {
                                        var q = (from p in db.ApplicationRecordTables
                                                 where p.ApplicationRecordID.ToString() == label7.Text
                                                 select p).FirstOrDefault();
                                        string s = "";

                                        if (q.MemberID.ToString() != textBox3.Text)
                                        {
                                            q.MemberID = Convert.ToInt32(textBox3.Text);
                                            s += ",會員ID";
                                        }
                                        if (q.ProjectID.ToString() != textBox4.Text)
                                        {
                                            q.ProjectID = Convert.ToInt32(textBox4.Text);
                                            s += ",專案ID";
                                        }
                                        if (q.ApplicationRecord != this.richTextBox1.Text)
                                        {
                                            q.ApplicationRecord = this.richTextBox1.Text;
                                            s += ",應徵描述";
                                        }
                                        if (q.ApplicationStatusID != statusSelect)
                                        {
                                            q.ApplicationStatusID = statusSelect;
                                            s += ",應徵狀態";
                                        }
                                        db.SaveChanges();
                                        MessageBox.Show($"本次修改 {s}");

                                        TryCount++;
                                    }
                                }
                            }
                        }
                        if (TryCount == 0)
                        {
                            MessageBox.Show("會員或專案不存在");
                        }

                        
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    LoadDataToGridView();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (int.TryParse(this.textBox1.Text, out int x)|| int.TryParse(this.textBox2.Text, out int y))
            {
                LoadDataToGridView();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否刪除應徵?", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                int.TryParse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString(), out int x);
                using (mdgroup1Entities db = new mdgroup1Entities())
                {
                    try
                    {
                        var q2 = (from p in db.ApplicationRecordTables
                                  where p.ApplicationRecordID == x
                                  select p).FirstOrDefault();
                        db.ApplicationRecordTables.Remove(q2);
                        db.SaveChanges();
                        MessageBox.Show("應徵刪除成功");
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
                if (selectedRowIndex > 0)
                {
                    selectedRowIndex--;
                    LoadDataToGridView();
                }
            }
        }
    }
}
