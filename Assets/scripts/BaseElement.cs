using scriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

public class BaseElement : MonoBehaviour
{
    public string Pattern { get; private set; }

    [SerializeField] private BaseElementSo elementSo;

    private GameObject _prefab;

    protected BaseNode parentNode;

    private void Awake()
    {
        if (elementSo.patterns is not { Count: > 0 }) return;
        Pattern = elementSo.patterns[0]; //TODO dawaj tutaj randomowy pattern z listy elementSo.patterns 
        Debug.Log("Randomly selected pattern: " + Pattern);

    }

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

    public BaseElementSo GetElementSo()
    {
        return elementSo;
    }

    public void DestroySelf()
    {
        parentNode.ClearElement();
        Destroy(gameObject);
    }
}