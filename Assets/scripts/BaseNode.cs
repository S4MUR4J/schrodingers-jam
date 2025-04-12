using System;
using JetBrains.Annotations;
using UnityEngine;

public class BaseNode : MonoBehaviour
{
    [SerializeField] private Transform nodeTopPoint;
    
    private NodeManager _manager;
    private BaseElement _element;

    public Tuple<int, int> Position;

    [CanBeNull]
    public string Pattern
    {
        get
        {
            return _element?.Pattern ?? "testy";
        }
    }

    private void Awake()
    {
        _manager = NodeManager.instance;
        Debug.Log("BaseNode Created" + name);
    }
   
    public void SetElement(BaseElement newElement)
    {
        _element = newElement;
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
    
    public Transform GetNodeTopPoint() {
        return nodeTopPoint;
    }
}