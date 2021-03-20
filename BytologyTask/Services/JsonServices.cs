using BytologyTask.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BytologyTask.Services
{
    public class JsonServices
    {
        public List<blogPosts> processFile (string FilePath)
        {
            using (StreamReader r = new StreamReader(FilePath))
            {
                string json = r.ReadToEnd();
                JObject o = JObject.Parse(json);
                JArray Topics = (JArray)o["blogPosts"];
                return Topics.ToObject<List<blogPosts>>();

            }
        }
    }
}