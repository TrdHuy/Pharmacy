//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pharmacy
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblMedicine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblMedicine()
        {
            this.tblPromoes = new HashSet<tblPromo>();
            this.tblWarehouseImportDetails = new HashSet<tblWarehouseImportDetail>();
            this.tblOrderDetails = new HashSet<tblOrderDetail>();
        }
    
        public string MedicineID { get; set; }
        public string MedicineName { get; set; }
        public int MedicineTypeID { get; set; }
        public int MedicineUnitID { get; set; }
        public int SupplierID { get; set; }
        public decimal BidPrice { get; set; }
        public decimal AskingPrice { get; set; }
        public string MedicineDescription { get; set; }
        public bool IsActive { get; set; }
    
        public virtual tblMedicineType tblMedicineType { get; set; }
        public virtual tblMedicineUnit tblMedicineUnit { get; set; }
        public virtual tblSupplier tblSupplier { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPromo> tblPromoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblWarehouseImportDetail> tblWarehouseImportDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblOrderDetail> tblOrderDetails { get; set; }

        public override string ToString()
        {
            return MedicineName;
        }
    }
}
