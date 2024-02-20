using Microsoft.AspNetCore.Mvc;

namespace TS.Controllers
{
    public class TestimonialController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Views/Dashboard/Testimonial/Index.cshtml");
        }
    }

}
