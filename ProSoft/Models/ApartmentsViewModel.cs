using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProSoft.Models
{
    public class ApartmentsViewModel
    {
        public string SearchFilter { set; get; }
        public List<ApartmentModel> Apartments { get; set; } = new List<ApartmentModel>();
        public ApartmentModel Default { get => new ApartmentModel(); }
    }
}
