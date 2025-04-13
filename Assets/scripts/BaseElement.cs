using scriptableObjects;
using UnityEngine;

public class BaseElement : MonoBehaviour
{
    [SerializeField] private float lerpSpeed = 5f;
    private bool _isLerping;

    private GameObject _prefab;
    private Vector3 _targetPosition;

    protected BaseNode parentNode;


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


    public void SetParentNode(BaseNode newNode, bool hide = false)
    {
        // if (newNode is EndNode endNode)
        // {
        //     EndNodeSo endNodeSo = endNode.GetEndNodeSo();
        //
        //     Debug.LogWarning("Tropka found loading next level: " + endNodeSo.nextLevelSo.name);
        //     GameManager.instance.LoadLevel(endNodeSo.nextLevelSo);
        // }

        if (parentNode != null)
        {
            parentNode.ClearElement(hide);
        }

        if (newNode.HasElement())
        {
            Debug.LogError("Node Already has an element!");
        }

        parentNode = newNode;

        newNode.SetElement(this, hide);


        transform.parent = newNode.GetNodeTopPoint();
        _targetPosition = newNode.GetNodeTopPoint().position;
        _isLerping = true;
    }

    public BaseNode GetParentNode()
    {
        return parentNode;
    }

    public virtual void DestroySelf()
    {
        parentNode.ClearElement();
        Destroy(gameObject);
    }
}
