using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [SerializeField] GameObject[] nodePrefabs;
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

    private bool _lateStarted = false;

    void Update()
    {
        if (!_lateStarted)
        {
            LateStart();
            _lateStarted = true;
        }
    }


    void LateStart()
    {
        FillNodesWithElements();
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

            GameObject gameObject = Instantiate(nodePrefabs[0], node.transform);

            Debug.Log("spawning element for node " + node.name);
            BaseElement element = gameObject.GetComponent<BaseElement>();
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