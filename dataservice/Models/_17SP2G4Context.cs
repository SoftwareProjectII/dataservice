using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace dataservice.Models
{
    public partial class _17SP2G4Context : DbContext
    {
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Certificate> Certificate { get; set; }
        public virtual DbSet<Faq> Faq { get; set; }
        public virtual DbSet<Followingtraining> Followingtraining { get; set; }
        public virtual DbSet<Infotosession> Infotosession { get; set; }
        public virtual DbSet<Survey> Survey { get; set; }
        public virtual DbSet<Surveyanswer> Surveyanswer { get; set; }
        public virtual DbSet<Surveyquestion> Surveyquestion { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }
        public virtual DbSet<Traininginfo> Traininginfo { get; set; }
        public virtual DbSet<Trainingsession> Trainingsession { get; set; }
        public virtual DbSet<User> User { get; set; }

        // Unable to generate entity type for table 'dbo.TRAININGSBOOK'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.TRAININGFAQ'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.TRAININGSURVEY'. Please see the warning messages.

        public _17SP2G4Context(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("ADDRESS");

                entity.Property(e => e.AddressId).HasColumnName("addressID");

                entity.Property(e => e.AdministrativeArea)
                    .IsRequired()
                    .HasColumnName("administrativeArea")
                    .HasMaxLength(50);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasMaxLength(50);

                entity.Property(e => e.Locality)
                    .IsRequired()
                    .HasColumnName("locality")
                    .HasMaxLength(50);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasColumnName("postalCode")
                    .HasMaxLength(20);

                entity.Property(e => e.Premise)
                    .HasColumnName("premise")
                    .HasMaxLength(30);

                entity.Property(e => e.StreetAddress)
                    .IsRequired()
                    .HasColumnName("streetAddress")
                    .HasMaxLength(85);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Isbn);

                entity.ToTable("BOOK");

                entity.Property(e => e.Isbn).HasColumnName("isbn");

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasColumnName("author")
                    .HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Publisher)
                    .IsRequired()
                    .HasColumnName("publisher")
                    .HasMaxLength(50);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Certificate>(entity =>
            {
                entity.ToTable("CERTIFICATE");

                entity.Property(e => e.CertificateId).HasColumnName("certificateID");

                entity.Property(e => e.Picture)
                    .HasColumnName("picture")
                    .HasColumnType("image");

                entity.Property(e => e.Titel)
                    .IsRequired()
                    .HasColumnName("titel")
                    .HasMaxLength(150);

                entity.Property(e => e.TrainingId).HasColumnName("trainingID");

                entity.HasOne(d => d.Training)
                    .WithMany(p => p.Certificate)
                    .HasForeignKey(d => d.TrainingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CERTIFICATE_TRAININGINFO");
            });

            modelBuilder.Entity<Faq>(entity =>
            {
                entity.ToTable("FAQ");

                entity.Property(e => e.FaqId).HasColumnName("faqID");

                entity.Property(e => e.AnswerFaq)
                    .IsRequired()
                    .HasColumnName("answerFAQ");

                entity.Property(e => e.QuestionFaq)
                    .IsRequired()
                    .HasColumnName("questionFAQ");
            });

            modelBuilder.Entity<Followingtraining>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.TrainingSessionId });

                entity.ToTable("FOLLOWINGTRAINING");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.TrainingSessionId).HasColumnName("trainingSessionID");

                entity.HasOne(d => d.TrainingSession)
                    .WithMany(p => p.Followingtraining)
                    .HasForeignKey(d => d.TrainingSessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FOLLOWINGTRAINING_TRAININGSESSION");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Followingtraining)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FOLLOWINGTRAINING_USER");
            });

            modelBuilder.Entity<Infotosession>(entity =>
            {
                entity.HasKey(e => new { e.TrainingId, e.TrainingSessionId });

                entity.ToTable("INFOTOSESSION");

                entity.Property(e => e.TrainingId).HasColumnName("trainingID");

                entity.Property(e => e.TrainingSessionId).HasColumnName("trainingSessionID");

                entity.HasOne(d => d.Training)
                    .WithMany(p => p.Infotosession)
                    .HasForeignKey(d => d.TrainingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_INFOTOSESSION_TRAININGINFO");
            });

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.ToTable("SURVEY");

                entity.Property(e => e.SurveyId).HasColumnName("surveyID");
            });

            modelBuilder.Entity<Surveyanswer>(entity =>
            {
                entity.HasKey(e => e.AnswerId);

                entity.ToTable("SURVEYANSWER");

                entity.Property(e => e.AnswerId).HasColumnName("answerID");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content");

                entity.Property(e => e.QuestionId).HasColumnName("questionID");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Surveyanswer)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SURVEYANSWER_SURVEYQUESTION");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Surveyanswer)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_SURVEYANSWER_USER");
            });

            modelBuilder.Entity<Surveyquestion>(entity =>
            {
                entity.HasKey(e => e.QuestionId);

                entity.ToTable("SURVEYQUESTION");

                entity.Property(e => e.QuestionId).HasColumnName("questionID");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content");

                entity.Property(e => e.SurveyId).HasColumnName("surveyID");

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.Surveyquestion)
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SURVEYQUESTION_SURVEY");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("TEACHER");

                entity.Property(e => e.TeacherId).HasColumnName("teacherID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(320);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasColumnName("phoneNumber")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Traininginfo>(entity =>
            {
                entity.HasKey(e => e.TrainingId);

                entity.ToTable("TRAININGINFO");

                entity.Property(e => e.TrainingId).HasColumnName("trainingID");

                entity.Property(e => e.InfoExam)
                    .IsRequired()
                    .HasColumnName("infoExam");

                entity.Property(e => e.InfoGeneral)
                    .IsRequired()
                    .HasColumnName("infoGeneral");

                entity.Property(e => e.InfoPayment)
                    .IsRequired()
                    .HasColumnName("infoPayment");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.NumberOfDays).HasColumnName("numberOfDays");

                entity.Property(e => e.Price).HasColumnName("price");
            });

            modelBuilder.Entity<Trainingsession>(entity =>
            {
                entity.ToTable("TRAININGSESSION");

                entity.Property(e => e.TrainingSessionId).HasColumnName("trainingSessionID");

                entity.Property(e => e.AddressId).HasColumnName("addressID");

                entity.Property(e => e.Cancelled).HasColumnName("cancelled");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.EndHour).HasColumnName("endHour");

                entity.Property(e => e.StartHour).HasColumnName("startHour");

                entity.Property(e => e.TeacherId).HasColumnName("teacherID");

                entity.Property(e => e.TrainingId).HasColumnName("trainingID");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Trainingsession)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TRAININGSESSION_ADDRESS");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Trainingsession)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TRAININGSESSION_TEACHER");

                entity.HasOne(d => d.Training)
                    .WithMany(p => p.Trainingsession)
                    .HasForeignKey(d => d.TrainingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TRAININGSESSION_TRAININGINFO");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USER");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(320);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");
            });
        }
    }
}
