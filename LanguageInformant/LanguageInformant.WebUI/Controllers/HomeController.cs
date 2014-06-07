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
            var words = from w in db.Words
                        select new
                        {
                            w.WordID,
                            w.Name,
                            w.Description
                        };

            SelectList name = new SelectList(words.Take(10), "Name", "Name");
            ViewData["Name"] = new SelectList(words, "Name", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult Dictionary(string Name)
        {
            var words = from w in db.Words
                        select new
                        {
                            w.WordID,
                            w.Name,
                            w.Description
                        };

            SelectList name = new SelectList(words.Take(10), "Name", "Name");
            ViewData["Name"] = new SelectList(words, "Name", "Name");

            Word word = repository.GetWord(Name);
            if (word == null)
            {
                ViewBag.wordError = "That word does not exist in our database! Please check back later.";
                return View("Dictionary");
            }
            else
                return RedirectToAction("ShowWord", new { name = Name });
        }

        public ActionResult ShowWord(string name)
        {
            Word word = repository.GetWord(name);
            return View(word);
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
        public ActionResult Comprehension(VocabQuiz Quiz)
        {
            _compLesson = new ComprehensionViewModel();
            var grade = _compLesson.Grade(Quiz);
            return View("Grade", grade);
            //return RedirectToAction("Grade", new { quiz = Quiz });
        }

        public PartialViewResult _question()
        {
            var quiz = _compLesson.GetQuiz();
            return PartialView(quiz);
        }

        public PartialViewResult _answer()
        {
            var quiz = _compLesson.GetQuiz();
            return PartialView(quiz);
        }
        
        public PartialViewResult WordListView()
        {
            var words = from w in db.Words
                        select new
                        {
                            w.WordID,
                            w.Name,
                            w.Description
                        };

            SelectList name = new SelectList(words.Take(10), "Name", "Name");
            ViewData["Name"] = new SelectList(words, "Name", "Name");

            return PartialView();
        }
       
        public ActionResult Grade(VocabQuiz quiz)
        {
            _compLesson = new ComprehensionViewModel();
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