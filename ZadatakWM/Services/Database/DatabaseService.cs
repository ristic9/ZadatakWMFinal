using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using ZadatakWM.Models;
using System.Web.UI.WebControls;

namespace ZadatakWM.Services.Database
{
    public class DatabaseService
    {
        private static List<Proizvod> proizList;

        public static List<Proizvod> GetData()
        {
            try
            {
                using (ZadatakDBEntities db = new ZadatakDBEntities())
                {
                    proizList = db.Proizvods.ToList();
                    return proizList;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public static Proizvod AddOrEdit(int id)
        {
            if (id == 0)
                return new Proizvod();
            else
            {
                using (ZadatakDBEntities db = new ZadatakDBEntities())
                {
                    return db.Proizvods.Where(x => x.id == id).FirstOrDefault();
                }
            }
        }

        public static string AddOrEdit(Proizvod proiz)
        {
            using (ZadatakDBEntities db = new ZadatakDBEntities())
            {
                try
                {
                    if (proiz.id == 0)
                    {
                        db.Proizvods.Add(proiz);
                        db.SaveChanges();
                        return "Saved Successfully";
                    }
                    else
                    {
                        db.Entry(proiz).State = EntityState.Modified;
                        db.SaveChanges();
                        return "Updated Successfully";
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        public static string Delete(int id)
        {
            using (ZadatakDBEntities db = new ZadatakDBEntities())
            {
                try
                {
                    Proizvod proiz = db.Proizvods.Where(x => x.id == id).FirstOrDefault();
                    db.Proizvods.Remove(proiz);
                    db.SaveChanges();
                    return "Deleted Successfully";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}