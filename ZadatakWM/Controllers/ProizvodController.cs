using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ZadatakWM.Models;

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
            try
            {
                using (ZadatakDBEntities db = new ZadatakDBEntities())
                {
                    List<Proizvod> proizList = db.Proizvods.ToList();
                    return Json(new { data = proizList }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Proizvod());
            else
            {
                using (ZadatakDBEntities db = new ZadatakDBEntities())
                {
                    return View(db.Proizvods.Where(x => x.id == id).FirstOrDefault());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(Proizvod proiz)
        {
            using (ZadatakDBEntities db = new ZadatakDBEntities())
            {
                try
                {
                    if (proiz.id == 0)
                    {
                        db.Proizvods.Add(proiz);
                        db.SaveChanges();
                        return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        db.Entry(proiz).State = EntityState.Modified;
                        db.SaveChanges();
                        return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { data = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (ZadatakDBEntities db = new ZadatakDBEntities())
            {
                try
                {
                    Proizvod proiz = db.Proizvods.Where(x => x.id == id).FirstOrDefault();
                    db.Proizvods.Remove(proiz);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { data = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        // SVE VEZANO ZA RAD SA JSONOM JE ISPOD
        public ActionResult IndexJson()
        {
            return View();
        }

        private List<Proizvod> DeserializeJson()
        {
            string jsonFile = "C:/Users/petar/source/repos/WMzadatak/WMzadatak/Json.json";
            string jsonData = System.IO.File.ReadAllText(jsonFile);
            var proizList = JsonConvert.DeserializeObject<List<Proizvod>>(jsonData);
            return proizList;
        }
        public ActionResult GetDataJson()
        {
            return Json(new { data = DeserializeJson() }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddOrEditJson(int id = 0)
        {
            if (id == 0)
                return View(new Proizvod());
            else
            {
                return View(DeserializeJson().Where(x => x.id == id).FirstOrDefault());
            }
        }

        [HttpPost]
        public ActionResult AddOrEditJson(Proizvod proiz)
        {
            int uniqueId = Guid.NewGuid().GetHashCode();
            string jsonFile = "C:/Users/petar/source/repos/WMzadatak/WMzadatak/Json.json";
            string jsonData = System.IO.File.ReadAllText(jsonFile);
            var proizList = JsonConvert.DeserializeObject<List<Proizvod>>(jsonData);
            if (proiz.id == 0)
            {
                proiz.id = uniqueId;
                proizList.Add(proiz);
                jsonData = JsonConvert.SerializeObject(proizList, Formatting.Indented);
                System.IO.File.WriteAllText(jsonFile, jsonData);
                return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var oldValue = proizList.Where(x => x.id == proiz.id).FirstOrDefault();
                proizList.Remove(oldValue);
                proizList.Add(proiz);
                jsonData = JsonConvert.SerializeObject(proizList, Formatting.Indented);
                System.IO.File.WriteAllText(jsonFile, jsonData);
                return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteJson(int id)
        {
            string jsonFile = "C:/Users/petar/source/repos/WMzadatak/WMzadatak/Json.json";
            string jsonData = System.IO.File.ReadAllText(jsonFile);
            var proizList = JsonConvert.DeserializeObject<List<Proizvod>>(jsonData);
            var remove = proizList.Where(x => x.id == id).FirstOrDefault();
            proizList.Remove(remove);
            jsonData = JsonConvert.SerializeObject(proizList, Formatting.Indented);
            System.IO.File.WriteAllText(jsonFile, jsonData);
            return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}