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

        private IWordRepository repository = new EFWordRepository();
        LanguageInformantDbContext db = new LanguageInformantDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ViewResult Dictionary()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Dictionary(string Name)
        {
            Word word = repository.GetWord(Name);

            return View("ShowWord", word);
        }

        public ViewResult ShowWord(Word word)
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