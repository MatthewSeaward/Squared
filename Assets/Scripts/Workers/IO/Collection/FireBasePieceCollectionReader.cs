using Assets.Scripts.Workers.Managers;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Helpers;
using Firebase.Database;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Workers.Helpers;

namespace Assets.Scripts.Workers.IO.Collection
{    
    class FireBasePieceCollectionReader : IPieceCollectionReader
    {
        public async Task LoadPieceCollectionAsync()
        {
            try
            {
               await FireBaseDatabase.Database.Child(FireBaseSavePaths.PlayerCollectionLocation())
                             .GetValueAsync().ContinueWith(task =>
                             {
                                 if (task.IsFaulted)
                                 {
                                 }
                                 else if (task.IsCompleted)
                                 {
                                     try
                                     {
                                         DataSnapshot snapshot = task.Result;
                                         
                                         string info = snapshot?.GetRawJsonValue()?.ToString();

                                         var array = new PiecesCollected.PieceCollectionInfo[0];
                                         if (info != null)
                                         {
                                             array = JsonHelper.FromJson<PiecesCollected.PieceCollectionInfo>(info);
                                         }

                                         var result = new PiecesCollected();
                                         if (array != null)
                                         {
                                             result.Pieces.AddRange(array);
                                         }

                                         PieceCollectionManager.Instance.PiecesCollected = result;

                                     }
                                     catch (Exception ex)
                                     {
                                         Debug.LogError(ex);
                                     }
                                 }
                             });
            }
            catch { }
        }
    }
}
