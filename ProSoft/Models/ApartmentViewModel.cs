using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ProSoft.Models
{
    public class ApartmentViewModel
    {
        public int ApartmentId { get; set; }
        [DisplayName("Адрес")]
        public string ApartmentName { get; set; }
        public int MeterId { get; set; }
        [DisplayName("Номер счетчика")]
        public string MeterNumber { get; set; }
        [DisplayName("Дата показания")]
        public string LastIndicationDateValue { get; set; }
        [DisplayName("Показание")]
        public int? LastIndicationValue { get; set; }        
        [DisplayName("Последняя поверка")]
        public string LastVerificationDate { get; set; }
        [DisplayName("Следущая поверка")]
        public string NextVerificationDate { get; set; }
        public int? LastIndicationId { get; set; }
    }
}
