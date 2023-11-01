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

namespace prjGroup1Plus.summit
{
    public partial class FrmAppliCreate : Form
    {
        public FrmAppliCreate()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否建立應徵?", "確認", MessageBoxButtons.YesNo);//詢問視窗
            if (result == DialogResult.Yes)
            {
                int.TryParse(txtboxMemID.Text, out int memid);
                int.TryParse(txtboxPrjID.Text, out int prjid);
                List<int> memIDs = null;
                List<int> prjIDs = null;
                using (mdgroup1Entities dbContext = new mdgroup1Entities())//獲取會員ID
                {
                    var w = from m in dbContext.MemberTables
                            select m.MemberID;
                    memIDs = w.ToList();
                }
                using (mdgroup1Entities dbContext = new mdgroup1Entities())//獲取專案ID
                {
                    var w = from m in dbContext.ProjectTables
                            select m.ProjectID;
                    prjIDs = w.ToList();
                }
                try
                {
                    using (mdgroup1Entities db = new mdgroup1Entities())
                    {
                        int TryCount = 0;
                        foreach (int id in memIDs)//確定會員ID存在與否
                        {
                            if (memid == id) 
                            {
                                int prjTryCount = 0;
                                foreach (int id2 in prjIDs)//確定專案ID存在與否
                                {
                                    if (prjid == id2)
                                    {
                                        ApplicationRecordTable table = new ApplicationRecordTable()
                                        {
                                            MemberID = memid,
                                            ProjectID = prjid,
                                            ApplicationStatusID = 1,
                                            ApplicationRecord = txtboxDescri.Text
                                        };
                                        db.ApplicationRecordTables.Add(table);
                                        db.SaveChanges();
                                        MessageBox.Show("建立應徵成功");
                                        this.Close();
                                        TryCount++;
                                        
                                    }
                                }
                            }
                        }
                        if (TryCount == 0) 
                        {
                            MessageBox.Show("會員或專案不存在");
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }                
        }

        private void btnNO_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否放棄以輸入資訊?", "確認", MessageBoxButtons.YesNo);//詢問視窗
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
