using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Helpers;
using UnityEngine;

namespace Assets.Scripts.Workers.IO.Collection
{
    class FireBasePieceCollectionWriter : IPieceCollectionWriter
    {
        public void WritePiecesCollected(PiecesCollected pieces)
        {
            var toJson = JsonHelper.ToJson(pieces.Pieces.ToArray());

            var result = System.Threading.Tasks.Task.Run(() => FireBaseDatabase.Database.Child(FireBaseSavePaths.PlayerCollectionLocation()).SetRawJsonValueAsync(toJson));
            if (result.IsCanceled || result.IsFaulted)
            {
                Debug.LogWarning(result.Exception);
            }
        }
    }
}
