namespace Doctrine;

public abstract record ActionProcess
{
    public abstract void Process(TurnInfo turn);
}