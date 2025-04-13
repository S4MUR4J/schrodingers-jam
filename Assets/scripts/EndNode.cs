using scriptableObjects;

public class EndNode : BaseNode
{
    private EndNodeSo _endNodeSo;

    private new void Awake()
    {
        base.Awake();
        _endNodeSo = (EndNodeSo)GetNodeSo();
    }

    public override void SetElement(BaseElement newElement, bool movePlayer = false)
    {
        base.SetElement(newElement, movePlayer);


        NodeManager.instance.LoadLevel(_endNodeSo.nextLevelSo);
    }

    public EndNodeSo GetEndNodeSo()
    {
        return _endNodeSo;
    }
}