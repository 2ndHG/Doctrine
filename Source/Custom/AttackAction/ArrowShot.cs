namespace Doctrine;

public record ArrowShot : AttackAction
{
    public override void Declare(CharacterContext user, TurnInfo turn)
    {
        turn.Attacker.Attributes.Decrement("arrowCount");
        turn.AttackAttributes.Add("rangeType", AttributeValue.Text("ranged"));
        turn.AttackAttributes.Add("damage", AttributeValue.Number(10));
    }
}