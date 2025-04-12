using UnityEngine;

public class BaseNode : MonoBehaviour
{
    [SerializeField] private Transform nodeTopPoint;
    
    private BaseElement element;
    
    public void SetElement(BaseElement newElement)
    {
        element = newElement;
        newElement.transform.parent = nodeTopPoint;
    }
    
    public void ClearElement()
    {
        element = null;
    }

    public bool HasElement()
    {
        return element != null;
    }
    
    public Transform GetNodeTopPoint() {
        return nodeTopPoint;
    }
}