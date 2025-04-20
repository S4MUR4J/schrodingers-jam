using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public static NodeManager Instance { get; private set; }

    private Dictionary<Vector2Int, BaseNode> nodeGrid = new();


    private int _wordIndex;

    private void Awake()
    {
        Instance = this;
    }

    public void Register(BaseNode node)
    {
        nodeGrid.TryAdd(node.GridPosition, node);
        if (_wordIndex >= nodeGrid.Count)
        {
            _wordIndex = 0;
        }

        node.Pattern = Constants.Words[_wordIndex++];
    }

    public BaseNode GetClosestNode(Vector3 position)
    {
        var gridPos = new Vector2Int(
            Mathf.RoundToInt(position.x),
            Mathf.RoundToInt(position.z)
        );

        return nodeGrid.GetValueOrDefault(gridPos);
    }


    public List<BaseNode> GetNeighbors(BaseNode node)
    {
        List<BaseNode> neighbors = new();

        foreach (var dir in Constants.Directions)
        {
            Vector2Int neighborPos = node.GridPosition + dir;
            if (nodeGrid.TryGetValue(neighborPos, out var neighbor))
            {
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }
}