using UnityEngine;

namespace GridGeneration.Interfaces
{
    interface IGridGenerator
    {        
        void GenerateTiles(Vector2 Start, float tileSpacing);
        GameObject GenerateTile(PieceFactory.PieceTypes pieceKey, float xPos, float yPos, int x, int y, bool initialSetup);
    }
}