using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageInformant.Domain.Entities
{
    public class Unit
    {
        public int UnitID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
