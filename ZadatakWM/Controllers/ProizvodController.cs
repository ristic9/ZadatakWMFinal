using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ZadatakWM.Models;
using ZadatakWM.Services.Database;
using ZadatakWM.Services.Json;

namespace ZadatakWM.Controllers
{
    public class ProizvodController : Controller
    {
        // GET: Proizvod
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            return Json(new { data = DatabaseService.GetData() }, JsonRequestBehavior.AllowGet);
            
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            return View(DatabaseService.AddOrEdit(id));
        }

        [HttpPost]
        public ActionResult AddOrEdit(Proizvod proiz)
        {
            return Json(new { success = true, message = DatabaseService.AddOrEdit(proiz) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return Json(new { success = true, message = DatabaseService.Delete(id) }, JsonRequestBehavior.AllowGet);
        }

        // SVE VEZANO ZA RAD SA JSONOM JE ISPOD; Ovo je moglo da se podeli u ProizvodDatabaseController i ProizvodJsonController
        public ActionResult IndexJson()
        {
            return View();
        }

        public ActionResult GetDataJson()
        {
            return Json(new { data = JsonService.DeserializeJson() }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddOrEditJson(int id = 0)
        {
            return View(JsonService.AddOrEditJson(id));
        }

        [HttpPost]
        public ActionResult AddOrEditJson(Proizvod proiz)
        {
            return Json(new { success = true, message = JsonService.AddOrEditJson(proiz) }, JsonRequestBehavior.AllowGet);
    
        }

        [HttpPost]
        public ActionResult DeleteJson(int id)
        {
            return Json(new { success = true, message = JsonService.DeleteJson(id) }, JsonRequestBehavior.AllowGet);
        }
    }
}