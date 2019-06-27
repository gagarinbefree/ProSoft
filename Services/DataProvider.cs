using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Ef.Dto;

namespace Services
{
    public class DataProvider : IDataProvider
    {
        private IGenUoW _unit;

        public DataProvider(IGenUoW unit)
        {
            _unit = unit;
        }

        public async Task<List<Apartment>> GetApartmentsAsync()
        {
            var apartments = await _unit
                .RepApartment
                .GetAllAsync(f => f.Meter);

            foreach (var apartment in apartments)
            {
                var indication = await _unit.RepIndication.GetAsync<DateTime>(f => f.Meterid == apartment.Meterid,
                    f => f.Datevalue,
                    SortOrder.Descending);

                apartment.Meter.Indication.Add(indication);
            }

            return apartments;
        }
    }
}
