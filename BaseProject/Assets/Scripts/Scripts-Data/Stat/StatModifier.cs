

public enum StatModType
{
    Flat = 100,
    PercentAdd = 200,
    PercentMult = 300,
}

public class StatModifier 
{
    public readonly float Value;
    public StatModType Type;
    public readonly int Order; //  Add this variable to the top of the class
    public readonly object Source;

    // Change the existing constructor to look like this
    public StatModifier(float Value, StatModType type, int Order, object Source)
    {
        this.Value = Value;
        Type = type;
        this.Order = Order;
        this.Source = Source;
    }
    // Add a new constructor that automatically sets a default Order, in case the user doesn't want to manually define it
    public StatModifier(float Value, StatModType type) : this (Value, type, (int)type , null)
    {

    }
    public StatModifier(float Value, StatModType type, int Order) : this(Value, type, Order, null)
    {

    }
    public StatModifier(float Value, StatModType type, object Source) : this(Value, type, (int)type, Source)
    {

    }
}
