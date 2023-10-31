using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class FormEvaluate : Form
    {
        public FormEvaluate(int Pnums)
        {
            InitializeComponent();
            Pnum = Pnums;
        }
        public int Pnum;
        private void FormEvaluate_Load(object sender, EventArgs e)
        {
            this.label1.Text = $"作品ID : {Pnum}";
            using (mdgroup1Entities db = new mdgroup1Entities())
            {
                var q2 = from w in db.MemberProductRelateAllTables
                         select new { w.RelationContent };
                foreach (var t in q2)
                {
                    this.comboBox1.Items.Add(t.RelationContent);
                }
            }
            LoadEvaToGrid();
            
        }

        private void LoadEvaToGrid()
        {
            using (mdgroup1Entities db = new mdgroup1Entities())
            {
                var q = from x in db.EvaluateAllTables
                        join y in db.MemberProductRelateAllTables
                        on x.RelationID equals y.RelationID
                        where x.ProductID == Pnum
                        select new { x.EvaluateID, y.RelationContent, x.MemberID };
                this.dataGridView1.DataSource = q.ToList();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int datap = Pnum;
            FormCreateEva createEva = new FormCreateEva(datap);  
            createEva.Owner=this;
            createEva.ShowDialog();
            LoadEvaToGrid();

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int.TryParse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString(), out int x);
            using (mdgroup1Entities db = new mdgroup1Entities())
            {
                var q = (from w in db.EvaluateAllTables
                          where w.EvaluateID == x
                          select w).FirstOrDefault();
                if (q != null)
                {
                    this.textBox1.Text = q.MemberID.ToString();
                    this.comboBox1.SelectedIndex = q.RelationID.Value - 1;
                    this.richTextBox1.Text = q.EvaluateContent;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否修改評論?", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                int.TryParse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString(), out int x);
                int statusSelect = this.comboBox1.SelectedIndex + 1;
                using (mdgroup1Entities db = new mdgroup1Entities())
                {
                    int.TryParse(this.textBox1.Text, out int y);
                    try 
                    {
                        var q = (from w in db.EvaluateAllTables
                                 where w.EvaluateID == x
                                 select w).FirstOrDefault();
                        q.MemberID = y;
                        q.RelationID = this.comboBox1.SelectedIndex + 1;
                        q.EvaluateContent = this.richTextBox1.Text;
                        db.SaveChanges();
                        MessageBox.Show("評論修改成功");
                    } catch (Exception ex){ MessageBox.Show(ex.Message); }
                    LoadEvaToGrid();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否刪除評論?", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                int.TryParse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString(), out int x);
                using (mdgroup1Entities db = new mdgroup1Entities())
                {
                    try 
                    {
                        var q = (from w in db.EvaluateAllTables
                                 where w.EvaluateID == x
                                 select w).FirstOrDefault();
                        db.EvaluateAllTables.Remove(q);
                        db.SaveChanges();
                        MessageBox.Show("成功刪除評論");
                    } catch (Exception ex){ MessageBox.Show(ex.Message); }
                }
                LoadEvaToGrid();
            }
            
        }
    }
}
