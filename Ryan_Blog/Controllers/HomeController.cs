using Microsoft.AspNet.Identity;
using Ryan_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ryan_Blog.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact([Bind(Include = "Id, Name, Email, Message, Phone, MessageSent")] Contacts contact)        
        {
            contact.MessageSent = DateTime.Now;

            var svc = new EmailService();
            var msg = new IdentityMessage();
            

            msg.Subject = "Contact From Web Site";
            msg.Body = "Name: " + contact.Name + "<br>" + "Email: " + contact.Email + "<br>" + "Message: " + contact.Message;

            await svc.SendAsync(msg);

            return View(contact);
        }
    }
}