using BytologyTask.Models;
using BytologyTask.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BytologyTask.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index()

        {
            JsonServices jsonService = new JsonServices();
            List<blogPosts> blogItems  =  jsonService.processFile(Server.MapPath("~/App_Data/Blog-Posts.json"));

            Session["data"] = blogItems;

            return View(blogItems);
        }


        public ActionResult Details(int id)
        {
            List<blogPosts> blogs =(List<blogPosts>) Session["data"];
           blogPosts item = blogs.Find(x => x.id == id);
          
            return View(item);
        }

        [HttpPost]
        [Route("/Blog/submitComment")]
        public ActionResult submitComment(  FormCollection form)
        {

            List<blogPosts> blogs = (List<blogPosts>)Session["data"];

            string name = form["name"];
            string email = form["emailAddress"];
            string message = form["message"];
            int postId = int.Parse( form["postId"]);
            Comment theComment = new Comment { emailAddress = email, message = message, name = name };
            theComment.date = DateTime.Now;

            blogPosts item = blogs.Find(x => x.id == postId);
            item.Comments.Add(theComment);

            System.IO.File.Delete(Server.MapPath("~/App_Data/Blog-Posts.json"));
            var jsonString = JsonConvert.SerializeObject(blogs);

            string result = "{\"blogPosts\":" + jsonString + "}";
            System.IO.File.WriteAllText(Server.MapPath("~/App_Data/Blog-Posts.json"), result);
            
             
            return RedirectToAction("Details",new {id = postId });
        }



        [HttpPost]
        [Route("/Blog/submitReply")]
        public ActionResult submitReply(FormCollection form)
        {

            List<blogPosts> blogs = (List<blogPosts>)Session["data"];

            string name = form["replyname"];
            string email = form["replyemailAddress"];
            string message = form["replymessage"];
            //int commentId = int.Parse(form["commentId"]);
            int postId = int.Parse(form["postId"]);

            DateTime commentDate = DateTime.Parse(form["commentDate"]);
            Replies theReply = new Replies { emailAddress = email, message = message, name = name };
            theReply.date = DateTime.Now;

            blogPosts item = blogs.Find(x => x.id == postId);
            Comment comment = item.Comments.Where(x => x.date.ToString() == commentDate.ToString()).FirstOrDefault();
            comment.replies = new List<Replies>();
            comment.replies.Add(theReply);

            System.IO.File.Delete(Server.MapPath("~/App_Data/Blog-Posts.json"));
            var jsonString = JsonConvert.SerializeObject(blogs);

            string result = "{\"blogPosts\":" + jsonString + "}";
            System.IO.File.WriteAllText(Server.MapPath("~/App_Data/Blog-Posts.json"), result);


            return RedirectToAction("Details", new { id = postId });
        }

    }
}