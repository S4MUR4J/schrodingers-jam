using System.Collections.Generic;
using levelsSO;
using scriptableObjects;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [SerializeField] private BaseLevelSo level;

    [SerializeField] private float nodeSize = 1f;


    private List<BaseNode> _nodes;

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
        SpawnNodeGrid();
    }

    private void SpawnNodeGrid()
    {
        Debug.Log("Spawning Node Grid");

        var gridHeight = level.nodes.Count;

        _nodes = new List<BaseNode>();

        var startPosition = transform.position;

        for (var x = 0; x < gridHeight; x++)
        {
            var gridWidth = level.nodes[x].row.Count;

            for (var z = 0; z < gridWidth; z++)
            {
                var nodeSo = level.nodes[x].row[z];

                if (nodeSo == null)
                {
                    continue;
                }

                var node = SpawnNode(nodeSo, startPosition, x, z);

                if (node == null)
                {
                    continue;
                }

                SpawnElement(node, nodeSo);
            }
        }
    }

    private BaseNode SpawnNode(BaseNodeSo nodeSo, Vector3 startPosition, int x, int z)
    {
        var position = startPosition + new Vector3(x * nodeSize, 0, z * nodeSize);

        var nodeObj = Instantiate(nodeSo.prefab, position, Quaternion.identity, transform);

        var node = nodeObj.GetComponent<BaseNode>();

        if (node != null)
        {
            _nodes.Add(node);
        }
        else
        {
            Debug.LogError("Node prefab is missing BaseNode component!");
        }

        return node;
    }

    private void SpawnElement(BaseNode node, BaseNodeSo nodeSo)
    {
        if (nodeSo.element == null)
        {
            return;
        }

        var elementGameObject = Instantiate(nodeSo.element, node.transform);
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
}