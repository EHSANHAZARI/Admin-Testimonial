using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TS.Models;
using System.Linq; // Add this namespace to use the LINQ extension methods
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TS.Data;
using TS.Models;
using System.Linq;
namespace TS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context; // Assuming ApplicationDbContext is your Entity Framework DbContext

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Retrieve testimonials where IsApproved is equal to 1
            var approvedTestimonials = _context.ApplicantTestimonials.Where(t => t.IsApprove).ToList();
            return View(approvedTestimonials);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
