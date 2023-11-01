using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class FormCreateMember : Form
    {
        public FormCreateMember()
        {
            InitializeComponent();
        }

        private (bool isVaild, List<ValidationResult> errors) Validate(MemberCreatVM vm)
        {
            ValidationContext context = new ValidationContext(vm, null, null);

            List<ValidationResult> errors = new List<ValidationResult>();

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
                {"Permission",textBoxPermission },
                {"Introduction",textBoxIntroduction },
                {"Email",textBoxEmail },
                {"CreationTime", dateTimePicker1},
                {"Birthdate",dateTimePickerDateOfBirth },
                 {"Gender", radioButtonFemale},
            };

            foreach (ValidationResult result in errors)

            {
                string propName = result.MemberNames.FirstOrDefault();
                if (map.TryGetValue(propName, out Control ctrl))
                {
                    this.errorProvider1.SetError(ctrl, result.ErrorMessage);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (labelAlarm.Visible == false)
            {
                MessageBox.Show("請先進行信箱認證!");
                return;
            }
            if (labelAlarm.Text != "信箱可使用!")
            {
                MessageBox.Show("信箱無法使用");
                return;
            }
            if (dateTimePickerDateOfBirth.Value > DateTime.Today)
            {
                MessageBox.Show("日期選擇錯誤");
                return;
            }
            //收集欄位資料
            string Name = textBoxName.Text;
            string Account = textBoxAccount.Text;
            string Password = textBoxPassword.Text;
            string Introduction = textBoxIntroduction.Text;
            string Permission = textBoxPermission.Text;
            string Phone = textBoxPhone.Text;
            string Email = textBoxEmail.Text;
            DateTime CreateDate = DateTime.Now;
            DateTime? birthdate = dateTimePickerDateOfBirth.Value;
            bool isFemale = radioButtonFemale.Checked;
            string GenderString = isFemale ? "女" : "男";
            bool GenderBool = isFemale;

            //將圖片存成二進制物件
            //==========================================
            byte[] bytes;

            //"Stream"資料流，建立支援的存放區為記憶體的資料流
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            this.pictureBoxCreate.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            bytes = ms.ToArray();

            var vm = new MemberCreatVM()
            {
                Name = Name,
                Account = Account,
                Password = Password,
                Introduction = Introduction,
                Permission = Permission,
                Phone = Phone,
                Email = Email,
                CreationTime = CreateDate,
                Birthdate = birthdate.Value,
                Gender = GenderBool

            };

            (bool isValid, List<ValidationResult> error) validationResult = Validate(vm);

            if (validationResult.isValid == false)
            {
                this.errorProvider1.Clear();
                DisplayErrors(validationResult.error);
                return;
            }

            MemberDto member = new MemberDto()
            {
                Name = Name,
                Account = Account,
                Password = Password,
                Introduction = Introduction,
                Permission = int.Parse(Permission),
                Phone = Phone,
                Email = Email,
                CreationTime = CreateDate,
                Birthdate = birthdate.Value,
                Picture=bytes,
                Gender = GenderString
            };

            var service = new MemberService();
            try
            {
                // Add the necessary logic to create the member using the service.
                MessageBox.Show("新增會員成功");
                service.Create(member);
            }
            catch (Exception ex)
            {
                MessageBox.Show("註冊失敗: " + ex.Message);
                return;
            }

            IGrid parent = this.Owner as IGrid;
            if (parent == null)
            {
                MessageBox.Show("開啟我的表單");
            }
            else
            {
                parent.Display();
            }
            this.Close();

        }
        private void buttonValidateEmail_Click(object sender, EventArgs e)
        {
            labelAlarm.Visible = false;

            if (!string.IsNullOrEmpty(textBoxEmail.Text))
            {
                var query = new MemberRepository().GetByEmail(textBoxEmail.Text);
                if (query != null)
                {
                    labelAlarm.Visible = true;
                    labelAlarm.Text = "信箱已被註冊";
                }
                else
                {
                    labelAlarm.Visible = true;
                    labelAlarm.Text = "信箱可使用!";
                    return;
                }
            }
            if (string.IsNullOrEmpty(textBoxEmail.Text))
            {
                labelAlarm.Visible = true;
                labelAlarm.Text = "信箱未填寫";
                return;
            }
        }

        private void FormCreateMember_Load(object sender, EventArgs e)
        {
            comboBoxMemberLevel.Items.Clear();
            var levelData = new mdgroup1Entities().MemberTables.Select(m => m.Name).ToList();
            comboBoxMemberLevel.Items.AddRange(levelData.ToArray());
            dateTimePicker1.Value = DateTime.Now;

        }
        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {
            labelAlarm.Visible = false;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pictureBoxCreate.Image = Image.FromFile(this.openFileDialog1.FileName);
            }
        }
    }
}

