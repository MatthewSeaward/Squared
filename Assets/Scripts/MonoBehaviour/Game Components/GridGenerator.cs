using GridGeneration;
using GridGeneration.Interfaces;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    IGridGenerator gridGenerator = new BasicGridGenerator();
    
    [SerializeField]
    Vector2 Startx;

    [SerializeField]
    private float TileSpacing = 0.6f;

    public void Start()
    {
        gridGenerator.GenerateTiles(Startx, TileSpacing);
    }

    public GameObject GenerateTile(float xPos, float yPos, int x, int y)
    {
        return gridGenerator.GenerateTile(PieceFactory.PieceTypes.Normal, xPos, yPos, x, y, false);
    }

}
