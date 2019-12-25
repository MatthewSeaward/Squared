using Assets.Scripts.Workers.IO.Data_Entities;
using System.Threading.Tasks;

namespace Assets.Scripts.Workers.IO
{
    public class TestProgressReader : ILevelProgressReader
    {
        public async Task<LevelProgress[]> LoadLevelProgress()
        {
            return await Task.Run(() =>
            {
                return new LevelProgress[]
                {
                    new LevelProgress() { Chapter= "test1", Level=1, StarAchieved=2},
                    new LevelProgress() { Chapter= "test1", Level=2, StarAchieved=1},
                    new LevelProgress() { Chapter= "test2", Level=1, StarAchieved=4}
                };
            });
        }
    }
}
