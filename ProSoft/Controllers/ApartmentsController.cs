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

        public async Task<IActionResult> Index()
        {
            List<Apartment> apartments = await _data.GetApartmentsAsync();

            var model = new ApartmentViewModel
            {
                Appartments = apartments.Adapt<List<Appartment>>()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(ApartmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //return View(model);
            }

            return RedirectToAction("Index");
        }
    }
}