using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

public class SystemDirectory : FileSystemComponent, IDirectoryComponent
{
    private readonly Writer? _writer;
    private readonly string _fileSymbol = "[F]";
    private readonly string _directorySymbol = "[D]";
    private readonly string _indentSymbol = "  ";

    public SystemDirectory()
    {
    }

    public SystemDirectory(Writer writer)
    {
        _writer = writer;
    }

    public SystemDirectory(string path, Writer writer)
    {
        Create(path);
        _writer = writer;
    }

    public static void Display(string path, Writer writer)
    {
        if (FileSystemPathManager.Instance.CurrentPath is null)
            throw new AggregateException("Path cant be null. Please use 'connect' command to set path");
        string fullSourcePath = System.IO.Path.Combine(FileSystemPathManager.Instance.CurrentPath, path);
        string? directoryName = System.IO.Path.GetDirectoryName(fullSourcePath);
        writer.Write(directoryName);
    }

    public override void Create(string path)
    {
        if (FileSystemPathManager.Instance.CurrentPath is null)
            throw new AggregateException("Path cant be null. Please use 'connect' command to set path");
        string fullPath = System.IO.Path.Combine(FileSystemPathManager.Instance.CurrentPath, path);
        if (Directory.Exists(path)) throw new AggregateException($"Directory on path: {fullPath} is already existed");
        Directory.CreateDirectory(fullPath);
    }

    public void TreeList(string path, int depth = 1)
    {
        if (FileSystemPathManager.Instance.CurrentPath is null)
            throw new AggregateException("Path cant be null. Please use 'connect' command to set path");
        string fullPath = System.IO.Path.Combine(FileSystemPathManager.Instance.CurrentPath, path);
        var dirInfo = new DirectoryInfo(fullPath);
        DisplayDirectory(dirInfo, 0, depth);
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
        string fullPath =
            System.IO.Path.GetFullPath(System.IO.Path.Combine(FileSystemPathManager.Instance.CurrentPath, path));
        if (!Directory.Exists(fullPath))
            throw new DirectoryNotFoundException($"Directory on path: {fullPath} is not found");
        FileSystemPathManager.Instance.CurrentPath = fullPath;
    }

    private void DisplayDirectory(DirectoryInfo directory, int currentDepth, int maxDepth)
    {
        if (currentDepth > maxDepth) return;

        _writer?.Write($"{new string(_indentSymbol[0], currentDepth * _indentSymbol.Length)}{_directorySymbol} {directory.Name}");

        IEnumerable<FileSystemInfo> entries = directory.EnumerateFileSystemInfos("*", SearchOption.TopDirectoryOnly).ToList();
        bool hasEntries = entries.Any();

        foreach (FileSystemInfo entry in entries)
        {
            if (currentDepth == maxDepth)
            {
                _writer?.Write($"{new string(_indentSymbol[0], (currentDepth + 1) * _indentSymbol.Length)}[...]");
                break;
            }

            if (entry is DirectoryInfo dir) DisplayDirectory(dir, currentDepth + 1, maxDepth);
            else _writer?.Write($"{new string(_indentSymbol[0], (currentDepth + 1) * _indentSymbol.Length)}{_fileSymbol} {entry.Name}");
        }

        if (!hasEntries && currentDepth < maxDepth) _writer?.Write($"{new string(_indentSymbol[0], (currentDepth + 1) * _indentSymbol.Length)}[No entries]");
    }
}