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


namespace WindowsFormsApp2
{
    public class MemberRepository
    {
        public List<MemberDto> Search(string s_Name, string s_Phone)
        {
            var db = new mdgroup1Entities();
            var data = from m in db.MemberTables.AsNoTracking()
                       select new MemberDto
                       {
                           MemberID = m.MemberID,
                           Name = m.Name,
                           Account = m.Account,
                           Phone = m.Phone,
                           Password = m.Password,
                           Email = m.Email,
                           Birthdate = (DateTime)m.Birthdate,
                           CreationTime = (DateTime)m.CreationTime,
                           Introduction = m.Introduction,
                           //Permission = m.Permission.ToString(),
                           Picture = m.Picture,
                           Gender = (bool)m.Gender ? "女" : "男",
                       };
            if (!string.IsNullOrEmpty(s_Name))
            {
                data = data.Where(m => m.Name.Contains(s_Name));
            }
            if (!string.IsNullOrEmpty(s_Phone))
            {
                data = data.Where(m => m.Phone.Contains(s_Phone));
            }
            return data.ToList();
        }
        public MemberDto Get(int MemberId) 
      
        {
        
            var db = new mdgroup1Entities();
            var query = from m in db.MemberTables.AsNoTracking()
                        //join m1 in db.AdministratorPermissionTables.AsNoTracking() on m.MemberID equals m1.MemberID
                        select new MemberDto
                        {
                            MemberID=m.MemberID,
                            Name = m.Name,
                            Password = m.Password,
                            Phone = m.Phone,
                            Account = m.Account,
                            Email = m.Email,
                            Permission = (int)m.Permission,
                            Introduction =m.Introduction,
                            CreationTime= DateTime.Now,
                            Birthdate=(DateTime)m.Birthdate,
                            Picture=m.Picture,
                        };
            query =query.Where(m=>m.MemberID == MemberId);
            MemberDto data= query.FirstOrDefault();
            return data;

        }
        public MemberDto GetByPhone(MemberDto dto)
        {
            return new mdgroup1Entities().MemberTables.Where(m=>m.Phone ==dto.Phone).Select
                (m=>new MemberDto { Phone = m.Phone,MemberID=m.MemberID}
                ).FirstOrDefault();
        }
        public string GetByEmail(string s_email )
        {
            var db = new mdgroup1Entities();
            var data = db.MemberTables.AsNoTracking().Where(m => m.Email == s_email).Select(m => m.Email).FirstOrDefault();

            return data;
        }
        public void Create(MemberDto dto)
        {
            var db = new mdgroup1Entities();
            var member=new MemberTable()
            {
                Name = dto.Name,
                Password = dto.Password,
                Account=dto.Account,
                Phone = dto.Phone,
                Introduction=dto.Introduction,
                 Picture = dto.Picture,
                Email = dto.Email,
                CreationTime = dto.CreationTime,
                Birthdate = dto.Birthdate,
                Gender = dto.Gender == "女",

            };
            db.MemberTables.Add(member);
            db.SaveChanges();
        }
    }
}