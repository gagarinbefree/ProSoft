using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Ef.Dto;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Models;
using Services;

namespace ProSoft.Controllers
{
    public class MetersController : Controller
    {
        private IDataProvider _data;

        public MetersController(IDataProvider data)
        {
            _data = data;
        }

        public async Task<IActionResult> Index(string address)
        {
            List<Address> addresses = await _data.GetAddresses();
            List<Apartment> apartments = await _data.GetUnverifiedMetersByAddress(address);

            var model = new MetersViewModel
            {
                Addresses = addresses.Adapt<List<AddressViewModel>>(),
                Meters = apartments.Adapt<List<MeterViewModel>>()                
            };

            return View(model);
        }
    }
}