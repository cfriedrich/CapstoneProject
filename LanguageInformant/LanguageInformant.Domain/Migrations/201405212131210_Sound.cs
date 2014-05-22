namespace LanguageInformant.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sound : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        LanguageID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.LanguageID);
            
            CreateTable(
                "dbo.Levels",
                c => new
                    {
                        LevelID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Language_LanguageID = c.Int(),
                    })
                .PrimaryKey(t => t.LevelID)
                .ForeignKey("dbo.Languages", t => t.Language_LanguageID)
                .Index(t => t.Language_LanguageID);
            
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        LessonID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Unit_UnitID = c.Int(),
                    })
                .PrimaryKey(t => t.LessonID)
                .ForeignKey("dbo.Units", t => t.Unit_UnitID)
                .Index(t => t.Unit_UnitID);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.MemberID);
            
            CreateTable(
                "dbo.Scenes",
                c => new
                    {
                        SceneID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Picture = c.Binary(),
                        Lesson_LessonID = c.Int(),
                    })
                .PrimaryKey(t => t.SceneID)
                .ForeignKey("dbo.Lessons", t => t.Lesson_LessonID)
                .Index(t => t.Lesson_LessonID);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        UnitID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.UnitID);
            
            CreateTable(
                "dbo.Vocabularies",
                c => new
                    {
                        VocabularyID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Lesson_LessonID = c.Int(),
                    })
                .PrimaryKey(t => t.VocabularyID)
                .ForeignKey("dbo.Lessons", t => t.Lesson_LessonID)
                .Index(t => t.Lesson_LessonID);
            
            CreateTable(
                "dbo.Words",
                c => new
                    {
                        WordID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Sound = c.Binary(),
                        ContentType = c.String(),
                        SoundVol = c.Int(nullable: false),
                        Language_LanguageID = c.Int(),
                        Vocabulary_VocabularyID = c.Int(),
                        Phrase_PhraseID = c.Int(),
                    })
                .PrimaryKey(t => t.WordID)
                .ForeignKey("dbo.Languages", t => t.Language_LanguageID)
                .ForeignKey("dbo.Vocabularies", t => t.Vocabulary_VocabularyID)
                .ForeignKey("dbo.Phrases", t => t.Phrase_PhraseID)
                .Index(t => t.Language_LanguageID)
                .Index(t => t.Vocabulary_VocabularyID)
                .Index(t => t.Phrase_PhraseID);
            
            CreateTable(
                "dbo.Meanings",
                c => new
                    {
                        MeaningID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                    })
                .PrimaryKey(t => t.MeaningID);
            
            CreateTable(
                "dbo.Phrases",
                c => new
                    {
                        PhraseID = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Sound = c.Binary(),
                    })
                .PrimaryKey(t => t.PhraseID);
            
            CreateTable(
                "dbo.MemberLessons",
                c => new
                    {
                        Member_MemberID = c.Int(nullable: false),
                        Lesson_LessonID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Member_MemberID, t.Lesson_LessonID })
                .ForeignKey("dbo.Members", t => t.Member_MemberID, cascadeDelete: true)
                .ForeignKey("dbo.Lessons", t => t.Lesson_LessonID, cascadeDelete: true)
                .Index(t => t.Member_MemberID)
                .Index(t => t.Lesson_LessonID);
            
            CreateTable(
                "dbo.MeaningWords",
                c => new
                    {
                        Meaning_MeaningID = c.Int(nullable: false),
                        Word_WordID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Meaning_MeaningID, t.Word_WordID })
                .ForeignKey("dbo.Meanings", t => t.Meaning_MeaningID, cascadeDelete: true)
                .ForeignKey("dbo.Words", t => t.Word_WordID, cascadeDelete: true)
                .Index(t => t.Meaning_MeaningID)
                .Index(t => t.Word_WordID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Words", "Phrase_PhraseID", "dbo.Phrases");
            DropForeignKey("dbo.Words", "Vocabulary_VocabularyID", "dbo.Vocabularies");
            DropForeignKey("dbo.MeaningWords", "Word_WordID", "dbo.Words");
            DropForeignKey("dbo.MeaningWords", "Meaning_MeaningID", "dbo.Meanings");
            DropForeignKey("dbo.Words", "Language_LanguageID", "dbo.Languages");
            DropForeignKey("dbo.Vocabularies", "Lesson_LessonID", "dbo.Lessons");
            DropForeignKey("dbo.Lessons", "Unit_UnitID", "dbo.Units");
            DropForeignKey("dbo.Scenes", "Lesson_LessonID", "dbo.Lessons");
            DropForeignKey("dbo.MemberLessons", "Lesson_LessonID", "dbo.Lessons");
            DropForeignKey("dbo.MemberLessons", "Member_MemberID", "dbo.Members");
            DropForeignKey("dbo.Levels", "Language_LanguageID", "dbo.Languages");
            DropIndex("dbo.MeaningWords", new[] { "Word_WordID" });
            DropIndex("dbo.MeaningWords", new[] { "Meaning_MeaningID" });
            DropIndex("dbo.MemberLessons", new[] { "Lesson_LessonID" });
            DropIndex("dbo.MemberLessons", new[] { "Member_MemberID" });
            DropIndex("dbo.Words", new[] { "Phrase_PhraseID" });
            DropIndex("dbo.Words", new[] { "Vocabulary_VocabularyID" });
            DropIndex("dbo.Words", new[] { "Language_LanguageID" });
            DropIndex("dbo.Vocabularies", new[] { "Lesson_LessonID" });
            DropIndex("dbo.Scenes", new[] { "Lesson_LessonID" });
            DropIndex("dbo.Lessons", new[] { "Unit_UnitID" });
            DropIndex("dbo.Levels", new[] { "Language_LanguageID" });
            DropTable("dbo.MeaningWords");
            DropTable("dbo.MemberLessons");
            DropTable("dbo.Phrases");
            DropTable("dbo.Meanings");
            DropTable("dbo.Words");
            DropTable("dbo.Vocabularies");
            DropTable("dbo.Units");
            DropTable("dbo.Scenes");
            DropTable("dbo.Members");
            DropTable("dbo.Lessons");
            DropTable("dbo.Levels");
            DropTable("dbo.Languages");
        }
    }
}
