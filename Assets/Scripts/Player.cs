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

        _positionNode.DisableTextMesh();


        GameManager.Instance.Player = this;
    }
}