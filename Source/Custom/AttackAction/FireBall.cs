namespace Doctrine;

// Mixed-element attack: shows multiple damage parts in one packet.
public record FireBall : AttackAction
{
    public override void Declare(CharacterContext user, TurnInfo turn)
    {
        turn.Attacker.Attributes.AddNumber("Mana", -10);
        turn.AttackAttributes.Add("rangeType", AttributeValue.Text("ranged"));
        turn.Damage.Add(new DamagePart(Element.Physical, 4));
        turn.Damage.Add(new DamagePart(Element.Fire, 12));
    }
}
