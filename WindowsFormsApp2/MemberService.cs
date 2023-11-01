using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public class MemberService
    {
        public void Create(MemberDto  member)
        {
            var repo = new MemberRepository();
            var dtoInDb = repo.GetByPhone(member);
            var dtoInDbEmail = repo.GetByEmail(member.Email);

            if (dtoInDb != null )
            {
                throw new Exception("電話已被使用");
            }
            else if(dtoInDbEmail != null )
            {
                throw new Exception("已被使用");

            }
            repo.Create(member);
        }
    }
}