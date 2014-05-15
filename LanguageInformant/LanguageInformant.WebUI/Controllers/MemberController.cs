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
    public class MemberController : Controller
    {
        private IMemberRepository repository = new EFMemberRepository();

        public ActionResult Index()
        {
            Lesson testLesson = new Lesson { Name = "TestLesson", Description = "TestLesson" };
            Member testMember = new Member();

            testMember.Lessons.Add(testLesson);

            return View(testMember);
        }


	}
}