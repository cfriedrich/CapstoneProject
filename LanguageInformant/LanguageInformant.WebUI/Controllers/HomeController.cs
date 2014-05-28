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
        private ComprehensionViewModel _compLesson;

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
            if (word == null)
            {
                ViewBag.wordError = "That word does not exist in our database! Please check back later.";
                return View("Dictionary");
            }
            else
                return View("ShowWord", word);
        }

        public ViewResult ShowWord(Word word)
        {
            return View();
        }

        public ViewResult Course()
        {
            return View();
        }

        public ViewResult Comprehension()
        {
            _compLesson = new ComprehensionViewModel();
            var quiz = _compLesson.GetQuiz();
            return View(quiz);
        }

        [HttpPost]
        public ViewResult Comprehension(VocabQuiz quiz)
        {
            //quiz = _compLesson.GetQuiz();
            return View("Grade",quiz);
        }

        public PartialViewResult _question()
        {
            var quiz = _compLesson.GetQuiz();
            return PartialView(quiz);
        }

        public PartialViewResult _answer()
        {

            return PartialView();
        }

       
        public ViewResult Grade(VocabQuiz quiz)
        {
            var grade = _compLesson.Grade(quiz);
            return View("Grade", grade);
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