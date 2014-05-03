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

        public ViewResult Edit(int meaningId)
        {
            Meaning thisMeaning = repository.GetMeaning(meaningId);
            return View(thisMeaning);
        }

        [HttpPost]
        public ActionResult Edit(Meaning meaning)
        {
            if (ModelState.IsValid)
            {
                repository.SaveMeaning(meaning);
                TempData["message"] = string.Format("{0} has been saved", meaning.Name);
                return RedirectToAction("List", "Meaning");
            }
            else
            {
                // There is something wrong with the data values

                return View(meaning);
            }
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