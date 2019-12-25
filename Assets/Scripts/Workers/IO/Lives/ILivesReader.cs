using System.Threading.Tasks;

namespace Assets.Scripts.Workers.IO.Lives
{
    interface ILivesReader
    {
        Task ReadLivesAsync();
    }
}
