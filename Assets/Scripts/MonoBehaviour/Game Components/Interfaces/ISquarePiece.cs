using Assets.Scripts.Workers.Piece_Effects;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using UnityEngine;

public interface ISquarePiece
{
    Sprite Sprite { get; set; }
    Transform transform { get; }
    Vector2Int Position { get; }
    IPieceConnection PieceConnection { get; set; }
    ISwapEffect SwapEffect { get; }
    SpriteRenderer SpriteRenderer { get; }

    void Deselected();
    void DestroyPiece();
}