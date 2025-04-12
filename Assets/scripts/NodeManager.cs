using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [SerializeField] GameObject[] nodePrefabs;
    [SerializeField] GameObject[] elementPrefabs;
    [SerializeField] private int gridHeight = 5;
    [SerializeField] private int gridWidth = 5;
    [SerializeField] private float nodeSize = 1f;


    private List<BaseNode> nodes = new List<BaseNode>();

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
    

    void Start()
    {
        SpawnNodeGrid();
        FillNodesWithElements();
    }

    private void SpawnNodeGrid()
    {
        Debug.Log("Spawning Node Grid");

        Vector3 startPosition = transform.position;

        for (int x = 0; x < gridHeight; x++)
        {
            for (int z = 0; z < gridWidth; z++)
            {
                Vector3 position = startPosition + new Vector3(x * nodeSize, 0, z * nodeSize);
                Instantiate(nodePrefabs[0], position, Quaternion.identity, transform);
            }
        }
    }


    private void FillNodesWithElements()
    {
        Debug.Log("FillNodesWithElements");

        foreach (var node in nodes)
        {
            if (node.HasElement())
            {
                Debug.Log("Node already has an element, skipping: " + node.name);
                continue;
            }

            GameObject elementGameObject = Instantiate(elementPrefabs[0], node.transform);

            Debug.Log("spawning element for node " + node.name);
            BaseElement element = elementGameObject.GetComponent<BaseElement>();
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

    public void AddNode(BaseNode node)
    {
        nodes.Add(node);
        Debug.Log("Node added to manager: " + node.name);
    }
}