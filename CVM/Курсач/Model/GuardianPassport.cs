//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Курсач.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class GuardianPassport
    {
        public string PassportSeria { get; set; }
        public string PassportNumber { get; set; }
        public string PassportWho { get; set; }
        public System.DateTime PassportDate { get; set; }
        public int ClientId { get; set; }
        public int GuardianId { get; set; }
    
        public virtual Guardian Guardian { get; set; }
    }
}
