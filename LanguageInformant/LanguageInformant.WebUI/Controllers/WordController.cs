using LanguageInformant.Domain.Abstract;
using LanguageInformant.Domain.Concrete;
using LanguageInformant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguageInformant.WebUI.Common;
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
                foreach (string upload in Request.Files)
                {
                    word.ContentType = Request.Files[upload].ContentType;
                    Stream fileStream = Request.Files[upload].InputStream;
                    word.SoundFileName = Path.GetFileName(Request.Files[upload].FileName);
                    int fileLength = Request.Files[upload].ContentLength;
                    byte[] fileData = new byte[fileLength];
                    word.Sound = fileData;
                    fileStream.Read(word.Sound, 0, fileLength);
                    repository.SaveWord(word);
                }

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

        public ViewResult EditSound(int wordId)
        {
            Word thisWord = repository.GetWord(wordId);
            return View(thisWord);
        }

        [HttpPost]
        public ActionResult EditSound(Word thisWord)
        {
            //Word thisWord = repository.GetWord(wordId);
            foreach (string upload in Request.Files)
            {
                thisWord.ContentType = Request.Files[upload].ContentType;
                Stream fileStream = Request.Files[upload].InputStream;
                thisWord.SoundFileName = Path.GetFileName(Request.Files[upload].FileName);
                int fileLength = Request.Files[upload].ContentLength;
                byte[] fileData = new byte[fileLength];
                thisWord.Sound = fileData;
                fileStream.Read(thisWord.Sound, 0, fileLength);
                repository.SaveWord(thisWord);
            }
            return View();
        }

        //[HttpPost]
        //public ActionResult EditSound(Word word)
        //{
        //    foreach (string upload in Request.Files)
        //    {
        //        if (!Request.Files[upload].HasFile()) continue;
        //        string path = AppDomain.CurrentDomain.BaseDirectory + "Content/Sounds/";
        //        string filename = Path.GetFileName(Request.Files[upload].FileName);
        //        Request.Files[upload].SaveAs(Path.Combine(path, filename));
        //    }
        //    return View();
        //}


        public ViewResult EditWord(int wordId)
        {
            Word thisWord = repository.GetWord(wordId);
            return View(thisWord);
        }

        [HttpPost]
        public ViewResult EditWord(Word word, HttpPostedFile file)
        {
       
    
            word.ContentType = file.ContentType;

            // Get the bytes from the uploaded file
            byte[] fileData = new byte[file.InputStream.Length];
            word.Sound = fileData;
            file.InputStream.Read(fileData, 0, fileData.Length);

            // Get the name without folder information from the uploaded file.
            //string originalName = Path.GetFileName(FileUpload1.PostedFile.FileName);

            // Create a new instance of the File class based on the uploaded file.
            //File myFile = new File(contentType, originalName, fileData);

    // Save the file, and tell the Save method what data store to use.
  //  switch (AppConfiguration.DataStoreType)
  //  {
  //    case DataStoreType.Database:
  //      myFile.Save();
  //      break;
  //    case DataStoreType.FileSystem:
  //      myFile.Save(Server.MapPath(Path.Combine(
  //      AppConfiguration.UploadsFolder, myFile.FileUrl)));
  //      break;
  //  }
  //  Response.Redirect("~/");
  //}  
            return View();
        }

        //[HttpPost]
        //public ActionResult EditWord(Word word)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (Request.Files[0] != null)
        //        {
        //            word.ContentType = Request.Files[0].ContentType;
        //            int offset = word.ContentType.Count();
        //            FileStream fileStream = Request.Files[0].BinaryRead.InputStream;
        //            string fileName = Path.GetFileName(Request.Files[0].FileName);
        //            int fileLength = Request.Files[0].ContentLength - offset;
        //            word.Sound = new byte[fileLength];
                    
        //            fileStream.Read(word.Sound, offset, fileLength);

                    

        //            //HttpPostedFileBase file = Request.Files[0];
        //            //byte[] soundSize = new byte[sound.ContentLength];
        //            //file.InputStream.Read(soundSize, 0, (int)file.ContentLength);

        //            //using (BinaryReader br = new BinaryReader(sound.InputStream))
        //            //{
        //            //    byte[] bytes = br.ReadBytes((int)sound.InputStream.Length);
        //            //    word.Sound = bytes;
        //            //}
        //            //word.ContentType = sound.ContentType;
        //            //word.Sound = new byte[sound.ContentLength];
        //            //sound.InputStream.Read(word.Sound, 0, sound.ContentLength);
        //        }
        //        repository.SaveWord(word);
        //        TempData["message"] = string.Format("{0} has been saved", word.Name);
        //        return RedirectToAction("List");
        //    }
        //    else
        //    {
        //        // there is something wrong with the data values
        //        return View(word);
        //    }
        //}

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

        public FileContentResult GetSound(int wordId)
        {
            Word word = repository.GetWord(wordId);
            if (word.Sound != null)
            {
                return File(word.Sound, word.ContentType, word.SoundFileName);
            }
            else
            {
                return null;
            }
        }

    }
}