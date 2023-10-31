using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp2.Skill
{
    public partial class FrmSkill : Form
    {
        mdgroup1Entities db = new mdgroup1Entities();
        public FrmSkill()
        {
            InitializeComponent();
            var a =
                from n in db.SkillDetailTables
                select new
                {
                    技能ID = n.SkillID,
                    技能名稱 = n.SkillName,
                    技能描述 = n.SkillDescription
                };
            dataGridView1.DataSource = a.ToList();

        }
        private void LoadProjectToGridView()
        {
            this.dataGridView1.DataSource = null;
            using (mdgroup1Entities db = new mdgroup1Entities())
            {
                var q = from p in db.SkillDetailTables
                        select new { 技能ID = p.SkillID, 技能名稱 = p.SkillName, 技能描述 = p.SkillDescription };
                this.dataGridView1.DataSource = q.ToList();
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int.TryParse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString(), out int x);
            try
            {
                var q = (
                    from p in db.SkillDetailTables
                    where p.SkillID == x
                    select new
                    {
                        p.SkillID,
                        p.SkillName,
                        p.SkillDescription,
                        p.SkillDetailPicture
                    }).FirstOrDefault();

                if (q != null)
                {
                    //===============================================================
                    //圖片叫出來
                    byte[] bytes = (byte[])q.SkillDetailPicture;
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                    this.pictureBox1.Image = Image.FromStream(ms);
                    //===============================================================
                    this.label5.Text = q.SkillID.ToString();
                    this.textBox2.Text = q.SkillName;
                    this.textBox3.Text = q.SkillDescription;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否修改技能?", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                {
                    try
                    {
                        var q = (from p in db.SkillDetailTables
                                 where p.SkillID.ToString() == label5.Text
                                 select p).FirstOrDefault();
                        string s = "";

                        if (q.SkillName != textBox2.Text)
                        {
                            q.SkillName = textBox2.Text;
                            s += " 技能名稱 ";
                        }
                        if (q.SkillDescription != textBox3.Text)
                        {
                            q.SkillDescription = textBox3.Text;
                            s += " 技能描述 ";
                        }
                        db.SaveChanges();
                        MessageBox.Show($"本次修改 {s}");
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            LoadProjectToGridView();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox2.Image = Image.FromFile(this.openFileDialog1.FileName);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //=================
            byte[] bytes;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            this.pictureBox2.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            bytes = ms.GetBuffer();
            //=================
            using (mdgroup1Entities db = new mdgroup1Entities())
            {
                SkillDetailTable p = new SkillDetailTable()
                {
                    SkillName = this.textBox13.Text,
                    SkillDescription = this.textBox12.Text,
                    SkillDetailPicture = bytes
                };
                db.SkillDetailTables.Add(p);
                db.SaveChanges();
            }
            MessageBox.Show("新增技能成功！");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否刪除技能?", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (mdgroup1Entities db = new mdgroup1Entities())
                {
                    try
                    {
                        var q = (from p in db.SkillDetailTables
                                 where p.SkillID.ToString() == label5.Text
                                 select p).FirstOrDefault();
                        db.SkillDetailTables.Remove(q);
                        db.SaveChanges();
                        MessageBox.Show("已刪除技能ID：" + q.SkillID);
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
        }
    }
}