using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageInformant.Domain.Entities
{
    public class Meaning
    {
        public int MeaningID { get; set; }
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public byte[] ImageData { get; set; }

        public virtual ICollection<Word> Words { get; set; }

        public string ImageMimeType { get; set; }
    }
}
