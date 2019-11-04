using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ZadatakWM.Models;

namespace ZadatakWM.Services.Json
{
    public class JsonService
    {
        private static string jsonFile = "C:/Users/petar/source/repos/WMzadatak/WMzadatak/Json.json";
        private static string jsonData = System.IO.File.ReadAllText(jsonFile);
        public static List<Proizvod> DeserializeJson()
        {
            var proizList = JsonConvert.DeserializeObject<List<Proizvod>>(jsonData);
            return proizList;
        }

        public static Proizvod AddOrEditJson(int id)
        {
            if (id == 0)
                return new Proizvod();
            else
            {
                return DeserializeJson().Where(x => x.id == id).FirstOrDefault();
            }
        }

        public static string AddOrEditJson(Proizvod proiz)
        {
            int uniqueId = Guid.NewGuid().GetHashCode();
            var proizList = JsonConvert.DeserializeObject<List<Proizvod>>(jsonData);
            if (proiz.id == 0)
            {
                proiz.id = uniqueId;
                proizList.Add(proiz);
                jsonData = JsonConvert.SerializeObject(proizList, Formatting.Indented);
                System.IO.File.WriteAllText(jsonFile, jsonData);
                return "Saved Successfully";
            }
            else
            {
                var oldValue = proizList.Where(x => x.id == proiz.id).FirstOrDefault();
                proizList.Remove(oldValue);
                proizList.Add(proiz);
                jsonData = JsonConvert.SerializeObject(proizList, Formatting.Indented);
                System.IO.File.WriteAllText(jsonFile, jsonData);
                return "Updated Successfully";
            }
        }

        public static string DeleteJson(int id)
        {
            var proizList = JsonConvert.DeserializeObject<List<Proizvod>>(jsonData);
            var remove = proizList.Where(x => x.id == id).FirstOrDefault();
            proizList.Remove(remove);
            jsonData = JsonConvert.SerializeObject(proizList, Formatting.Indented);
            System.IO.File.WriteAllText(jsonFile, jsonData);
            return "Deleted Successfully";
        }
    }
}