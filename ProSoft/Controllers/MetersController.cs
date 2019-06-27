using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ProSoft.Controllers
{
    public class MetersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}