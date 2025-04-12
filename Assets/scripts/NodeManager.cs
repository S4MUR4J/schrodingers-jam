using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [SerializeField] private GameObject[] nodePrefabs;
    [SerializeField] private GameObject[] elementPrefabs;
    [SerializeField] private int gridHeight = 5;
    [SerializeField] private int gridWidth = 5;
    [SerializeField] private float nodeSize = 1f;


    private BaseNode[,] _nodes;

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
        FillNodesWithElements();
    }

    private void SpawnNodeGrid()
    {
        Debug.Log("Spawning Node Grid");

        _nodes = new BaseNode[gridHeight, gridWidth];
        var startPosition = transform.position;

        for (var x = 0; x < gridHeight; x++)
        {
            for (var z = 0; z < gridWidth; z++)
            {
                var position = startPosition + new Vector3(x * nodeSize, 0, z * nodeSize);
                var nodeObj = Instantiate(nodePrefabs[0], position, Quaternion.identity, transform);

                var node = nodeObj.GetComponent<BaseNode>();
                if (node != null)
                {
                    _nodes[x, z] = node;
                    Debug.Log($"Node added at [{x},{z}]");
                }
                else
                {
                    Debug.LogError("Node prefab is missing BaseNode component!");
                }
            }
        }
    }


    private void FillNodesWithElements()
    {
        Debug.Log("FillNodesWithElements");

        foreach (var node in _nodes)
        {
            if (node.HasElement())
            {
                Debug.Log("Node already has an element, skipping: " + node.name);
                continue;
            }


            var elementGameObject = Instantiate(elementPrefabs[Random.Range(0, elementPrefabs.Length)], node.transform);

            Debug.Log("spawning element for node " + node.name);
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
}