using LanguageInformant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageInformant.WebUI.Models
{
    public class WordDetailsViewModel
    {
        public Language Language { get; set; }
        public Word Word {get; set;}
        public IEnumerable<Meaning> Meanings { get; set; }
    }
}