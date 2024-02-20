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
    public class DashboardController : Controller
    {
        private readonly UserManager<RegisteredUser> _userManager;
        private readonly ApplicationDbContext _context;

        public DashboardController(UserManager<RegisteredUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index(int page = 1, int pageSize = 2)
        {
            // Calculate the number of records to skip
            int skip = (page - 1) * pageSize;

            // Retrieve the specified page of testimonials
            List<ApplicantTestimonial> testimonials = _context.ApplicantTestimonials
                                                            .OrderByDescending(t => t.Id)
                                                            .Skip(skip)
                                                            .Take(pageSize)
                                                            .ToList();

            // Pass pagination information to the view
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)_context.ApplicantTestimonials.Count() / pageSize);
            ViewBag.PageSize = pageSize; // Pass pageSize to the view

            return View(testimonials);
        }




        public IActionResult AdminApproval()
        {
            List<ApplicantTestimonial> testimonials = _context.ApplicantTestimonials.OrderByDescending(t=>t.Id).ToList();
            return View("~/Views/Dashboard/Testimonial/Admin/Index.cshtml", testimonials);
        }

        public ActionResult Home()
        {
            return View("");
        }

        public ActionResult Slider()
        {
            return View("~/Views/Dashboard/Testimonial/Slider/Index.cshtml");
        }

        public ActionResult Testimonial()
        {
            return View("~/Views/Dashboard/Testimonial/Index.cshtml");
        }

        public ActionResult Admin()
        {
            return View("~/Views/Dashboard/Testimonial/Admin/Index.cshtml");
        }

        [HttpPost]
        public IActionResult SaveTestimonial(ApplicantTestimonial model)
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result; // Synchronous call in action method

                
                List<ApplicantTestimonial> testimonials = _context.ApplicantTestimonials.ToList();
                model.ApplicantProfileId = testimonials[0].ApplicantProfileId;

                _context.ApplicantTestimonials.Add(model);
                _context.SaveChanges();

                ViewBag.Message = "Testimonial successfully added!";
            }
            catch (Exception ex)
            {
                ViewBag.Message = "An error occurred while adding the testimonial. Please try again later.";
            }

            return View("Success"); // Assuming you have a Success view
        }

        [HttpPost]
        public IActionResult ApproveTestimonial(string testimonial)
        {
            try
            {
                var testimonialEntity = _context.ApplicantTestimonials.FirstOrDefault(t => t.Testimonial == testimonial);
                if (testimonialEntity != null)
                {
                    testimonialEntity.IsApprove = true;
                    _context.SaveChanges();
                    ViewBag.Message = "Testimonial successfully approved!";
                }
                else
                {
                    ViewBag.Message = "Testimonial not found!";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "An error occurred while approving the testimonial. Please try again later.";
            }

            return RedirectToAction("Index"); // Redirect to the action that displays testimonials
        }

        [HttpPost]
        public IActionResult DeleteTestimonial(int id)
        {
            try
            {
                var testimonialEntity = _context.ApplicantTestimonials.FirstOrDefault(t => t.Id == id);
                if (testimonialEntity != null)
                {
                    _context.ApplicantTestimonials.Remove(testimonialEntity);
                    _context.SaveChanges();
                    ViewBag.Message = "Testimonial successfully deleted!";
                }
                else
                {
                    ViewBag.Message = "Testimonial not found!";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "An error occurred while deleting the testimonial. Please try again later.";
            }

            return RedirectToAction("Index"); // Redirect to the action that displays testimonials
        }

        public IActionResult Approved()
        {
            return View("~Views/Dashboard/Testimonial/Approved/Index.cshtml");
        }
    }
}
