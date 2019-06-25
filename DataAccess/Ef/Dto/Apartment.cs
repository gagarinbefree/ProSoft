using System;
using System.Collections.Generic;

namespace DataAccess.Ef.Dto
{
    public partial class Apartment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Meterid { get; set; }

        public virtual Meter Meter { get; set; }
    }
}
