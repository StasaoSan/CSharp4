using System;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsParser.Strateges;

public class TreeGoToCommand : ICommandStrategy
{
    private const int ExpectedCountArguments = 1;
    private readonly Writer _writer;

    public TreeGoToCommand(Writer writer)
    {
        _writer = writer;
    }

    public void Execute(string[] args)
    {
        if (args.Length != ExpectedCountArguments)
            throw new AggregateException($"For command 'treegoto' expected {ExpectedCountArguments} arguments, recived {args.Length} count arguments");

        string path = args[0];

        var dir = new SystemDirectory();
        dir.TreeGoto(path);
        _writer.Write($"TreeGoto command: {path}");
    }
}