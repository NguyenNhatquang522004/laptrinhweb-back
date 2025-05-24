
using backapi.Model;
using Microsoft.EntityFrameworkCore;

namespace backapi.Configuration
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserPreference> UserPreferences { get; set; }

        // Content Management
        public DbSet<Category> Categories { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Vocabulary> Vocabularies { get; set; }
        public DbSet<GrammarRule> GrammarRules { get; set; }

        // Exercises & Assessments
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestQuestion> TestQuestions { get; set; }

        // Progress Tracking
        public DbSet<UserProgress> UserProgresses { get; set; }
        public DbSet<UserTestResult> UserTestResults { get; set; }
        public DbSet<DailyProgress> DailyProgresses { get; set; }

        // Gamification
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<UserAchievement> UserAchievements { get; set; }
        public DbSet<Leaderboard> Leaderboards { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<UserChallenge> UserChallenges { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<PaymentHistory> PaymentHistories { get; set; }
        public DbSet<PlanFeature> PlanFeatures { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Subscription>(entity =>
            {
                // Primary Key
                entity.HasKey(s => s.SubscriptionId);

                // Indexes for performance
                entity.HasIndex(s => s.UserId);
                entity.HasIndex(s => s.Status);
                entity.HasIndex(s => new { s.UserId, s.Status }); // Composite index
                entity.HasIndex(s => s.EndDate);
                entity.HasIndex(s => s.CreatedAt);

                // Foreign Key Relationships
                entity.HasOne(s => s.User)
                      .WithMany(u => u.Subscriptions) // Assuming User has Subscriptions collection
                      .HasForeignKey(s => s.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(s => s.PaymentHistories)
                      .WithOne(p => p.Subscription)
                      .HasForeignKey(p => p.SubscriptionId)
                      .OnDelete(DeleteBehavior.SetNull); // Allow orphaned payments

                // Column Configurations
                entity.Property(s => s.PlanType)
                      .HasConversion<string>() // Store enum as string
                      .HasMaxLength(20);

                entity.Property(s => s.Status)
                      .HasConversion<string>() // Store enum as string
                      .HasMaxLength(20);

                entity.Property(s => s.Currency)
                      .HasDefaultValue("USD");

                entity.Property(s => s.AutoRenew)
                      .HasDefaultValue(true);

                // Constraints
                entity.HasCheckConstraint("CK_Subscription_Amount", "Amount >= 0");
                entity.HasCheckConstraint("CK_Subscription_EndDate", "EndDate IS NULL OR EndDate > StartDate");
            });
            modelBuilder.Entity<PaymentHistory>(entity =>
            {
                // Primary Key
                entity.HasKey(p => p.PaymentId);

                // Indexes for performance
                entity.HasIndex(p => p.UserId);
                entity.HasIndex(p => p.SubscriptionId);
                entity.HasIndex(p => p.Status);
                entity.HasIndex(p => p.PaymentDate);
                entity.HasIndex(p => p.TransactionId);
                entity.HasIndex(p => p.GatewayTransactionId);
                entity.HasIndex(p => new { p.UserId, p.PaymentDate }); // For user payment history

                // Foreign Key Relationships
                entity.HasOne(p => p.User)
                      .WithMany(u => u.PaymentHistories) // Assuming User has PaymentHistories collection
                      .HasForeignKey(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.Subscription)
                      .WithMany(s => s.PaymentHistories)
                      .HasForeignKey(p => p.SubscriptionId)
                      .OnDelete(DeleteBehavior.SetNull); // Allow payments without subscription

                // Column Configurations
                entity.Property(p => p.Status)
                      .HasConversion<string>() // Store enum as string
                      .HasMaxLength(20);

                entity.Property(p => p.Currency)
                      .HasDefaultValue("USD");

                entity.Property(p => p.Amount)
                      .HasPrecision(10, 2); // More explicit than TypeName

                // Constraints
                entity.HasCheckConstraint("CK_PaymentHistory_Amount", "Amount > 0");
            });
            modelBuilder.Entity<PlanFeature>(entity =>
            {
                // Primary Key
                entity.HasKey(pf => pf.FeatureId);

                // Indexes for performance
                entity.HasIndex(pf => pf.PlanType);
                entity.HasIndex(pf => pf.FeatureName);
                entity.HasIndex(pf => new { pf.PlanType, pf.FeatureName }).IsUnique(); // Prevent duplicate features per plan
                entity.HasIndex(pf => pf.IsEnabled);

                // Column Configurations
                entity.Property(pf => pf.PlanType)
                      .HasConversion<string>() // Store enum as string
                      .HasMaxLength(20);

                entity.Property(pf => pf.IsEnabled)
                      .HasDefaultValue(true);

                // Constraints
                entity.HasCheckConstraint("CK_PlanFeature_LimitValue", "LimitValue IS NULL OR LimitValue >= 0");
            });
            // User Management Configurations
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.CurrentLevel);
                entity.HasIndex(e => e.LastActiveDate);

                entity.HasOne(u => u.UserPreference)
                      .WithOne(up => up.User)
                      .HasForeignKey<UserPreference>(up => up.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Subscriptions)
                     .WithOne(s => s.User)
                     .HasForeignKey(s => s.UserId);

                entity.HasMany(u => u.PaymentHistories)
                      .WithOne(p => p.User)
                      .HasForeignKey(p => p.UserId);
            });

            // Content Management Configurations
            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasIndex(e => e.Level);
                entity.HasIndex(e => e.LessonType);
                entity.HasIndex(e => e.CategoryId);

                entity.HasOne(l => l.Category)
                      .WithMany(c => c.Lessons)
                      .HasForeignKey(l => l.CategoryId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(l => l.CreatedByUser)
                      .WithMany()
                      .HasForeignKey(l => l.CreatedBy)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Vocabulary>(entity =>
            {
                entity.HasIndex(e => e.Word);
                entity.HasIndex(e => e.Level);

                entity.HasOne(v => v.Category)
                      .WithMany(c => c.Vocabularies)
                      .HasForeignKey(v => v.CategoryId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // Exercise Configurations
            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.HasOne(e => e.Lesson)
                      .WithMany(l => l.Exercises)
                      .HasForeignKey(e => e.LessonId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TestQuestion>(entity =>
            {
                entity.HasOne(tq => tq.Test)
                      .WithMany(t => t.TestQuestions)
                      .HasForeignKey(tq => tq.TestId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Progress Tracking Configurations
            modelBuilder.Entity<UserProgress>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.LessonId }).IsUnique();
                entity.HasIndex(e => e.Status);

                entity.HasOne(up => up.User)
                      .WithMany(u => u.UserProgresses)
                      .HasForeignKey(up => up.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(up => up.Lesson)
                      .WithMany(l => l.UserProgresses)
                      .HasForeignKey(up => up.LessonId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UserTestResult>(entity =>
            {
                entity.HasOne(utr => utr.User)
                      .WithMany(u => u.UserTestResults)
                      .HasForeignKey(utr => utr.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(utr => utr.Test)
                      .WithMany(t => t.UserTestResults)
                      .HasForeignKey(utr => utr.TestId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DailyProgress>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.Date }).IsUnique();

                entity.HasOne(dp => dp.User)
                      .WithMany(u => u.DailyProgresses)
                      .HasForeignKey(dp => dp.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Gamification Configurations
            modelBuilder.Entity<UserAchievement>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.AchievementId }).IsUnique();

                entity.HasOne(ua => ua.User)
                      .WithMany(u => u.UserAchievements)
                      .HasForeignKey(ua => ua.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ua => ua.Achievement)
                      .WithMany(a => a.UserAchievements)
                      .HasForeignKey(ua => ua.AchievementId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Leaderboard>(entity =>
            {
                entity.HasIndex(e => new { e.Period, e.PeriodStart });

                entity.HasOne(l => l.User)
                      .WithMany()
                      .HasForeignKey(l => l.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Challenge>(entity =>
            {
                entity.HasOne(c => c.RewardBadge)
                      .WithMany(a => a.Challenges)
                      .HasForeignKey(c => c.RewardBadgeId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<UserChallenge>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.HasOne(uc => uc.User)
                      .WithMany()
                      .HasForeignKey(uc => uc.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(uc => uc.Challenge)
                      .WithMany(c => c.UserChallenges)
                      .HasForeignKey(uc => uc.ChallengeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Enum Conversions
            modelBuilder.Entity<User>()
                .Property(e => e.Gender)
                .HasConversion<string>();

            modelBuilder.Entity<User>()
                .Property(e => e.CurrentLevel)
                .HasConversion<string>();

            modelBuilder.Entity<UserPreference>()
                .Property(e => e.LearningGoal)
                .HasConversion<string>();

            modelBuilder.Entity<UserPreference>()
                .Property(e => e.PreferredDifficulty)
                .HasConversion<string>();

            modelBuilder.Entity<UserPreference>()
                .Property(e => e.FontSize)
                .HasConversion<string>();

            modelBuilder.Entity<Lesson>()
                .Property(e => e.Level)
                .HasConversion<string>();

            modelBuilder.Entity<Lesson>()
                .Property(e => e.LessonType)
                .HasConversion<string>();

            modelBuilder.Entity<Vocabulary>()
                .Property(e => e.PartOfSpeech)
                .HasConversion<string>();

            modelBuilder.Entity<Vocabulary>()
                .Property(e => e.Level)
                .HasConversion<string>();

            modelBuilder.Entity<GrammarRule>()
                .Property(e => e.Level)
                .HasConversion<string>();

            modelBuilder.Entity<Exercise>()
                .Property(e => e.ExerciseType)
                .HasConversion<string>();

            modelBuilder.Entity<Exercise>()
                .Property(e => e.Difficulty)
                .HasConversion<string>();

            modelBuilder.Entity<Test>()
                .Property(e => e.TestType)
                .HasConversion<string>();

            modelBuilder.Entity<Test>()
                .Property(e => e.Level)
                .HasConversion<string>();

            modelBuilder.Entity<TestQuestion>()
                .Property(e => e.QuestionType)
                .HasConversion<string>();

            modelBuilder.Entity<TestQuestion>()
                .Property(e => e.SkillFocus)
                .HasConversion<string>();

            modelBuilder.Entity<TestQuestion>()
                .Property(e => e.Difficulty)
                .HasConversion<string>();

            modelBuilder.Entity<UserProgress>()
                .Property(e => e.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Achievement>()
                .Property(e => e.Category)
                .HasConversion<string>();

            modelBuilder.Entity<Achievement>()
                .Property(e => e.Rarity)
                .HasConversion<string>();

            modelBuilder.Entity<Leaderboard>()
                .Property(e => e.Period)
                .HasConversion<string>();

            modelBuilder.Entity<Challenge>()
                .Property(e => e.ChallengeType)
                .HasConversion<string>();

            modelBuilder.Entity<Challenge>()
                .Property(e => e.Metric)
                .HasConversion<string>();
        }
    }
}
