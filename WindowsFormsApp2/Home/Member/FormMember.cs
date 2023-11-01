using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WindowsFormsApp2.Member;

namespace WindowsFormsApp2
{

    public partial class FormMember : Form
    {

        private List<MemberDto> data;

        public FormMember()
        {
            InitializeComponent();
            this.Load += FormMembers_Load;
            this.dataGridView1.CellClick += dataGridView1_CellContentClick;
        }

        public void FormMembers_Load(object sender, EventArgs e)
        {
            Display();
        }

        public void Display()
        {

            string name = textBoxName.Text;
            string phone = textBoxPhone.Text;
            data = new MemberRepository().Search(name, phone);
            dataGridView1.DataSource = data;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textBoxName.Clear();
            textBoxPhone.Clear();
            Display();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Display();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var frm = new FormCreateMember();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex <= 0) return;
            int MemberId = this.data[e.RowIndex].MemberID;
            var Frm = new FormEditMember(MemberId);
            Frm.Owner = this;
            Frm.ShowDialog();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormSocial social = new FormSocial();
            social.Owner = this;
            social.ShowDialog();
        }
    }

        //public class MemberRepository
        //{
        //    public List<MemberDto> Search(string s_Name, string s_Phone)
        //    {
        //        var db = new group1Entities();
        //        var data = from m in db.MemberTables.AsNoTracking()
        //                   select new MemberDto
        //                   {
        //                       MemberID = m.MemberID,
        //                       Name = m.Name,
        //                       Account = m.Account,
        //                       Phone = m.Phone,
        //                       Password = m.Password,
        //                       Email = m.Email,
        //                       Birthdate = (DateTime)m.Birthdate,
        //                       CreationTime = (DateTime)m.CreationTime,
        //                       Introduction = m.Introduction,
        //                       Permission = m.Permission.ToString(),
        //                       Picture = m.Picture,
        //                   };
        //        if (!string.IsNullOrEmpty(s_Name))
        //        {
        //            data = data.Where(m => m.Name.Contains(s_Name));
        //        }
        //        if (!string.IsNullOrEmpty(s_Phone))
        //        {
        //            data = data.Where(m => m.Phone.Contains(s_Phone));
        //        }
        //        return data.ToList();
        //    }
    }


