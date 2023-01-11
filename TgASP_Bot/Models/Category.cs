using System;
using System.Collections.Generic;

namespace TgASP_Bot.Models
{
    public partial class Category
    {
        public Category()
        {
            Gadgets = new HashSet<Gadget>();
        }

        public int Id { get; set; }
        public string? NameGadgets { get; set; }

        public virtual ICollection<Gadget> Gadgets { get; set; }
    }
}
