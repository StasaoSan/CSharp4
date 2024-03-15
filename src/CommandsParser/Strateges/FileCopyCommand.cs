using System;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsParser.Strateges;

public class FileCopyCommand : ICommandStrategy
{
    private const int ExpectedCountArguments = 2;
    private readonly Writer _writer;
    public FileCopyCommand(Writer writer)
    {
        _writer = writer;
    }

    public void Execute(string[] args)
    {
        if (args.Length != ExpectedCountArguments)
            throw new AggregateException($"For command 'filecopy' expected {ExpectedCountArguments} arguments, recived {args.Length} count arguments");

        string sourcePath = args[0];
        string destinationPath = args[1];

        var file = new SystemFile();
        file.CopyFile(sourcePath, destinationPath);
        _writer.Write($"Success copy file from {sourcePath} to {destinationPath}");
    }
}
