using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class BaseNode : MonoBehaviour
{
    [SerializeField] private Transform nodeTopPoint;
    [SerializeField] private TextMeshProUGUI _text;

    private NodeManager _manager;
    private BaseElement _element;


    public Tuple<int, int> Position;


    private void Awake()
    {
        _manager = NodeManager.instance;

        Debug.Log("BaseNode Created" + name);
    }

    public void SetElement(BaseElement newElement)
    {
        if (newElement == null)
        {
            _element = null;
            return;
        }

        _element = newElement;
        newElement.transform.parent = nodeTopPoint;

        if (_element == null)
        {
            _text.text = "test";
        }
        else
        {
            _text.text = _element.Pattern;
        }


        Debug.Log("Text set to: " + _element.Pattern);
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