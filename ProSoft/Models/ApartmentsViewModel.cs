using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProSoft.Models
{
    public class ApartmentsViewModel
    {
        public string SearchFilter { set; get; }
        public List<ApartmentViewModel> Apartments { get; set; } = new List<ApartmentViewModel>();
        public ApartmentViewModel Default { get => new ApartmentViewModel(); }
    }
}
