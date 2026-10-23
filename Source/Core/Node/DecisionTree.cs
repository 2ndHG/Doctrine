namespace MyGame;

public class DecisionTree
{
    public Node? EntryNode { get; set; } = null;

    public TriggerResult Trigger()
    {
        Node? currentNode = EntryNode;
        while (currentNode is BranchNode branchNode)
        {
            // temporary using new()
            if (branchNode.Evaluate(new(), new()))
            {
                currentNode = branchNode.IfNode;
            }
            else
            {
                currentNode = branchNode.ElseNode;
            }
        }

        if (currentNode is EndActionNode endAction)
        {
            return new TriggerResult() { Success = true, EndAction = endAction };
        }

        return new TriggerResult() { Success = false, EndAction = null };
    }


    public readonly record struct TriggerResult(bool Success, EndActionNode? EndAction)
    {
    }
}