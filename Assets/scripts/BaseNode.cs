using System.Collections.Generic;
using System.Linq;
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

    private List<string> _allPatterns;
    private BaseElement _element;


    private void Awake()
    {
        _allPatterns = new List<string>();

        var newPattern = Constants.GetWord();
        while (_allPatterns.Any(p => p == newPattern))
        {
            newPattern = Constants.GetWord();
        }

        if (_nodeSo.withPattern)
        {
            pattern = newPattern;
            _text.text = newPattern;
            _allPatterns.Add(newPattern);
        }

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

    public BaseNodeSo GetNodeSo()
    {
        return _nodeSo;
    }
}
