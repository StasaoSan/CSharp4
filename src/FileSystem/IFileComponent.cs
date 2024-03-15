namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

public interface IFileComponent
{
    public void MoveFile(string sourcePath, string destinationPath);

    public void CopyFile(string sourcePath, string destinationPath);

    public void RenameFile(string path, string newName);
}