using System;

namespace Doctrine;

// One element's share of an attack. An attack can carry several parts (e.g. 6 physical + 4 fire).
// Amount = (Base + Flat) * (1 + Increased) * More, never below 0.
// Increased stacks additively (+50% and +30% = +80%), More stacks multiplicatively (x1.5 * x0.75).
public class DamagePart
{
    public string Element { get; set; }
    public double Base { get; set; }
    public double Flat { get; set; }
    public double Increased { get; set; }
    public double More { get; set; } = 1;

    public DamagePart(string element, double baseAmount)
    {
        Element = element;
        Base = baseAmount;
    }

    public double Amount => Math.Max(0, (Base + Flat) * (1 + Increased) * More);

    public override string ToString() => $"{Amount:0.##} {Element}";
}
