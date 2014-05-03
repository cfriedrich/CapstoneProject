using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageInformant.Domain.Entities
{
    public class Language
    {
        public int LanguageID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Level> Levels { get; set; }
    }
}
