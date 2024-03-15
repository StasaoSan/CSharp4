namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

public class FileSystemPathManager
{
    private static FileSystemPathManager? _instance;

    public static FileSystemPathManager Instance
    {
        get { return _instance ??= new FileSystemPathManager(); }
    }

    public string? CurrentPath { get; set; }
}