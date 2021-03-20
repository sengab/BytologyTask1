using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BytologyTask.Models
{
    public class Replies
    {
        public string name { set; get; }
        public DateTime date { set; get; }

        public string emailAddress { set; get; }

        public string message { set; get; }
        
        public int CommentId { set; get; }
    }
}