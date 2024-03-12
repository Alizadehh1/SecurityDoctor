using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityDoctor.Models.DataContexts;
using SecurityDoctor.Models.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Firlansa.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FaqsController : Controller
    {
        private readonly SecurityDoctorDbContext db;

        public FaqsController(SecurityDoctorDbContext db)
        {
            this.db = db;
        }
        //[Authorize(Policy = "admin.faqs.index")]
        [Route("/admin/faqs/index")]
        public async Task<IActionResult> Index()
        {
            return View(await db.Faqs.Where(f=>f.DeletedById==null).ToListAsync());
        }
        //[Authorize(Policy = "admin.faqs.details")]
        [Route("/admin/faqs/details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faq = await db.Faqs
                .FirstOrDefaultAsync(m =>m.DeletedById==null && m.Id == id);
            if (faq == null)
            {
                return NotFound();
            }

            return View(faq);
        }
        //[Authorize(Policy = "admin.faqs.create")]
        [Route("/admin/faqs/create")]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        //[Authorize(Policy = "admin.faqs.create")]
        [Route("/admin/faqs/create")]
        public async Task<IActionResult> Create([Bind("Id,Question,Answer")] Faq faq)
        {
            if (ModelState.IsValid)
            {
                db.Add(faq);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(faq);
        }
        //[Authorize(Policy = "admin.faqs.edit")]
        [Route("/admin/faqs/edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faq = await db.Faqs.FirstOrDefaultAsync(f=>f.DeletedById==null && f.Id==id);
            if (faq == null)
            {
                return NotFound();
            }
            return View(faq);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/admin/faqs/edit")]
        //[Authorize(Policy = "admin.faqs.edit")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question,Answer")] Faq faq)
        {
            if (id != faq.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(faq);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaqExists(faq.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(faq);
        }


        [HttpPost]
        [Route("/admin/faqs/delete")]
        //[Authorize(Policy = "admin.faqs.delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var entity = db.Faqs.FirstOrDefault(f => f.Id == id && f.DeletedById==null);
            if (entity == null)
            {
                return Json(new
                {
                    error = true,
                    message = "Mövcud deyil"
                });
            }
            //var user = await userManager.GetUserAsync(User);
            entity.DeletedById = 1;
            entity.DeletedDate = DateTime.UtcNow.AddHours(4);
            db.SaveChanges();
            return Json(new
            {
                error = false,
                message = "Uğurla silindi"
            });
        }

        private bool FaqExists(int id)
        {
            return db.Faqs.Any(e => e.Id == id);
        }
    }
}
