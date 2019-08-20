using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Workers.IO.Data_Loaders
{
    interface IDataReader
    {
        T[] ReadData<T>(string input);
        T ReadSingleData<T>(string input);
    }
}
