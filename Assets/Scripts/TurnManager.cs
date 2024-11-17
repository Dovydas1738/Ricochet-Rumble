using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [Header("List of units in the level in turn order:")]
    [SerializeField] private List<Unit> units = new List<Unit>();

    private int index = 0;

    private void Start()
    {
        StartTurn();
    }

    public void StartTurn()
    {
        units[index].TurnToPlay = true;
    }

    public void EndTurn()
    {
        units[index].TurnToPlay = false;

        if (index == units.Count - 1)
        {
            index = 0;
        }
        else
        {
            index++;
        }

        StartTurn();
    }
}
