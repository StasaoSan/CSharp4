using System;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsParser.Strateges;

public class FileMoveCommand : ICommandStrategy
{
    private const int ExpectedCountArguments = 2;
    private readonly Writer _writer;
    public FileMoveCommand(Writer writer)
    {
        _writer = writer;
    }

    public void Execute(string[] args)
    {
        if (args.Length != ExpectedCountArguments)
            throw new AggregateException($"For command 'filemove' expected {ExpectedCountArguments} arguments, recived {args.Length} count arguments");

        string sourcePath = args[0];
        string destinationPath = args[1];

        var file = new SystemFile();
        file.MoveFile(sourcePath, destinationPath);
        _writer.Write($"Success move file from {sourcePath} to {destinationPath}");
    }
}