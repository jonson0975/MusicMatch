using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.g1DataSetTableAdapters;

namespace WindowsFormsApp2.Main.Class
{
    public partial class MainClassForm : Form
    {
        public int w = 0;//間隔寬
        public int h = 0;//間隔高
        public int count = 0;//執行次數

        public MainClassForm()
        {
            InitializeComponent();
            classAllTableTableAdapter1.Fill(g1DataSet1.ClassAllTable);
            memberTableTableAdapter1.Fill(g1DataSet1.MemberTable);
            siteTableTableAdapter1.Fill(g1DataSet1.SiteTable);

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            byte[] bytes = new byte[ms.Length];

            var q = from c in g1DataSet1.ClassAllTable
                    join m in g1DataSet1.MemberTable
                    on c.MemberID equals m.MemberID
                    join s in g1DataSet1.SiteTable
                    on c.SiteID equals s.SiteID
                    select new { 價錢 = c.Price,
                        課程名稱 = c.ClassName,
                        課程敘述 = c.ClassDescription,
                        會員名稱 = m.Name,
                        課程地點 = s.SiteName,
                        圖片 = BytesToImage(c.ClassPicture),
                    };

            foreach (var item in q)
            {
                UserControlClass userControlClass = new UserControlClass()
                {
                    價錢UC = item.價錢,
                    課程名稱UC = item.課程名稱,
                    課程敘述UC = item.課程敘述,
                    會員名稱UC = item.會員名稱,
                    課程地點UC = item.課程地點,
                    圖片UC = item.圖片,

                Location = new Point(12 + w, 60 + h),
                    TabIndex = 10
                };
                count++;
                if (count % 3 == 0)
                {
                    h += 300;
                    w = 0;
                }
                else
                {
                    w += 320;
                }
                this.Controls.Add(userControlClass);
            }
        }

        public static Image BytesToImage(byte[] buffer)
        {
            MemoryStream ms = new MemoryStream(buffer);
            Image image = System.Drawing.Image.FromStream(ms);
            return image;
        }
    }
}
