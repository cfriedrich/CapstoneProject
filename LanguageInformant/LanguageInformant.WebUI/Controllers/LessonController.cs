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
    public class LessonController : Controller
    {
        private ILessonRepository repository = new EFLessonRepository();

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
            return View(repository.GetLessons());
        }

        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Create(Lesson lesson)
        {
            repository.AddLesson(lesson);
            return View();
        }

        public ViewResult Edit(int lessonId)
        {
            Lesson thisLesson = repository.GetLesson(lessonId);
            return View(thisLesson);
        }

        [HttpPost]
        public ActionResult Edit(Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                repository.SaveLesson(lesson);
                TempData["message"] = string.Format("{0} has been saved", lesson.Name);
                return RedirectToAction("List", "Lesson");
            }
            else
            {
                // There is something wrong with the data values

                return View(lesson);
            }
        }

        public ViewResult Delete(int lessonId)
        {
            Lesson thisLesson = repository.GetLesson(lessonId);
            return View(thisLesson);
        }

        [HttpPost]
        public ActionResult Delete(Lesson lesson)
        {
            repository.DeleteLesson(lesson.LessonID);

            return RedirectToAction("List", "Lesson");

        }

        public ViewResult Details(int lessonId)
        {
            Lesson thisLesson = repository.GetLesson(lessonId);
            return View(thisLesson);
        }
    }
}