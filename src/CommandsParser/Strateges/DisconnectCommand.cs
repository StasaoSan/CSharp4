using System;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsParser.Strateges;

public class DisconnectCommand : ICommandStrategy
{
    private const int ExpectedCountArguments = 0;
    private readonly Writer _writer;

    public DisconnectCommand(Writer writer)
    {
        _writer = writer;
    }

    public void Execute(string[] args)
    {
        if (args.Length > ExpectedCountArguments)
            throw new AggregateException($"For command 'disconnect' expected {ExpectedCountArguments} arguments, recived {args.Length} count arguments");
        _writer.Write("Disconnect command");
    }
}