using scriptableObjects;
using TMPro;
using UnityEngine;
using Utils;

public class BaseNode : MonoBehaviour
{
    [SerializeField] private Transform nodeTopPoint;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private BaseNodeSo _nodeSo;
    public string pattern;

    private BaseElement _element;


    private void Awake()
    {
        pattern = Constants.Words[Random.Range(0, Constants.Words.Count - 1)];

        if (_nodeSo.withPattern)
            _text.text = pattern;
    }

    public virtual void SetElement(BaseElement newElement, bool hide = false)
    {
        if (newElement == null)
        {
            _element = null;
            return;
        }

        _element = newElement;
        if (hide)
            _text.text = string.Empty;
        newElement.transform.parent = nodeTopPoint;
    }

    public BaseElement GetElement()
    {
        return _element;
    }

    public void ClearElement()
    {
        _element = null;
    }

    public bool HasElement()
    {
        return _element != null;
    }

    public Transform GetNodeTopPoint()
    {
        return nodeTopPoint;
    }
}
