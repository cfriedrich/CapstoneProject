using LanguageInformant.Domain.Abstract;
using LanguageInformant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageInformant.WebUI.Models
{
    public class WordViewModel
    {

        private IMeaningRepository meaningRepo;
        private IWordRepository wordRepo;

        public Word word { get; set; }
        public Meaning meaning { get; set; }

    }
}