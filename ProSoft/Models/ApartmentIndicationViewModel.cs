using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProSoft.Models
{
    public class ApartmentIndicationViewModel
    {
        public int ApartmentId { get; set; }
        public string ApartmentName { get; set; }
        public int MeterId { get; set; }
        public string MeterNumber { get; set; }
        public int? LastIndicationValue { get; set; }
        public DateTime? LastIndicationDateValue { get; set; }
        public DateTime? LastVerificationDate { get; set; }
        public DateTime? NextVerificationDate { get; set; }
    }
}
