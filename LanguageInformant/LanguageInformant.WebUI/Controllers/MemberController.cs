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
            return View();
        }

        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Create(Member member)
        {
            repository.AddMember(member);
            return View();
        }

        public ViewResult List()
        {
            return View(repository.GetMembers());
        }
        
        public PartialViewResult MemberInfo(string userName)
        {
            Member member = repository.GetMember(userName);
            return PartialView(member);
        }

	}
}