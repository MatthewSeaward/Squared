using Assets.Scripts.Workers.IO.Level_Loader.Order;

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
