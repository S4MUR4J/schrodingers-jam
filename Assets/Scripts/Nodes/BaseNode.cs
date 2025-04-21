using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;

namespace Nodes
{
    public class BaseNode : MonoBehaviour
    {
        public string Pattern
        {
            get => textMesh.text;
            set => textMesh.text = value;
            
        }


        [Header("Design")] [SerializeField] private bool canMove;
        [SerializeField] private Color baseTextColor;
        [SerializeField] private Color highlightedTextColor;

        [Header("Unity Setup")] [SerializeField]
        private TextMeshProUGUI textMesh;

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

            textMesh.enabled = true;
            textMesh.color = baseTextColor;
            NodeManager.Instance.Register(this);
        }

        public void Highlight(bool highlighted)
        {
            if (!textMesh)
            {
                return;
            }

            if (highlighted)
            {
                textMesh.color = highlightedTextColor;
                return;
            }

            textMesh.color = baseTextColor;
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
}