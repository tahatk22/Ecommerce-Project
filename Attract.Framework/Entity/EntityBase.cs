using System;
using System.Collections.Generic;
using System.Text;

namespace Attract.Framework.Entity
{
    public abstract class EntityBase
    {
        public EntityBase()
        {
            CreatedOn = DateTime.Now;
            IsActive = false;
        }
        public virtual bool IsActive { get; set; }
        public virtual int CreatedBy { get; set; }
        public virtual DateTime CreatedOn { get;  set; }
        public virtual int? ModifyBy { get;  set; }
        public virtual DateTime? ModifyOn { get;  set; }
    }
}
