using scriptableObjects;
using UnityEngine;

public class EndNode : BaseNode
{
    [SerializeField] private EndNodeSo endNodeSo;

    public override void SetElement(BaseElement newElement, bool movePlayer = false)
    {
        base.SetElement(newElement, movePlayer);
        NodeManager.instance.LoadNextLevel(endNodeSo.nextLevelSo);
    }

    public EndNodeSo GetEndNodeSo()
    {
        return endNodeSo;
    }
}