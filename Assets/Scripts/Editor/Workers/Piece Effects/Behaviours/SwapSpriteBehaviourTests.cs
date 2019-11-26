using Assets.Scripts;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.IO.Data_Entities;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static SquarePiece;

[Category("Piece Effects")]
public class SwapSpriteBehaviourTests
{
    [OneTimeSetUp]
    public void Setup()
    {
        GameResources.PieceSprites.Clear();

        foreach (int sprite in System.Enum.GetValues(typeof(Colour)))
        {
            if (sprite < 0)
            {
                continue;
            }
            GameResources.PieceSprites.Add(sprite.ToString(), TestHelpers.GetSprite(sprite.ToString()));
        }

        var go = new GameObject();
        var ps = go.AddComponent<PieceSelectionManager>();
        ps.Awake();
    }

    [Test]
    public void NotSelected_SpriteChangesAfterTime()
    {
        var sut = TestHelpers.CreatePiece();

        sut.PieceBehaviour = new Assets.Scripts.Workers.Piece_Effects.SwapSpriteBehaviour();
        sut.Sprite = TestHelpers.GetSprite("1");

        sut.PieceBehaviour.Update(sut, 6f);

        Assert.AreNotEqual("1", sut.Sprite.name);
    }

    [Test]
    public void NotSelected_SpriteDoesNotChange_NotEnoughTime()
    {
        var sut = TestHelpers.CreatePiece();

        sut.PieceBehaviour = new Assets.Scripts.Workers.Piece_Effects.SwapSpriteBehaviour();
        sut.Sprite = TestHelpers.GetSprite("1");

        sut.PieceBehaviour.Update(sut, 4f);

        Assert.AreEqual("1", sut.Sprite.name);
    }

    [Test]
    public void Selected_SpriteDoesNotChange_ItIsSelected()
    {
        var sut = TestHelpers.CreatePiece();

        sut.PieceBehaviour = new Assets.Scripts.Workers.Piece_Effects.SwapSpriteBehaviour();
        sut.Sprite = TestHelpers.GetSprite("1");

       PieceSelectionManager.Instance.Add(sut, false);

        sut.PieceBehaviour.Update(sut, 6f);

        Assert.AreEqual("1", sut.Sprite.name);
    }

    [Test]
    public void Selected_SpriteDoesNotChange_ItIsStoredMoves()
    {
        var sut = TestHelpers.CreatePiece();

        sut.PieceBehaviour = new Assets.Scripts.Workers.Piece_Effects.SwapSpriteBehaviour();
        sut.Sprite = TestHelpers.GetSprite("1");

        PieceSelectionManager.Instance.StoredMoves.Add(new List<ISquarePiece>() { sut });

        sut.PieceBehaviour.Update(sut, 6f);

        Assert.AreEqual("1", sut.Sprite.name);
    }
}
