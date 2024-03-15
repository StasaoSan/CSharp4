using System;
using System.Globalization;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsParser.Strateges;

public class TreeListCommand : ICommandStrategy
{
    private const int ExpectedCountArguments = 2;
    private readonly Writer _writer;

    public TreeListCommand(Writer writer)
    {
        _writer = writer;
    }

    public void Execute(string[] args)
    {
        int depth = 1;
        if (args.Length == ExpectedCountArguments && args[0] == "-d")
        {
            if (!int.TryParse(args[1], NumberStyles.None, CultureInfo.InvariantCulture, out depth))
                throw new AggregateException("Invalid depth argument");
        }

        var dir = new SystemDirectory(new ConsoleWriter());
        if (FileSystemPathManager.Instance.CurrentPath is null)
            throw new AggregateException("Path cant be null. Please use 'connect' command to set path");
        dir.TreeList(FileSystemPathManager.Instance.CurrentPath, depth);

        _writer.Write($"TreeListCommand, depth = {depth}");
    }
}