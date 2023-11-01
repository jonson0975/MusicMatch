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

namespace WindowsFormsApp2
{
    public partial class UserControlClass : UserControl
    {
        public UserControlClass()
        {
            InitializeComponent();
        }

        public Nullable<decimal> 價錢UC
        {
            get { return decimal.Parse(label1.Text); }
            set { label1.Text = value.ToString()+" / 一堂"; }
        }

        public string 課程名稱UC
        {
            get { return label2.Text; }
            set { label2.Text = value; }
        }

        public string 課程敘述UC
        {
            get { return label3.Text; }
            set { label3.Text = value; }
        }

        public string 會員名稱UC
        {
            get { return label4.Text; }
            set { label4.Text = value; }
        }

        public string 課程地點UC
        {
            get { return label5.Text; }
            set { label5.Text = value; }
        }

        public Image 圖片UC
        {
            get { return pictureBox1.Image; }
            set { pictureBox1.Image = value; }
        }
    }
}
