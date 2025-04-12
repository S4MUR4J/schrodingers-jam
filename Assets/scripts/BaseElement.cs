using UnityEngine;

public class BaseElement : MonoBehaviour
{
    public string Pattern { get; private set; }

    private GameObject _prefab;

    protected BaseNode parentNode;

    public void SetParentNode(BaseNode newNode)
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

        newNode.SetElement(this);

        transform.parent = newNode.GetNodeTopPoint();
        transform.localPosition = Vector3.zero;
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