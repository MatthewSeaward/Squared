using System.Linq;
using UnityEngine;
using static SquarePiece;
using DataEntities;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using Assets.Scripts.Workers.Piece_Effects;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Effects.Destruction;
using Assets;
using Assets.Scripts.Constants;
using System.Collections.Generic;
using Assets.Scripts.Workers.Helpers.Extensions;
using Assets.Scripts.Workers.Piece_Effects.Collection;
using System;
using Random = UnityEngine.Random;

public class PieceFactory
{
    public PieceColour[] Sprites;

    private static PieceFactory _instance;

    public static PieceFactory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PieceFactory();
            }

            return _instance;
        }
    }

    public enum PieceTypes 
    {
        Empty = '-',
        Normal = 'x',
        Rainbow = 'r',
        Locked = 'l',
        Swapping = 's',
        Heavy = 'h',
        DoublePoints = 'd',
        TriplePoints = 't',
        TwoPoints = '2',
        ThreePoints = '3',
        FourPoints = '4',
        Heart = 'H'
     }

    private PieceFactory()
    {
    }

    public GameObject CreateRandomSquarePiece()
    {
        var type = GetSpecialPiece();

        return CreateSquarePiece(type, false);
    }

    public GameObject CreateSquarePiece(PieceTypes type, bool initlalSetup)
    {
        var piece = ObjectPool.Instantiate(GameResources.GameObjects["Piece"], Vector3.zero);

        var squarePiece = piece.GetComponent<SquarePiece>();
        squarePiece.Type = type;

        int scoreValue = GetScoreValue(ref type);

        BuildSprite(type, piece);
        BuildConnection(type, squarePiece);
        BuildDestroyEffect(type, piece, squarePiece, initlalSetup);
        BuildBehaviours(type, squarePiece);
        BuildOnCollection(type, squarePiece);
        BuildScoring(type, squarePiece, scoreValue);
        BuildLayers(piece, squarePiece);
        BuildTextLayer(squarePiece);

        return piece;
    }

    private static PieceTypes GetSpecialPiece()
    {
        var type = PieceTypes.Normal;

        string[] specialDropPieces = LevelManager.Instance.SelectedLevel.SpecialDropPieces;
        if (specialDropPieces != null && specialDropPieces.Length > 0 && UnityEngine.Random.Range(0, 100) <= GameSettings.ChanceToUseSpecialPiece)
        {
            var randomSpecial = specialDropPieces[UnityEngine.Random.Range(0, specialDropPieces.Length)];
            type = (PieceTypes) randomSpecial[0];
        }

        return type;
    }

    private static int GetScoreValue(ref PieceTypes type)
    {
        int scoreValue = 1;
        char typeAsString = (char)type;
        if (int.TryParse(typeAsString.ToString(), out scoreValue))
        {
            // If it's a numeric value - make note of the scorevalue and consider the piece to be normal.
            type = PieceTypes.Normal;
        }
        else
        {
            scoreValue = 1;
        }

        return scoreValue;
    }

    private void BuildSprite(PieceTypes type, GameObject piece)
    {
        Sprite sprite;
        Colour selectedColour = Colour.None;
        if (type == PieceTypes.Rainbow)
        {
            sprite = GameResources.Sprites["Rainbow"];
        }
        else
        {
            var randomSprite = CreateRandomSprite();
            sprite = randomSprite.sprite;
            selectedColour = randomSprite.colour;
        }
        piece.GetComponent<SpriteRenderer>().sprite = sprite;
        piece.GetComponent<SquarePiece>().PieceColour = selectedColour;
    }

    private void BuildConnection(PieceTypes type, SquarePiece squarePiece)
    {
        if (type == PieceTypes.Rainbow)
        {
            squarePiece.PieceConnection = new AnyAdjancentConnection();
        }
        else
        {
            squarePiece.PieceConnection = new StandardConnection();
        }
    }

    private void BuildDestroyEffect(PieceTypes type, GameObject piece, SquarePiece squarePiece, bool initialSetup)
    {
        bool isLocked = type == PieceTypes.Locked;
        bool lockedAsPartOfSetup = initialSetup && type.EqualsAny(PieceTypes.Rainbow, PieceTypes.Swapping);
        if (isLocked || lockedAsPartOfSetup)
        {
            squarePiece.DestroyPieceHandler = new LockedSwap(squarePiece);
        }
        else if (type == PieceTypes.Heavy)
        {
            squarePiece.DestroyPieceHandler = new HeavyDestroyTriggerFall(squarePiece);
        }
        else
        {
            squarePiece.DestroyPieceHandler = new DestroyTriggerFall(squarePiece);
        }
    }

    private void BuildBehaviours(PieceTypes type, SquarePiece squarePiece)
    {
        if (type == PieceTypes.Swapping)
        {
            squarePiece.PieceBehaviour = new SwapSpriteBehaviour();
        }
        else
        {
            squarePiece.PieceBehaviour = null;
        }
    }

    private void BuildOnCollection(PieceTypes type, SquarePiece squarePiece)
    {
        if (type == PieceTypes.Heart)
        {
            squarePiece.OnCollection = new GainHeart();
        }
        else
        {
            squarePiece.OnCollection = null;
        }
    }

    private void BuildScoring(PieceTypes type, SquarePiece squarePiece, int scoreValue)
    {
        if (type == PieceTypes.DoublePoints)
        {
            squarePiece.Scoring = new MultipliedScore(2);
        }
        else if (type == PieceTypes.TriplePoints)
        {
            squarePiece.Scoring = new MultipliedScore(3);
        }
        else
        {
            squarePiece.Scoring = new SingleScore(scoreValue);
        }
    }

    private void BuildTextLayer(SquarePiece squarePiece)
    {               
        string displayText = string.Empty;

        ApplyTextLayer(ref displayText, squarePiece.PieceConnection as ITextLayer);
        ApplyTextLayer(ref displayText, squarePiece.PieceBehaviour as ITextLayer);
        ApplyTextLayer(ref displayText, squarePiece.DestroyPieceHandler as ITextLayer);
        ApplyTextLayer(ref displayText, squarePiece.Scoring as ITextLayer);

        squarePiece.SetText(displayText);
    }

    private void ApplyTextLayer(ref string text, ITextLayer layer)
    {
        if (layer == null)
        {
            return;
        }

        text = layer.GetText();
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

        ApplyLayer(piece, squarePiece.PieceConnection as ILayeredSprite);
        ApplyLayer(piece, squarePiece.PieceBehaviour as ILayeredSprite);
        ApplyLayer(piece, squarePiece.DestroyPieceHandler as ILayeredSprite);
        ApplyLayer(piece, squarePiece.Scoring as ILayeredSprite);
        ApplyLayer(piece, squarePiece.OnCollection as ILayeredSprite);

    }

    private void ApplyLayer(GameObject piece, ILayeredSprite layer)
    {
        if (layer != null && layer.GetSprite() != null)
        {
            var l = ObjectPool.Instantiate(GameResources.GameObjects["PieceLayer"], Vector3.zero);
            l.GetComponent<SpriteRenderer>().sprite = layer.GetSprite();
            l.transform.parent = piece.transform;
        }
    }

    public (Sprite sprite, Colour colour) CreateRandomSprite()
    {
        var permittedValues = new List<Colour>();
        if (LevelManager.Instance == null || LevelManager.Instance.SelectedLevel == null)
        {
            permittedValues.AddRange((Colour[]) Enum.GetValues(typeof(Colour))); 
        }
        else
        {
            permittedValues.AddRange((Colour[])LevelManager.Instance.SelectedLevel.colours.Clone());

            if (Random.Range(0, 100) < GameSettings.ChanceToUseBannedPiece)
            {
                var bannedSprite = LevelManager.Instance.SelectedLevel.BannedPiece();
                if (bannedSprite >= 0)
                {
                    permittedValues.Remove((Colour)bannedSprite);
                }
            }
        }

        int selectedColour = (int) permittedValues[Random.Range(0, permittedValues.Count)];
        
        return (GameResources.PieceSprites[selectedColour.ToString()], (Colour) selectedColour);
    }
}
