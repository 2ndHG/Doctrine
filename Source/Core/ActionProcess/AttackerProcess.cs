namespace Doctrine;

public record AttackProcess(
    string AttributeKey,
    AttributeValue Value,
    ProcessType Op
) : ActionProcess
{
    public override void Process(TurnInfo turn)
    {

        
    }
}