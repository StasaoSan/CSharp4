using System;
using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.CommandsParser.Strateges;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsParser;

public class CommandParser
{
    private IDictionary<string, ICommandStrategy> commandStrategies;
    private Writer _writer;

    public CommandParser(Writer writer)
    {
        _writer = writer;
        commandStrategies = new Dictionary<string, ICommandStrategy>()
        {
            { "connect", new ConnectCommand(writer) },
            { "disconnect", new DisconnectCommand(writer) },
            { "tree goto", new TreeGoToCommand(writer) },
            { "tree list", new TreeListCommand(writer) },
            { "file show", new FileShowCommand(writer) },
            { "file move", new FileMoveCommand(writer) },
            { "file copy", new FileCopyCommand(writer) },
            { "file delete", new FileDeleteCommand(writer) },
            { "file rename", new FileRenameCommand(writer) },
        };
    }

    public CommandParser(Writer writer, IDictionary<string, ICommandStrategy> strategies)
    {
        _writer = writer;
        commandStrategies = strategies;
    }

    public void ParseAndExecute(string input)
    {
        if (string.IsNullOrEmpty(input))
            throw new ArgumentException("Command string cannot be null or empty");

        string[] inputParts = ParseArguments(input).ToArray();
        if (inputParts.Length == 0) return;

        string commandKey = string.Empty;
        bool commandFound = false;

        for (int i = 0; i < inputParts.Length; i++)
        {
            commandKey += inputParts[i];
            if (commandStrategies.TryGetValue(commandKey, out ICommandStrategy? strategy))
            {
                string[] args = inputParts.Skip(i + 1).ToArray();
                strategy.Execute(args);
                commandFound = true;
                break;
            }

            commandKey += " ";
        }

        if (!commandFound)
        {
            _writer.Write($"Unexpected command: {commandKey}");
        }
    }

    private static IEnumerable<string> ParseArguments(string input)
    {
        var args = new List<string>();
        string currentArg = string.Empty;
        bool inQuotes = false;

        foreach (char c in input)
        {
            if (c == '\"')
            {
                inQuotes = !inQuotes;
            }
            else if (char.IsWhiteSpace(c) && !inQuotes)
            {
                if (string.IsNullOrEmpty(currentArg)) continue;
                args.Add(currentArg);
                currentArg = string.Empty;
            }
            else
            {
                currentArg += c;
            }
        }

        if (!string.IsNullOrEmpty(currentArg))
            args.Add(currentArg);

        return args;
    }
}