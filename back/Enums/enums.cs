namespace backapi.Enums
{
    public class enums
    {

        public enum CefrLevel
        {
            A1, A2, B1, B2, C1, C2
        }

        public enum Gender
        {
            Male, Female, Other
        }

        public enum LearningGoal
        {
            Casual, Business, Academic, Travel, ExamPrep
        }

        public enum PreferredDifficulty
        {
            Easy, Medium, Hard, Adaptive
        }

        public enum FontSize
        {
            Small, Medium, Large
        }

        public enum LessonType
        {
            Vocabulary, Grammar, Listening, Speaking, Reading, Writing
        }

        public enum PartOfSpeech
        {
            Noun, Verb, Adjective, Adverb, Preposition, Conjunction, Interjection
        }

        public enum ExerciseType
        {
            MultipleChoice, FillBlank, Matching, DragDrop, Speaking, Writing, Listening
        }

        public enum TestType
        {
            Placement, Progress, MockIelts, MockToeic, MockToefl, SkillTest
        }

        public enum QuestionType
        {
            MultipleChoice, TrueFalse, FillBlank, Essay, Speaking
        }

        public enum SkillFocus
        {
            Listening, Reading, Writing, Speaking, Grammar, Vocabulary
        }

        public enum ProgressStatus
        {
            NotStarted, InProgress, Completed, Mastered
        }

        public enum AchievementCategory
        {
            Progress, Streak, Social, Skill, Special
        }

        public enum AchievementRarity
        {
            Common, Rare, Epic, Legendary
        }

        public enum LeaderboardPeriod
        {
            Daily, Weekly, Monthly, AllTime
        }

        public enum ChallengeType
        {
            Daily, Weekly, Monthly, Special
        }

        public enum ChallengeMetric
        {
            Lessons, Exercises, Time, Points, Streak
        }

        public enum GroupRole
        {
            Admin, Moderator, Member
        }

        public enum DiscussionType
        {
            General, Question, StudyMaterial, Announcement
        }

        public enum FriendshipStatus
        {
            Pending, Accepted, Blocked
        }

        public enum AgentType
        {
            Teacher, Pronunciation, Grammar, Vocabulary, Speaking, Progress
        }

        public enum SenderType
        {
            User, AI
        }

        public enum MessageType
        {
            Text, Audio, Image, Exercise
        }

        public enum MistakeType
        {
            Grammar, Vocabulary, Pronunciation, Spelling
        }

        public enum FileType
        {
            Image, Audio, Video, Document
        }

        public enum CertificateType
        {
            CourseCompletion, LevelAchievement, SkillMastery, SpecialRecognition
        }

        public enum NotificationType
        {
            Reminder, Achievement, Social, System, Promotional
        }

        public enum NotificationPriority
        {
            Low, Medium, High, Urgent
        }

        public enum PlanType
        {
            Free, Basic, Premium, Ultimate
        }

        public enum SubscriptionStatus
        {
            Active, Cancelled, Expired, Suspended
        }

        public enum PaymentStatus
        {
            Pending, Completed, Failed, Refunded
        }

        public enum UserSatisfaction
        {
            VeryPoor, Poor, Average, Good, Excellent
        }

        public enum Difficulty
        {
            Easy, Medium, Hard
        }
    }
}
