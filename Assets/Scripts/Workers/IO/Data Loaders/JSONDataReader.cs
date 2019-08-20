using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Workers.IO.Data_Loaders
{
    class JSONDataReader : IDataReader
    {
        public T[] ReadData<T>(string input)
        {
            return JsonHelper.FromJson<T>(input);
        }

        public T ReadSingleData<T>(string input)
        {
            return JsonUtility.FromJson<T>(input);
         }
    }
}
