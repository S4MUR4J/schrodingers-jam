using UnityEngine;

public class Player : MonoBehaviour
{
    private BaseNode _positionNode;

    private void Start()
    {
        GameManager.Instance.Player = this;

        FindStartingPosition();
        
    }
    private void FindStartingPosition()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out var hit))
        {
            var node = hit.collider.GetComponent<BaseNode>();
            if (node != null)
            {
                _positionNode = node;
            }
        }

        if (_positionNode == null)
        {
            Debug.LogError("Starting Node Not Found For Player");
        }
    }
}