using System;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsParser.Strateges;

public class ConnectCommand : ICommandStrategy
{
    private const int ExpectedCountArguments = 3;
    private readonly Writer _writer;

    public ConnectCommand(Writer writer)
    {
        _writer = writer;
    }

    public void Execute(string[] args)
    {
        if (args.Length != ExpectedCountArguments)
            throw new AggregateException($"For command 'connect' expected {ExpectedCountArguments} arguments, received {args.Length} count arguments");

        string path = args[0];
        string strategyName = args[2];

        ConnectStrategyFactory.CreateStrategy(strategyName).Execute(path);

        _writer.Write($"Connect command {FileSystemPathManager.Instance.CurrentPath}, mode {strategyName}");
    }
}