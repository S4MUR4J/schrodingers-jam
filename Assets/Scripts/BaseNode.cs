using TMPro;
using UnityEngine;

public class BaseNode : MonoBehaviour
{
    [SerializeField] private string pattern;
    [SerializeField] private bool canMove;


    [Header("Setup")] [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private Transform target;

    private void Awake()
    {
        if (!canMove)
        {
            return;
        }

        if (target == null)
        {
            Debug.LogError("Target is null");
            return;
        }

        if (textMesh == null)
        {
            Debug.LogError("TextMesh is null");
            return;
        }

        textMesh.text = pattern;

        NodeManager.Instance.Register(this);
    }

    public void EnableTextMesh()
    {
        textMesh.enabled = true;
    }

    public void DisableTextMesh()
    {
        textMesh.enabled = false;
    }

    public bool CanMove()
    {
        return canMove;
    }

    public Transform GetTarget()
    {
        return target;
    }

    public string GetPattern()
    {
        return pattern;
    }
}