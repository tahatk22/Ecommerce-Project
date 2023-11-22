using System;
using System.Collections.Generic;
using System.Text;

namespace Attract.Framework
{
    public class LookupBase
    {
        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string InternalCode { get; set; }
        public int InternalRef { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual int CreatedBy { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual int? ModifyBy { get; set; }
        public virtual DateTime? ModifyOn { get; set; }
    }
}
