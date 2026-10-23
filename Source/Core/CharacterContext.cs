using System.Collections.Generic;

namespace MyGame;

public class CharacterContext
{
    public float Hp { get; set; }
    public float Mana { get; set; }
    public float Stamina { get; set; }
    public Dictionary<string, object> Attribute { get; init; } = [];
}