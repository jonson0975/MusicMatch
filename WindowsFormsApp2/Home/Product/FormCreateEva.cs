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
    public partial class FormCreateEva : Form
    {
        public FormCreateEva(int Pnums)
        {
            InitializeComponent();
            Pnum = Pnums;

            using (mdgroup1Entities db = new mdgroup1Entities())
            {
                var q2 = from w in db.MemberProductRelateAllTables
                         select new { w.RelationContent };
                foreach (var t in q2)
                {
                    this.comboBox1.Items.Add(t.RelationContent);
                }
            }
        }
        public int Pnum;

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否建立評論?", "確認", MessageBoxButtons.YesNo);//詢問視窗
            if (result == DialogResult.Yes)
            {
                try 
                {
                    int.TryParse(this.textBox1.Text, out int x);
                    using (mdgroup1Entities db = new mdgroup1Entities())
                    {
                        EvaluateAllTable eva = new EvaluateAllTable()
                        {
                            MemberID = x,
                            EvaluateContent = this.richTextBox1.Text,
                            RelationID = this.comboBox1.SelectedIndex +1,
                            ProductID = Pnum,
                            IssusingTime = DateTime.Now,
                        };
                        db.EvaluateAllTables.Add(eva);
                        db.SaveChanges();
                        MessageBox.Show("新增評論成功");
                        this.Close();
                    }
                } catch (Exception ex){ MessageBox.Show(ex.Message); }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
