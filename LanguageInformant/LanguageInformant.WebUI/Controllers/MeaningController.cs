using LanguageInformant.Domain.Abstract;
using LanguageInformant.Domain.Concrete;
using LanguageInformant.Domain.Entities;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LanguageInformant.WebUI.Controllers
{
    public class MeaningController : Controller
    {
        private IMeaningRepository repository = new EFMeaningRepository();

        //public MeaningController(IMeaningRepository meaningRepository)
        //{
        //    this.repository = meaningRepository;
        //}

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

        public ViewResult EditMeaning(int meaningId)
        {
            Meaning thisMeaning = repository.GetMeaning(meaningId);
            return View(thisMeaning);
        }

        [HttpPost]
        public ActionResult EditMeaning(Meaning meaning, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    meaning.ImageMimeType = image.ContentType;
                    meaning.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(meaning.ImageData, 0, image.ContentLength);
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

        //[HttpPost]
        //public ActionResult EditMeaning(Meaning meaning, HttpPostedFileBase Picture)
        //{

        //    //if (ModelState.IsValid)
        //    //{
        //    //    //if (Picture != null)
        //    //    //{

        //            Int32 length = Picture.ContentLength;

        //            //    byte[] tempImage = new byte[length];
        //            //    Picture.InputStream.Read(tempImage, 0, length);

        //            //    meaning.Picture = tempImage;

        //            //    //string dataurl = Picture.ToString();
        //            //    //var data = dataurl.Substring(dataurl.IndexOf(",") + 1);
        //            //    //var newfile = Convert.FromBase64String(data);
        //            //    //    string bs64OfBytes = Convert.ToBase64String();


        //            //    //           // meaning.ImageMimeType = newfile.ContentType;
        //            //    //            meaning.Picture = new byte[newfile.Length];
        //            //    //            Picture.InputStream.Read(meaning.Picture, 0, newfile.Length);
        //            // }
        //            //            repository.SaveMeaning(meaning);
        //            //            TempData["message"] = string.Format("{0} has been saved", meaning.Name);
        //            //            return RedirectToAction("SaveMeaning", "Meaning", meaning);
        //            //}
        //            //else
        //            //{
        //            //    //        TempData["mesage"] = string.Format("{0} has not been saved", meaning.Name);
        //            //    //        return RedirectToAction("ListItems", "Item", meaning);
        //            //    //    }
        //            //    ////    string thisImage = image.ToString();
        //            //    ////    thisImage = ToBase64Transform;

        //            //    ////    const string ExpectedImagePrefix = "data:image/jpeg;base64,";
        //            //    ////    //if (thisImage.StartsWith(ExpectedImagePrefix))
        //            //    ////    //{
        //            //    ////    string base64 = thisImage.Substring(ExpectedImagePrefix.Length);
        //            //    ////    byte[] convertedImage = Convert.FromBase64String(base64);

        //            //    ////    //meaning.ImageMimeType = image.ContentType;
        //            //    ////    meaning.Picture = convertedImage;
        //            //    ////    image.InputStream.Read(meaning.Picture, 0, convertedImage.Length);
        //            //    ////}
        //            //    ////    repository.SaveMeaning(meaning);
        //            //        TempData["message"] = string.Format("{0} has been saved", meaning.Name);
        //            //        return RedirectToAction("SaveMeaning", "Meaning", meaning);
        //            //    }
        //            ////else
        //            ////    {

        //            ////        TempData["mesage"] = string.Format("{0} has not been saved", meaning.Name);
        //            ////        return RedirectToAction("List", "Meaning", meaning);
        //            ////        }  
        //            return View();
                
            
        //}

        public ViewResult SaveMeaning(Meaning meaning)
        {
            return View("SaveMeaning", meaning);
        }

        //public FileContentResult GetPicture(int meaningId)
        //{
        //    Meaning meaning = repository.GetMeaning(meaningId);
        //    if (meaning != null)
        //    {
        //        return File(meaning.Picture, meaning.ImageMimeType);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //[HttpPost]
        //public ActionResult Edit(Meaning meaning)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        repository.SaveMeaning(meaning);
        //        TempData["message"] = string.Format("{0} has been saved", meaning.Name);
        //        return RedirectToAction("List", "Meaning");
        //    }
        //    else
        //    {
        //        // There is something wrong with the data values

        //        return View(meaning);
        //    }
        //}

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