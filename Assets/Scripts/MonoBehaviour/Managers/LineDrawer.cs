using Assets.Scripts;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{

    public void Update()
    {
        Clear();

        Color colour = Color.red;
        float decrementAmount = 1f / (PieceSelectionManager.Instance.CurrentPieces.Count - 1);

        Vector3 oldPoint = Vector3.zero;
        foreach (var item in PieceSelectionManager.Instance.CurrentPieces)
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
