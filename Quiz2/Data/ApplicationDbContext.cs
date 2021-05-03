using System;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Quiz2.Models;


namespace Quiz2.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
        DbContextOptions options,
        IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {

    }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<UserAnswer> UserAnswers { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    //public DbSet<GameUsers> GameUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Game>()
            .HasMany(p => p.JoinedUsers)
            .WithMany(p => p.Games)
            .UsingEntity(j => j.ToTable("JoinedUsers"));
        
        modelBuilder.Entity<Game>()
            .HasOne(p => p.Owner)
            .WithMany()
            .HasForeignKey(p => p.OwnerId);
      
       /* modelBuilder.Entity<Answer>().ToTable("Answers");
        modelBuilder.Entity<Game>().ToTable("Games");
        modelBuilder.Entity<Question>().ToTable("Questions");
        modelBuilder.Entity<Quiz>().ToTable("Quizzes");
        modelBuilder.Entity<UserAnswer>().ToTable("UserAnswers");*/
       // modelBuilder.Entity<GameUsers>()
        //    .HasKey(bc => new { bc.GameId, bc.ApplicationUserId });
        
        ApplicationUser user1 = new ApplicationUser
        {
            AccessFailedCount = 0,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            Id = Guid.NewGuid().ToString(),
            LockoutEnabled = false,
            LockoutEnd = null,
            NormalizedEmail = "user@gmail.com",
            NormalizedUserName =  "user@gmail.com",
            Email = "user@gmail.com",
            PasswordHash = "",
            PhoneNumber = "",
            PhoneNumberConfirmed = false,
            SecurityStamp = Guid.NewGuid().ToString(),
            TwoFactorEnabled = false,
            UserName = "user@gmail.com",
            EmailConfirmed = true
        };
        IPasswordHasher<ApplicationUser> _passwordHasher = new PasswordHasher<ApplicationUser>();
        user1.PasswordHash = _passwordHasher.HashPassword(user1, "User123*");
        modelBuilder.Entity<ApplicationUser>().HasData(user1);
        
         modelBuilder.Entity<Quiz>().HasData(
                    new Quiz
                    {
                        Id = 1,
                        Name = "Quiz 1",
                        Questions = null, 
                        OwnerId = user1.Id,
                        Owner = null,
                        Games = null
                    });
         modelBuilder.Entity<Quiz>().HasData(
             new Quiz
             {
                 Id = 2,
                 Name = "Quiz 2",
                 Questions = null,
                 OwnerId = user1.Id,
                 Owner = null,
                 Games = null
             });
         modelBuilder.Entity<Question>().HasData(
             new Question
             {
                 Id = 1,
                 QuizId = 1,
                 Quiz = null,
                 Text = "Question 1",
                 Answers = null,
                 SecondsToAnswer = 120,
                 Position = 1,
                 Points = 1
             });
         
         modelBuilder.Entity<Question>().HasData(
             new Question
             {
                 Id = 2,
                 QuizId = 1,
                 Quiz = null,
                 Text = "Question 2",
                 Answers = null,
                 SecondsToAnswer = 120,
                 Position = 1,
                 Points = 1
             });
         
         modelBuilder.Entity<Question>().HasData(
             new Question
             {
                 Id = 3,
                 QuizId = 2,
                 Quiz = null,
                 Text = "Question 1",
                 Answers = null,
                 SecondsToAnswer = 120,
                 Position = 1,
                 Points = 1
             });
         
         modelBuilder.Entity<Question>().HasData(
             new Question
             {
                 Id = 4,
                 QuizId = 2,
                 Quiz = null,
                 Text = "Question 2",
                 Answers = null,
                 SecondsToAnswer = 120,
                 Position = 1,
                 Points = 1
             });
         modelBuilder.Entity<Answer>().HasData(
             new Answer
             {
                 Id = 1,
                 Correct = false,
                 QuestionId = 1,
                 Question = null,
                 Text = "Answer 1"
             });
         modelBuilder.Entity<Answer>().HasData(
             new Answer
             {
                 Id = 2,
                 Correct = true,
                 QuestionId = 1,
                 Question = null,
                 Text = "Answer 2"
             });
         modelBuilder.Entity<Answer>().HasData(
             new Answer
             {
                 Id = 3,
                 Correct = false,
                 QuestionId = 1,
                 Question = null,
                 Text = "Answer 3"
             });
         modelBuilder.Entity<Answer>().HasData(
             new Answer
             {
                 Id = 4,
                 Correct = false,
                 QuestionId = 1,
                 Question = null,
                 Text = "Answer 4"
             });
         modelBuilder.Entity<Answer>().HasData(
             new Answer
             {
                 Id = 5,
                 Correct = false,
                 QuestionId = 2,
                 Question = null,
                 Text = "Answer 1"
             });
         modelBuilder.Entity<Answer>().HasData(
             new Answer
             {
                 Id = 6,
                 Correct = true,
                 QuestionId = 2,
                 Question = null,
                 Text = "Answer 2"
             });
         modelBuilder.Entity<Answer>().HasData(
             new Answer
             {
                 Id = 7,
                 Correct = true,
                 QuestionId = 2,
                 Question = null,
                 Text = "Answer 3"
             });
         modelBuilder.Entity<Answer>().HasData(
             new Answer
             {
                 Id = 8,
                 Correct = false,
                 QuestionId = 2,
                 Question = null,
                 Text = "Answer 4"
             });
         
         modelBuilder.Entity<Answer>().HasData(
             new Answer
             {
                 Id = 9,
                 Correct = false,
                 QuestionId = 3,
                 Question = null,
                 Text = "Answer 1"
             });
         modelBuilder.Entity<Answer>().HasData(
             new Answer
             {
                 Id = 10,
                 Correct = true,
                 QuestionId = 3,
                 Question = null,
                 Text = "Answer 2"
             });
         modelBuilder.Entity<Answer>().HasData(
             new Answer
             {
                 Id = 11,
                 Correct = false,
                 QuestionId = 3,
                 Question = null,
                 Text = "Answer 3"
             });
         modelBuilder.Entity<Answer>().HasData(
             new Answer
             {
                 Id = 12,
                 Correct = true,
                 QuestionId = 3,
                 Question = null,
                 Text = "Answer 4"
             });
         modelBuilder.Entity<Answer>().HasData(
             new Answer
             {
                 Id = 13,
                 Correct = false,
                 QuestionId = 4,
                 Question = null,
                 Text = "Answer 1"
             });
         modelBuilder.Entity<Answer>().HasData(
             new Answer
             {
                 Id = 14,
                 Correct = true,
                 QuestionId = 4,
                 Question = null,
                 Text = "Answer 2"
             });
         modelBuilder.Entity<Answer>().HasData(
             new Answer
             {
                 Id = 15,
                 Correct = false,
                 QuestionId = 4,
                 Question = null,
                 Text = "Answer 3"
             });
         modelBuilder.Entity<Answer>().HasData(
             new Answer
             {
                 Id = 16,
                 Correct = false,
                 QuestionId = 4,
                 Question = null,
                 Text = "Answer 4"
             });
        base.OnModelCreating(modelBuilder);
        }
    }
}
