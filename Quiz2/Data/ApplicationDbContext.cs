using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      
        modelBuilder.Entity<Answer>().ToTable("Answers");
        modelBuilder.Entity<Game>().ToTable("Games");
        modelBuilder.Entity<Question>().ToTable("Questions");
        modelBuilder.Entity<Quiz>().ToTable("Quizzes");
        modelBuilder.Entity<UserAnswer>().ToTable("UserAnswers");
       // modelBuilder.Entity<GameUsers>()
        //    .HasKey(bc => new { bc.GameId, bc.ApplicationUserId });
        base.OnModelCreating(modelBuilder);
        }
    }
}
