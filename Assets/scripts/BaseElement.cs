using UnityEngine;

public class BaseElement : MonoBehaviour
{
    [SerializeField] public string Pattern => "TEST3";
    
    private GameObject _prefab;
    
    private BaseNode _parentNode;

    public void SetParentNode(BaseNode newNode)
    {
        if (_parentNode != null)
        {
            _parentNode.ClearElement();
        }

        _parentNode = newNode;

        if (newNode.HasElement())
        {
            Debug.LogError("Node Already has an element!");
        }

        newNode.SetElement(this);

        transform.parent = newNode.GetNodeTopPoint();
        transform.localPosition = Vector3.zero;
    }

    public BaseNode GetParentNode()
    {
        return _parentNode;
    }

    public void DestroySelf()
    {
        _parentNode.ClearElement();
        Destroy(gameObject);
    }
    
}
