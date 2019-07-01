using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProSoft.Models
{
    public class MetersViewModel
    {
        public string Address { get; set; } = "";
        public List<AddressViewModel> Addresses { get; set; } = new List<AddressViewModel>();
        public List<MeterViewModel> Meters { get; set; } = new List<MeterViewModel>();
        public MeterViewModel Default { get => new MeterViewModel(); }
    }
}
