using System;
using System.IO;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

public class SystemFile : FileSystemComponent, IFileComponent
{
    public SystemFile()
    {
    }

    public SystemFile(string path)
    {
        Create(path);
    }

    public static void Display(string path, Writer writer)
    {
        if (FileSystemPathManager.Instance.CurrentPath is null)
            throw new AggregateException("Path cant be null. Please use 'connect' command to set path");
        string fullPath = System.IO.Path.Combine(FileSystemPathManager.Instance.CurrentPath, path);
        if (!File.Exists(fullPath))
            throw new FileNotFoundException($"File is not found on path: {fullPath}");

        string content = File.ReadAllText(fullPath);
        writer.Write(content);
    }

    public override void Create(string path)
    {
        if (FileSystemPathManager.Instance.CurrentPath is null)
            throw new AggregateException("Path cant be null. Please use 'connect' command to set path");
        string fullPath = System.IO.Path.Combine(FileSystemPathManager.Instance.CurrentPath, path);
        if (File.Exists(fullPath)) throw new AggregateException($"File on path: {fullPath} is already existed");
        File.Create(fullPath).Dispose();
    }

    public void MoveFile(string sourcePath, string destinationPath)
    {
        if (FileSystemPathManager.Instance.CurrentPath is null)
            throw new AggregateException("Path cant be null. Please use 'connect' command to set path");
        string fullSourcePath = System.IO.Path.Combine(FileSystemPathManager.Instance.CurrentPath, sourcePath);
        string fullDestinationPath = System.IO.Path.Combine(FileSystemPathManager.Instance.CurrentPath, destinationPath);
        string fileName = System.IO.Path.GetFileName(fullSourcePath);
        if (!destinationPath.EndsWith(fileName, StringComparison.Ordinal)) fullDestinationPath = System.IO.Path.Combine(fullDestinationPath, fileName);
        if (!File.Exists(fullSourcePath)) throw new FileNotFoundException($"File is not found on path: {fullSourcePath}");
        File.Move(fullSourcePath, fullDestinationPath);
    }

    public void CopyFile(string sourcePath, string destinationPath)
    {
        if (FileSystemPathManager.Instance.CurrentPath is null)
            throw new AggregateException("Path cant be null. Please use 'connect' command to set path");

        string fullSourcePath = System.IO.Path.Combine(FileSystemPathManager.Instance.CurrentPath, sourcePath);
        string fullDestinationPath = System.IO.Path.Combine(FileSystemPathManager.Instance.CurrentPath, destinationPath);

        if (!File.Exists(fullSourcePath))
            throw new FileNotFoundException($"File is not found on path: {fullSourcePath}");

        if (Directory.Exists(fullDestinationPath))
        {
            string fileName = System.IO.Path.GetFileName(fullSourcePath);
            fullDestinationPath = System.IO.Path.Combine(fullDestinationPath, fileName);
        }

        if (File.Exists(fullDestinationPath)) fullDestinationPath = GetUniqueFilePath(fullDestinationPath);
        File.Copy(fullSourcePath, fullDestinationPath);
    }

    public void RenameFile(string path, string newName)
    {
        if (FileSystemPathManager.Instance.CurrentPath is null)
            throw new AggregateException("Path cant be null. Please use 'connect' command to set path");
        string fullPath = System.IO.Path.Combine(FileSystemPathManager.Instance.CurrentPath, path);
        if (!File.Exists(fullPath)) throw new FileNotFoundException($"File is not found on path: {fullPath}");
        string? directory = System.IO.Path.GetDirectoryName(fullPath);
        if (directory is null) throw new AggregateException("Directory cant be null");
        string newPath = System.IO.Path.Combine(directory, newName);
        File.Move(fullPath, newPath);
    }

    public override void Delete(string path)
    {
        if (FileSystemPathManager.Instance.CurrentPath is null)
            throw new AggregateException("Path cant be null. Please use 'connect' command to set path");
        string fullPath = System.IO.Path.Combine(FileSystemPathManager.Instance.CurrentPath, path);
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
        else if (Directory.Exists(fullPath))
        {
            Directory.Delete(fullPath, true);
        }
        else
        {
            throw new FileNotFoundException($"File or directory is not found on path: {fullPath}");
        }
    }

    public override void TreeGoto(string path)
    {
        if (FileSystemPathManager.Instance.CurrentPath is null)
            throw new AggregateException("Path cant be null. Please use 'connect' command to set path");
        string fullPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(FileSystemPathManager.Instance.CurrentPath, path));
        if (!Directory.Exists(fullPath)) throw new DirectoryNotFoundException($"Directory on path: {fullPath} is not found");
        FileSystemPathManager.Instance.CurrentPath = fullPath;
    }

    private static string GetUniqueFilePath(string filePath)
    {
        string directory = System.IO.Path.GetDirectoryName(filePath) ?? string.Empty;
        string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(filePath);
        string extension = System.IO.Path.GetExtension(filePath);

        int copyNumber = 1;
        string newFilePath;
        do
        {
            newFilePath = System.IO.Path.Combine(directory, $"{fileNameWithoutExtension} - copy{copyNumber}{extension}");
            copyNumber++;
        }
        while (File.Exists(newFilePath));

        return newFilePath;
    }
}