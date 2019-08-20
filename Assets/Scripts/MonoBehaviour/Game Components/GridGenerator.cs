using GridGeneration;
using GridGeneration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    IGridGenerator gridGenerator = new BasicGridGenerator();

    [SerializeField]
    GameObject EmptyCell;

    [SerializeField]
    Vector2 Startx;

    [SerializeField]
    private float TileSpacing = 0.6f;

    public void Start()
    {
        (gridGenerator as BasicGridGenerator).GridCell = EmptyCell;
        gridGenerator.GenerateTiles(Startx, TileSpacing);
    }

    public GameObject GenerateTile(float xPos, float yPos, int x, int y)
    {
        return gridGenerator.GenerateTile('x', xPos, yPos, x, y, false);
    }

}
