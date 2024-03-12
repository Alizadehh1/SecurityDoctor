using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SecurityDoctor.Models;
using SecurityDoctor.Models.DataContexts;
using SecurityDoctor.WebUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityDoctor.Controllers
{
    public class HomeController : Controller
    {
        private readonly SecurityDoctorDbContext db;

        public HomeController(SecurityDoctorDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Contact(Contact model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    error = true,
                    message = ModelState.SelectMany(ms => ms.Value.Errors).First().ErrorMessage
                });
            }
            await db.Contacts.AddAsync(model);
            await db.SaveChangesAsync();
            return Json(new
            {
                error = false,
                message = "Müraciyyətiniz qeydə alındı!"
            });
        }
        public async Task<IActionResult> Faq()
        {
            var faqs = await db.Faqs.Where(f => f.DeletedById == null).ToListAsync();
            return View(faqs);
        }
    }
}
