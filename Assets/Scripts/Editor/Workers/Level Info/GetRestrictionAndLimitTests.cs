using Assets.Scripts.Workers.Level_Info;
using Assets.Scripts.Workers.Score_and_Limits;
using NUnit.Framework;

namespace Assets.Scripts.Editor.Workers.Level_Info
{
    [Category("Level Info")]
    class GetRestrictionAndLimitTests
    {
        Level level = new Level();

        [SetUp]
        public void SetUp()
        {
            level.Star1Progress = new Scripts.Workers.IO.Data_Entities.StarProgress() { Restriction = new Scripts.Workers.Score_and_Limits.TurnTimeLimit(5), Limit = new TurnLimit(2) };
            level.Star2Progress = new Scripts.Workers.IO.Data_Entities.StarProgress() { Restriction = new Scripts.Workers.Score_and_Limits.SwapEffectLimit(), Limit = new TurnLimit(2) };
            level.Star3Progress = new Scripts.Workers.IO.Data_Entities.StarProgress() { Restriction = new Scripts.Workers.Score_and_Limits.NoRestriction(), Limit = new TimeLimit(2) };
        }

        [Test]
        public void Restriction_Star1()
        {
            level.LevelProgress = new Scripts.Workers.IO.Data_Entities.LevelProgress() { StarAchieved = 0 };

            Assert.AreEqual(typeof(TurnTimeLimit), level.GetCurrentRestriction().GetType());
        }

        [Test]
        public void Restriction_Star3()
        {
            level.LevelProgress = new Scripts.Workers.IO.Data_Entities.LevelProgress() { StarAchieved = 2 };

            Assert.AreEqual(typeof(Scripts.Workers.Score_and_Limits.NoRestriction), level.GetCurrentRestriction().GetType());
        }

        [Test]
        public void Limit_Star1()
        {
            level.LevelProgress = new Scripts.Workers.IO.Data_Entities.LevelProgress() { StarAchieved = 0 };

            Assert.AreEqual(typeof(TurnLimit), level.GetCurrentLimit().GetType());
        }

        [Test]
        public void Limit_Star3()
        {
            level.LevelProgress = new Scripts.Workers.IO.Data_Entities.LevelProgress() { StarAchieved = 2 };

            Assert.AreEqual(typeof(TimeLimit), level.GetCurrentLimit().GetType());
        }

        [Test]
        public void GetBannedSprite_WithValidRestriction()
        {
            level.Star1Progress = new Scripts.Workers.IO.Data_Entities.StarProgress() { Restriction = new Scripts.Workers.Score_and_Limits.BannedSprite(2)};

            level.LevelProgress = new Scripts.Workers.IO.Data_Entities.LevelProgress() { StarAchieved = 0 };

            Assert.AreEqual(2, level.BannedPiece());
        }

        [Test]
        public void GetBannedSprite_WithInvalidRestriction()
        {
            level.Star1Progress = new Scripts.Workers.IO.Data_Entities.StarProgress() { Restriction = new Scripts.Workers.Score_and_Limits.NoRestriction() };

            level.LevelProgress = new Scripts.Workers.IO.Data_Entities.LevelProgress() { StarAchieved = 0 };

            Assert.AreEqual(-1, level.BannedPiece());
        }
    }
}
