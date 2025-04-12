using UnityEngine;

public class BaseElement : MonoBehaviour
{
    [SerializeField] private float offset = 0.3f; 
    private GameObject prefab;
    
    private BaseNode parentNode;
    
    public void SetParentNode(BaseNode newNode)
    {
        if (parentNode != null)
        {
            parentNode.ClearElement();
        }

        parentNode = newNode;

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
        return parentNode;
    }

    public void DestroySelf()
    {
        parentNode.ClearElement();
        Destroy(gameObject);
    }
    
}
