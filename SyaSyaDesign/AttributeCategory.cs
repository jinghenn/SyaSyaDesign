//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SyaSyaDesign
{
    using System;
    using System.Collections.Generic;
    
    public partial class AttributeCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AttributeCategory()
        {
            this.Attributes = new HashSet<Attribute>();
        }
    
        public int AttributeCategoryID { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int ModifiedBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attribute> Attributes { get; set; }
        public virtual User User { get; set; }
    }
}
