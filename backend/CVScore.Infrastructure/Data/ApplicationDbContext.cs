using CVScore.Application.Abstractions.Persistence;
using CVScore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CVScore.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<CvDocument> CvDocuments { get; set; }
    public DbSet<InterviewProfile> InterviewProfiles { get; set; }
    public DbSet<InterviewSession> InterviewSessions { get; set; }
    public DbSet<InterviewQuestion> InterviewQuestions { get; set; }
    public DbSet<InterviewAnswer> InterviewAnswers { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }

    IQueryable<User> IApplicationDbContext.Users => Users;
    IQueryable<CvDocument> IApplicationDbContext.CvDocuments => CvDocuments;
    IQueryable<InterviewProfile> IApplicationDbContext.InterviewProfiles => InterviewProfiles;
    IQueryable<InterviewSession> IApplicationDbContext.InterviewSessions => InterviewSessions;
    IQueryable<InterviewQuestion> IApplicationDbContext.InterviewQuestions => InterviewQuestions;
    IQueryable<InterviewAnswer> IApplicationDbContext.InterviewAnswers => InterviewAnswers;
    IQueryable<Feedback> IApplicationDbContext.Feedbacks => Feedbacks;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FullName).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(255).IsRequired();
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.HasIndex(e => e.Email).IsUnique();
        });

        modelBuilder.Entity<CvDocument>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).HasMaxLength(200).IsRequired();
            entity.Property(e => e.FileName).HasMaxLength(255).IsRequired();
            entity.Property(e => e.FileUrl).HasMaxLength(500).IsRequired();
            entity.Property(e => e.FileType).HasMaxLength(50).IsRequired();

            entity.HasOne(e => e.User)
                .WithMany(u => u.CvDocuments)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<InterviewProfile>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TargetRole).HasMaxLength(150).IsRequired();
            entity.Property(e => e.CompanyName).HasMaxLength(150);

            entity.HasOne(e => e.User)
                .WithMany(u => u.InterviewProfiles)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.CvDocument)
                .WithMany(c => c.InterviewProfiles)
                .HasForeignKey(e => e.CvDocumentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<InterviewSession>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.OverallScore).HasPrecision(5, 2);

            entity.HasOne(e => e.InterviewProfile)
                .WithMany(p => p.InterviewSessions)
                .HasForeignKey(e => e.InterviewProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<InterviewQuestion>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Content).IsRequired();
            entity.HasIndex(e => new { e.InterviewSessionId, e.DisplayOrder }).IsUnique();

            entity.HasOne(e => e.InterviewSession)
                .WithMany(s => s.Questions)
                .HasForeignKey(e => e.InterviewSessionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<InterviewAnswer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Content).IsRequired();

            entity.HasOne(e => e.InterviewQuestion)
                .WithMany(q => q.Answers)
                .HasForeignKey(e => e.InterviewQuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Score).HasPrecision(5, 2);

            entity.HasOne(e => e.InterviewAnswer)
                .WithOne(a => a.Feedback)
                .HasForeignKey<Feedback>(e => e.InterviewAnswerId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    async Task IApplicationDbContext.AddAsync<T>(T entity, CancellationToken cancellationToken)
    {
        await Set<T>().AddAsync(entity, cancellationToken);
    }
}
