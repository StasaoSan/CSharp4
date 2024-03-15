namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsParser;

public interface ICommandStrategy
{
    void Execute(string[] args);
}