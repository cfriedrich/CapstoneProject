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
        LanguageInformantDbContext db = new LanguageInformantDbContext();

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
            SelectList Languages = new SelectList(db.Languages.Take(10), "LanguageID", "Name");
            ViewData["Languages"] = new SelectList(db.Languages, "LanguageID", "Name");
            return View();
        }

        [HttpPost]
        public ViewResult Create(Word word, string languages)
        {
            int languageId = int.Parse(languages);
            Language thisLanguage = db.Languages.Find(languageId);
            this.repository.AddWord(word);
            this.repository.AddLanguage(word.WordID, languageId);
            
            SelectList Languages = new SelectList(db.Languages.Take(10), "LanguageID", "Name");
            ViewData["Languages"] = new SelectList(db.Languages, "LanguageID", "Name");
            
            return View();
        }

        public ViewResult EditMeanings(int WordID)
        {
            Word word = repository.GetWord(WordID);   
            
            var meanings = from m in db.Meanings
                           select new
                           {
                               m.MeaningID,
                               m.Name,
                               m.Description
                           };

            SelectList wordMeanings = new SelectList(word.Meanings, "MeaningID", "Name");
            SelectList meaningList = new SelectList(meanings.Take(10), "MeaningID", "Name");
            ViewData["WordMeanings"] = new SelectList(word.Meanings, "MeaningID", "Name");
            ViewData["MeaningList"] = new SelectList(meanings, "MeaningID", "Name");

            return View(word);
        }

        [HttpPost]
        public ViewResult EditMeanings(string MeaningList, int WordID)
        {
            Word word = repository.GetWord(WordID);
            if (MeaningList != null)
            {
                int meaningId = int.Parse(MeaningList);
                

                Meaning meaning = db.Meanings.Find(meaningId);

                var meanings = from m in db.Meanings
                               select new
                               {
                                   m.MeaningID,
                                   m.Name,
                                   m.Description
                               };

                repository.AddMeaning(word.WordID, meaningId);

                SelectList wordMeanings = new SelectList(word.Meanings.Take(10), "MeaningID", "Name");
                SelectList meaningList = new SelectList(meanings.Take(10), "MeaningID", "Name");
                ViewData["WordMeanings"] = new SelectList(word.Meanings, "MeaningID", "Name");
                ViewData["MeaningList"] = new SelectList(meanings, "MeaningID", "Name");

                
            }
            return View(word);
        }

        public ViewResult DeleteMeanings(string WordMeanings, int WordID)
        {
            Word word = repository.GetWord(WordID);
            if (WordMeanings != null)
            {
                int meaningId = int.Parse(WordMeanings);


                Meaning meaning = db.Meanings.Find(meaningId);

                var meanings = from m in db.Meanings
                               select new
                               {
                                   m.MeaningID,
                                   m.Name,
                                   m.Description
                               };

                repository.RemoveMeaning(word.WordID, meaningId);

                SelectList wordMeanings = new SelectList(word.Meanings.Take(10), "MeaningID", "Name");
                SelectList meaningList = new SelectList(meanings.Take(10), "MeaningID", "Name");
                ViewData["WordMeanings"] = new SelectList(word.Meanings, "MeaningID", "Name");
                ViewData["MeaningList"] = new SelectList(meanings, "MeaningID", "Name");


            }
            return View(word);
        }

        //[HttpPost]
        //public ViewResult EditMeanings(string WordMeanings, string MeaningList, int WordID)
        //{
        //    int meaningId = int.Parse(WordMeanings);
        //    Word word = repository.GetWord(WordID);

        //    Meaning meaning = db.Meanings.Find(meaningId);

        //    var meanings = from m in db.Meanings
        //                   select new
        //                   {
        //                       m.MeaningID,
        //                       m.Name,
        //                       m.Description
        //                   };

        //    SelectList wordMeanings = new SelectList(word.Meanings.Take(10), "MeaningID", "Name");
        //    SelectList meaningList = new SelectList(meanings.Take(10), "MeaningID", "Name");
        //    ViewData["WordMeanings"] = new SelectList(word.Meanings, "MeaningID", "Name");
        //    ViewData["MeaningList"] = new SelectList(meanings, "MeaningID", "Name");

        //    repository.DeleteMeaning(word.WordID, meaningId);

        //    return View();
        //}


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

        public ViewResult Dictionary()
        {
            LanguageInformantDbContext db = new LanguageInformantDbContext();
            var words = from w in db.Words
                           select new
                           {
                               w.WordID,
                               w.Name,
                               w.Description
                           };

            SelectList WordList = new SelectList(words, "WordID", "Name");
            ViewData["WordList"] = new SelectList(words, "WordID", "Name");
            ViewData["Definition"] = "This";

            return View();
        }

        [HttpPost]
        public ViewResult Dictionary(string WordList)
        {
            int wordId = int.Parse(WordList);
            LanguageInformantDbContext db = new LanguageInformantDbContext();
            Word word = db.Words.Find(wordId);
            var words = from w in db.Words
                        select new
                        {
                            w.WordID,
                            w.Name,
                            w.Description
                        };

            ViewData["Definition"] = word.Description;
            ViewData["WordList"] = new SelectList(words, "WordID", "Name");

            return View();
        }

        public ViewResult Defintion(int wordId)
        {
            return View();
        }

    }
}