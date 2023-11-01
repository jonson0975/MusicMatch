//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsFormsApp2
{
    using System;
    using System.Collections.Generic;
    
    public partial class MemberTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MemberTable()
        {
            this.ApplicationRecordTables = new HashSet<ApplicationRecordTable>();
            this.ClassAllTables = new HashSet<ClassAllTable>();
            this.DealClassTables = new HashSet<DealClassTable>();
            this.DealProjectTables = new HashSet<DealProjectTable>();
            this.DealSiteLoanTables = new HashSet<DealSiteLoanTable>();
            this.DealSiteLoanTables1 = new HashSet<DealSiteLoanTable>();
            this.EvaluateAllTables = new HashSet<EvaluateAllTable>();
            this.MemberCouponTables = new HashSet<MemberCouponTable>();
            this.MemberSiteAllTables = new HashSet<MemberSiteAllTable>();
            this.MemberSkillDetailTables = new HashSet<MemberSkillDetailTable>();
            this.MusicarticleTables = new HashSet<MusicarticleTable>();
            this.ProjectTables = new HashSet<ProjectTable>();
            this.SocialRelationTables = new HashSet<SocialRelationTable>();
            this.SocialRelationTables1 = new HashSet<SocialRelationTable>();
            this.ProductTables = new HashSet<ProductTable>();
        }
    
        public int MemberID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> Birthdate { get; set; }
        public string Introduction { get; set; }
        public Nullable<int> Permission { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Account { get; set; }
        public byte[] Picture { get; set; }
        public Nullable<bool> Gender { get; set; }
    
        public virtual AdministratorPermissionTable AdministratorPermissionTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApplicationRecordTable> ApplicationRecordTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassAllTable> ClassAllTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DealClassTable> DealClassTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DealProjectTable> DealProjectTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DealSiteLoanTable> DealSiteLoanTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DealSiteLoanTable> DealSiteLoanTables1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EvaluateAllTable> EvaluateAllTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MemberCouponTable> MemberCouponTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MemberSiteAllTable> MemberSiteAllTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MemberSkillDetailTable> MemberSkillDetailTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MusicarticleTable> MusicarticleTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectTable> ProjectTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SocialRelationTable> SocialRelationTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SocialRelationTable> SocialRelationTables1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductTable> ProductTables { get; set; }
    }
}