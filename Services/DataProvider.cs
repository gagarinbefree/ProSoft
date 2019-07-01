using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
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

        public async Task<List<Apartment>> GetApartmentsAsync(string searchString)
        {
            List<Apartment> apartments = new List<Apartment>();
          
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                Regex rgx = new Regex("[^a-zA-Z0-9]");
                string alphaNumericOnly = rgx.Replace(searchString, "");

                apartments = await _unit
                    .RepApartment
                    .GetAllAsync(f => f.Name.IndexOf(alphaNumericOnly, StringComparison.OrdinalIgnoreCase) != -1,
                        f => f.Meter);
            }
            else
            {
                apartments = await _unit
                    .RepApartment
                    .GetAllAsync(f => f.Meter);
            }

            foreach (var apartment in apartments)
            {
                var indication = await _unit.RepIndication.GetAsync<DateTime>(f => f.Meterid == apartment.Meterid,
                    f => f.Datevalue,
                    SortOrder.Descending);

                apartment.Meter.Indication.Add(indication);
            }

            return apartments;
        }

        public async Task SetIndicationAsync(int id, int value)
        {
            Indication indication = await _unit.RepIndication.GetAsync(f => f.Id == id);

            if (indication == null)
                return;

            if (indication.Value < value)
                indication.Value = value;

            _unit.RepIndication.Update(indication);
        }       
    }
}
