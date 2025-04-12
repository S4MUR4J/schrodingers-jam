using System;
using UnityEngine;

public class BaseElement : MonoBehaviour
{
    [SerializeField] private float lerpSpeed = 5f;
    
    protected BaseNode parentNode;
    private GameObject _prefab;
    private bool _isLerping = false;
    private Vector3 _targetPosition;

    private void Update()
    {
        if (!_isLerping)
        {
            return;
        }
        transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * lerpSpeed);

        if (!(Vector3.Distance(transform.position, _targetPosition) < 0.001f))
        {
            return;
        }
        transform.position = _targetPosition;
        _isLerping = false;
    }


    public void SetParentNode(BaseNode newNode, bool movePlayer = false)
    {
        if (parentNode != null)
        {
            parentNode.ClearElement();
        }

        if (newNode.HasElement())
        {
            Debug.LogError("Node Already has an element!");
        }

        parentNode = newNode;

        newNode.SetElement(this, movePlayer);

        transform.parent = newNode.GetNodeTopPoint();
        _targetPosition = newNode.GetNodeTopPoint().position;
        _isLerping = true;
    }

    public BaseNode GetParentNode()
    {
        return parentNode;
    }

    public void DestroySelf()
    {
        parentNode.ClearElement();
        Destroy(gameObject);
    }
}