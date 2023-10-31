using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using PnumClassFormsApp1;

namespace WindowsFormsApp2
{
    public partial class FormProductTable : Form
    {
        public FormProductTable()
        {
            InitializeComponent();
        }

        private void FormProductTable_Load(object sender, EventArgs e)
        {
            mdgroup1Entities db=new mdgroup1Entities();
            var q = from x in db.ProductTables
                    join y in db.StyleAllTables
                    on x.StyleID equals y.StyleID
                    select new { x.ProductID, x.MemberID, x.ProductName, y.StyleID, y.StyleName, x.Photo, x.ProductMediaFile, x.ProductDescription };
            this.dataGridView1.DataSource = q.ToList();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var CreatProduct = new FormCreartProducts();
            CreatProduct.Owner = this;
            CreatProduct.ShowDialog();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            mdgroup1Entities db = new mdgroup1Entities();
            var q = from x in db.ProductTables
                    join y in db.StyleAllTables
                    on x.StyleID equals y.StyleID
                    select new { x.ProductID, x.MemberID, x.ProductName, y.StyleID, y.StyleName, x.Photo, x.ProductMediaFile, x.ProductDescription };
            this.dataGridView1.DataSource = q.ToList();
        }
        
        private void button7_Click(object sender, EventArgs e)
        {
            
            int datap = (int)dataGridView1.CurrentRow.Cells[0].Value;
            FormEditProducts EditProduct = new FormEditProducts(datap);
            EditProduct.Owner = this;
            EditProduct.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int datap = (int)dataGridView1.CurrentRow.Cells[0].Value;
            FormEvaluate formEvaluate = new FormEvaluate(datap);
            formEvaluate.Owner = this;
            formEvaluate.ShowDialog();
        }
    }
}
