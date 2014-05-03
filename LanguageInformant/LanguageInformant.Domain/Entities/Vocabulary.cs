using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageInformant.Domain.Entities
{
    public class Vocabulary
    {
        public int VocabularyID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Lesson Lesson { get; set; }
        public virtual ICollection<Word> Words { get; set; }
    }
}
