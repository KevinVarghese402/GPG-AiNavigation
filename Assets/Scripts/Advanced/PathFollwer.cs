using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFollwer : MonoBehaviour
{
    public WorldScanner worldScanner;
    public List<Node> open = new List<Node>(); // Frontier nodes
    public List<Node> closed = new List<Node>(); // Processed nodes

    private Vector2Int playerGridPosition; // Player's current grid position

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Press 'F' to start the flood fill
        {
            StartFill();
        }
    }

    void StartFill()
    {
        if (worldScanner == null || worldScanner.gridNodeReferences == null)
        {
            Debug.LogError("WorldScanner not assigned or not initialized!");
            return;
        }

        playerGridPosition = GetGridPosition(transform.position); // Get player position in the grid
        FillGrid(playerGridPosition);
    }

    void FillGrid(Vector2Int startPos)
    {
        if (!IsValidNode(startPos)) return;

        Node startNode = worldScanner.gridNodeReferences[startPos.x, startPos.y];
        if (startNode.isBlocked)
        {
            Debug.Log("Start position is blocked!");
            return;
        }

        open.Clear();
        closed.Clear();
        open.Add(startNode);

        while (open.Count > 0)
        {
            Node currentNode = open[Random.Range(0, open.Count)];
            open.Remove(currentNode);
            closed.Add(currentNode);

            Vector2Int currentPos = GetNodePosition(currentNode);

            for (int xOffset = -1; xOffset <= 1; xOffset++)
            {
                for (int zOffset = -1; zOffset <= 1; zOffset++)
                {
                    if (Mathf.Abs(xOffset) == Mathf.Abs(zOffset)) continue;

                    Vector2Int neighborPos = new Vector2Int(currentPos.x + xOffset, currentPos.y + zOffset);

                    if (!IsValidNode(neighborPos)) continue;

                    Node neighborNode = worldScanner.gridNodeReferences[neighborPos.x, neighborPos.y];

                    if (!neighborNode.isBlocked && !open.Contains(neighborNode) && !closed.Contains(neighborNode))
                    {
                        open.Add(neighborNode);
                    }
                }
            }
        }

        Debug.Log("Flood fill complete!");
    }

    Vector2Int GetGridPosition(Vector3 worldPos)
    {
        int x = Mathf.RoundToInt(worldPos.x);
        int z = Mathf.RoundToInt(worldPos.z);
        return new Vector2Int(x, z);
    }

    Vector2Int GetNodePosition(Node node)
    {
        for (int x = 0; x < worldScanner.gridSizeX; x++)
        {
            for (int z = 0; z < worldScanner.gridSizeZ; z++)
            {
                if (worldScanner.gridNodeReferences[x, z] == node)
                {
                    return new Vector2Int(x, z);
                }
            }
        }
        return Vector2Int.zero;
    }

    bool IsValidNode(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < worldScanner.gridSizeX && pos.y >= 0 && pos.y < worldScanner.gridSizeZ;
    }

    private void OnDrawGizmos()
    {
        if (worldScanner == null || worldScanner.gridNodeReferences == null) return;

        for (int x = 0; x < worldScanner.gridSizeX; x++)
        {
            for (int z = 0; z < worldScanner.gridSizeZ; z++)
            {
                Node node = worldScanner.gridNodeReferences[x, z];

                if (node.isBlocked)
                {
                    Gizmos.color = Color.red;
                }
                else if (closed.Contains(node))
                {
                    Gizmos.color = new Color(1f, 0.92f, 0.01f, 0.5f); // Yellow for flooded areas
                }
                else
                {
                    Gizmos.color = Color.white;
                }

                Gizmos.DrawCube(new Vector3(x, 0, z), Vector3.one * 0.8f);
            }
        }
    }
}


