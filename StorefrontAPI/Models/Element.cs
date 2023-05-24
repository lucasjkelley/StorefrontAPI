using System;
using System.Collections.Generic;

namespace StorefrontAPI.Models
{
    public partial class Element
    {
        public Element()
        {
            Weapons = new HashSet<Weapon>();
        }

        public int ElementId { get; set; }
        public string ElementType { get; set; } = null!;

        public virtual ICollection<Weapon> Weapons { get; set; }
    }
}
