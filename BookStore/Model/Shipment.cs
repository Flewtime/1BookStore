//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookStore.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shipment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shipment()
        {
            this.Orders = new HashSet<Order>();
            this.TransactionHeaders = new HashSet<TransactionHeader>();
        }
    
        public int ShipmentID { get; set; }
        public int ShipmentWeight { get; set; }
        public string ShipmentAddress { get; set; }
        public System.DateTime ShipmentDateTime { get; set; }
        public int ShipmentPrice { get; set; }
        public string ShipmentStatus { get; set; }
        public string ShipmentTrackingID { get; set; }
        public int ShipmentTypeID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ShipmentType ShipmentType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionHeader> TransactionHeaders { get; set; }
    }
}
