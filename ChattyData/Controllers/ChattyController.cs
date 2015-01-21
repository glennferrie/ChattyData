using ChattyData.Cloud;
using ChattyData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChattyData.Controllers
{
    [Authorize]
    public class ChattyController : Controller
    {

        public ActionResult Index()
        {
            var model = new List<ChattyMessage>();
            model.Add(new ChattyMessage { Message = "Test 1" });
            return View(model);
        }

        private string __FormatIdentity()
        {
            return User.Identity.AuthenticationType + "``" + User.Identity.Name;
        }

        private ViewResult Render(ChattyMessage model)
        {
            return View(model);
        }

        public ViewResult Query()
        {
            var model = new ChattyMessage();
            model.ActionName = "Query";
            model.ContentType = "text/sql";
            model.LastUpdated = DateTime.Now;
            model.Source = this.__FormatIdentity();
            return Render(model);
        }
        
        public ViewResult CreateData()
        {
            var model = new ChattyMessage();
            model.ActionName = "CreateData";
            model.ContentType = "text/sql;application/json";
            model.LastUpdated = DateTime.Now;
            model.Source = this.__FormatIdentity();
            return Render(model);
        }

        public ViewResult MergeData()
        {
            var model = new ChattyMessage();
            model.ActionName = "CreateData";
            model.ContentType = "text/sql;dontnet/linq>[[CreateData]]";
            model.LastUpdated = DateTime.Now;
            model.Source = this.__FormatIdentity();
            return Render(model);
        }

        public ViewResult Cart()
        {
            return View();
        }

        public ViewResult History()
        {
            var model = new ChattyMessage();
            model.ActionName = "Query.Historic";
            model.ContentType = "application/json";
            model.LastUpdated = DateTime.Now.AddDays(-180);
            model.Source = this.__FormatIdentity();
            return Render(model);
        }

        [HttpPost]
        public ActionResult DoIt(ChattyMessage msg)
        {
            // post content to storage and put a message in the queue. 

            var mgr = new MessageManager();
            mgr.PostMessage(msg);

            return RedirectToAction("Index", "Home"); 
        }
    }
}
