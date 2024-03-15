using System;
using Itmo.ObjectOrientedProgramming.Lab4.CommandsParser;
using Itmo.ObjectOrientedProgramming.Lab4.CommandsParser.Strateges.FileShow;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class FileShowCommand : ICommandStrategy
{
    private const int MaxCountArguments = 3;
    private readonly Writer _writer;

    public FileShowCommand(Writer writer)
    {
        _writer = writer;
    }

    public void Execute(string[] args)
    {
        if (args.Length == 0)
            throw new AggregateException("No arguments provided for 'file show' command");

        string path = args[0];
        string mode = "console";

        if (args.Length > 1 && args[1] == "-m" && args.Length == MaxCountArguments) mode = args[2];
        else if (args.Length > 1) throw new AggregateException($"For command 'file show' expected {MaxCountArguments} arguments, recived {args.Length} count arguments");

        SystemFile.Display(path, FileShowStrategyFactory.CreateStrategy(mode));
        _writer.Write($"FileShow: {path} in mode: {mode}");
    }
}