using System;
using System.Collections.Generic;

namespace DataAccess.Ef.Dto
{
    public partial class Meter
    {
        public Meter()
        {
            Apartment = new HashSet<Apartment>();
            Indication = new HashSet<Indication>();
        }

        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Lastverification { get; set; }
        public DateTime Nextverification { get; set; }

        public virtual ICollection<Apartment> Apartment { get; set; }
        public virtual ICollection<Indication> Indication { get; set; }
    }
}
