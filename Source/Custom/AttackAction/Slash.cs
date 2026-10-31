namespace Doctrine;

public record Slash : AttackAction
{
    public override void Declare(CharacterContext user, TurnInfo turn)
    {
        turn.AttackAttributes.Add("rangeType", AttributeValue.Text("melee"));
        turn.Damage.Add(new DamagePart(Element.Physical, 8));
    }
}
