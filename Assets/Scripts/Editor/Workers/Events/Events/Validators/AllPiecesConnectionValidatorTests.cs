using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Piece_Effects.Destruction;
using Assets.Scripts.Workers.Test_Mockers.Lerp;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Editor.Workers.Events.Events.Validators
{

    [Category("Events")]
    class AllPiecesConnectionValidatorTests
    {
        [Test]
        public void NullPiece()
        {
            var sut = new AllPiecesConnectionValidator();
            
            var validForSelection = sut.ValidForSelection(null);

            Assert.IsFalse(validForSelection);
        }

        [Test]
        public void InactivePiece()
        {
            var sut = new AllPiecesConnectionValidator();

            var piece = TestHelpers.CreatePiece(0, 0, false);

            var validForSelection = sut.ValidForSelection(piece);

            Assert.IsFalse(validForSelection);
        }
        
        [Test]
        public void LerpInProgress()
        {
            var sut = new AllPiecesConnectionValidator();

            var go = new GameObject();
            var piece = go.AddComponent<SquarePiece>();
            go.AddComponent<LerpInProgress>();

            var validForSelection = sut.ValidForSelection(piece);

            Assert.IsFalse(validForSelection);
        }

        [Test]
        public void ToBeDestroyed()
        {
            var sut = new AllPiecesConnectionValidator();

            var go = new GameObject();
            var piece = go.AddComponent<SquarePiece>();
            piece.DestroyPieceHandler = new DestroyTriggerFall(piece);
            piece.DestroyPieceHandler.NotifyOfDestroy();

            var validForSelection = sut.ValidForSelection(piece);

            Assert.IsFalse(validForSelection);
        }


        [Test]
        public void ValidSelection()
        {
            var sut = new AllPiecesConnectionValidator();

            var go = new GameObject();
            var piece = go.AddComponent<SquarePiece>();
            go.AddComponent<LerpNotInProgress>();
            piece.DestroyPieceHandler = new LockedSwap(piece);
            piece.DestroyPieceHandler.NotifyOfDestroy();

            var validForSelection = sut.ValidForSelection(piece);

            Assert.IsTrue(validForSelection);
        }
    }
}
