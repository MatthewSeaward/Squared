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
using Assets.Scripts.Constants;
using System.Collections.Generic;
using Assets.Scripts.Workers.Helpers.Extensions;
using System;

public class PieceFactory : MonoBehaviour
{
    public PieceColour[] Sprites;

    public static PieceFactory Instance { private set; get; }

    public enum PieceTypes 
    {
        Empty = '-',
        Normal = 'x',
        Rainbow = 'r',
        Locked = 'l',
        Swapping = 's',
        Heavy = 'h',
        DoublePoints = '2',
        TriplePoints = '3'        
     }

    private void Awake()
    {
        Instance = this;
    }
    
    public GameObject CreateRandomSquarePiece(PieceTypes type, bool initlalSetup)
    {
        string[] specialDropPieces = LevelManager.Instance.SelectedLevel.SpecialDropPieces;

       var piece = ObjectPool.Instantiate(GameResources.GameObjects["Piece"], Vector3.zero);

        var squarePiece = piece.GetComponent<SquarePiece>();
               
        if (!initlalSetup)
        {
            if(specialDropPieces != null && specialDropPieces.Length > 0 && UnityEngine.Random.Range(0, 100) <= GameSettings.ChanceToUseSpecialPiece)
            {
                var randomSpecial = specialDropPieces[UnityEngine.Random.Range(0, specialDropPieces.Length)];
                type = (PieceTypes)randomSpecial[0];
            }            
        }

        BuildSprite(type, piece);
        BuildSwapEffects(type, squarePiece, initlalSetup);
        BuildConnection(type, squarePiece);
        BuildDestroyEffect(type, piece, squarePiece);
        BuildBehaviours(type, squarePiece);
        BuildScoring(type, squarePiece);
        BuildLayers(piece, squarePiece);

        return piece;
    }

    private void BuildSprite(PieceTypes type, GameObject piece)
    {
        Sprite sprite; 
        if (type == PieceTypes.Rainbow)
        {
            sprite = GameResources.Sprites["Rainbow"];
        }
        else
        {
            sprite = CreateRandomSprite();
        }
        piece.GetComponent<SpriteRenderer>().sprite = sprite;

    }

    private void BuildSwapEffects(PieceTypes type, SquarePiece squarePiece, bool initialSetup)
    {
        if (initialSetup && type.EqualsAny(PieceTypes.Locked, PieceTypes.Rainbow, PieceTypes.Swapping))
        {
            squarePiece.SwapEffect = new LockedSwap();
        }
        else
        {
            squarePiece.SwapEffect = new SwapToNext();
        }
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

    private void BuildDestroyEffect(PieceTypes type, GameObject piece, SquarePiece squarePiece)
    {
        if (squarePiece.SwapEffect is LockedSwap)
        {
            squarePiece.DestroyPieceHandler = new SwapSpriteDestroy(squarePiece.SwapEffect, squarePiece.InnerSprite, squarePiece, transform.localScale);
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
    }
 
    private void BuildScoring(PieceTypes type, SquarePiece squarePiece)
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
            squarePiece.Scoring = new SingleScore();
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

    public Sprite CreateRandomSprite()
    {
        var permittedValues = new List<Colour>();
        permittedValues.AddRange((Colour[]) LevelManager.Instance.SelectedLevel.colours.Clone());
        
        if (UnityEngine.Random.Range(0, 100) < GameSettings.ChanceToUseBannedPiece)
        {
            var bannedSprite = LevelManager.Instance.SelectedLevel.BannedPiece();
            if (bannedSprite >= 0)
            {
                permittedValues.Remove((Colour)bannedSprite);
             }
        }

        Colour selectedColour = permittedValues[UnityEngine.Random.Range(0, permittedValues.Count)];
        
        return Sprites.FirstOrDefault(x => x.Colour == selectedColour).Sprite;
    }
}
