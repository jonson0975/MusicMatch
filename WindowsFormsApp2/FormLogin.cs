using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace WindowsFormsApp2
{
    public partial class FormLogin : Form
    {  
        public FormLogin()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var db = new mdgroup1Entities();

            var query = db.MemberTables.Where(x => x.Email == txtEamil.Text).Select(x => new MemberDto
            {
                Email = x.Email,
                Password = x.Password,
            });

            var result =query.FirstOrDefault();

            if (result == null)
            {

                MessageBox.Show("帳號密碼錯誤");
                return;
            }
            if (result != null)
            {
                if (result.Password == txtPassword.Text)
                {
                    var frm = new FormHome();
                    frm.closefather += new childclose(this.closethis);
                    this.Hide();
                    frm.Show();
                    txtEamil.Text = "";
                    txtPassword.Text = "";
                }
                else
                {
                    MessageBox.Show("帳號密碼錯誤");
                    return;
                }
            }
        }

        public delegate void childclose();
        public void closethis()//關掉自己
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            txtEamil.Text = "Kevin11@gmail.com";
            txtPassword.Text = "123";
        }
    }
}
