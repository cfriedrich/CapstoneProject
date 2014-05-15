using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageInformant.Domain.Entities
{
    public class Lesson
    {
        public int LessonID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Unit Unit { get; set; }
        public virtual ICollection<Member> Members { get; set; }
        public virtual ICollection<Scene> Scenes { get; set; }
        public virtual ICollection<Vocabulary> Vocabularies { get; set; }
    }
}
