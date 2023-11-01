using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class FormEditMember : Form
    {
        private readonly int _MemberId;
        public List<MemberDto> data;
        public FormEditMember(int memberId)
        {
            this._MemberId = memberId;
            InitializeComponent();

        }

        private (bool isValid, List<ValidationResult> errors) Validate(MemberCreatVM vm)

        {
            //得知要驗證規則
            ValidationContext context = new ValidationContext(vm, null, null);

            // 用來存放錯誤的集合,因為可能有零到多個錯誤
            List<ValidationResult> errors = new List<ValidationResult>();
            // 驗證 model
            bool validateAllProperties = true;
            bool isValid = Validator.TryValidateObject(vm, context, errors, validateAllProperties);

            return (isValid, errors);
        }
        private void DisplayErrors(List<ValidationResult> errors)
        {
            Dictionary<string, Control> map = new Dictionary<string, Control>(StringComparer.CurrentCultureIgnoreCase)
            {

                {"Name",textBoxName },
                {"Account",textBoxAccount },
                {"Password",textBoxPassword},
                {"Phone",textBoxPhone },
                {"Permission",comboBoxMemberLevel },
                {"Introduction",textBoxIntroduction },
                {"Email",textBoxEmail },
                {"CreationTime", dateTimePicker1},
                {"Birthdate",dateTimePickerDateOfBirth }
            };
            this.errorProvider1.Clear();

            foreach (ValidationResult error in errors)
            {
                string propName = error.MemberNames.FirstOrDefault();
                if (map.TryGetValue(propName, out Control ctrl))
                {
                    this.errorProvider1.SetError(ctrl, error.ErrorMessage);
                }
            }
        }

        private void FormEditMember_Load(object sender, EventArgs e)
        {
            LoadMemberData();
        }

        private void LoadMemberData()
        {
            comboBoxMemberLevel.Items.Clear();
            var levelData = new mdgroup1Entities().MemberTables.Select(m => m.Name).ToList();
            comboBoxMemberLevel.Items.AddRange(levelData.ToArray());
            var repo = new MemberRepository();
            MemberDto dto = repo.Get(_MemberId);

            if (dto == null)
            {
                MessageBox.Show("找不到指定的成員。");
                return;
            }

            textBoxMemberId.Text = dto.MemberID.ToString();
            textBoxName.Text = dto.Name;
            textBoxPassword.Text = dto.Password;
            textBoxPhone.Text = dto.Phone;
            textBoxEmail.Text = dto.Email;
            textBoxAccount.Text = dto.Account;
            textBoxIntroduction.Text = dto.Introduction;

            dateTimePickerDateOfBirth.Value = dto.Birthdate;
            dateTimePicker1.Value = dto.CreationTime;





        }

        private void button1_Click(object sender, EventArgs e)
        {
            var db = new mdgroup1Entities();
            var member = db.MemberTables.Find(_MemberId);

            member.MemberID = _MemberId;
            member.Name = textBoxName.Text;
            member.Account = textBoxAccount.Text;
            member.Password = textBoxPassword.Text;
            member.Phone = textBoxPhone.Text;
            member.Email = textBoxEmail.Text;
            member.Birthdate = dateTimePickerDateOfBirth.Value;
            member.CreationTime = dateTimePicker1.Value;
            member.Introduction = textBoxIntroduction.Text;


            //vm
            var vm = new MemberCreatVM()
            {
                Name = member.Name,
                Account = member.Account,
                Password = member.Password,
                Phone = member.Phone,
                Email = member.Email,
                Birthdate = dateTimePickerDateOfBirth.Value,
                CreationTime = dateTimePicker1.Value,
                Introduction = member.Introduction,

            };

            //驗證vm 是否通過欄位驗證規則
            (bool isValid, List<ValidationResult> errors) validationResult = Validate(vm);

            //如果有錯，就顯示他
            if (validationResult.isValid == false)
            {
                this.errorProvider1.Clear();
                DisplayErrors(validationResult.errors);
                return;
            }

            var query = db.MemberTables.Where(x => x.Phone == textBoxPhone.Text || x.Email == textBoxEmail.Text)
            .Select(x => new MemberDto { Phone = x.Phone, Email = x.Email, MemberID = x.MemberID }).FirstOrDefault();

            if (query != null)
            {
                if (query.Email == textBoxEmail.Text && query.MemberID != _MemberId)
                {
                    MessageBox.Show("信箱已被註冊");
                    return;
                }
                if (query.Phone == textBoxPhone.Text && query.MemberID != _MemberId)
                {
                    MessageBox.Show("電話已被註冊");
                    return;
                }


                db.SaveChanges();

                RenewGridVierData();
                this.Close();
            }

        }

        private void RenewGridVierData()
        {
            IGrid parent = this.Owner as IGrid;
            if (parent == null)
            {
                MessageBox.Show("開啟我的表單");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDeleteMember_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("確認刪除?", "刪除會員資料", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                var db = new mdgroup1Entities();
                var memberdto = db.MemberTables.Find(_MemberId);

                if (memberdto == null)
                {
                    MessageBox.Show("record not found");
                    return;
                }
                db.MemberTables.Remove(memberdto);
                db.SaveChanges();
                RenewGridVierData();
                this.Close();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBoxEdit.Image == null)
            {
                MessageBox.Show("請先選擇圖片");
                return;
            }
            using(MemoryStream ms = new MemoryStream())
            {
                pictureBoxEdit.Image.Save(ms,System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                using(var db = new mdgroup1Entities())
                {
                    var membertoupdate =db.MemberTables.Find(_MemberId);
                    if(membertoupdate != null)
                    {
                        membertoupdate.Picture = imageBytes;

                        db.SaveChanges();
                        MessageBox.Show("圖片已成功更新");
                    }
                    else
                    {
                        MessageBox.Show("找不到指定的成員");
                    }
                }
            }
        }

        private void LoadMemberPhoto(string picture)
        {
           
            if (picture==null)
            {
                picture = "default.jpg";
            }
            try
            {
                FileStream fileStream = File.OpenRead(picture);
                pictureBoxEdit.Image = Image.FromStream(fileStream);
                fileStream.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("照片讀取失敗!");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(this.openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                Image image = Image.FromFile(this.openFileDialog1.FileName);
                if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
                {
                    this.pictureBoxEdit.Image = Image.FromFile(this.openFileDialog1.FileName);
                }
                else
                {
                    MessageBox.Show("只可以使用png");
                }
            }
        }
    }
}

