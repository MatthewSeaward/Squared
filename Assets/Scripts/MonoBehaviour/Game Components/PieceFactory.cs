using System.Linq;
using UnityEngine;
using static SquarePiece;
using DataEntities;
using Assets.Scripts.Workers.Piece_Effects.SwapEffects;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using Assets.Scripts.Workers.Piece_Effects;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Effects.Destruction;
using Assets;

public class PieceFactory : MonoBehaviour
{
    public PieceColour[] Sprites;

    public static PieceFactory Instance { private set; get; }

    private void Awake()
    {
        Instance = this;
    }
    
    public GameObject CreateRandomSquarePiece(char pieceKey, bool initlalSetup)
    {
        string[] specialDropPieces = LevelManager.Instance.SelectedLevel.SpecialDropPieces;

       var piece = ObjectPool.Instantiate(GameResources.GameObjects["Piece"], Vector3.zero);

        var squarePiece = piece.GetComponent<SquarePiece>();
               
        if (!initlalSetup)
        {
            if(specialDropPieces != null && specialDropPieces.Length > 0 && Random.Range(0, 100) <= 10)
            {
                pieceKey = char.Parse(specialDropPieces[Random.Range(0, specialDropPieces.Length)]);
            }            
        }

        BuildSprite(pieceKey, piece);
        BuildSwapEffects(pieceKey, squarePiece, initlalSetup);
        BuildConnection(pieceKey, squarePiece);
        BuildDestroyEffect(pieceKey, piece, squarePiece);
        BuildBehaviours(pieceKey, squarePiece);
        BuildScoring(pieceKey, squarePiece);
        BuildLayers(piece, squarePiece);

        return piece;
    }

    private void BuildScoring(char pieceKey, SquarePiece squarePiece)
    {
        if (pieceKey == '2')
        {
            squarePiece.Scoring = new MultipliedScore(2);
        }
        else if (pieceKey == '3')
        {
            squarePiece.Scoring = new MultipliedScore(3);
        }
        else
        {
            squarePiece.Scoring = new SingleScore();
        }
    }

    private void BuildBehaviours(char pieceKey, SquarePiece squarePiece)
    {
        if (pieceKey == 's')
        {
            squarePiece.PieceBehaviour = new SwapSpriteBehaviour();
        }
    }

    private void BuildDestroyEffect(char pieceKey, GameObject piece, SquarePiece squarePiece)
    {
        if (squarePiece.SwapEffect is LockedSwap)
        {
            squarePiece.DestroyPieceHandler = new SwapSpriteDestroy(squarePiece.SwapEffect, squarePiece.InnerSprite, squarePiece, transform.localScale);
        }
        else if (pieceKey == 'h')
        {
            squarePiece.DestroyPieceHandler = new HeavyDestroyTriggerFall(squarePiece);
        }
        else
        {
            squarePiece.DestroyPieceHandler = new DestroyTriggerFall(squarePiece);
        }
    }

    private void BuildSprite(char pieceKey, GameObject piece)
    {
        Sprite sprite; 
        if (pieceKey == 'r')
        {
            sprite = GameResources.Sprites["Rainbow"];
        }
        else
        {
            sprite = CreateRandomSprite();
        }
        piece.GetComponent<SpriteRenderer>().sprite = sprite;

    }

    private void BuildConnection(char pieceKey, SquarePiece squarePiece)
    {
        if (pieceKey == 'r')
        {
            squarePiece.PieceConnection = new AnyAdjancentConnection();
        }
        else
        {
            squarePiece.PieceConnection = new StandardConnection();
        }
    }

    private void BuildLayers(GameObject piece, SquarePiece squarePiece)
    {
        for (int i = 0; i < squarePiece.transform.childCount; i++)
        {
            var child = squarePiece.transform.GetChild(i);
            if (child == null)
            {
                continue;
            }
            if (child.gameObject.GetComponent<SpriteRenderer>() == null)
            {
                continue;
            }

            child.gameObject.SetActive(false);
        }

        ApplyLayer(piece, squarePiece.SwapEffect as ILayeredSprite);
        ApplyLayer(piece, squarePiece.PieceConnection as ILayeredSprite);
        ApplyLayer(piece, squarePiece.PieceBehaviour as ILayeredSprite);
        ApplyLayer(piece, squarePiece.DestroyPieceHandler as ILayeredSprite);
        ApplyLayer(piece, squarePiece.Scoring as ILayeredSprite);

    }

    private void ApplyLayer(GameObject piece, ILayeredSprite layer)
    {
        if (layer != null)
        {
            var l = ObjectPool.Instantiate(GameResources.GameObjects["PieceLayer"], Vector3.zero);
            l.GetComponent<SpriteRenderer>().sprite = layer.GetSprite();
            l.transform.parent = piece.transform;
        }
    }

    private static void BuildSwapEffects(char pieceKey, SquarePiece squarePiece, bool initialSetup)
    {
        if (initialSetup && (pieceKey == 'l' || pieceKey == 'r' || pieceKey == 's'))
        {
            squarePiece.SwapEffect = new LockedSwap();
        }
        else
        {
            squarePiece.SwapEffect = new SwapToNext();
        }
    }

    public Sprite CreateRandomSprite()
    {
        var permittedValues = LevelManager.Instance.SelectedLevel.colours;

        Colour selectedColour = permittedValues[Random.Range(0, permittedValues.Length)];

        return Sprites.FirstOrDefault(x => x.Colour == selectedColour).Sprite;
    }
}
