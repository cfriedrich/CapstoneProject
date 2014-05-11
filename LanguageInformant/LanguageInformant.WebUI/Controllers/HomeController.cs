using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using LanguageInformant.Domain.Entities;
using LanguageInformant.Domain.Concrete;
using LanguageInformant.Domain.Abstract;
using LanguageInformant.WebUI.Models;

namespace LanguageInformant.WebUI.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ViewResult Dictionary()
        {
            var wordModel = new WordViewModel();
            return View(wordModel);
        }

        [HttpPost]
        public ActionResult Dictionary(WordViewModel wordModel)
        {
            return RedirectToAction("ShowWord");
        }

        public ViewResult ShowWord(string word)
        {
            /*
            Word newWord = wordRepo.GetWord(word);
            if (newWord == null)
            {
                ViewBag.wordError = "That word does not exist in our database! Please check back later.";
                return View("Dictionary");
            }
            else
             */
            
            return View();
        }

        public ViewResult Course()
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

        
    }
}