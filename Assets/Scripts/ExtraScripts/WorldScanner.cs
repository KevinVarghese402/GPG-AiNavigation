
using System.Collections.Generic;
using UnityEngine;

public class WorldScanner : MonoBehaviour
{
    public int gridSizeX = 100;
    public int gridSizeZ = 100;
    public float cellSize = 1.0f;
    public LayerMask layerMask;

    public Node[,] gridNodeReferences;

    void Start()
    {
        ScanGameWorld();
    }

    void ScanGameWorld()
    {
        gridNodeReferences = new Node[gridSizeX, gridSizeZ];

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                Vector3 position = new Vector3(x * cellSize, 0, z * cellSize) + transform.position;

                bool isBlocked = Physics.CheckBox(position, new Vector3(0.5f, 10f, 0.5f), Quaternion.identity, layerMask);

                gridNodeReferences[x, z] = new Node(x, z, isBlocked);
            }
        }
    } 

    private void OnDrawGizmos()
    {
        if (gridNodeReferences == null) return;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                Node node = gridNodeReferences[x, z];

                if (node.isBlocked)
                {
                    Gizmos.color = Color.red;
                }
                else
                {
                    Gizmos.color = Color.green;
                }

                Gizmos.DrawCube(transform.position + new Vector3(x * cellSize, 0, z * cellSize), Vector3.one * 0.9f);
            }
        }
    }
}

public class Node
{
    public int x, z;
    public bool isBlocked;

    public Node(int x, int z, bool isBlocked)
    {
        this.x = x;
        this.z = z;
        this.isBlocked = isBlocked;
    }
}