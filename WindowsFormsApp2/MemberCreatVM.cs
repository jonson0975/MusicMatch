using System;
using System.ComponentModel.DataAnnotations;

namespace WindowsFormsApp2
{
    public class MemberCreatVM
    {
        public MemberCreatVM()
        {
        }
        [Display(Name="會員編號")]
        public int Id { get; set; }

        [Display(Name ="會員姓名")]
        [Required(ErrorMessage ="{0}必填")]
        [MaxLength(20, ErrorMessage ="{0}長度不可大於{1}")]
        public string Name { get; set; }

        public string Account { get; set; }

        [Display(Name = "會員姓名")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(20, ErrorMessage = "{0}長度不可大於{1}")]
        public string Password { get; set; }
        public string Introduction { get; set; }
        public string Permission { get; set; }

        [Display(Name = "手機號碼")]
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(10, ErrorMessage = "{0}填寫錯誤")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0}必填")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50, ErrorMessage = "{0}長度不可大於{1}")]
        public string Email { get; set; }

        [Display(Name ="註冊日期")]
        public DateTime CreationTime { get; set; }
        [Display(Name ="出生日期")]
        public DateTime Birthdate { get; set; }

        [Display(Name = "性別")]
        [Required(ErrorMessage = "{0}必填")]
        public bool Gender { get; set; }
    }
}