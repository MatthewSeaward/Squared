using Assets.Scripts.Workers.IO.Interfaces;

namespace Assets.Scripts.Workers.IO
{
    public class TestLevelOrderLoader : ILevelOrderLoader
    {
        public string[] LoadLevelOrder()
        {
            return new string[]
            {
                "Chapter 1",
                "Chapter 2"
            };
        }
    }
}
