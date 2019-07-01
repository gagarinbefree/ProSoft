using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Ef.Dto;
using System.Linq;

namespace Services
{
    public class DataProvider : IDataProvider
    {
        private IGenUoW _unit;

        public DataProvider(IGenUoW unit)
        {
            _unit = unit;
        }

        public async Task<List<Address>> GetAddresses()
        {
            return await _unit.RepAddress.GetAllAsync();
        }

        public async Task<List<Apartment>> GetApartmentsAsync(string searchString)
        {
            var apartments = new List<Apartment>();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                string cleanSearchString = searchString.Replace('/', ' ');

                if (!string.IsNullOrWhiteSpace(cleanSearchString))
                {
                    apartments = await _unit
                        .RepApartment
                        .GetAllAsync(f => f.Name.IndexOf(cleanSearchString, StringComparison.OrdinalIgnoreCase) != -1,
                            f => f.Meter);
                }
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

        public async Task<List<Apartment>> GetUnverifiedMetersByAddress(string address)
        {
            var apartments = new List<Apartment>();

            if (!string.IsNullOrWhiteSpace(address))
            {
                string searchString = address.Replace(",", "/");

                apartments = await _unit.RepApartment.GetAllAsync(f => f.Name.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1 
                    && f.Meter.Nextverification <= DateTime.Now,
                   f => f.Meter);
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
