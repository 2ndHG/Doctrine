namespace MyGame;

public abstract record ActionProcess
{
    public abstract void Process(TurnInfo turn);
}