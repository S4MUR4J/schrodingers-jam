using scriptableObjects;
using UnityEngine;

public class EndNode : BaseNode
{
    private EndNodeSo _endNodeSo;

    private void Awake()
    {
        _endNodeSo = (EndNodeSo)GetNodeSo();
    }

    public override void SetElement(BaseElement newElement, bool movePlayer = false)
    {
        base.SetElement(newElement, movePlayer);


        NodeManager.instance.LoadNextLevel(_endNodeSo.nextLevelSo);
    }
}