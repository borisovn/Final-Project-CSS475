//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Final_VSE_CSS475.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ticket
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ticket()
        {
            this.UserHistories = new HashSet<UserHistory>();
        }
    
        public int TicketId { get; set; }
        public Nullable<int> TicketNumber { get; set; }
        public string PerformanceName { get; set; }
        public Nullable<decimal> Price { get; set; }
    
        public virtual Performance Performance { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserHistory> UserHistories { get; set; }
    }
}
