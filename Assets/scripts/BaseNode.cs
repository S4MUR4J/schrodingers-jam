using System;
using TMPro;
using UnityEngine;
using Utils;

public class BaseNode : MonoBehaviour
{
    [SerializeField] private Transform nodeTopPoint;
    [SerializeField] private TextMeshProUGUI _text;

    private BaseElement _element;
    public String pattern;


    private void Awake()
    {

        Debug.Log("BaseNode Created" + name);

        pattern = Constants.Words[UnityEngine.Random.Range(0, Constants.Words.Count - 1)];
        _text.text = pattern;
        Debug.Log("Pattern: " + pattern);
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