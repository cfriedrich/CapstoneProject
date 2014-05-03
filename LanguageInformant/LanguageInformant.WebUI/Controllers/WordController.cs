using LanguageInformant.Domain.Abstract;
using LanguageInformant.Domain.Concrete;
using LanguageInformant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LanguageInformant.WebUI.Controllers
{
    public class WordController : Controller
    {
        private IWordRepository repository = new EFWordRepository();

        //public WordController(IWordRepository wordRepository)
        //{
        //    this.repository = wordRepository;
        //}

        public ActionResult Index()
        {
            return View();
        }

        public ViewResult List()
        {
            return View(repository.GetWords());
        }

        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Create(Word word)
        {
            repository.AddWord(word);
            return View();
        }

        public ViewResult Edit(int wordId)
        {
            Word thisWord = repository.GetWord(wordId);
            return View(thisWord);
        }

        [HttpPost]
        public ActionResult Edit(Word word)
        {
            if (ModelState.IsValid)
            {
                repository.SaveWord(word);
                TempData["message"] = string.Format("{0} has been saved", word.Name);
                return RedirectToAction("List", "Word");
            }
            else
            {
                // There is something wrong with the data values

                return View(word);
            }
        }

        public ViewResult Delete(int wordId)
        {
            Word thisWord = repository.GetWord(wordId);
            return View(thisWord);
        }

        [HttpPost]
        public ActionResult Delete(Word word)
        {
            repository.DeleteWord(word.WordID);

            return RedirectToAction("List", "Word");

        }

        public ViewResult Details(int wordId)
        {
            Word thisWord = repository.GetWord(wordId);
            return View(thisWord);
        }

    }
}