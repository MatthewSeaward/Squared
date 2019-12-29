using Assets.Scripts.Workers.Piece_Effects;
using Assets.Scripts.Workers.Piece_Effects.Collection;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using UnityEngine;
using static Assets.Scripts.Workers.Factorys.PieceBuilderDirector;
using static SquarePiece;

public interface ISquarePiece
{
    Sprite Sprite { get; set; }
    Transform transform { get; }
    Vector2Int Position { get; set; }
    IPieceConnection PieceConnection { get; set; }
    IScoreable Scoring { get; set; }
    IBehaviour PieceBehaviour { get; set; }
    SpriteRenderer SpriteRenderer { get; }
    GameObject gameObject { get; }
    IPieceDestroy DestroyPieceHandler { get; set; }
    IOnCollection OnCollection { get; }
    PieceTypes Type { get; set; }
    PieceDestroyed PieceDestroyed { set; get; }
    Colour PieceColour { get;set; }

    void Deselected();
    void DestroyPiece();
    void Destroy();
    void Pressed(bool checkForAdditional);
    void SetMouseDown(bool v);
    void Selected();
}