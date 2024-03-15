using System;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsParser.Strateges;

public class FileDeleteCommand : ICommandStrategy
{
    private const int ExpectedCountArguments = 1;
    private readonly Writer _writer;

    public FileDeleteCommand(Writer writer)
    {
        _writer = writer;
    }

    public void Execute(string[] args)
    {
        if (args.Length != ExpectedCountArguments)
            throw new AggregateException($"For command 'filedelete' expected {ExpectedCountArguments} arguments, recived {args.Length} count arguments");

        string path = args[0];

        var file = new SystemFile();
        file.Delete(path);
        _writer.Write($"Success delete file: {path}");
    }
}
