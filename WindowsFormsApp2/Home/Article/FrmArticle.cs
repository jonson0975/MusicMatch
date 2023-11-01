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
//using test4;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace prjGroup1Article.Article
{
    public partial class FrmArticle : Form
    {
        int selectedArticleID = 0;
        public FrmArticle()
        {
            InitializeComponent();
        }

        private void FrmArticle_Load(object sender, EventArgs e)
        {
            LoadDataToComboBox();
            LoadDataToGritView();
        }
        int selectedRowIndex = 0;

        private void LoadDataToComboBox()
        {
            using (mdgroup1Entities db = new mdgroup1Entities())
            {
                try 
                {
                    var q = from p in db.StyleAllTables
                            select p.StyleName;
                    foreach (var item in q) 
                    {
                        this.comboBox1.Items.Add(item);
                    }
                } catch (Exception ex){ MessageBox.Show(ex.Message); }
            }
        }

        private void LoadDataToGritView()
        {
            this.dataGridView1.DataSource = null;
            using (mdgroup1Entities db = new mdgroup1Entities())
            {
                try
                {
                    if (int.TryParse(textBox1.Text, out int x))
                    {
                        var q = from p in db.MusicarticleTables
                                where p.memberID == x
                                select new { p.MusicarticleID, p.memberID };
                        this.dataGridView1.DataSource = q.ToList();
                        selectedRowIndex = this.dataGridView1.Rows.Count - 1;
                    }
                    else 
                    {
                        var q = from p in db.MusicarticleTables
                                select new { p.MusicarticleID, p.memberID };
                        this.dataGridView1.DataSource= q.ToList();
                        selectedRowIndex = this.dataGridView1.Rows.Count - 1;
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (int.TryParse(this.textBox1.Text, out int x))
            {
                LoadDataToGritView();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            using (mdgroup1Entities db = new mdgroup1Entities())
            {
                int.TryParse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString(), out int x);
                try
                {
                    var q = (from p in db.MusicarticleTables
                             where p.MusicarticleID == x
                             select p).FirstOrDefault();
                    if (q != null)
                    {
                        LoadPIDToPicList(x);
                        this.label3.Text = q.memberID.ToString();                        
                        int styleSelected = q.StyleID;
                        this.comboBox1.SelectedIndex = styleSelected - 1;
                        this.richTextBox1.Text = q.article;
                        selectedArticleID = q.MusicarticleID;
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void LoadPIDToPicList(int x)
        {
            this.listBox1.Items.Clear();
            using (mdgroup1Entities db = new mdgroup1Entities())
            {
                try
                {
                    var q = from p in db.MusicarticlePics
                            where p.MusicarticleID == x
                            select p.MusicarticlePicID;
                    foreach (var item in q)
                    {
                        this.listBox1.Items.Add(item);
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedItem.ToString() != "")
            {
                try 
                {
                    using (mdgroup1Entities db = new mdgroup1Entities())
                    {
                        var q = (from p in db.MusicarticlePics
                                where p.MusicarticlePicID.ToString() == this.listBox1.SelectedItem.ToString()
                                select new { p.MusicarticlePic1 }).FirstOrDefault();
                        if (q != null)
                        {
                            byte[] bytes = (byte[])q.MusicarticlePic1;
                            System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                            this.pictureBox1.Image = Image.FromStream(ms);
                        }
                    }
                } catch (Exception ex){ MessageBox.Show(ex.Message); }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (selectedArticleID != 0)
            {
                FrmArticlePicNew f = new FrmArticlePicNew(selectedArticleID);
                f.ShowDialog();
                LoadPIDToPicList(selectedArticleID);
            }
            else
            {
                MessageBox.Show("請先選擇欲新增圖片的文章");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmArticleCreate f = new FrmArticleCreate();
            f.ShowDialog();
            LoadDataToGritView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否修改文章?", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                int.TryParse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString(), out int x);
                int statusStyleID = this.comboBox1.SelectedIndex + 1;
                using (mdgroup1Entities db = new mdgroup1Entities())
                {
                    try
                    {
                        var q = (from p in db.MusicarticleTables
                                 where p.MusicarticleID == x
                                 select p).FirstOrDefault();
                        string s = "";

                        if (q.article != this.richTextBox1.Text)
                        {
                            q.article = this.richTextBox1.Text;
                            s += ",文章內容";
                        }
                        if (q.StyleID != statusStyleID)
                        {
                            q.StyleID = statusStyleID;
                            s += ",評論風格";
                        }
                        db.SaveChanges();
                        MessageBox.Show($"本次修改 {s}");
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否刪除文章?", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                int.TryParse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString(), out int x);
                using (mdgroup1Entities db = new mdgroup1Entities())
                {
                    try 
                    {
                        var q = from p in db.MusicarticlePics
                                where p.MusicarticleID == x
                                select p;
                        foreach (var p in q) 
                        {
                            db.MusicarticlePics.Remove(p);
                        }
                        try
                        {
                            var q2 = (from p in db.MusicarticleTables
                                     where p.MusicarticleID == x
                                     select p).FirstOrDefault();
                            db.MusicarticleTables.Remove(q2);
                            db.SaveChanges();
                            MessageBox.Show("文章刪除成功");
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    } catch (Exception ex) { MessageBox.Show(ex.Message); }
                    
                }
                if (selectedRowIndex > 0)
                {
                    selectedRowIndex--;
                    LoadDataToGritView();
                }
            }
        }
    }
}
