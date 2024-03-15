using System;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsParser;

public static class ConnectStrategyFactory
{
    public static IConnectStrategy CreateStrategy(string strategyName)
    {
        switch (strategyName)
        {
            case "local":
                return new LocalConnectStrategy();
            default:
                throw new ArgumentException("Unknown strategy name", nameof(strategyName));
        }
    }
}