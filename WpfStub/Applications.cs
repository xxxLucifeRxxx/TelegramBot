//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfStub
{
    using System;
    using System.Collections.Generic;
    
    public partial class Applications
    {
        public int ApplicationId { get; set; }
        public int UserId { get; set; }
        public Nullable<int> DriverId { get; set; }
        public Nullable<float> LongitudeFrom { get; set; }
        public Nullable<float> LatitudeFrom { get; set; }
        public Nullable<float> LongitudeTo { get; set; }
        public Nullable<float> LatitudeTo { get; set; }
        public string NumbPhone { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.DateTime> Time { get; set; }
        public Nullable<int> PaymentMethod { get; set; }
        public Nullable<int> State { get; set; }
    
        public virtual Drivers Drivers { get; set; }
        public virtual Users Users { get; set; }
    }
}
