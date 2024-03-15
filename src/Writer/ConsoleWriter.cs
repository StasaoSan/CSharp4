using System;

namespace Itmo.ObjectOrientedProgramming.Lab4;

public class ConsoleWriter : Writer
{
    public override void Write(string? data)
    {
        if (data is not null)
            Console.WriteLine(data);
    }
}