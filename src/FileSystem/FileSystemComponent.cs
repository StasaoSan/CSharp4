namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

public abstract class FileSystemComponent
{
    public abstract void TreeGoto(string path);
    public abstract void Delete(string path);
    public abstract void Create(string path);
}