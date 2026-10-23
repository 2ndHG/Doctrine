namespace MyGame;

public record AttackProcess(
    string AttributeKey,
    AttributeValue Value,
    ProcessType Comparator
) : ActionProcess
{
    public override void Process(TurnInfo turn)
    {

        
    }
}