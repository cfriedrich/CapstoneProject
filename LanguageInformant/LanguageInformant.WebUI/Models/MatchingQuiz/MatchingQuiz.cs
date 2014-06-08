using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageInformant.WebUI.Models
{
    public class MatchingQuiz
    {
        public int Id { get; set; }
        private IList<MatchingImages> _images = new List<MatchingImages>();
        public string Name { get; set; }
        public string Word { get; set; }

        public IList<MatchingImages> Images
        {
            get { return _images; }
            set { _images = value; }
        }

        public void AddImage(IList<MatchingImages> images)
        {
            foreach (var image in images)
            {
                AddImage(image);
            }
        }

        public void AddImage(MatchingImages image)
        {
            _images.Add(image);
        }

        public double TotalPoints
        {
            get
            {
                return (from i in _images
                        select i.Point).Sum();
            }
        }
    }
}