using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SquarePiece;

namespace Assets.Scripts.Workers.IO.Collection.Piece_Collection_Iterations
{
    class FireBaseRemoteConfigPieceCollectionIterationReader : IPieceCollectionIterationReader
    {
        public void ReadPieceCollectionIterationAsync()
        {
            foreach(Colour value in Enum.GetValues(typeof(Colour)))
            {
                if (value == Colour.None)
                {
                    continue;
                }
                
                Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("CollectionInteration" + (int)value);
            }
        }
    }
}
