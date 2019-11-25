using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.Managers;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    private void Awake()
    {
        PieceSelectionManager.SelectedPiecesChanged += PieceSelectionManager_SelectedPiecesChanged;
    }

    private void OnDestroy()
    {
        PieceSelectionManager.SelectedPiecesChanged -= PieceSelectionManager_SelectedPiecesChanged;
    }

    private void PieceSelectionManager_SelectedPiecesChanged(LinkedList<ISquarePiece> pieces)
    {
        Clear();

        if (MenuProvider.Instance.OnDisplay)
        {
            return;
        }

        if (GameManager.Instance.Restriction.IsRestrictionViolated(pieces.ToArray()))
        {
            DrawLines(pieces, Color.red);
        }
        else
        {
            DrawLines(pieces, Color.green, Color.yellow);
        }

        foreach (var storedMoves in PieceSelectionManager.Instance.StoredMoves)
        {
            DrawLines(storedMoves, Color.gray);
        }
    }

    private void DrawLines(ICollection<ISquarePiece> points, Color colour)
    {
        DrawLines(points, colour, colour);
    }

    private void DrawLines(ICollection<ISquarePiece> points, Color startColour, Color endColour)
    {
        Color colour = startColour;

        float rDiff = endColour.r - startColour.r;
        float gDiff = endColour.g - startColour.g;
        float bDiff = endColour.b - startColour.b;

        float rChange = rDiff / points.Count;
        float gChange = gDiff / points.Count;
        float bChange = bDiff / points.Count;

        Vector3 oldPoint = Vector3.zero;
        foreach (var item in points)
        {
            if (oldPoint != Vector3.zero)
            {
                colour.r += rChange;
                colour.g += gChange;
                colour.b += bChange;

                LineFactory.Instance.GetLine(oldPoint, item.transform.position, 0.02f, colour);
            }

            oldPoint = item.transform.position;
        }
    }

    public void Clear()
    {
        var activeLines = LineFactory.Instance.GetActive();

        foreach (var line in activeLines)
        {
            line.gameObject.SetActive(false);
        }
    }
}
