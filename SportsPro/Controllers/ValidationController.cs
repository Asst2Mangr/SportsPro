using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using System.Linq;

namespace SportsPro.Controllers
{
    public class ValidationController : Controller
    {
        private SportsProContext context;
        public ValidationController(SportsProContext ctx) => context = ctx;

        public JsonResult CheckEmail(string emailAddress)
        {
            string msg = EmailExists(context, emailAddress);
            
            if (string.IsNullOrEmpty(msg))
            {
                TempData["okEmail"] = true;
                return Json(true);
            }
            else return Json(msg);
        }
        public static string EmailExists(SportsProContext ctx, string email)
        {
            string msg = "";
            if (!string.IsNullOrEmpty(email))
            {
                var customer = ctx.Customers.FirstOrDefault(
                    c => c.Email.ToLower() == email.ToLower()); 
                if (customer != null)
                    msg = $"Email address {email} already in use.";

            }
            return msg;
        }
    }
}
