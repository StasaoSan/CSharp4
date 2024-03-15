namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsParser.Strateges.FileShow;

public interface IFileShowStrategy
{
    void Execute(string path, Writer writer);
}