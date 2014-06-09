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
    public class VocabularyController : Controller
    {

        private IVocabularyRepository repository = new EFVocabularyRepository();
        LanguageInformantDbContext db = new LanguageInformantDbContext();

        //public LessonController(ILessonRepository lessonRepository)
        //{
        //    this.repository = lessonRepository;
        //}

        public ActionResult Index()
        {
            return View();
        }

        public ViewResult List()
        {
            return View(repository.GetVocabularies());
        }

        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Create(Vocabulary vocabulary)
        {
            repository.AddVocabulary(vocabulary);
            return View();
        }

        public ViewResult Edit(int vocabularyId)
        {
            Vocabulary thisVocabulary = repository.GetVocabulary(vocabularyId);
            return View(thisVocabulary);
        }

        [HttpPost]
        public ActionResult Edit(Vocabulary vocabulary)
        {
            if (ModelState.IsValid)
            {
                repository.SaveVocabulary(vocabulary);
                TempData["message"] = string.Format("{0} has been saved", vocabulary.Name);
                return RedirectToAction("List", "Vocabulary");
            }
            else
            {
                // There is something wrong with the data values

                return View(vocabulary);
            }
        }

        public ViewResult Delete(int vocabularyId)
        {
            Vocabulary thisVocabulary = repository.GetVocabulary(vocabularyId);
            return View(thisVocabulary);
        }

        [HttpPost]
        public ActionResult Delete(Vocabulary vocabulary)
        {
            repository.DeleteVocabulary(vocabulary.VocabularyID);

            return RedirectToAction("List", "Vocabulary");

        }

        public ViewResult Details(int vocabularyId)
        {
            Vocabulary vocabulary = repository.GetVocabulary(vocabularyId);
            return View(vocabulary);
        }

        public ViewResult AssignWords(int VocabularyID)
        {
            Vocabulary vocabulary = repository.GetVocabulary(VocabularyID);

            var words = from w in db.Words
                           select new
                           {
                               w.WordID,
                               w.Name,
                               w.Description
                           };

            SelectList vocabularyWords = new SelectList(vocabulary.Words, "WordID", "Name");
            SelectList wordList = new SelectList(words.Take(10), "WordID", "Name");
            ViewData["VocabularyWords"] = new SelectList(vocabulary.Words, "WordID", "Name");
            ViewData["WordList"] = new SelectList(words, "WordID", "Name");

            return View(vocabulary);
        }

        [HttpPost]
        public ViewResult AssignWords(string WordList, int VocabularyID)
        {
            Vocabulary vocabulary = repository.GetVocabulary(VocabularyID); 
            
            if (WordList != null)
            {
                int wordId = int.Parse(WordList);


                Word word = db.Words.Find(wordId);

                var words = from w in db.Words
                            select new
                            {
                                w.WordID,
                                w.Name,
                                w.Description
                            };

                repository.AddWord(vocabulary.VocabularyID, word.WordID);

                SelectList vocabularyWords = new SelectList(vocabulary.Words, "WordID", "Name");
                SelectList wordList = new SelectList(words.Take(10), "WordID", "Name");
                ViewData["VocabularyWords"] = new SelectList(vocabulary.Words, "WordID", "Name");
                ViewData["WordList"] = new SelectList(words, "WordID", "Name");


            }
            return View(vocabulary);
        }

        public ViewResult DeleteWords(int VocabularyID)
        {
            if (ModelState.IsValid)
            {
                Vocabulary vocabulary = repository.GetVocabulary(VocabularyID);

                var words = from w in db.Words
                            select new
                            {
                                w.WordID,
                                w.Name,
                                w.Description
                            };

                SelectList vocabularyWords = new SelectList(vocabulary.Words, "WordID", "Name");
                //SelectList wordList = new SelectList(words.Take(10), "WordID", "Name");
                ViewData["VocabularyWords"] = new SelectList(vocabulary.Words, "WordID", "Name");
                //ViewData["WordList"] = new SelectList(words, "WordID", "Name");

                return View(vocabulary);
            }
            return View();
        }

        [HttpPost]
        public ViewResult DeleteWords(string VocabularyWords, int VocabularyID)
        {
            if (ModelState.IsValid)
            {
                Vocabulary vocabulary = repository.GetVocabulary(VocabularyID);
                if (VocabularyWords != null)
                {
                    int wordId = int.Parse(VocabularyWords);


                    Word word = db.Words.Find(wordId);

                    var words = from w in db.Words
                                select new
                                {
                                    w.WordID,
                                    w.Name,
                                    w.Description
                                };

                    repository.RemoveWord(vocabulary.VocabularyID, wordId);

                    SelectList vocabularyWords = new SelectList(vocabulary.Words, "WordID", "Name");
                    //SelectList wordList = new SelectList(words.Take(10), "WordID", "Name");
                    ViewData["VocabularyWords"] = new SelectList(vocabulary.Words, "WordID", "Name");
                    //ViewData["WordList"] = new SelectList(words, "WordID", "Name");


                }
                return View(vocabulary);
            }
            return View();
        }

        public PartialViewResult VocabularyWords(Vocabulary vocabulary)
        {
            return PartialView(vocabulary);
        }
    }
}