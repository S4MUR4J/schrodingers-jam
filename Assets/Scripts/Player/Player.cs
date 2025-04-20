using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BaseNode _positionNode;

    private void Start()
    {
        _positionNode = NodeManager.Instance.GetClosestNode(transform.position);

        if (_positionNode == null)
        {
            Debug.LogError("Starting Node Not Found For Player");
        }
        else
        {
            _positionNode.DisableTextMesh();
        }


        GameManager.Instance.Player = this;
    }

    private void Update()
    {
        var neighbours = NodeManager.Instance.GetNeighbors(_positionNode);

        foreach (var node in neighbours.Where(node => node.CanMove()))
        {
            Debug.Log("Found Node with pattern=" + node.Pattern);
        }
    }
}