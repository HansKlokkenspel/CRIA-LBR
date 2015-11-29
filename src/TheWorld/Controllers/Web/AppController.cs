using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private WorldContext _context;

        public AppController(IMailService mailService, WorldContext context)
        {
            _mailService = mailService;
            _context = context;
        }

        public IActionResult Index()
        {
            var trips = _context.Trips.OrderBy(t => t.Name).ToList();

            return View(trips);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        //Method that will get called whenever a POST request is sent to the server for this particular action.
        //ContactViewModel is the ViewModel class that is bound to the Contact.cshtml View
        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            // Server side data validation on the specified ViewModel
            if (ModelState.IsValid)
            {
                var email = Startup.Configuration["AppSettings:SiteEmailAddress"];

                if (string.IsNullOrWhiteSpace(email))
                {
                    ModelState.AddModelError("", "Could not send email, configuration problem.");
                }

                if (_mailService.SendMail(email,
                    email,
                    $"Contact Page from {model.Name} ({model.Email})",
                    model.Message))
                {
                    // Clear the previous form data
                    ModelState.Clear();

                    ViewBag.Message = "Mail sent. Thanks!";
                }
            }

            return View();
        }
    }
}
