using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Ef.Dto;
using System.Data.SqlClient;
using Mapster;
using ProSoft.Models;
using Services;

namespace ProSoft.Controllers
{
    public class ApartmentsController : Controller
    {
        private IDataProvider _data;

        public ApartmentsController(IDataProvider data)
        {
            _data = data;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["SearchString"] = searchString;

            List<DataAccess.Ef.Dto.Apartment> apartments = await _data.GetApartmentsAsync(searchString);

            var model = new ApartmentsViewModel
            {
                Apartments = apartments.Adapt<List<Models.ApartmentModel>>()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ApartmentsViewModel model, string searchString)
        {
            if (ModelState.IsValid)
            {
                if (model.Apartments.Any())
                {
                    ApartmentModel apartment = model.Apartments[0];

                    if (apartment.LastIndicationId != null && apartment.LastIndicationValue != null)
                        await _data.SetIndicationAsync((int)apartment.LastIndicationId, (int)apartment.LastIndicationValue);
                }
            }

            return RedirectToAction("Index", new { searchString = searchString});
        }
    }
}