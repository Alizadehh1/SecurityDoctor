using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecurityDoctor.AppCode.Modules.ContactModule;
using SecurityDoctor.Models.DataContexts;
using SecurityDoctor.WebUI.Models.Entities;

namespace SecurityDoctor.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactsController : Controller
    {
        readonly IMediator mediator;
        private readonly SecurityDoctorDbContext db;

        public ContactsController(IMediator mediator, SecurityDoctorDbContext db)
        {
            this.mediator = mediator;
            this.db = db;
        }
        //[Authorize(Policy = "admin.contacts.index")]
        [Route("/admin/contacts/index")]
        public async Task<IActionResult> Index(ContactAllQuery query)
        {
            var data = await mediator.Send(query);
            return View(data);
        }
        //[Authorize(Policy = "admin.contacts.details")]
        [Route("/admin/contacts/details")]
        public async Task<IActionResult> Details(ContactSingleQuery query)
        {
            var data = await mediator.Send(query);
            return View(data);
        }
        //[Authorize(Policy = "admin.contacts.answer")]
        [Route("/admin/contacts/answer")]
        public async Task<IActionResult> Answer(ContactSingleQuery query)
        {
            var data = await mediator.Send(query);
            if (data.AnsweredById != null)
            {
                return RedirectToAction(nameof(Details), routeValues: new
                {
                    id = query.Id
                });
            }
            return View(data);
        }
        [HttpPost]
        //[Authorize(Policy = "admin.contacts.answer")]
        [Route("/admin/contacts/answer")]
        public async Task<IActionResult> Answer(ContactAnswerCommand command)
        {
            var data = await mediator.Send(command);
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            data.AnsweredById =1;
            data.AnswerDate = DateTime.UtcNow.AddHours(4);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Details), routeValues: new
            {
                id = command.Id
            });
        }
    }
}
