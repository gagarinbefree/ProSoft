using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ProSoft.Models
{
    public class MeterViewModel
    {
        [DisplayName("Адрес")]
        public string ApartmentName { get; set; }
        [DisplayName("Номер счетчика")]
        public string MeterNumber { get; set; }
        [DisplayName("Следующая поверка")]
        public string NextVerificationDate { get; set; }
    }
}
