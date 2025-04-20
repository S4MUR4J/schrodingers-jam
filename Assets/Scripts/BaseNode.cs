using TMPro;
using UnityEngine;

public class BaseNode : MonoBehaviour
{
    public Vector2Int GridPosition { get; private set; }

    public string Pattern
    {
        get => textMesh.text;
        set => textMesh.text = value;
    }


    [Header("Design")] [SerializeField] private bool canMove;

    [Header("Unity Setup")] [SerializeField]
    private TextMeshProUGUI textMesh;

    [SerializeField] private Transform target;


    private void Awake()
    {
        GridPosition = new Vector2Int(
            Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.z)
        );

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

        NodeManager.Instance.Register(this);
    }

    public void EnableTextMesh()
    {
        if (textMesh)
        {
            textMesh.enabled = true;
        }
    }

    public void DisableTextMesh()
    {
        if (textMesh)
        {
            textMesh.enabled = false;
        }
    }

    public bool CanMove()
    {
        return canMove;
    }

    public Transform GetTarget()
    {
        return target;
    }
}