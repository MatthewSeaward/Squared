using System;

namespace Assets.Scripts.Workers.IO.Lives
{
    interface ILivesWriter
    {
        void WriteLives(int numberOfLives, DateTime lastEarnedLives);
    }
}
