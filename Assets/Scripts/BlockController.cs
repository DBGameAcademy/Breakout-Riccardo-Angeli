using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public GameObject BlockPrefab;

    List<Vector3> BlockPositions = new List<Vector3>();
    Block[] Blocks;

    private void Awake()
    {
        Blocks = GetComponentsInChildren<Block>();
        foreach (Block block in Blocks)
        {
            BlockPositions.Add(block.transform.position);
        }
        ResetBlocks();
    }

    public void ResetBlocks()
    {
        //remove old blocks
        for (int i = Blocks.Length - 1; i >= 0; i--)
        {
            if (Blocks[i] != null)
            {
                Destroy(Blocks[i].gameObject);
            }
        }
        //add new blocks
        for (int i = 0; i < BlockPositions.Count; i++)
        {
            GameObject newBlockObj = GameObject.Instantiate(BlockPrefab, BlockPositions[i], Quaternion.identity, transform);
            Blocks[i] = newBlockObj.GetComponent<Block>();
        }
    }
}
