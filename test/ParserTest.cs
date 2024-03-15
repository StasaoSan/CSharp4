using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.CommandsParser;
using NSubstitute;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests;

public class ParserTest
{
    private CommandParser _parser;
    private Writer _writer;
    private ICommandStrategy _connect;
    private ICommandStrategy _disconnect;
    private ICommandStrategy _treeGoto;
    private ICommandStrategy _treeList;
    private ICommandStrategy _fileShow;
    private ICommandStrategy _fileMove;
    private ICommandStrategy _fileCopy;
    private ICommandStrategy _fileDelete;
    private ICommandStrategy _fileRename;

    public ParserTest()
    {
        _writer = Substitute.For<Writer>();
        _connect = Substitute.For<ICommandStrategy>();
        _disconnect = Substitute.For<ICommandStrategy>();
        _treeGoto = Substitute.For<ICommandStrategy>();
        _treeList = Substitute.For<ICommandStrategy>();
        _fileShow = Substitute.For<ICommandStrategy>();
        _fileMove = Substitute.For<ICommandStrategy>();
        _fileCopy = Substitute.For<ICommandStrategy>();
        _fileDelete = Substitute.For<ICommandStrategy>();
        _fileRename = Substitute.For<ICommandStrategy>();

        var commandStrategies = new Dictionary<string, ICommandStrategy>
        {
            { "connect", _connect },
            { "disconnect", _disconnect },
            { "tree goto", _treeGoto },
            { "tree list", _treeList },
            { "file show", _fileShow },
            { "file move", _fileMove },
            { "file copy", _fileCopy },
            { "file delete", _fileDelete },
            { "file rename", _fileRename },
        };

        _parser = new CommandParser(_writer, commandStrategies);
    }

    [Fact]
    public void ParseAndExecuteConnect()
    {
        const string input = "connect ./ -m local";
        _parser.ParseAndExecute(input);

        _connect.Received().Execute(Arg.Is<string[]>(args => args.Length == 3 && args[0] == "./" && args[1] == "-m" && args[2] == "local"));
    }

    [Fact]
    public void ParseAndExecuteDisconnect()
    {
        const string input = "disconnect";
        _parser.ParseAndExecute(input);

        _disconnect.Received().Execute(Arg.Is<string[]>(args => args.Length == 0));
    }

    [Fact]
    public void ParseAndExecuteTreeGoTo()
    {
        const string input = "tree goto SomeDir";
        _parser.ParseAndExecute(input);

        _treeGoto.Received().Execute(Arg.Is<string[]>(args => args.Length == 1 && args[0] == "SomeDir"));
    }

    [Fact]
    public void ParseAndExecuteTreeList()
    {
        const string input = "tree list";
        _parser.ParseAndExecute(input);

        _treeList.Received().Execute(Arg.Is<string[]>(args => args.Length == 0));
    }

    [Fact]
    public void ParseAndExecuteFileShow()
    {
        const string input = "file show test.txt";
        _parser.ParseAndExecute(input);

        _fileShow.Received().Execute(Arg.Is<string[]>(args => args.Length == 1 && args[0] == "test.txt"));
    }

    [Fact]
    public void ParseAndExecuteFileShowWithParameters()
    {
        const string input = "file show test.txt -m local";
        _parser.ParseAndExecute(input);

        _fileShow.Received().Execute(Arg.Is<string[]>(args => args.Length == 3 && args[0] == "test.txt" && args[1] == "-m" && args[2] == "local"));
    }

    [Fact]
    public void ParseAndExecuteFileCopy()
    {
        const string input = "file copy test.txt SomeDir";
        _parser.ParseAndExecute(input);

        _fileCopy.Received().Execute(Arg.Is<string[]>(args => args.Length == 2 && args[0] == "test.txt" && args[1] == "SomeDir"));
    }

    [Fact]
    public void ParseAndExecuteFileDelete()
    {
        const string input = "file delete myfile.txt";
        _parser.ParseAndExecute(input);

        _fileDelete.Received().Execute(Arg.Is<string[]>(args => args.Length == 1 && args[0] == "myfile.txt"));
    }

    [Fact]
    public void ParseAndExecuteFileRename()
    {
        const string input = "file rename oldfile.txt newfile.txt";
        _parser.ParseAndExecute(input);

        _fileRename.Received().Execute(Arg.Is<string[]>(args => args.Length == 2 && args[0] == "oldfile.txt" && args[1] == "newfile.txt"));
    }

    [Fact]
    public void ParseAndExecuteFileMove()
    {
        const string input = "file move olddir newdir";
        _parser.ParseAndExecute(input);

        _fileMove.Received().Execute(Arg.Is<string[]>(args => args.Length == 2 && args[0] == "olddir" && args[1] == "newdir"));
    }
}