using System;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsParser.Strateges.FileShow;

public static class FileShowStrategyFactory
{
    public static Writer CreateStrategy(string strategyName)
    {
        switch (strategyName)
        {
            case "console":
                return new ConsoleWriter();
            default:
                throw new AggregateException($"Unknown fileshow command: {strategyName}");
        }
    }
}