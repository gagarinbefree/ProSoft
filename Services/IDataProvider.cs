using DataAccess.Ef.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IDataProvider
    {
        Task<List<Apartment>> GetApartmentsAsync(string searchString);
        Task SetIndicationAsync(int id, int value);
    }
}
