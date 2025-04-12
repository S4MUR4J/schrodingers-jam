using System.Collections.Generic;
using levelsSO;
using scriptableObjects;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [SerializeField] private BaseLevelSo level;

    [SerializeField] private float nodeSize = 1f;


    private List<List<BaseNode>> _nodes;

    public static NodeManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("NodeManager instance created.");
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        _nodes = new List<List<BaseNode>>();
        SpawnNodeGrid();
    }

    private void SpawnNodeGrid()
    {
        Debug.Log("Spawning Node Grid");

        var gridHeight = level.nodes.Count;

        var startPosition = transform.position;

        for (var x = 0; x < gridHeight; x++)
        {
            var rowList = new List<BaseNode>();
            var gridWidth = level.nodes[x].row.Count;

            for (var z = 0; z < gridWidth; z++)
            {
                var nodeSo = level.nodes[x].row[z];

                if (nodeSo == null)
                {
                    continue;
                }

                var node = SpawnNode(nodeSo, startPosition, x, z, rowList);

                if (node == null)
                {
                    continue;
                }

                SpawnElement(node, nodeSo);
            }
            _nodes.Add(rowList);
        }
    }

    private BaseNode SpawnNode(BaseNodeSo nodeSo, Vector3 startPosition, int x, int z,List<BaseNode> row)
    {
        var position = startPosition + new Vector3(x * nodeSize, 0, z * nodeSize);

        var nodeObj = Instantiate(nodeSo.prefab, position, Quaternion.identity, transform);

        var node = nodeObj.GetComponent<BaseNode>();

        if (node != null)
        {
            row.Add(node);
        }
        else
        {
            Debug.LogError("Node prefab is missing BaseNode component!");
        }

        return node;
    }

    private void SpawnElement(BaseNode node, BaseNodeSo nodeSo)
    {
        if (nodeSo.elementSo == null)
        {
            return;
        }

        var elementGameObject = Instantiate(nodeSo.elementSo.prefab, node.transform);
        var element = elementGameObject.GetComponent<BaseElement>();
        if (element != null)
        {
            element.SetParentNode(node);
            Debug.Log("Element Spawned on node: " + node.name);
        }
        else
        {
            Debug.LogError("No BaseElement found on the prefab!");
        }
    }
    
    public List<BaseNode> GetNeighbors(BaseNode node)
    {
        var neighbors = new List<BaseNode>();

        // Find node's position in grid
        int nodeX = -1, nodeZ = -1;

        for (var x = 0; x < _nodes.Count; x++)
        {
            for (var z = 0; z < _nodes[x].Count; z++)
            {
                if (_nodes[x][z] != node) continue;
                nodeX = x;
                nodeZ = z;
                break;
            }

            if (nodeX != -1) break;
        }

        // Check for neighbors in the 4 possible directions
        // Up
        if (nodeX > 0)
            neighbors.Add(_nodes[nodeX - 1][nodeZ]);

        // Down
        if (nodeX < _nodes.Count - 1)
            neighbors.Add(_nodes[nodeX + 1][nodeZ]);

        // Left
        if (nodeZ > 0)
            neighbors.Add(_nodes[nodeX][nodeZ - 1]);

        // Right
        if (nodeZ < _nodes[nodeX].Count - 1)
            neighbors.Add(_nodes[nodeX][nodeZ + 1]);

        return neighbors;
    } 
}