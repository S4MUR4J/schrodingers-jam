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
        if (target == null)
        {
            Debug.LogError("Target is null");
            return;
        }

        if (textMesh == null && canMove)
        {
            Debug.LogError("TextMesh is null");
            return;
        }

        textMesh.text = pattern;
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