using UnityEngine;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Constants;
using Assets.Scripts.Workers.Factorys.PieceTemplates;
using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Managers;

namespace Assets.Scripts.Workers.Factorys
{
    public class PieceBuilderDirector
    {
        public PieceColour[] Sprites;

        private static PieceBuilderDirector _instance;

        public static PieceBuilderDirector Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PieceBuilderDirector();
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
            Heart = 'H',
            Chest = 'c',
            Change = 'C'
        }

        private PieceBuilderDirector()
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

            var pieceBuilder = GetPieceBuilder(type);
            pieceBuilder.initialsetup = initlalSetup;
            pieceBuilder.scoreValue = GetScoreValue(ref type);
            pieceBuilder.squarePiece = squarePiece;
            pieceBuilder.BuildPiece();

            return piece;
        }

        private PieceBuilder GetPieceBuilder(PieceTypes type)
        {
            switch (type)
            {
                case PieceTypes.Change:
                    return new Change();
                case PieceTypes.Chest:
                    return new Chest();
                case PieceTypes.DoublePoints:
                case PieceTypes.TriplePoints:
                    return new MultipliedPoints();
                case PieceTypes.Heart:
                    return new ExtraLife();
                case PieceTypes.Heavy:
                    return new Heavy();
                case PieceTypes.Locked:
                    return new Locked();
                case PieceTypes.Rainbow:
                    return new Rainbow();
                case PieceTypes.Swapping:
                    return new Swapping();
                case PieceTypes.Normal:
                case PieceTypes.Empty:
                default:
                    return new Normal();
            }
        }

        private static PieceTypes GetSpecialPiece()
        {
            var type = PieceTypes.Normal;

            string[] specialDropPieces = LevelManager.Instance.SelectedLevel.SpecialDropPieces;
            if (specialDropPieces != null && specialDropPieces.Length > 0 && Random.Range(0, 100) <= GameSettings.ChanceToUseSpecialPiece)
            {
                var randomSpecial = specialDropPieces[Random.Range(0, specialDropPieces.Length)];
                type = (PieceTypes)randomSpecial[0];
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
            else if (type == PieceTypes.DoublePoints)
            {
                return 2;
            }
            else if (type == PieceTypes.TriplePoints)
            {
                return 3;
            }
            else
            {
                scoreValue = 1;
            }

            return scoreValue;
        }
    }

}
