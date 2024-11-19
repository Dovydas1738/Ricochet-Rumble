using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundGridController : MonoBehaviour
{
    private List<GameObject> movableBlocks = new List<GameObject>();

    private void Start()
    {
        CollectMovableBlocks();

        RandomizeBlockLayout();
    }

    void CollectMovableBlocks()
    {
        movableBlocks.Clear();

        foreach (Transform block in transform)
        {
            if (!block.CompareTag("NotMovableGround"))
            {
                movableBlocks.Add(block.gameObject);
            }
        }
    }

    void RandomizeBlockLayout()
    {
        foreach (GameObject block in movableBlocks)
        {
            Vector3 initialPosition = block.transform.position;

            int randomFactor = Random.Range(1, 4);

            if (randomFactor == 1)
            {
                block.transform.position = new Vector3(initialPosition.x, initialPosition.y + Random.Range(0, 6), initialPosition.z);
            }
            else
            {
                block.transform.position = initialPosition;
            }
        }
    }
}
