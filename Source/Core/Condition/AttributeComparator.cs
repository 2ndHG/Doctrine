using System;

namespace MyGame;

public enum AttributeComparator
{
    Greater,
    GreaterEqual,
    Less,
    LessEqual,
    Equal,
    NotEqual
}

public static class AttributeComparatorExtensions
{
    public static bool Compare<T>(this AttributeComparator comparator, T value, T target)
        where T : IComparable<T>
    {
        int result = value.CompareTo(target);
        return comparator switch
        {
            AttributeComparator.Greater => result > 0,
            AttributeComparator.GreaterEqual => result >= 0,
            AttributeComparator.Less => result < 0,
            AttributeComparator.LessEqual => result <= 0,
            AttributeComparator.Equal => result == 0,
            AttributeComparator.NotEqual => result != 0,
            _ => false
        };
    }
}
