using Assets.Scripts.Workers.Enemy;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyInfo", order = 1)]
public class EnemyInfo : ScriptableObject
{
    public int PiecesForRage = 7;
    public int PiecesAffectedByRage = 4;

    public IEnemyRage GetRage()
    {
        switch (this.name)
        {
            default:
            case "Golem":
                return new DestroyRage()
                {
                    SelectionAmount = PiecesAffectedByRage
                };
            case "Wolf":
                return new SwapRage()
                {
                    SelectionAmount = PiecesAffectedByRage
                };
        }
    }
}
