using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.Main.Class;
using WindowsFormsApp2.Main.Member;

namespace WindowsFormsApp2
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.splitContainer2.Panel2.Controls.Clear();
            MainClassForm MainClassForm = new MainClassForm();
            MainClassForm.FormBorderStyle = FormBorderStyle.None;
            MainClassForm.Dock = DockStyle.Fill;
            MainClassForm.TopLevel = false;
            MainClassForm.AutoScroll = true;
            this.splitContainer2.Panel2.Controls.Add(MainClassForm);
            MainClassForm.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.splitContainer2.Panel2.Controls.Clear();
            MainMemberForm MainMemberForm = new MainMemberForm();
            MainMemberForm.FormBorderStyle = FormBorderStyle.None;
            MainMemberForm.Dock = DockStyle.Fill;
            MainMemberForm.TopLevel = false;
            this.splitContainer2.Panel2.Controls.Add(MainMemberForm);
            MainMemberForm.Show();
        }
    }
}
