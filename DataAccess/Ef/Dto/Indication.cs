﻿using System;
using System.Collections.Generic;

namespace DataAccess.Ef.Dto
{
    public partial class Indication : Entity
    {
        public int Meterid { get; set; }
        public int Value { get; set; }
        public DateTime Datevalue { get; set; }

        //public virtual Meter Meter { get; set; }
    }
}
