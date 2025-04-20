using System;
using TMPro;
using UnityEngine;

public class BaseNode : MonoBehaviour
{
    [SerializeField] private bool canMove;


    [Header("Setup")] [SerializeField] private TextMeshProUGUI textMesh;

    private string pattern;


    private void Awake()
    {
        pattern = "test";
        textMesh.text = pattern;

        if (!canMove)
        {
            textMesh.enabled = false;
        }
    }


    public bool CanMove()
    {
        return canMove;
    }
}