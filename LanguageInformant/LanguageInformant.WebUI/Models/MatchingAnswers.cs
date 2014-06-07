using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageInformant.WebUI.Models
{
    public class MatchingAnswers
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsAnswer { get; set; }
        private MatchingImages _images = new MatchingImages();
        public bool IsSelected { get; set; }

        public MatchingImages Image
        {
            get { return _images; }
            set { _images = value; }
        }
    }
}