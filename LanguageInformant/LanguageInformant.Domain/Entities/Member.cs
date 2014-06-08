using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LanguageInformant.Domain.Entities
{
    public class Member
    {
        public int MemberID { get; set; }

        [Required]
        public string Name { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<Word> Words { get; set; }
    }
}
