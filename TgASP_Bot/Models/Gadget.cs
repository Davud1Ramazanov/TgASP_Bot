using System;
using System.Collections.Generic;

namespace TgASP_Bot.Models
{
    public partial class Gadget
    {
        public int Id { get; set; }
        public int? IdCategory { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }

        public virtual Category? IdCategoryNavigation { get; set; }
    }
}
