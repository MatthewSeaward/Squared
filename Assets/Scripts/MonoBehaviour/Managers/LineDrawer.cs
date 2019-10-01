using Assets.Scripts;
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

    private void PieceSelectionManager_SelectedPiecesChanged(System.Collections.Generic.LinkedList<ISquarePiece> pieces)
    {
        Clear();

        if (pieces.Count == 0 || MenuProvider.Instance.OnDisplay)
        {
            return;
        }

        Color colour = Color.red;
        float decrementAmount = 1f / (pieces.Count - 1);

        Vector3 oldPoint = Vector3.zero;
        foreach (var item in pieces)
        {
            if (oldPoint != Vector3.zero)
            {
                colour.b += decrementAmount;
                colour.g += decrementAmount;

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
