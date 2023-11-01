using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2.ProductAndEva
{
    public partial class FormStyle : Form
    {
        public FormStyle()
        {
            InitializeComponent();
            LoadPROToGridsty();
        }
        private void LoadPROToGridsty()
        {
            using (mdgroup1Entities db = new mdgroup1Entities())
            {

                var q = from x in db.StyleAllTables
                        select new { x.StyleID, x.StyleName, x.StyleDescription, x.StylePicture };
                this.dataGridView1.DataSource = q.ToList();
            }
        }
        private void FormStyle_Load(object sender, EventArgs e)
        {
            using (mdgroup1Entities db = new mdgroup1Entities())
            {
                var q = from x in db.StyleAllTables
                        select new { x.StyleID, x.StyleName, x.StyleDescription, x.StylePicture };
                this.dataGridView1.DataSource = q.ToList();


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            FormCreatStyle formCreatStyle = new FormCreatStyle();
            formCreatStyle.Owner = this;
            formCreatStyle.ShowDialog();
            LoadPROToGridsty();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int datap = (int)dataGridView1.CurrentRow.Cells[0].Value;
            FormEditStyle style = new FormEditStyle(datap);
           style.Owner = this;
            style.ShowDialog();
            LoadPROToGridsty();
        }
    }
    
}
