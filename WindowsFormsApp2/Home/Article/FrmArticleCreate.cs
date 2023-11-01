using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2;
//using test4;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace prjGroup1Article.Article
{
    public partial class FrmArticleCreate : Form
    {
        public FrmArticleCreate()
        {
            InitializeComponent();
            LoadDataToComboBox();
        }
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
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否新增文章?", "確認", MessageBoxButtons.YesNo);//詢問視窗
            if (result == DialogResult.Yes)
            {
                List<int>q = null;
                using (mdgroup1Entities dbContext = new mdgroup1Entities())
                {
                    var w = from m in dbContext.MemberTables
                        select m.MemberID;
                    q = w.ToList();
                }
                using (mdgroup1Entities dbContext2 = new mdgroup1Entities())
                {

                    try
                    {
                        int.TryParse(this.textBox1.Text, out int x);
                        int count = 0;
                        foreach (var item in q)
                        {
                            if (item == x)
                            {
                                MusicarticleTable table = new MusicarticleTable()
                                {
                                    memberID = x,
                                    article = richTextBox1.Text,
                                    StyleID = this.comboBox1.SelectedIndex + 1
                                };
                                dbContext2.MusicarticleTables.Add(table);
                                dbContext2.SaveChanges();
                                MessageBox.Show("文章新增成功");
                                this.Close();
                                count++;
                            }
                            
                        }
                        if (count <1)
                        {
                            MessageBox.Show("會員不存在");
                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否放棄內容?", "確認", MessageBoxButtons.YesNo);//詢問視窗
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
