//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Realestate.Classes
{
    using System;
    using System.Collections.Generic;
    
    public partial class Citizen
    {
        public int IDcitizen { get; set; }
        public string FIO { get; set; }
        public Nullable<int> Passport { get; set; }
        public string Telephone { get; set; }
        public Nullable<int> IDestate { get; set; }
    
        public virtual RealEstate RealEstate { get; set; }
    }
}
