using LanguageInformant.Domain.Abstract;
using LanguageInformant.Domain.Concrete;
using LanguageInformant.Domain.Entities;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace LanguageInformant.WebUI.Controllers
{
    public class MeaningController : Controller
    {
        private IMeaningRepository repository = new EFMeaningRepository();

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult List()
        {
            return View(repository.GetMeanings());
        }

        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Create(Meaning meaning)
        {
            this.repository.AddMeaning(meaning);
            return View();
        }

        public ViewResult Edit(int meaningId)
        {
            Meaning thisMeaning = repository.GetMeaning(meaningId);
            return View(thisMeaning);
        }

        //[HttpPost]
        //public ActionResult Edit(Meaning meaning, HttpPostedFileBase image)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (image != null)
        //        {
        //            meaning.ImageMimeType = image.ContentType;
        //            meaning.ImageData = new byte[image.ContentLength];
        //            image.InputStream.Read(meaning.ImageData, 0, image.ContentLength);
        //        }
        //        repository.SaveMeaning(meaning);
        //        TempData["message"] = string.Format("{0} has been saved", meaning.Name);
        //        return RedirectToAction("List");
        //    }
        //    else
        //    {
        //        // there is something wrong with the data values
        //        return View(meaning);
        //    }
        //}

        [HttpPost]
        public ActionResult EditMeaning(Meaning meaning)
        {
            if (ModelState.IsValid)
            {
              foreach (string upload in Request.Files)
                {
                    meaning.ImageMimeType = Request.Files[upload].ContentType;
                    Stream fileStream = Request.Files[upload].InputStream;
                    //word.SoundFileName = Path.GetFileName(Request.Files[upload].FileName);
                    int fileLength = Request.Files[upload].ContentLength;
                    byte[] fileData = new byte[fileLength];
                    meaning.ImageData = fileData;
                    fileStream.Read(meaning.ImageData, 0, fileLength);
                    repository.SaveMeaning(meaning);
                }
                repository.SaveMeaning(meaning);
                TempData["message"] = string.Format("{0} has been saved", meaning.Name);
                return RedirectToAction("List");
            }
            else
            {
                // there is something wrong with the data values
                return View(meaning);
            }
        }



        public FileContentResult GetPicture(int meaningId)
        {
            Meaning meaning = repository.GetMeaning(meaningId);
            if (meaning != null)
            {
                return File(meaning.ImageData, meaning.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        public ViewResult SaveMeaning(Meaning meaning)
        {
            return View("SaveMeaning", meaning);
        }

        public ViewResult Delete(int meaningId)
        {
            Meaning thisMeaning = repository.GetMeaning(meaningId);
            return View(thisMeaning);
        }

        [HttpPost]
        public ActionResult Delete(Meaning meaning)
        {
            repository.DeleteMeaning(meaning.MeaningID);

            return RedirectToAction("List", "Meaning");

        }

        public ViewResult Details(int meaningId)
        {
            Meaning thisMeaning = repository.GetMeaning(meaningId);
            return View(thisMeaning);
        }

    }
}