using UnityEngine;

public class BaseElement : MonoBehaviour
{
    private GameObject _prefab;

    protected BaseNode parentNode;

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
