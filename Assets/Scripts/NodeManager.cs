using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public static NodeManager Instance { get; private set; }

    private List<BaseNode> allNodes = new();

    private void Awake()
    {
        Instance = this;
    }

    public void Register(BaseNode node)
    {
        allNodes.Add(node);
    }

    public BaseNode GetClosestNode(Vector3 position)
    {
        BaseNode closest = null;
        var closestDist = float.MaxValue;

        foreach (var node in allNodes)
        {
            var dist = Vector3.Distance(position, node.transform.position);
            if (!(dist < closestDist))
            {
                continue;
            }

            closestDist = dist;
            closest = node;
        }

        return closest;
    }
}