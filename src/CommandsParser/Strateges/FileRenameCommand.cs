using System;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsParser.Strateges;

public class FileRenameCommand : ICommandStrategy
{
    private const int ExpectedCountArguments = 2;
    private readonly Writer _writer;

    public FileRenameCommand(Writer writer)
    {
        _writer = writer;
    }

    public void Execute(string[] args)
    {
        if (args.Length != ExpectedCountArguments)
            throw new AggregateException($"For command 'filerename' expected {ExpectedCountArguments} arguments, recived {args.Length} count arguments");

        string path = args[0];
        string newName = args[1];

        var file = new SystemFile();
        file.RenameFile(path, newName);
        _writer.Write($"Rename file {path} new name is {newName}");
    }
}