namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

public interface IDirectoryComponent
{
    public void TreeGoto(string path);

    public void TreeList(string path, int depth);
}