using LanguageInformant.Domain.Abstract;
using LanguageInformant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageInformant.Domain.Concrete
{
    public class EFMemberRepository : IMemberRepository
    {

        public IQueryable<Member> GetMembers()
        {
            var db = new LanguageInformantDbContext();
            return (from w in db.Members select w);
        }

        public void AddMember(Member Member)
        {
            var db = new LanguageInformantDbContext();
            db.Members.Add(Member);
            db.SaveChanges();
        }

        public void SaveMember(Member Member)
        {
            var db = new LanguageInformantDbContext();

            if (Member.MemberID == 0)
            {
                db.Members.Add(Member);
                db.SaveChanges();
            }
            else
            {
                Member dbEntry = db.Members.Find(Member.MemberID);
                if (dbEntry != null)
                {
                    dbEntry.Lessons = Member.Lessons;
                    db.SaveChanges();
                }
            }
        }

        public Member DeleteMember(int MemberID)
        {
            var db = new LanguageInformantDbContext();
            Member dbEntry = db.Members.Find(MemberID);
            if (dbEntry != null)
            {
                db.Members.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void AddLesson(int MemberID, int lessonID)
        {
            var db = new LanguageInformantDbContext();
            Member Member = db.Members.Find(MemberID);
            Lesson lesson = db.Lessons.Find(lessonID);
            Member.Lessons.Add(lesson);
            db.SaveChanges();
        }

        public void RemoveLesson(int MemberID, int lessonID)
        {
            var db = new LanguageInformantDbContext();
            Member Member = db.Members.Find(MemberID);
           Lesson lesson = db.Lessons.Find(lessonID);
            Member.Lessons.Remove(lesson);
            db.SaveChanges();
        }

        public Member GetMember(int MemberID)
        {
            var db = new LanguageInformantDbContext();
            Member dbEntry = db.Members.Find(MemberID);

            return dbEntry;
        }
    }
}
