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
            this.splitContainer2.Panel2.Controls.Add(MainClassForm);
            MainClassForm.Show();
        }
    }
}
