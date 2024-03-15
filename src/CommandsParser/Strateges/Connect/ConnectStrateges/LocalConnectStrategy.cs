using System.IO;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsParser;

public class LocalConnectStrategy : IConnectStrategy
{
    public void Execute(string path)
    {
        string fullPath;
        if (FileSystemPathManager.Instance.CurrentPath is null)
        {
            fullPath = Directory.GetCurrentDirectory();
        }
        else
        {
            fullPath = Path.GetFullPath(Path.Combine(FileSystemPathManager.Instance.CurrentPath, path));
        }

        if (!Directory.Exists(fullPath))
            throw new DirectoryNotFoundException($"Directory on path: {fullPath} is not found");
        FileSystemPathManager.Instance.CurrentPath = fullPath;
    }
}
