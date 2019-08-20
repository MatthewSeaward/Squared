using Assets.Scripts.Workers.IO.Data_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Workers.IO
{
    interface ILevelProgressLoader
    {
        LevelProgress[] LoadLevelProgress();
        void SaveLevelProgress(LevelProgress[] levelProgress);
    }
}
