using Microsoft.EntityFrameworkCore;
using OnlinePoll.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePoll.Infrastucture.Persistance
{
    public class OnlinePollContext : DbContext 
    {
        public OnlinePollContext(DbContextOptions<OnlinePollContext> options) : base(options) { }

        public DbSet<Poll> Polls { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.Property(a => a.AnswerText)
                      .IsRequired()
                      .HasMaxLength(1000);

                entity.HasOne(a => a.Question)
                      .WithMany()
                      .HasForeignKey(a => a.QuestionId)
                      .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
