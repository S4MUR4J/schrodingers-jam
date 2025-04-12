using scriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

public class BaseElement : MonoBehaviour
{
    public string Pattern { get; private set; }

    [SerializeField] private BaseElementSo elementSo;

    private GameObject _prefab;

    protected BaseNode parentNode;

    private void Start()
    {
        if (elementSo.patterns is { Count: > 0 })
        {
            Pattern = elementSo.patterns[Random.Range(0, elementSo.patterns.Count)];
        }
        else
        {
            Debug.LogWarning("No patterns available in elementSo for: " + gameObject.name);
            Pattern = string.Empty;
        }
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
        transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, 2.0f * Time.deltaTime);
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