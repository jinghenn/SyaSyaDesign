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
    
    public partial class OrderDetail
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int Color { get; set; }
        public int Size { get; set; }
    
        public virtual Attribute Attribute { get; set; }
        public virtual Attribute Attribute1 { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
