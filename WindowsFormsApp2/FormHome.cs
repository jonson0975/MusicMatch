using prjGroup1Article.Article;
using prjGroup1Plus.summit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.Project.ForAdm;
using static WindowsFormsApp2.FormLogin;

namespace WindowsFormsApp2
{
    public partial class FormHome : Form
    {
        public FormHome()
        {
            InitializeComponent();
        }

        Form SaveForm = null;

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("確認是否離開此視窗", "",MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Close();
                closefather();
            }
        }

        public event childclose closefather;

        private void FormHome_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.splitContainer2.Panel2.Controls.Clear();
            FormMember member = new FormMember();
            member.FormBorderStyle = FormBorderStyle.None;
            member.Dock = DockStyle.Fill;
            member.TopLevel = false;
            this.splitContainer2.Panel2.Controls.Add(member);
            AddAndCheckForm(member);
        }

        private void AddAndCheckForm(FormMember member)
        {
       
            if (SaveForm != null)
            {
                SaveForm.Close();
                SaveForm = null;
            }
            member.TopLevel = false;
            splitContainer2.Panel2.Controls.Add(member);
            member.Show();

            SaveForm = member;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.splitContainer2.Panel2.Controls.Clear();
            FrmAdmProjectMenager project = new FrmAdmProjectMenager();
            project.FormBorderStyle = FormBorderStyle.None;
            project.Dock = DockStyle.Fill;
            project.TopLevel = false;
            this.splitContainer2.Panel2.Controls.Add(project);
            project.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.splitContainer2.Panel2.Controls.Clear();
            FormClassManage formClassManage = new FormClassManage();
            formClassManage.FormBorderStyle = FormBorderStyle.None;
            formClassManage.Dock = DockStyle.Fill;
            formClassManage.TopLevel = false;
            this.splitContainer2.Panel2.Controls.Add(formClassManage);
            formClassManage.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.splitContainer2.Panel2.Controls.Clear();
            FormPlace FormPlace = new FormPlace();
            FormPlace.FormBorderStyle = FormBorderStyle.None;
            FormPlace.Dock = DockStyle.Fill;
            FormPlace.TopLevel = false;
            this.splitContainer2.Panel2.Controls.Add(FormPlace);
            FormPlace.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.splitContainer2.Panel2.Controls.Clear();
            FormCoupon FormCoupon = new FormCoupon();
            FormCoupon.FormBorderStyle = FormBorderStyle.None;
            FormCoupon.Dock = DockStyle.Fill;
            FormCoupon.TopLevel = false;
            this.splitContainer2.Panel2.Controls.Add(FormCoupon);
            FormCoupon.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.splitContainer2.Panel2.Controls.Clear();
            FormProductTable FormProductTable = new FormProductTable();
            FormProductTable.FormBorderStyle = FormBorderStyle.None;
            FormProductTable.Dock = DockStyle.Fill;
            FormProductTable.TopLevel = false;
            this.splitContainer2.Panel2.Controls.Add(FormProductTable);
            FormProductTable.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.splitContainer2.Panel2.Controls.Clear();
            FrmArticle FrmArticle = new FrmArticle();
            FrmArticle.FormBorderStyle = FormBorderStyle.None;
            FrmArticle.Dock = DockStyle.Fill;
            FrmArticle.TopLevel = false;
            this.splitContainer2.Panel2.Controls.Add(FrmArticle);
            FrmArticle.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.splitContainer2.Panel2.Controls.Clear();
            FrmAppli FrmAppli = new FrmAppli();
            FrmAppli.FormBorderStyle = FormBorderStyle.None;
            FrmAppli.Dock = DockStyle.Fill;
            FrmAppli.TopLevel = false;
            this.splitContainer2.Panel2.Controls.Add(FrmAppli);
            FrmAppli.Show();
        }
    }
}
