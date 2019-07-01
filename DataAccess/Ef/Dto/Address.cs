using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Ef.Dto
{
    public partial class Address : Entity
    {
        public string Street { get; set; }
        public string Building { get; set; }
    }
}
