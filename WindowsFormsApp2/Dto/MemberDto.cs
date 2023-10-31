using System;
using System.Security.Policy;

namespace WindowsFormsApp2
{
    public class MemberDto
    {
        public int MemberID {  get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime Birthdate { get; set; }
        public string Introduction {  get; set; }
        public string Permission {  get; set; }
        public DateTime CreationTime {  get; set; }
        public string Phone {  get; set; }
        public string Email { get; set; }
        public string Account {  get; set; }
        public byte[] Picture { get; set; }

      
    }
}