using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using WindowsFormsApp2.Dto;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp2.Member
{
    public partial class FormSocial : Form
    {

        //private List<SocialDto> data;

        public FormSocial()
        {
            InitializeComponent();
            mdgroup1Entities db = new mdgroup1Entities();
            var data1 = from s in db.SocialRelationTables
                        select new
                        {
                            主會員 = s.FirstMemberID,
                            對象會員 = s.SecondMemberID,
                            關係狀態ID = s.RelationshipStatusID,
                            社交關係 = s.SocialRelationID,

                        };
            this.dataGridView1.DataSource = data1.ToList();
            this.buttonReload.Click += this.button1_Click;

            //==================================================
            mdgroup1Entities db2 = new mdgroup1Entities();
            var data = from d in db.MemberRelationshipAllTables
                       select new
                       {
                           ID = d.RelationshipStatusID,
                           狀態 = d.RelationshipStatusName
                       };
            this.dataGridView2.DataSource = data.ToList();

            this.dataGridView1.CellClick += dataGridView1_CellContentClick;

        }
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            using (mdgroup1Entities db = new mdgroup1Entities())
            {
                int s = int.Parse(textBox1.Text);
                int s1 = int.Parse(textBox2.Text);

                if (s1 == s)
                {
                    MessageBox.Show("主會員與副會員不可以同時出現");
                    return;
                }

                var create = from r in db.SocialRelationTables
                             where r.FirstMemberID == s && r.SecondMemberID == s1 //搜尋會員1與會員2之間關係
                             select new
                             {
                                 主會員 = r.FirstMemberID,
                                 對象會員 = r.SecondMemberID,
                                 關係狀態ID = r.RelationshipStatusID,
                             };

                dataGridView1.DataSource = create.ToList();

            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否修改會員關係?", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (mdgroup1Entities db = new mdgroup1Entities())
                {
                    int firstMemberID = int.Parse(textBox1.Text);
                    int SecondMemberID = int.Parse(textBox2.Text);

                    bool relationExists = db.SocialRelationTables.Any(t =>
                    (t.FirstMemberID == firstMemberID && t.SecondMemberID == SecondMemberID) ||
                    (t.FirstMemberID == SecondMemberID && t.SecondMemberID == firstMemberID));

                    if (relationExists)
                    {
                        MessageBox.Show("這個會員關係已經存在了，不能重複建立。", "警告", MessageBoxButtons.OK);
                    }
                    else
                    {
                        var update = db.SocialRelationTables.FirstOrDefault(x => x.FirstMemberID == firstMemberID);
                        if (update != null)
                        {
                            update.SecondMemberID = int.Parse(textBox2.Text);
                            update.RelationshipStatusID = int.Parse(textBox3.Text);

                            db.SaveChanges();
                            MessageBox.Show("修改成功");
                        }
                    }
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否確定刪除該會員關係?", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (mdgroup1Entities db = new mdgroup1Entities())
                {
                    int firstMemberID = int.Parse(textBox1.Text);
                    var entityToDelete = db.SocialRelationTables.FirstOrDefault(x => x.FirstMemberID == firstMemberID);

                    if (entityToDelete != null)
                    {
                        db.SocialRelationTables.Remove(entityToDelete);
                        db.SaveChanges();
                        MessageBox.Show("刪除成功");

                    }
                    else
                    {
                        MessageBox.Show("找不到該會員關係");
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["主會員"].Value?.ToString();
                textBox2.Text = row.Cells["對象會員"].Value?.ToString();
                textBox3.Text = row.Cells["關係狀態ID"].Value?.ToString();
            }
        }



        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void buttoncreate_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否新增會員關係?", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (mdgroup1Entities db = new mdgroup1Entities())
                {
                    int FirstMemberID = int.Parse(textBox1.Text);
                    int SecondMemberID = int.Parse(textBox2.Text);              
                    int RelationshipStatusID = int.Parse(textBox3.Text);

                    bool relationExists = db.SocialRelationTables.Any
                      (t => (t.FirstMemberID == FirstMemberID
                    && t.SecondMemberID == SecondMemberID)
                      || (t.FirstMemberID == SecondMemberID && t.SecondMemberID == FirstMemberID));




                    if (relationExists)
                    {
                        MessageBox.Show("這個會員關係已經存在了，不能重複建立。", "警告", MessageBoxButtons.OK);
                    }
                   
                    else
                    {
                        SocialRelationTable t = new SocialRelationTable()
                        {
                            FirstMemberID = FirstMemberID,
                            SecondMemberID = SecondMemberID,
                            RelationshipStatusID = RelationshipStatusID
                        };
                        db.SocialRelationTables.Add(t);
                        db.SaveChanges();
                        MessageBox.Show("會員關係已經新增成功。", "警告", MessageBoxButtons.OK);
                    }

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           using(mdgroup1Entities db = new mdgroup1Entities())
            {
                var data = from c in db.SocialRelationTables
                           select new
                           {
                               主會員=c.FirstMemberID,
                               對象會員=c.SecondMemberID,
                               關係狀態ID=c.RelationshipStatusID,
                               社交關係=c.SocialRelationID
                           };
                this.dataGridView1.DataSource= data.ToList();
            }
        }
    }
}
