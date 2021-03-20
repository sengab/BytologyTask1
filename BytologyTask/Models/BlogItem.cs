using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BytologyTask.Models
{
    public class blogPosts
    {
        [JsonProperty("id")]
        public int id { set; get; }

        [JsonProperty("date")]
        public DateTime date { set; get; }
        [JsonProperty("title")]
        public string title { set; get; }
        [JsonProperty("image")]
        public string image { set; get; }
        [JsonProperty("htmlContent")]
        public string htmlContent { set; get; }
        [JsonProperty("comments")]
        public List< Comment> Comments { set; get; }

    }
}