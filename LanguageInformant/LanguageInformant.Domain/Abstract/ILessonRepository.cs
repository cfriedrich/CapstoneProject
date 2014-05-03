using LanguageInformant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageInformant.Domain.Abstract
{
    public interface ILessonRepository
    {
        IQueryable<Lesson> GetLessons();

        Lesson GetLesson(int lessonID);
        void AddLesson(Lesson lesson);
        void SaveLesson(Lesson lesson);
        Lesson DeleteLesson(int lessonID);
    }
}
