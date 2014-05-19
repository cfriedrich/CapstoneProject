using LanguageInformant.Domain.Abstract;
using LanguageInformant.Domain.Concrete;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LanguageInformant.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IWordRepository>().To<EFWordRepository>();
            ninjectKernel.Bind<IMeaningRepository>().To<EFMeaningRepository>();
            ninjectKernel.Bind<ILessonRepository>().To<EFLessonRepository>();
            ninjectKernel.Bind<IVocabularyRepository>().To<EFVocabularyRepository>();
            ninjectKernel.Bind<ILessonRepository>().To<EFLessonRepository>();
        }


    }
}