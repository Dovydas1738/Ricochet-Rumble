using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuilder : MonoBehaviour
{
    // The purpose of this script is only to generate the ground grid.

    [SerializeField] private GameObject[] blockPrefabs;
    [SerializeField] private float blockSize = 3f;
    [SerializeField] private int gridWidth = 30;
    [SerializeField] private int gridHeight = 30;

    private int index = 0;

    private void Start()
    {
        BuildGrid();
    }

    void BuildGrid()
    {
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                int index = (i + j) % blockPrefabs.Length;

                Vector3 position = new Vector3(i * blockSize - gridWidth, blockSize / -2, j * blockSize - gridHeight);
                GameObject block = Instantiate(blockPrefabs[index], position, Quaternion.identity);
                block.transform.SetParent(this.transform);
            }
        }
    }
}
