using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using UnityEngine;
using static PieceFactory;

public interface ISquarePiece
{
    Sprite Sprite { get; set; }
    Transform transform { get; }
    Vector2Int Position { get; set; }
    IPieceConnection PieceConnection { get; set; }
    ISwapEffect SwapEffect { get; set; }
    IScoreable Scoring { get; set; }
    SpriteRenderer SpriteRenderer { get; }
    GameObject gameObject { get; }
    IPieceDestroy DestroyPieceHandler { get; }
    PieceTypes Type { get; set; }

    void Deselected();
    void DestroyPiece();
    void Destroy();
}