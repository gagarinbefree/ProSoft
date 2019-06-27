using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProSoft.Models
{
    public class ApartmentViewModel
    {
        public List<Appartment> Appartments { get; set; } = new List<Appartment>();
        public Appartment Default { get => new Appartment(); }
    }
}
