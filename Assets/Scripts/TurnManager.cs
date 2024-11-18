using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [HideInInspector] public List<Unit> units = new List<Unit>();
    private GameObject player;
    private SpawnManager spawnManager;

    private int index = 0;

    // Player always goes first.
    private void Awake()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        player = GameObject.Find("Player");
        units.Add(player.GetComponent<Unit>());
    }

    private void Start()
    {
        StartTurn();
    }

    public void StartTurn()
    {
        units[index].TurnToPlay = true;
    }

    // Always spawn a new enemy at the end of the round.
    public void EndTurn()
    {
        units[index].TurnToPlay = false;

        if (index == units.Count - 1)
        {
            index = 0;
            Unit newEnemy = spawnManager.SpawnEnemy().GetComponent<Unit>();
            units.Add(newEnemy);
        }
        else
        {
            index++;
        }

        StartTurn();
    }
}
