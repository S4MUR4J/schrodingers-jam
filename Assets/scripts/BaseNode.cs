using UnityEngine;

public class BaseNode : MonoBehaviour
{
    [SerializeField] private Transform nodeTopPoint;
    
    private NodeManager manager;
    private BaseElement element;

   
    private void Awake()
    {
        manager = NodeManager.instance;
        manager.AddNode(this);
        Debug.Log("BaseNode Created" + name);
    }
   
    public void SetElement(BaseElement newElement)
    {
        element = newElement;
        newElement.transform.parent = nodeTopPoint;
    }
    
    public BaseElement GetElement()
    {
        return element;
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