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
    
    public partial class SiteTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SiteTable()
        {
            this.ClassAllTables = new HashSet<ClassAllTable>();
            this.MemberSiteAllTables = new HashSet<MemberSiteAllTable>();
            this.SitePeriodTables = new HashSet<SitePeriodTable>();
            this.SitePicTables = new HashSet<SitePicTable>();
        }
    
        public int SiteID { get; set; }
        public string SiteType { get; set; }
        public string Address { get; set; }
        public string SitePhone { get; set; }
        public Nullable<int> CityID { get; set; }
        public Nullable<int> MemberID { get; set; }
        public string SiteName { get; set; }
    
        public virtual CityTable CityTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassAllTable> ClassAllTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MemberSiteAllTable> MemberSiteAllTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SitePeriodTable> SitePeriodTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SitePicTable> SitePicTables { get; set; }
    }
}
